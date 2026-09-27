using System.Data.SqlClient;

namespace SaMoCode.RepairManagement.Data
{
    public static class DatabaseConnection
    {
        private const string ConnectionString =
            @"Server=OBD;Database=SaMoCodeRepairManagement;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}