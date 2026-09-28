using System;
using System.Data.SqlClient;
using SaMoCode.RepairManagement.Models;

namespace SaMoCode.RepairManagement.Data
{
    public class RepairDetailsRepository
    {
        public RepairDetails GetById(int repairOrderId)
        {
            RepairDetails repair = GetRepairOrder(repairOrderId);

            if (repair == null)
                return null;

            LoadDevices(repair);

            return repair;
        }

        private RepairDetails GetRepairOrder(int repairOrderId)
        {
            const string query = @"
                SELECT
                    ro.RepairOrderId,
                    ro.CreatedAt,
                    ro.Status AS OrderStatus,
                    ro.Notes AS OrderNotes,

                    c.CustomerId,
                    c.Name AS CustomerName,
                    c.PhoneNumber

                FROM RepairOrders ro

                INNER JOIN Customers c
                    ON ro.CustomerId = c.CustomerId

                WHERE ro.RepairOrderId = @RepairOrderId;";

            using (SqlConnection connection =
                   DatabaseConnection.CreateConnection())
            using (SqlCommand command =
                   new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue(
                    "@RepairOrderId",
                    repairOrderId);

                connection.Open();

                using (SqlDataReader reader =
                       command.ExecuteReader())
                {
                    if (!reader.Read())
                        return null;

                    return new RepairDetails
                    {
                        RepairOrderId =
                            reader.GetInt32(
                                reader.GetOrdinal("RepairOrderId")),

                        CreatedAt =
                            reader.GetDateTime(
                                reader.GetOrdinal("CreatedAt")),

                        OrderStatus =
                            reader.GetString(
                                reader.GetOrdinal("OrderStatus")),

                        OrderNotes =
                            GetNullableString(
                                reader,
                                "OrderNotes"),

                        CustomerId =
                            reader.GetInt32(
                                reader.GetOrdinal("CustomerId")),

                        CustomerName =
                            reader.GetString(
                                reader.GetOrdinal("CustomerName")),

                        PhoneNumber =
                            reader.GetString(
                                reader.GetOrdinal("PhoneNumber"))
                    };
                }
            }
        }

        private void LoadDevices(RepairDetails repair)
        {
            const string query = @"
                SELECT
                    DeviceId,
                    Brand,
                    Model,
                    Color,
                    SerialNumber,
                    Status

                FROM Devices

                WHERE RepairOrderId = @RepairOrderId

                ORDER BY DeviceId;";

            using (SqlConnection connection =
                   DatabaseConnection.CreateConnection())
            using (SqlCommand command =
                   new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue(
                    "@RepairOrderId",
                    repair.RepairOrderId);

                connection.Open();

                using (SqlDataReader reader =
                       command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RepairDeviceDetails device =
                            new RepairDeviceDetails
                            {
                                DeviceId =
                                    reader.GetInt32(
                                        reader.GetOrdinal("DeviceId")),

                                Brand =
                                    reader.GetString(
                                        reader.GetOrdinal("Brand")),

                                Model =
                                    reader.GetString(
                                        reader.GetOrdinal("Model")),

                                Color =
                                    GetNullableString(
                                        reader,
                                        "Color"),

                                SerialNumber =
                                    GetNullableString(
                                        reader,
                                        "SerialNumber"),

                                Status =
                                    reader.GetString(
                                        reader.GetOrdinal("Status"))
                            };

                        repair.Devices.Add(device);
                    }
                }
            }

            foreach (RepairDeviceDetails device in repair.Devices)
            {
                LoadIssues(device);
            }
        }

        private void LoadIssues(RepairDeviceDetails device)
        {
            const string query = @"
                SELECT
                    RepairIssueId,
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

                FROM RepairIssues

                WHERE DeviceId = @DeviceId

                ORDER BY RepairIssueId;";

            using (SqlConnection connection =
                   DatabaseConnection.CreateConnection())
            using (SqlCommand command =
                   new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue(
                    "@DeviceId",
                    device.DeviceId);

                connection.Open();

                using (SqlDataReader reader =
                       command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RepairIssue issue =
                            new RepairIssue
                            {
                                RepairIssueId =
                                    reader.GetInt32(
                                        reader.GetOrdinal("RepairIssueId")),

                                DeviceId =
                                    reader.GetInt32(
                                        reader.GetOrdinal("DeviceId")),

                                ReportedProblem =
                                    reader.GetString(
                                        reader.GetOrdinal("ReportedProblem")),

                                Diagnosis =
                                    GetNullableString(reader, "Diagnosis"),

                                WorkDone =
                                    GetNullableString(reader, "WorkDone"),

                                Status =
                                    reader.GetString(
                                        reader.GetOrdinal("Status")),

                                EstimatedPriceMin =
                                    GetNullableDecimal(
                                        reader,
                                        "EstimatedPriceMin"),

                                EstimatedPriceMax =
                                    GetNullableDecimal(
                                        reader,
                                        "EstimatedPriceMax"),

                                FinalPrice =
                                    GetNullableDecimal(
                                        reader,
                                        "FinalPrice"),

                                CreatedAt =
                                    reader.GetDateTime(
                                        reader.GetOrdinal("CreatedAt")),

                                Notes =
                                    GetNullableString(reader, "Notes")
                            };

                        device.Issues.Add(issue);
                    }
                }
            }
        }

        private string GetNullableString(
            SqlDataReader reader,
            string columnName)
        {
            int index = reader.GetOrdinal(columnName);

            return reader.IsDBNull(index)
                ? null
                : reader.GetString(index);
        }

        private decimal? GetNullableDecimal(
            SqlDataReader reader,
            string columnName)
        {
            int index = reader.GetOrdinal(columnName);

            return reader.IsDBNull(index)
                ? (decimal?)null
                : reader.GetDecimal(index);
        }
    }
}