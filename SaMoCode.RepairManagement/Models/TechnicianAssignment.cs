using System;

namespace SaMoCode.RepairManagement.Models
{
    public class TechnicianAssignment
    {
        public int TechnicianAssignmentId { get; set; }

        public int DeviceId { get; set; }

        public int TechnicianId { get; set; }

        public string TechnicianName { get; set; }

        public DateTime AssignedAt { get; set; }

        public DateTime? UnassignedAt { get; set; }

        public string TransferReason { get; set; }

        public string Notes { get; set; }
    }
}
