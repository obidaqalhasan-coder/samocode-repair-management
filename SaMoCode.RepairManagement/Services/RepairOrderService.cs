using System;
using SaMoCode.RepairManagement.Data;
using SaMoCode.RepairManagement.Models;
using System.Collections.Generic;

namespace SaMoCode.RepairManagement.Services
{
    public class RepairOrderService
    {
        private readonly RepairOrderRepository _repairOrderRepository;

        public RepairOrderService()
        {
            _repairOrderRepository =
                new RepairOrderRepository();
        }

        public int CreateRepairOrder(
            int customerId,
            string notes)
        {
            if (customerId <= 0)
                throw new ArgumentException(
                    "A valid customer is required.");

            RepairOrder repairOrder =
                new RepairOrder
                {
                    CustomerId = customerId,

                    CreatedAt = DateTime.Now,

                    Status = "Open",

                    Notes =
                        string.IsNullOrWhiteSpace(notes)
                            ? null
                            : notes.Trim()
                };

            return _repairOrderRepository.Add(repairOrder);
        }

        public List<RepairListItem> GetRepairList()
        {
            return _repairOrderRepository.GetRepairList();
        }
    }


}