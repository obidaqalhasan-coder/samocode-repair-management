using System;

namespace SaMoCode.RepairManagement.Models
{
    public class RepairOrder
    {
        public int RepairOrderId { get; set; }

        public int CustomerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; }

        public string Notes { get; set; }
    }
}