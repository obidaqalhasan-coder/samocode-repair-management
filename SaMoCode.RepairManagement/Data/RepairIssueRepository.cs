using System.Data.SqlClient;
using SaMoCode.RepairManagement.Models;

namespace SaMoCode.RepairManagement.Data
{
    public class RepairIssueRepository
    {
        public int Add(RepairIssue issue)
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
                    @Diagnosis,
                    @WorkDone,
                    @Status,
                    @EstimatedPriceMin,
                    @EstimatedPriceMax,
                    @FinalPrice,
                    @CreatedAt,
                    @Notes
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlConnection connection =
                   DatabaseConnection.CreateConnection())
            using (SqlCommand command =
                   new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue(
                    "@DeviceId",
                    issue.DeviceId);

                command.Parameters.AddWithValue(
                    "@ReportedProblem",
                    issue.ReportedProblem);

                command.Parameters.AddWithValue(
                    "@Diagnosis",
                    (object)issue.Diagnosis ?? System.DBNull.Value);

                command.Parameters.AddWithValue(
                    "@WorkDone",
                    (object)issue.WorkDone ?? System.DBNull.Value);

                command.Parameters.AddWithValue(
                    "@Status",
                    issue.Status);

                command.Parameters.AddWithValue(
                    "@EstimatedPriceMin",
                    (object)issue.EstimatedPriceMin ?? System.DBNull.Value);

                command.Parameters.AddWithValue(
                    "@EstimatedPriceMax",
                    (object)issue.EstimatedPriceMax ?? System.DBNull.Value);

                command.Parameters.AddWithValue(
                    "@FinalPrice",
                    (object)issue.FinalPrice ?? System.DBNull.Value);

                command.Parameters.AddWithValue(
                    "@CreatedAt",
                    issue.CreatedAt);

                command.Parameters.AddWithValue(
                    "@Notes",
                    (object)issue.Notes ?? System.DBNull.Value);

                connection.Open();

                return (int)command.ExecuteScalar();
            }
        }
    }
}