using System;
using System.Data;
using System.Data.SqlClient;

namespace SaMoCode.RepairManagement.Data
{
    // Commands remain inside the Data layer; values are always parameters.
    internal sealed class SqlSession
    {
        internal readonly SqlConnection Connection;
        internal readonly SqlTransaction Transaction;
        internal SqlSession(SqlConnection connection, SqlTransaction transaction = null)
        { Connection = connection; Transaction = transaction; }
        private SqlCommand Command(string sql, object[] values)
        {
            var command = new SqlCommand(sql, Connection, Transaction);
            for (int i = 0; i < values.Length; i++)
                command.Parameters.AddWithValue("@p" + i, values[i] ?? DBNull.Value);
            return command;
        }
        internal object Scalar(string sql, params object[] values)
        { using (var command = Command(sql, values)) return command.ExecuteScalar(); }
        internal int Execute(string sql, params object[] values)
        { using (var command = Command(sql, values)) return command.ExecuteNonQuery(); }
        internal DataTable Query(string sql, params object[] values)
        {
            using (var command = Command(sql, values))
            using (var reader = command.ExecuteReader())
            { var table = new DataTable(); table.Load(reader); return table; }
        }
    }
}
