using System;

namespace SaMoCode.RepairManagement.Models
{
    public class CustomerContact
    {
        public int CustomerContactId { get; set; }

        public int RepairIssueId { get; set; }

        // Example: Phone Call, WhatsApp, In Person
        public string ContactMethod { get; set; }

        // Example: Approved, Rejected, No Answer
        public string Result { get; set; }

        public DateTime ContactedAt { get; set; }

        public string Notes { get; set; }
    }
}