using System;
using System.Collections.Generic;
using System.Data;
using SaMoCode.RepairManagement.Data;
using SaMoCode.RepairManagement.Models;

namespace SaMoCode.RepairManagement.Services
{
    public class RepairWorkflowService
    {
        private readonly RepairWorkflowRepository _repository = new RepairWorkflowRepository();
        public static void Text(string value, string label, int length, bool required = true)
        {
            if (required && string.IsNullOrWhiteSpace(value)) throw new ArgumentException(label + " is required.");
            if (value != null && value.Trim().Length > length) throw new ArgumentException(label + " must be " + length + " characters or fewer.");
        }
        public static void Money(decimal value)
        {
            if (value < 0 || value > 99999999.99m || decimal.Round(value,2) != value)
                throw new ArgumentException("Enter a non-negative amount with at most two decimal places.");
        }
        public static bool Terminal(string status)
        { return status == "Completed" || status == "Rejected" || status == "No Repair Required"; }
        private static void Editable(WorkflowIssue issue)
        {
            if (Terminal(issue.Status)) throw new InvalidOperationException("This issue is resolved. Add a new issue for additional work.");
        }
        public static IssueChange Diagnose(WorkflowIssue issue, string diagnosis)
        {
            Editable(issue); Text(diagnosis,"Diagnosis",500);
            issue.Diagnosis = diagnosis.Trim();
            if (issue.Status == "Reported") issue.Status = "Diagnosis";
            return new IssueChange { Event="Diagnosis", Detail=issue.Diagnosis };
        }
        public static IssueChange Quote(WorkflowIssue issue, decimal minimum, decimal maximum)
        {
            Editable(issue); Money(minimum); Money(maximum);
            if (minimum > maximum) throw new ArgumentException("Minimum price cannot exceed maximum price.");
            issue.Minimum=minimum; issue.Maximum=maximum; issue.QuoteVersion++;
            issue.Approval="Pending"; issue.Status="Waiting Approval";
            return new IssueChange { Event="Quote", Detail=string.Format(System.Globalization.CultureInfo.InvariantCulture,"Quote #{0}: {1:0.00}–{2:0.00} AED. New approval required.",issue.QuoteVersion,minimum,maximum) };
        }
        public static IssueChange Contact(WorkflowIssue issue, string method, string result, string notes, int expectedQuoteVersion)
        {
            Editable(issue); Text(notes,"Contact notes",500,false);
            if (method != "Phone" && method != "In person" && method != "WhatsApp") throw new ArgumentException("Select a contact method.");
            if (result != "Approved" && result != "Rejected" && result != "No Answer") throw new ArgumentException("Select a contact result.");
            if (issue.QuoteVersion != expectedQuoteVersion) throw new InvalidOperationException("The quote changed. Reopen this window before recording approval.");
            if (issue.Status != "Waiting Approval" || !issue.Minimum.HasValue || !issue.Maximum.HasValue)
                throw new InvalidOperationException("Set a price or price range before recording the customer's decision.");
            if (result != "No Answer") { issue.Approval=result; issue.Status=result; }
            return new IssueChange { Event="Customer contact", Detail=notes ?? "", ContactMethod=method,ContactResult=result };
        }
        public static IssueChange Start(WorkflowIssue issue)
        {
            if (issue.Status != "Approved" || issue.Approval != "Approved") throw new InvalidOperationException("Customer approval is required before starting repair.");
            issue.Status="In Repair";
            return new IssueChange { Event="Repair started",Detail="Work started under quote #"+issue.QuoteVersion };
        }
        public static IssueChange Complete(WorkflowIssue issue, int technicianId, string work, decimal price, IList<UsedPart> parts)
        {
            if (issue.Status != "In Repair" || issue.Approval != "Approved") throw new InvalidOperationException("Start the approved repair before recording completion.");
            if (technicianId <= 0) throw new ArgumentException("Select the technician who performed the work.");
            Text(work,"Work done",500); Money(price);
            if (!issue.Maximum.HasValue || price > issue.Maximum.Value)
                throw new InvalidOperationException("The final price exceeds the approved maximum. Set a new quote and obtain approval first.");
            if (parts == null) throw new ArgumentException("Parts list is required (it may be empty).");
            foreach (var part in parts)
            {
                if (part == null) throw new ArgumentException("Invalid part.");
                Text(part.Name,"Part name",200);
                if (part.Quantity <= 0 || part.Quantity > 99999999.99m || decimal.Round(part.Quantity,2) != part.Quantity)
                    throw new ArgumentException("Part quantity must be positive with at most two decimal places.");
            }
            issue.WorkDone=work.Trim(); issue.FinalPrice=price; issue.Status="Completed";
            return new IssueChange { Event="Work completed", Detail=issue.WorkDone,TechnicianId=technicianId,Parts=new List<UsedPart>(parts) };
        }
        public static IssueChange NoRepair(WorkflowIssue issue, string reason)
        {
            Editable(issue); Text(reason,"Reason",500);
            if (issue.Status == "In Repair") throw new InvalidOperationException("An in-progress repair cannot be marked as requiring no repair.");
            issue.Status="No Repair Required"; issue.FinalPrice=0;
            return new IssueChange { Event="No repair required",Detail=reason.Trim() };
        }
        public void DiagnoseAndQuote(int device,int issue,string diagnosis,decimal minimum,decimal maximum,bool noRepair)
        {
            _repository.ChangeIssue(device,issue,state=> {
                Diagnose(state,diagnosis);
                IssueChange change=noRepair?NoRepair(state,diagnosis):Quote(state,minimum,maximum);
                change.Event=noRepair?"Diagnosis / no repair":"Diagnosis / quote";
                change.Detail="Diagnosis: "+state.Diagnosis+". "+change.Detail;
                return change;
            });
        }        public void DiagnoseIssue(int device, int issue, string diagnosis) { _repository.ChangeIssue(device,issue,s=>Diagnose(s,diagnosis)); }
        public void QuoteIssue(int device, int issue, decimal min, decimal max) { _repository.ChangeIssue(device,issue,s=>Quote(s,min,max)); }
        public void ContactCustomer(int device,int issue,string method,string result,string notes,int version)
        { _repository.ChangeIssue(device,issue,s=>Contact(s,method,result,notes,version)); }
        public void StartIssue(int device,int issue) { _repository.ChangeIssue(device,issue,Start); }
        public void CompleteIssue(int device,int issue,int technician,string work,decimal price,IList<UsedPart> parts)
        { _repository.ChangeIssue(device,issue,s=>Complete(s,technician,work,price,parts)); }
        public void NoRepairIssue(int device,int issue,string reason) { _repository.ChangeIssue(device,issue,s=>NoRepair(s,reason)); }
        public void AddIssue(int device,string problem) { Text(problem,"Problem",500); _repository.AddIssue(device,problem.Trim()); }
        public static void ValidateReady(string status,IList<WorkflowIssue> issues)
        {
            if(status=="Ready for Pickup"||status=="Delivered")throw new InvalidOperationException("This device is already ready or delivered.");
            if(issues.Count==0||System.Linq.Enumerable.Any(issues,i=>!Terminal(i.Status)))throw new InvalidOperationException("Resolve all issues before marking the device ready.");
            foreach(var issue in issues)
                if(issue.Status=="Completed"&&!issue.FinalPrice.HasValue)throw new InvalidOperationException("A completed issue has no final price. Review its record before delivery.");
        }
        public static void ValidateDelivery(string status,decimal amount,string method,decimal expectedAmount)
        {
            if(status!="Ready for Pickup")throw new InvalidOperationException("Mark this device ready for pickup before delivery.");
            Money(amount);Money(expectedAmount);
            if(method!="Cash"&&method!="Visa"&&method!="No charge")throw new ArgumentException("Select a payment method.");
            if(amount!=expectedAmount)throw new InvalidOperationException("The total changed. Reopen the delivery window.");
            if((amount==0)!=(method=="No charge"))throw new InvalidOperationException("Use No charge only when the amount due is zero.");
        }
        public void Ready(int device) { _repository.Ready(device,ValidateReady); }
        public decimal AmountDue(int device) { return _repository.AmountDue(device); }
        public void Deliver(int device,string method,decimal expectedAmount)
        {
            if (method != "Cash" && method != "Visa" && method != "No charge") throw new ArgumentException("Select a payment method.");
            Money(expectedAmount); _repository.Deliver(device,method,expectedAmount,ValidateDelivery);
        }
        public DataTable Issues(int device) { return _repository.Issues(device); }
        public DataTable Timeline(int device) { return _repository.Timeline(device); }
        public DataTable Work(int device) { return _repository.Work(device); }
        public DataTable IntakeDetails(int device) { return _repository.IntakeDetails(device); }
        public DataTable CustomerHistory(int customer) { return _repository.CustomerHistory(customer); }
    }
}


