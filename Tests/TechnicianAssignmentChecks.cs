using System;
using System.Drawing;
using System.Windows.Forms;
using SaMoCode.RepairManagement.Forms;
using SaMoCode.RepairManagement.Models;
using SaMoCode.RepairManagement.Services;

internal static class TechnicianAssignmentChecks
{
    private static int _passed;
    [STAThread]
    private static void Main()
    {
        var service = new TechnicianAssignmentService();
        Reject(() => service.GetCurrentAssignment(0));
        Reject(() => service.Assign(0, 1));
        Reject(() => service.Assign(1, 0));
        Reject(() => service.Transfer(0, 1, "Reason"));
        Reject(() => service.Transfer(1, 0, "Reason"));
        Reject(() => service.Transfer(1, 2, null));
        Reject(() => service.Transfer(1, 2, "  "));
        Reject(() => service.Transfer(1, 2, new string('x', 301)));
        Reject(() => service.Transfer(1, 2, "Reason", 0));
        Application.EnableVisualStyles();
        using (var form = new TechnicianAssignmentForm(1, null))
        {
            if (form.Text != "Assign Technician" || form.FormBorderStyle != FormBorderStyle.FixedDialog)
                throw new Exception("Assign dialog configuration is incorrect.");
        }
        using (var form = new TechnicianAssignmentForm(1, new TechnicianAssignment
            { TechnicianAssignmentId = 1, TechnicianId = 1, TechnicianName = "Khaled" }))
        {
            if (form.Text != "Transfer Technician")
                throw new Exception("Transfer dialog configuration is incorrect.");
        }
        Console.WriteLine(_passed + " validation checks and both dialog construction checks passed.");
    }
    private static void Reject(Action action)
    {
        try { action(); }
        catch (ArgumentException) { _passed++; return; }
        throw new Exception("Expected validation failure.");
    }
}
