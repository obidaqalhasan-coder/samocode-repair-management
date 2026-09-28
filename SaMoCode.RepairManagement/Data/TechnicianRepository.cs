using System.Collections.Generic;
using System.Data.SqlClient;
using SaMoCode.RepairManagement.Models;

namespace SaMoCode.RepairManagement.Data
{
    public class TechnicianRepository
    {
        public List<Technician> GetActive()
        {
            const string query = @"
                SELECT
                    TechnicianId,
                    Name,
                    Specialization,
                    IsActive
                FROM Technicians
                WHERE IsActive = 1
                ORDER BY Name;";

            List<Technician> technicians =
                new List<Technician>();

            using (SqlConnection connection =
                   DatabaseConnection.CreateConnection())
            using (SqlCommand command =
                   new SqlCommand(query, connection))
            {
                connection.Open();

                using (SqlDataReader reader =
                       command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        technicians.Add(
                            new Technician
                            {
                                TechnicianId =
                                    reader.GetInt32(
                                        reader.GetOrdinal(
                                            "TechnicianId")),

                                Name =
                                    reader.GetString(
                                        reader.GetOrdinal(
                                            "Name")),

                                Specialization =
                                    reader.IsDBNull(
                                        reader.GetOrdinal(
                                            "Specialization"))
                                        ? null
                                        : reader.GetString(
                                            reader.GetOrdinal(
                                                "Specialization")),

                                IsActive =
                                    reader.GetBoolean(
                                        reader.GetOrdinal(
                                            "IsActive"))
                            });
                    }
                }
            }

            return technicians;
        }

        public int Add(Technician technician)
        {
            const string query = @"
                INSERT INTO Technicians
                    (Name, Specialization, IsActive)
                VALUES
                    (@Name, @Specialization, @IsActive);

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlConnection connection =
                   DatabaseConnection.CreateConnection())
            using (SqlCommand command =
                   new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue(
                    "@Name",
                    technician.Name);

                command.Parameters.AddWithValue(
                    "@Specialization",
                    (object)technician.Specialization
                    ?? System.DBNull.Value);

                command.Parameters.AddWithValue(
                    "@IsActive",
                    technician.IsActive);

                connection.Open();

                return (int)command.ExecuteScalar();
            }
        }
    }
}