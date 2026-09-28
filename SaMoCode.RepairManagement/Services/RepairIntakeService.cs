using System;
using System.Data.SqlClient;
using SaMoCode.RepairManagement.Data;

namespace SaMoCode.RepairManagement.Services
{
    public class RepairIntakeService
    {
        public int CreateRepairIntake(
            int customerId,
            string brand,
            string model,
            string color,
            string serialNumber,
            string reportedProblem,
            string notes)
        {
            // Validation
            if (customerId <= 0)
                throw new ArgumentException("Please select a customer.");

            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentException("Device brand is required.");

            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Device model is required.");

            if (string.IsNullOrWhiteSpace(reportedProblem))
                throw new ArgumentException("Reported problem is required.");

            using (SqlConnection connection =
                   DatabaseConnection.CreateConnection())
            {
                connection.Open();

                using (SqlTransaction transaction =
                       connection.BeginTransaction())
                {
                    try
                    {
                        int repairOrderId =
                            InsertRepairOrder(
                                connection,
                                transaction,
                                customerId,
                                notes);

                        int deviceId =
                            InsertDevice(
                                connection,
                                transaction,
                                repairOrderId,
                                brand,
                                model,
                                color,
                                serialNumber);

                        InsertRepairIssue(
                            connection,
                            transaction,
                            deviceId,
                            reportedProblem,
                            notes);

                        transaction.Commit();

                        return repairOrderId;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private int InsertRepairOrder(
            SqlConnection connection,
            SqlTransaction transaction,
            int customerId,
            string notes)
        {
            const string query = @"
                INSERT INTO RepairOrders
                (
                    CustomerId,
                    CreatedAt,
                    Status,
                    Notes
                )
                VALUES
                (
                    @CustomerId,
                    @CreatedAt,
                    @Status,
                    @Notes
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlCommand command =
                   new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue(
                    "@CustomerId",
                    customerId);

                command.Parameters.AddWithValue(
                    "@CreatedAt",
                    DateTime.Now);

                command.Parameters.AddWithValue(
                    "@Status",
                    "Open");

                command.Parameters.AddWithValue(
                    "@Notes",
                    DbValue(notes));

                return (int)command.ExecuteScalar();
            }
        }

        private int InsertDevice(
            SqlConnection connection,
            SqlTransaction transaction,
            int repairOrderId,
            string brand,
            string model,
            string color,
            string serialNumber)
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
                    NULL,
                    NULL
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlCommand command =
                   new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue(
                    "@RepairOrderId",
                    repairOrderId);

                command.Parameters.AddWithValue(
                    "@Brand",
                    brand.Trim());

                command.Parameters.AddWithValue(
                    "@Model",
                    model.Trim());

                command.Parameters.AddWithValue(
                    "@Color",
                    DbValue(color));

                command.Parameters.AddWithValue(
                    "@SerialNumber",
                    DbValue(serialNumber));

                command.Parameters.AddWithValue(
                    "@Status",
                    "Received");

                return (int)command.ExecuteScalar();
            }
        }

        private void InsertRepairIssue(
            SqlConnection connection,
            SqlTransaction transaction,
            int deviceId,
            string reportedProblem,
            string notes)
        {
            const string query = @"
                INSERT INTO RepairIssues
                (
                    DeviceId,
                    ReportedProblem,
                    Diagnosis,
                    WorkDone,
                    Status,
                    EstimatedPriceMin,
                    EstimatedPriceMax,
                    FinalPrice,
                    CreatedAt,
                    Notes
                )
                VALUES
                (
                    @DeviceId,
                    @ReportedProblem,
                    NULL,
                    NULL,
                    @Status,
                    NULL,
                    NULL,
                    NULL,
                    @CreatedAt,
                    @Notes
                );";

            using (SqlCommand command =
                   new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue(
                    "@DeviceId",
                    deviceId);

                command.Parameters.AddWithValue(
                    "@ReportedProblem",
                    reportedProblem.Trim());

                command.Parameters.AddWithValue(
                    "@Status",
                    "Reported");

                command.Parameters.AddWithValue(
                    "@CreatedAt",
                    DateTime.Now);

                command.Parameters.AddWithValue(
                    "@Notes",
                    DbValue(notes));

                command.ExecuteNonQuery();
            }
        }

        private object DbValue(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? DBNull.Value
                : (object)value.Trim();
        }
    }
}