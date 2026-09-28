using System;
using System.Collections.Generic;

namespace SaMoCode.RepairManagement.Models
{
    public class IntakeDevice
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string SerialNumber { get; set; }
        public string ConditionNotes { get; set; }
        public List<int> ConditionIds { get; set; } = new List<int>();
        public List<int> AccessoryIds { get; set; } = new List<int>();
        public string OtherAccessory { get; set; }
        public List<string> Problems { get; set; } = new List<string>();
        public string DisplayName { get { return Brand + " " + Model + " — " + Problems.Count + " issue(s)"; } }
    }

    public class WorkflowIssue
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string Problem { get; set; }
        public string Diagnosis { get; set; }
        public string Status { get; set; }
        public decimal? Minimum { get; set; }
        public decimal? Maximum { get; set; }
        public decimal? FinalPrice { get; set; }
        public string Approval { get; set; }
        public int QuoteVersion { get; set; }
        public string WorkDone { get; set; }
        public string Notes { get; set; }
    }

    public class UsedPart
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
    }

    public class IssueChange
    {
        public string Event { get; set; }
        public string Detail { get; set; }
        public string ContactMethod { get; set; }
        public string ContactResult { get; set; }
        public int? TechnicianId { get; set; }
        public List<UsedPart> Parts { get; set; } = new List<UsedPart>();
    }
}
