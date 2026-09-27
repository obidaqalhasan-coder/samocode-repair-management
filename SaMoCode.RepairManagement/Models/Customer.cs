using System;

namespace SaMoCode.RepairManagement.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Notes { get; set; }
    }
}