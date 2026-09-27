namespace SaMoCode.RepairManagement.Models
{
    public class Technician
    {
        public int TechnicianId { get; set; }

        public string Name { get; set; }

        // Example: General Technician, Board Technician
        public string Specialization { get; set; }

        public bool IsActive { get; set; }
    }
}