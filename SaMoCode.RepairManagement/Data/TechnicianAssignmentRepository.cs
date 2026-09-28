using System;
using System.Data;
using System.Data.SqlClient;
using SaMoCode.RepairManagement.Models;

namespace SaMoCode.RepairManagement.Data
{
    public class TechnicianAssignmentRepository
    {
        public TechnicianAssignment GetCurrentAssignment(int deviceId)
        {
            using (SqlConnection connection = DatabaseConnection.CreateConnection())
            {
                connection.Open();
                return ReadCurrent(connection, null, deviceId);
            }
        }

        public int Assign(int deviceId, int technicianId)
        {
            return SaveAssignment(deviceId, technicianId, null, false, null);
        }

        public int Transfer(int deviceId, int newTechnicianId, string reason,
            int? expectedAssignmentId = null)
        {
            return SaveAssignment(deviceId, newTechnicianId, reason, true, expectedAssignmentId);
        }

        private static TechnicianAssignment ReadCurrent(SqlConnection connection,
            SqlTransaction transaction, int deviceId)
        {
            string query = @"
                SELECT a.TechnicianAssignmentId, a.DeviceId, a.TechnicianId,
                       a.AssignedAt, a.TransferReason, a.Notes, t.Name AS TechnicianName
                FROM dbo.TechnicianAssignments a " +
                (transaction == null ? "" : "WITH (UPDLOCK, HOLDLOCK) ") + @"
                INNER JOIN dbo.Technicians t ON t.TechnicianId = a.TechnicianId
                WHERE a.DeviceId = @DeviceId AND a.UnassignedAt IS NULL;";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.Add("@DeviceId", SqlDbType.Int).Value = deviceId;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return null;
                    TechnicianAssignment assignment = new TechnicianAssignment
                    {
                        TechnicianAssignmentId = reader.GetInt32(0),
                        DeviceId = reader.GetInt32(1),
                        TechnicianId = reader.GetInt32(2),
                        AssignedAt = reader.GetDateTime(3),
                        TransferReason = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Notes = reader.IsDBNull(5) ? null : reader.GetString(5),
                        TechnicianName = reader.GetString(6)
                    };
                    if (reader.Read())
                        throw new InvalidOperationException("This device has multiple current assignments. Correct the assignment history before continuing.");
                    return assignment;
                }
            }
        }

        private static int SaveAssignment(int deviceId, int technicianId, string reason,
            bool transfer, int? expectedAssignmentId)
        {
            using (SqlConnection connection = DatabaseConnection.CreateConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    // Lock the device even when it has no assignment yet. All assignment
                    // writes for this device must wait until this transaction completes.
                    using (SqlCommand command = new SqlCommand(@"
                        SELECT Status FROM dbo.Devices WITH (UPDLOCK, HOLDLOCK)
                        WHERE DeviceId = @DeviceId;", connection, transaction))
                    {
                        command.Parameters.Add("@DeviceId", SqlDbType.Int).Value = deviceId;
                        object deviceStatus = command.ExecuteScalar();
                        if (deviceStatus == null)
                            throw new InvalidOperationException("The device no longer exists.");
                        if ((string)deviceStatus == "Delivered" || (string)deviceStatus == "Ready for Pickup")
                            throw new InvalidOperationException("This device is already at reception or delivered.");
                    }

                    TechnicianAssignment current = ReadCurrent(connection, transaction, deviceId);
                    if (!transfer && current != null)
                        throw new InvalidOperationException("This device already has a technician. Refresh and use Transfer Technician.");
                    if (transfer && current == null)
                        throw new InvalidOperationException("This device has no current technician. Refresh and use Assign Technician.");
                    if (transfer && expectedAssignmentId.HasValue &&
                        current.TechnicianAssignmentId != expectedAssignmentId.Value)
                        throw new InvalidOperationException("The technician changed while this window was open. Close this window and try again.");
                    if (transfer && current.TechnicianId == technicianId)
                        throw new InvalidOperationException("Please select a different technician.");

                    using (SqlCommand command = new SqlCommand(@"
                        SELECT TechnicianId FROM dbo.Technicians WITH (HOLDLOCK)
                        WHERE TechnicianId = @TechnicianId AND IsActive = 1;", connection, transaction))
                    {
                        command.Parameters.Add("@TechnicianId", SqlDbType.Int).Value = technicianId;
                        object deviceStatus = command.ExecuteScalar();
                        if (deviceStatus == null)
                            throw new InvalidOperationException("Please select an active technician.");
                    }

                    // One database timestamp marks both the end and start of custody.
                    const string query = @"
                        DECLARE @ChangedAt datetime2 = SYSDATETIME();
                        IF @CurrentAssignmentId IS NOT NULL
                        BEGIN
                            UPDATE dbo.TechnicianAssignments
                            SET UnassignedAt = @ChangedAt, TransferReason = @Reason
                            WHERE TechnicianAssignmentId = @CurrentAssignmentId
                              AND UnassignedAt IS NULL;
                            IF @@ROWCOUNT <> 1
                                THROW 50001, 'The assignment changed. Refresh and try again.', 1;
                        END;
                        INSERT INTO dbo.TechnicianAssignments
                            (DeviceId, TechnicianId, AssignedAt)
                        VALUES (@DeviceId, @TechnicianId, @ChangedAt);
                        SELECT CAST(SCOPE_IDENTITY() AS int);";
                    int assignmentId;
                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.Add("@DeviceId", SqlDbType.Int).Value = deviceId;
                        command.Parameters.Add("@TechnicianId", SqlDbType.Int).Value = technicianId;
                        command.Parameters.Add("@CurrentAssignmentId", SqlDbType.Int).Value =
                            current == null ? (object)DBNull.Value : current.TechnicianAssignmentId;
                        command.Parameters.Add("@Reason", SqlDbType.NVarChar, 300).Value =
                            (object)reason ?? DBNull.Value;
                        assignmentId = (int)command.ExecuteScalar();
                    }
                    transaction.Commit();
                    return assignmentId;
                    // Disposing an uncommitted transaction rolls back both changes on error.
                }
            }
        }
    }
}

