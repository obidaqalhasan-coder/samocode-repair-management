using System;
using SaMoCode.RepairManagement.Data;
using SaMoCode.RepairManagement.Models;

namespace SaMoCode.RepairManagement.Services
{
    public class RepairIssueService
    {
        private readonly RepairIssueRepository _repairIssueRepository;

        public RepairIssueService()
        {
            _repairIssueRepository =
                new RepairIssueRepository();
        }

        public int AddReportedIssue(
            int deviceId,
            string reportedProblem,
            string notes)
        {
            if (deviceId <= 0)
                throw new ArgumentException(
                    "A valid device is required.");

            if (string.IsNullOrWhiteSpace(reportedProblem))
                throw new ArgumentException(
                    "Reported problem is required.");

            RepairIssue issue = new RepairIssue
            {
                DeviceId = deviceId,

                ReportedProblem =
                    reportedProblem.Trim(),

                // We don't know these yet at reception.
                Diagnosis = null,
                WorkDone = null,

                Status = "Reported",

                EstimatedPriceMin = null,
                EstimatedPriceMax = null,
                FinalPrice = null,

                CreatedAt = DateTime.Now,

                Notes =
                    string.IsNullOrWhiteSpace(notes)
                        ? null
                        : notes.Trim()
            };

            return _repairIssueRepository.Add(issue);
        }
    }
}