using System;
using SaMoCode.RepairManagement.Data;
using SaMoCode.RepairManagement.Models;

namespace SaMoCode.RepairManagement.Services
{
    public class TechnicianAssignmentService
    {
        private readonly TechnicianAssignmentRepository _repository =
            new TechnicianAssignmentRepository();

        public TechnicianAssignment GetCurrentAssignment(int deviceId)
        {
            ValidateId(deviceId, "device");
            return _repository.GetCurrentAssignment(deviceId);
        }

        public int Assign(int deviceId, int technicianId)
        {
            ValidateId(deviceId, "device");
            ValidateId(technicianId, "technician");
            return _repository.Assign(deviceId, technicianId);
        }

        public int Transfer(int deviceId, int newTechnicianId, string reason,
            int? expectedAssignmentId = null)
        {
            ValidateId(deviceId, "device");
            ValidateId(newTechnicianId, "technician");
            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Please enter a transfer reason.");
            reason = reason.Trim();
            if (reason.Length > 300)
                throw new ArgumentException("Transfer reason must be 300 characters or fewer.");
            if (expectedAssignmentId.HasValue)
                ValidateId(expectedAssignmentId.Value, "current assignment");
            return _repository.Transfer(deviceId, newTechnicianId, reason, expectedAssignmentId);
        }

        private static void ValidateId(int id, string name)
        {
            if (id <= 0)
                throw new ArgumentException("Please select a valid " + name + ".");
        }
    }
}
