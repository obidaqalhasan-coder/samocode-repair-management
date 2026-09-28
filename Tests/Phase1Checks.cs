using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using SaMoCode.RepairManagement.Models;
using SaMoCode.RepairManagement.Services;
using SaMoCode.RepairManagement.Forms;

internal static class Phase1Checks
{
    private static int passed;
    private static void Check(bool condition,string name)
    {if(!condition)throw new Exception("FAILED: "+name);passed++;Console.WriteLine("PASS: "+name);}
    private static void Reject(Action action,string name)
    {try{action();}catch(ArgumentException){Check(true,name);return;}catch(InvalidOperationException){Check(true,name);return;}throw new Exception("FAILED: "+name);}
    private static WorkflowIssue Fresh(int id=1)
    {return new WorkflowIssue {Id=id,DeviceId=1,Problem="Board repair",Status="Reported",Approval="Pending"};}
    private static RepairGuidance Guide(WorkflowIssue issue,bool assigned=true,string device="Diagnosis")
    {return RepairGuidanceService.Next(device,assigned,new[]{issue},issue.Id);}
    [STAThread]
    private static int Main(string[] args) { try { Run();return 0; } catch(Exception ex) { Console.Error.WriteLine(ex);return 1; } }
    private static void Run()
    {
        var issue=Fresh();
        Check(Guide(issue,false).Action==RepairAction.Assign,"unassigned device prompts assignment");
        Check(Guide(issue).Action==RepairAction.Diagnose,"assigned device prompts diagnosis");
        Reject(()=>RepairWorkflowService.Start(issue),"repair without approval rejected");
        RepairWorkflowService.Diagnose(issue,"Power IC fault");
        RepairWorkflowService.Quote(issue,150,300);
        Check(Guide(issue).Action==RepairAction.Contact,"quote prompts customer decision");
        var unanswered=RepairWorkflowService.Contact(issue,"Phone","No Answer","Called once",1);
        Check(issue.Status=="Waiting Approval" && unanswered.ContactResult=="No Answer","no-answer leaves decision pending");
        RepairWorkflowService.Contact(issue,"Phone","Approved","Agreed",1);
        Check(Guide(issue).Action==RepairAction.Start,"approval prompts repair start");
        RepairWorkflowService.Start(issue);
        Check(Guide(issue).Action==RepairAction.Complete,"in-progress prompts completion");
        Reject(()=>RepairWorkflowService.Complete(issue,1,"Replaced IC",301,new List<UsedPart>()),"above-approved maximum rejected");
        Check(issue.Status=="In Repair" && issue.FinalPrice==null,"failed completion does not mutate issue");
        var work=RepairWorkflowService.Complete(issue,2,"Replaced IC",250,new[]{new UsedPart{Name="Power IC",Quantity=1}});
        Check(issue.Status=="Completed" && issue.FinalPrice==250 && work.TechnicianId==2 && work.Parts.Count==1,"happy path 150-300 approved, completed at 250 with attribution and parts");
        Check(Guide(issue).Action==RepairAction.Ready,"completed issues prompt ready");
        Check(Guide(issue,false,"Ready for Pickup").Action==RepairAction.Deliver,"ready prompts delivery, no reassignment");
        Check(Guide(issue,false,"Delivered").Action==RepairAction.None,"delivered has no workflow action");
        Reject(()=>RepairWorkflowService.Quote(issue,100,500),"resolved issue cannot be repriced");
        var rejected=Fresh();RepairWorkflowService.Diagnose(rejected,"Screen damage");RepairWorkflowService.Quote(rejected,150,300);
        RepairWorkflowService.Contact(rejected,"In person","Rejected","Customer declined",1);
        Check(Guide(rejected,false).Action==RepairAction.Ready,"rejected issue can be prepared for return without repair");
        Reject(()=>RepairWorkflowService.Start(rejected),"rejected issue cannot start");
        var revised=Fresh();RepairWorkflowService.Quote(revised,150,300);RepairWorkflowService.Contact(revised,"Phone","Approved","",1);RepairWorkflowService.Start(revised);
        RepairWorkflowService.Quote(revised,150,450);
        Check(revised.Approval=="Pending" && revised.QuoteVersion==2,"new quote invalidates old approval");
        Reject(()=>RepairWorkflowService.Contact(revised,"Phone","Approved","",1),"stale quote approval rejected");
        Reject(()=>RepairWorkflowService.Start(revised),"new approval required after revision");
        var other=Fresh(2);
        Check(RepairGuidanceService.Next("Diagnosis",true,new[]{issue,other},1).Action==RepairAction.SelectIssue,"completed issue leads to unresolved issue");
        Check(RepairGuidanceService.Next("Diagnosis",true,new[]{issue,other},2).Action==RepairAction.Diagnose,"multiple issues retain independent actions");
        Check(issue.Status=="Completed" && issue.FinalPrice==250,"additional issue does not change completed work");
        Reject(()=>RepairWorkflowService.Money(-1),"negative money rejected");
        Reject(()=>RepairWorkflowService.Money(1.001m),"fractional-cent price rejected");
        Reject(()=>RepairWorkflowService.Quote(Fresh(),300,150),"inverted price range rejected");
        Reject(()=>new RepairWorkflowService().Deliver(1,"Wire",0),"invalid payment method rejected before database access");
        var device=new IntakeDevice {Brand="Apple",Model="iPhone",Problems=new List<string>{"Screen","Battery"}};
        MultiDeviceIntakeService.ValidateDevice(device);Check(true,"multi-issue intake validation");
        device.Problems.Clear();Reject(()=>MultiDeviceIntakeService.ValidateDevice(device),"device requires an issue");
        RepairWorkflowService.ValidateReady("Diagnosis",new[]{issue,rejected});Check(true,"completed and rejected issues can become ready together");
        Reject(()=>RepairWorkflowService.ValidateReady("Diagnosis",new[]{issue,Fresh(2)}),"one unresolved issue blocks readiness");
        Reject(()=>RepairWorkflowService.ValidateReady("Diagnosis",new WorkflowIssue[0]),"empty device blocks readiness");
        Reject(()=>RepairWorkflowService.ValidateReady("Delivered",new[]{issue}),"delivered device cannot be marked ready again");
        RepairWorkflowService.ValidateDelivery("Ready for Pickup",250,"Cash",250);Check(true,"ready payment at 250 accepted");
        RepairWorkflowService.ValidateDelivery("Ready for Pickup",0,"No charge",0);Check(true,"rejected device return supports no charge");
        Reject(()=>RepairWorkflowService.ValidateDelivery("In Repair",250,"Cash",250),"delivery before ready rejected");
        Reject(()=>RepairWorkflowService.ValidateDelivery("Delivered",250,"Cash",250),"duplicate delivery rejected");
        Reject(()=>RepairWorkflowService.ValidateDelivery("Ready for Pickup",250,"No charge",250),"positive balance cannot use no charge");
        Reject(()=>RepairWorkflowService.ValidateDelivery("Ready for Pickup",0,"Cash",0),"zero balance requires no charge");
        Reject(()=>RepairWorkflowService.ValidateDelivery("Ready for Pickup",250,"Cash",200),"stale payment total rejected");
        Reject(()=>RepairWorkflowService.ValidateDelivery("Ready for Pickup",-1,"Cash",-1),"negative delivery rejected");
        Reject(()=>RepairWorkflowService.ValidateDelivery("Ready for Pickup",250,"Other",250),"unsupported payment method rejected");        Application.EnableVisualStyles();
        Directory.CreateDirectory("Tests/Artifacts");
        foreach(int width in new[]{800,1100,1600})
        {
            using(var repairs=(UserControl)Activator.CreateInstance(typeof(RepairsPage),BindingFlags.Instance|BindingFlags.NonPublic,null,new object[]{false},null))
                Layout(repairs,"btnNewRepair",width,"Repairs");
            using(var technicians=(UserControl)Activator.CreateInstance(typeof(TechniciansPage),BindingFlags.Instance|BindingFlags.NonPublic,null,new object[]{false},null))
                Layout(technicians,"btnAddTechnician",width,"Technicians");
        }
        using(var intake=(UserControl)Activator.CreateInstance(typeof(NewRepairOrderPage),BindingFlags.Instance|BindingFlags.NonPublic,null,new object[]{false},null))
            Layout(intake,"btnCreateRepair",1100,"Intake");
        using(var page=(UserControl)Activator.CreateInstance(typeof(RepairDetailsPage),BindingFlags.Instance|BindingFlags.NonPublic,null,new object[]{1,false},null))
        {
            SetLabel(page,"lblTitle","Repair Order #1042");SetLabel(page,"lblCreated","Created 28 Sep 2026 • Open");
            SetLabel(page,"lblCustomerName","Sample customer");SetLabel(page,"lblCustomerPhone","050 000 0000");
            SetLabel(page,"lblDeviceStatus","Waiting Approval");SetLabel(page,"lblDeviceDetails","Blue • No serial / IMEI");
            SetLabel(page,"lblCurrentTechnician","Ahmad");SetLabel(page,"lblAssignedAt","Assigned 28 Sep 2026 - 10:00 AM");
            Field(page,"_deviceStatus").SetValue(page,"Waiting Approval");Field(page,"_current").SetValue(page,new TechnicianAssignment{TechnicianId=1,TechnicianName="Ahmad"});
            Field(page,"_deviceId").SetValue(page,(int?)1);
            var states=(List<WorkflowIssue>)Field(page,"_issueStates").GetValue(page);
            states.Add(new WorkflowIssue{Id=1,Problem="Board repair",Status="Waiting Approval",Minimum=150,Maximum=300});
            var grid=(DataGridView)Field(page,"dgvIssues").GetValue(page);
            var table=new DataTable();table.Columns.Add("Id",typeof(int));table.Columns.Add("Problem");table.Columns.Add("Status");table.Columns.Add("Approval");table.Columns.Add("FinalPrice",typeof(decimal));
            table.Rows.Add(1,"Board repair","Waiting Approval","Pending",DBNull.Value);grid.DataSource=table;
            Field(page,"_loaded").SetValue(page,true);
            typeof(RepairDetailsPage).GetMethod("UpdateGuidance",BindingFlags.Instance|BindingFlags.NonPublic).Invoke(page,null);
            Layout(page,"_primary",1100,"GuidedDetails");
        }
        Console.WriteLine("TOTAL: "+passed+" checks passed. SQL integration is separate.");
    }
    private static FieldInfo Field(object obj,string name){return obj.GetType().GetField(name,BindingFlags.Instance|BindingFlags.NonPublic);}
    private static void SetLabel(object obj,string name,string text){((Label)Field(obj,name).GetValue(obj)).Text=text;}
    private static void ForceLayout(Control control) { var handle=control.Handle;control.PerformLayout();foreach(Control child in control.Controls)ForceLayout(child); }
    private static void Layout(UserControl page,string buttonField,int width,string name)
    {
        using(var host=new Form {ClientSize=new Size(width,760),Font=new Font("Segoe UI",9F)})
        {
            host.Controls.Add(page);page.Dock=DockStyle.Fill;host.CreateControl();page.CreateControl();host.PerformLayout();page.PerformLayout();ForceLayout(page);

            using(var bitmap=new Bitmap(width,760))
            {page.DrawToBitmap(bitmap,new Rectangle(0,0,width,760));bitmap.Save("Tests/Artifacts/"+name+"-"+width+".png");}
            var button=(Button)Field(page,buttonField).GetValue(page);
            var point=page.PointToClient(button.Parent.PointToScreen(button.Location));
            Check(point.X>=0 && point.Y>=0 && point.X+button.Width<=page.Width && point.Y+button.Height<=page.Height,name+" primary button contained at width "+width);
            host.Controls.Remove(page);
        }
    }
}



