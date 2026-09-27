using System.Collections.Generic;

namespace SaMoCode.RepairManagement.Models
{
    public class Device
    {
        public int DeviceId { get; set; }

        public int RepairOrderId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        // Optional - not required for most repairs
        public string SerialNumber { get; set; }

        // Allows extra description that doesn't fit predefined conditions
        public string IntakeConditionNotes { get; set; }

        // Predefined conditions selected during intake
        public List<IntakeCondition> IntakeConditions { get; set; }

        // Accessories received with the device
        public List<DeviceAccessory> Accessories { get; set; }

        public string Notes { get; set; }
        public string Status { get; set; }
        public Device()
        {
            IntakeConditions = new List<IntakeCondition>();
            Accessories = new List<DeviceAccessory>();
        }
    }
}