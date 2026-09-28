using System.Collections.Generic;
using System.Linq;
using SaMoCode.RepairManagement.Models;
namespace SaMoCode.RepairManagement.Services
{
    public enum RepairAction { None, Assign, Diagnose, Contact, Start, Complete, Ready, Deliver, SelectIssue, AddIssue }
    public sealed class RepairGuidance
    {
        public RepairAction Action {get;set;}
        public string Caption {get;set;}
        public string Explanation {get;set;}
        public int? NextIssueId {get;set;}
        public bool CanReviseQuote {get;set;}
    }
    public static class RepairGuidanceService
    {
        public static RepairGuidance Next(string deviceStatus,bool assigned,IList<WorkflowIssue> issues,int? selectedId)
        {
            if(deviceStatus=="Delivered")return Step(RepairAction.None,"Delivered","This device is complete. Its history remains available below.");
            if(deviceStatus=="Ready for Pickup")return Step(RepairAction.Deliver,"Deliver / Payment","Confirm payment and return this device to the customer.");
            if(issues.Count>0 && issues.All(i=>RepairWorkflowService.Terminal(i.Status)))
                return Step(RepairAction.Ready,"Mark Ready for Pickup","All issues are resolved. Return the device to reception for collection.");
            if(!assigned)return Step(RepairAction.Assign,"Assign Technician","Choose the technician who will receive this device.");
            if(issues.Count==0)return Step(RepairAction.AddIssue,"Add an issue","Record the problem reported for this device.");
            var selected=issues.FirstOrDefault(i=>i.Id==selectedId);
            if(selected==null || RepairWorkflowService.Terminal(selected.Status))
            {
                var next=issues.First(i=>!RepairWorkflowService.Terminal(i.Status));
                var step=Step(RepairAction.SelectIssue,"Continue next issue","There are unresolved issues. Continue with: "+next.Problem);step.NextIssueId=next.Id;return step;
            }
            RepairGuidance result;
            if(selected.Status=="In Repair")result=Step(RepairAction.Complete,"Complete Repair","Record the work, responsible technician, parts used and final price for the selected issue.");
            else if(selected.Status=="Approved")result=Step(RepairAction.Start,"Start Repair","The customer approved this issue. The technician can start work.");
            else if(selected.Status=="Waiting Approval")result=Step(RepairAction.Contact,"Record Customer Decision","Record approval, rejection or a contact attempt with no answer for this issue.");
            else result=Step(RepairAction.Diagnose,"Record Diagnosis","Record the diagnosis and quote together, or mark this issue as requiring no repair.");
            if(selected.Minimum.HasValue && selected.Maximum.HasValue)
                result.Explanation+=" Quote: "+selected.Minimum.Value.ToString("N2")+"–"+selected.Maximum.Value.ToString("N2")+" AED.";
            result.CanReviseQuote=selected.Status=="Waiting Approval"||selected.Status=="Approved"||selected.Status=="In Repair";
            return result;
        }
        private static RepairGuidance Step(RepairAction action,string caption,string text)
        {return new RepairGuidance {Action=action,Caption=caption,Explanation=text};}
    }
}
