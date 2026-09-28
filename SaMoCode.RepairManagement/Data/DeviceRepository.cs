using System.Data.SqlClient;
using SaMoCode.RepairManagement.Models;

namespace SaMoCode.RepairManagement.Data
{
    public class DeviceRepository
    {
        public int Add(Device device)
        {
            const string query = @"
                INSERT INTO Devices
                (
                    RepairOrderId,
                    Brand,
                    Model,
                    Color,
                    SerialNumber,
                    Status,
                    IntakeConditionNotes,
                    Notes
                )
                VALUES
                (
                    @RepairOrderId,
                    @Brand,
                    @Model,
                    @Color,
                    @SerialNumber,
                    @Status,
                    @IntakeConditionNotes,
                    @Notes
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlConnection connection =
                   DatabaseConnection.CreateConnection())
            using (SqlCommand command =
                   new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue(
                    "@RepairOrderId",
                    device.RepairOrderId);

                command.Parameters.AddWithValue(
                    "@Brand",
                    device.Brand);

                command.Parameters.AddWithValue(
                    "@Model",
                    device.Model);

                command.Parameters.AddWithValue(
                    "@Color",
                    (object)device.Color ?? System.DBNull.Value);

                command.Parameters.AddWithValue(
                    "@SerialNumber",
                    (object)device.SerialNumber ?? System.DBNull.Value);

                command.Parameters.AddWithValue(
                    "@Status",
                    device.Status);

                command.Parameters.AddWithValue(
                    "@IntakeConditionNotes",
                    (object)device.IntakeConditionNotes ??
                    System.DBNull.Value);

                command.Parameters.AddWithValue(
                    "@Notes",
                    (object)device.Notes ??
                    System.DBNull.Value);

                connection.Open();

                return (int)command.ExecuteScalar();
            }
        }
    }
}