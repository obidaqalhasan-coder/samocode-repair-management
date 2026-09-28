using System;
using SaMoCode.RepairManagement.Data;
using SaMoCode.RepairManagement.Models;

namespace SaMoCode.RepairManagement.Services
{
    public class DeviceService
    {
        private readonly DeviceRepository _deviceRepository;

        public DeviceService()
        {
            _deviceRepository = new DeviceRepository();
        }

        public int AddDevice(
            int repairOrderId,
            string brand,
            string model,
            string color,
            string serialNumber,
            string intakeConditionNotes,
            string notes)
        {
            if (repairOrderId <= 0)
                throw new ArgumentException(
                    "A valid repair order is required.");

            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentException(
                    "Device brand is required.");

            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException(
                    "Device model is required.");

            Device device = new Device
            {
                RepairOrderId = repairOrderId,

                Brand = brand.Trim(),

                Model = model.Trim(),

                Color =
                    string.IsNullOrWhiteSpace(color)
                        ? null
                        : color.Trim(),

                SerialNumber =
                    string.IsNullOrWhiteSpace(serialNumber)
                        ? null
                        : serialNumber.Trim(),

                Status = "Received",

                IntakeConditionNotes =
                    string.IsNullOrWhiteSpace(intakeConditionNotes)
                        ? null
                        : intakeConditionNotes.Trim(),

                Notes =
                    string.IsNullOrWhiteSpace(notes)
                        ? null
                        : notes.Trim()
            };

            return _deviceRepository.Add(device);
        }
    }
}