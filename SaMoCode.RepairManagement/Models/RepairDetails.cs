using System;
using System.Collections.Generic;

namespace SaMoCode.RepairManagement.Models
{
    public class RepairDetails
    {
        public int RepairOrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string OrderStatus { get; set; }
        public string OrderNotes { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }

        public List<RepairDeviceDetails> Devices { get; set; }

        public RepairDetails()
        {
            Devices = new List<RepairDeviceDetails>();
        }
    }

    public class RepairDeviceDetails
    {
        public int DeviceId { get; set; }
        public string DisplayName { get { return "#" + DeviceId + " — " + Brand + " " + Model; } }

        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string SerialNumber { get; set; }
        public string Status { get; set; }

        public List<RepairIssue> Issues { get; set; }

        public RepairDeviceDetails()
        {
            Issues = new List<RepairIssue>();
        }
    }
}
