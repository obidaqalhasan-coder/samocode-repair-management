using System.Data.SqlClient;
using SaMoCode.RepairManagement.Models;
using System;
using System.Collections.Generic;

namespace SaMoCode.RepairManagement.Data
{
    public class RepairOrderRepository
    {
        public int Add(RepairOrder repairOrder)
        {
            const string query = @"
                INSERT INTO RepairOrders
                    (CustomerId, CreatedAt, Status, Notes)
                VALUES
                    (@CustomerId, @CreatedAt, @Status, @Notes);

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlConnection connection =
                   DatabaseConnection.CreateConnection())
            using (SqlCommand command =
                   new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue(
                    "@CustomerId",
                    repairOrder.CustomerId);

                command.Parameters.AddWithValue(
                    "@CreatedAt",
                    repairOrder.CreatedAt);

                command.Parameters.AddWithValue(
                    "@Status",
                    repairOrder.Status);

                command.Parameters.AddWithValue(
                    "@Notes",
                    (object)repairOrder.Notes ??
                    System.DBNull.Value);

                connection.Open();

                return (int)command.ExecuteScalar();
            }
        }

        public List<RepairListItem> GetRepairList()
        {
            const string query = @"
        SELECT
            ro.RepairOrderId,
            c.Name AS CustomerName,
            c.PhoneNumber,
            d.DeviceId,
            d.Brand,
            d.Model,
            d.Status AS DeviceStatus,
            ro.CreatedAt
        FROM RepairOrders ro
        INNER JOIN Customers c
            ON ro.CustomerId = c.CustomerId
        INNER JOIN Devices d
            ON ro.RepairOrderId = d.RepairOrderId
        ORDER BY ro.RepairOrderId DESC;";

            List<RepairListItem> repairs =
                new List<RepairListItem>();

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
                        RepairListItem repair =
                            new RepairListItem
                            {
                                DeviceId = reader.GetInt32(reader.GetOrdinal("DeviceId")),
                                RepairOrderId =
                                    reader.GetInt32(
                                        reader.GetOrdinal("RepairOrderId")),

                                CustomerName =
                                    reader.GetString(
                                        reader.GetOrdinal("CustomerName")),

                                PhoneNumber =
                                    reader.GetString(
                                        reader.GetOrdinal("PhoneNumber")),

                                DeviceName =
                                    reader.GetString(
                                        reader.GetOrdinal("Brand"))
                                    + " "
                                    + reader.GetString(
                                        reader.GetOrdinal("Model")),

                                DeviceStatus =
                                    reader.GetString(
                                        reader.GetOrdinal("DeviceStatus")),

                                CreatedAt =
                                    reader.GetDateTime(
                                        reader.GetOrdinal("CreatedAt"))
                            };

                        repairs.Add(repair);
                    }
                }
            }

            return repairs;
        }

    }
}
