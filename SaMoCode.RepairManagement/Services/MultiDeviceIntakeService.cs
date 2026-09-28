using System;
using System.Collections.Generic;
using System.Linq;
using SaMoCode.RepairManagement.Models;
using SaMoCode.RepairManagement.Data;

namespace SaMoCode.RepairManagement.Services
{
    public class MultiDeviceIntakeService
    {
        public static void ValidateDevice(IntakeDevice device)
        {
            if (device == null) throw new ArgumentException("Device is required.");
            RepairWorkflowService.Text(device.Brand, "Brand", 50);
            RepairWorkflowService.Text(device.Model, "Model", 100);
            RepairWorkflowService.Text(device.Color, "Color", 50, false);
            RepairWorkflowService.Text(device.SerialNumber, "Serial / IMEI", 100, false);
            RepairWorkflowService.Text(device.ConditionNotes, "Condition notes", 500, false);
            RepairWorkflowService.Text(device.OtherAccessory, "Other accessory", 200, false);
            if (device.Problems == null || device.Problems.Count == 0) throw new ArgumentException("Add at least one issue for each device.");
            foreach (string problem in device.Problems) RepairWorkflowService.Text(problem, "Problem", 500);
            if (device.ConditionIds == null || device.AccessoryIds == null || device.ConditionIds.Any(i => i <= 0) || device.AccessoryIds.Any(i => i <= 0))
                throw new ArgumentException("Invalid condition or accessory.");
            if (device.ConditionIds.Distinct().Count() != device.ConditionIds.Count || device.AccessoryIds.Distinct().Count() != device.AccessoryIds.Count)
                throw new ArgumentException("Select each condition or accessory only once.");
        }
        public int Create(int customerId, IList<IntakeDevice> devices, string notes)
        {
            if (customerId <= 0) throw new ArgumentException("Select a customer.");
            if (devices == null || devices.Count == 0) throw new ArgumentException("Add at least one device.");
            foreach (var device in devices) ValidateDevice(device);
            RepairWorkflowService.Text(notes, "Order notes", 500, false);
            return new IntakeRepository().Create(customerId, devices, notes);
        }
        public System.Data.DataTable Conditions() { return new IntakeRepository().Conditions(); }
        public System.Data.DataTable Accessories() { return new IntakeRepository().Accessories(); }
    }
}
