using System;
using SaMoCode.RepairManagement.Data;
using SaMoCode.RepairManagement.Models;

namespace SaMoCode.RepairManagement.Services
{
    public class RepairDetailsService
    {
        private readonly RepairDetailsRepository
            _repairDetailsRepository;

        public RepairDetailsService()
        {
            _repairDetailsRepository =
                new RepairDetailsRepository();
        }

        public RepairDetails GetRepairDetails(
            int repairOrderId)
        {
            if (repairOrderId <= 0)
                throw new ArgumentException(
                    "Invalid repair order.");

            RepairDetails repair =
                _repairDetailsRepository.GetById(
                    repairOrderId);

            if (repair == null)
                throw new InvalidOperationException(
                    "Repair order was not found.");

            return repair;
        }
    }
}