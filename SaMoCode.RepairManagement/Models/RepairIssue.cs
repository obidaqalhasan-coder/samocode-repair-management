using System;

namespace SaMoCode.RepairManagement.Models
{
    public class RepairIssue
    {
        public int RepairIssueId { get; set; }

        public int DeviceId { get; set; }

        // What was initially observed/reported
        public string ReportedProblem { get; set; }

        // Technician's diagnosis
        public string Diagnosis { get; set; }

        // What was actually repaired/done
        public string WorkDone { get; set; }

        public string Status { get; set; }

        // Estimated repair price range
        public decimal? EstimatedPriceMin { get; set; }

        public decimal? EstimatedPriceMax { get; set; }

        // Actual final price
        public decimal? FinalPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Notes { get; set; }
    }
}   