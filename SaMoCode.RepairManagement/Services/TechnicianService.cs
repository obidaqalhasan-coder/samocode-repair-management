using System;
using System.Collections.Generic;
using SaMoCode.RepairManagement.Data;
using SaMoCode.RepairManagement.Models;

namespace SaMoCode.RepairManagement.Services
{
    public class TechnicianService
    {
        private readonly TechnicianRepository
            _technicianRepository;

        public TechnicianService()
        {
            _technicianRepository =
                new TechnicianRepository();
        }

        public List<Technician> GetActiveTechnicians()
        {
            return _technicianRepository.GetActive();
        }

        public int AddTechnician(
            string name,
            string specialization)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    "Technician name is required.");

            RepairWorkflowService.Text(name,"Technician name",100);
            RepairWorkflowService.Text(specialization,"Specialization",100,false);
            Technician technician =
                new Technician
                {
                    Name = name.Trim(),

                    Specialization =
                        string.IsNullOrWhiteSpace(
                            specialization)
                            ? null
                            : specialization.Trim(),

                    IsActive = true
                };

            return _technicianRepository.Add(
                technician);
        }
    }
}
