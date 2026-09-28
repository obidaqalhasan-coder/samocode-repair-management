using System;

namespace SaMoCode.RepairManagement.Models
{
    public class RepairListItem
    {
        public int DeviceId { get; set; }
        public int RepairOrderId { get; set; }

        public string CustomerName { get; set; }

        public string PhoneNumber { get; set; }

        public string DeviceName { get; set; }

        public string DeviceStatus { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
