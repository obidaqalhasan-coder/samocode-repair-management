using System.Data.SqlClient;
using SaMoCode.RepairManagement.Models;
using System;
using System.Collections.Generic;

namespace SaMoCode.RepairManagement.Data
{
    public class CustomerRepository
    {
        public int Add(Customer customer)
        {
            const string query = @"
                INSERT INTO Customers
                    (Name, PhoneNumber, CreatedAt, Notes)
                VALUES
                    (@Name, @PhoneNumber, @CreatedAt, @Notes);

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlConnection connection = DatabaseConnection.CreateConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", customer.Name);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                command.Parameters.AddWithValue("@CreatedAt", customer.CreatedAt);

                command.Parameters.AddWithValue(
                    "@Notes",
                    (object)customer.Notes ?? System.DBNull.Value);

                connection.Open();

                return (int)command.ExecuteScalar();
            }
        }
        public List<Customer> GetAll()
        {
            const string query = @"
        SELECT
            CustomerId,
            Name,
            PhoneNumber,
            CreatedAt,
            Notes
        FROM Customers
        ORDER BY CustomerId DESC;";

            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = DatabaseConnection.CreateConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer
                        {
                            CustomerId = reader.GetInt32(
                                reader.GetOrdinal("CustomerId")),

                            Name = reader.GetString(
                                reader.GetOrdinal("Name")),

                            PhoneNumber = reader.GetString(
                                reader.GetOrdinal("PhoneNumber")),

                            CreatedAt = reader.GetDateTime(
                                reader.GetOrdinal("CreatedAt")),

                            Notes = reader.IsDBNull(
                                reader.GetOrdinal("Notes"))
                                ? null
                                : reader.GetString(
                                    reader.GetOrdinal("Notes"))
                        };

                        customers.Add(customer);
                    }
                }
            }

            return customers;
        }






    }
}