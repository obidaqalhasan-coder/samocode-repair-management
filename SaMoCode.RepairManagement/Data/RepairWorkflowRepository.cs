using System.Collections.Generic;
using System;
using System.Data;
using SaMoCode.RepairManagement.Models;

namespace SaMoCode.RepairManagement.Data
{
    public class RepairWorkflowRepository
    {
        private T Read<T>(Func<SqlSession,T> read)
        {
            using (var c = DatabaseConnection.CreateConnection()) { c.Open(); return read(new SqlSession(c)); }
        }
        private T Write<T>(int deviceId, Func<SqlSession,T> change)
        {
            using (var c = DatabaseConnection.CreateConnection())
            {
                c.Open();
                using (var tx = c.BeginTransaction())
                {
                    var db = new SqlSession(c, tx);
                    // All issue, ready and delivery writes serialize on the owning order.
                    object orderId = db.Scalar("SELECT RepairOrderId FROM dbo.Devices WHERE DeviceId=@p0", deviceId);
                    if (orderId == null) throw new InvalidOperationException("Device not found.");
                    db.Scalar("SELECT RepairOrderId FROM dbo.RepairOrders WITH(UPDLOCK,HOLDLOCK) WHERE RepairOrderId=@p0", orderId);
                    string status = (string)db.Scalar("SELECT Status FROM dbo.Devices WITH(UPDLOCK,HOLDLOCK) WHERE DeviceId=@p0", deviceId);
                    if (status == "Delivered") throw new InvalidOperationException("This device has already been delivered.");
                    T result = change(db);
                    tx.Commit();
                    return result;
                }
            }
        }
        public DataTable Issues(int deviceId)
        { return Read(db => db.Query("SELECT RepairIssueId AS Id, ReportedProblem AS Problem, Diagnosis, Status, EstimatedPriceMin AS Minimum, EstimatedPriceMax AS Maximum, FinalPrice, ApprovalStatus AS Approval, QuoteVersion, WorkDone, Notes, DeviceId FROM dbo.RepairIssues WHERE DeviceId=@p0 ORDER BY RepairIssueId", deviceId)); }
        private static WorkflowIssue Map(DataRow r)
        {
            return new WorkflowIssue { Id=(int)r["RepairIssueId"], DeviceId=(int)r["DeviceId"], Problem=(string)r["ReportedProblem"],
                Diagnosis=r["Diagnosis"] as string, Status=(string)r["Status"], Minimum=r.Field<decimal?>("EstimatedPriceMin"),
                Maximum=r.Field<decimal?>("EstimatedPriceMax"), FinalPrice=r.Field<decimal?>("FinalPrice"),
                Approval=(string)r["ApprovalStatus"], QuoteVersion=(int)r["QuoteVersion"], WorkDone=r["WorkDone"] as string, Notes=r["Notes"] as string };
        }
        internal static void Event(SqlSession db, int deviceId, int? issueId, string kind, string detail)
        { db.Execute("INSERT INTO dbo.RepairEvents(DeviceId,RepairIssueId,EventType,Detail) VALUES(@p0,@p1,@p2,@p3)", deviceId, issueId, kind, detail); }
        public void ChangeIssue(int deviceId, int issueId, Func<WorkflowIssue,IssueChange> change)
        {
            Write(deviceId, db => {
                var rows = db.Query("SELECT * FROM dbo.RepairIssues WITH(UPDLOCK,HOLDLOCK) WHERE RepairIssueId=@p0 AND DeviceId=@p1", issueId, deviceId);
                if (rows.Rows.Count != 1) throw new InvalidOperationException("Issue not found for this device.");
                var state = Map(rows.Rows[0]);
                IssueChange record = change(state); // Business rules run against the locked, current state.
                if (record.TechnicianId.HasValue && db.Scalar("SELECT TechnicianId FROM dbo.Technicians WITH(HOLDLOCK) WHERE TechnicianId=@p0 AND IsActive=1", record.TechnicianId) == null)
                    throw new InvalidOperationException("Select an active technician.");
                db.Execute(@"UPDATE dbo.RepairIssues SET Diagnosis=@p0,Status=@p1,EstimatedPriceMin=@p2,EstimatedPriceMax=@p3,
                    FinalPrice=@p4,ApprovalStatus=@p5,QuoteVersion=@p6,WorkDone=@p7 WHERE RepairIssueId=@p8",
                    state.Diagnosis, state.Status, state.Minimum, state.Maximum, state.FinalPrice, state.Approval, state.QuoteVersion, state.WorkDone, issueId);
                if (record.ContactResult != null)
                    db.Execute(@"INSERT INTO dbo.CustomerContacts(RepairIssueId,ContactMethod,Result,ContactedAt,Notes,QuotedMin,QuotedMax,QuoteVersion)
                        VALUES(@p0,@p1,@p2,SYSDATETIME(),@p3,@p4,@p5,@p6)", issueId, record.ContactMethod, record.ContactResult, record.Detail, state.Minimum, state.Maximum, state.QuoteVersion);
                if (record.Event == "Work completed")
                {
                    int workId = (int)db.Scalar(@"INSERT INTO dbo.RepairWork(RepairIssueId,TechnicianId,WorkDone,FinalPrice)
                        VALUES(@p0,@p1,@p2,@p3); SELECT CAST(SCOPE_IDENTITY() AS int);", issueId, record.TechnicianId, state.WorkDone, state.FinalPrice);
                    foreach (var part in record.Parts)
                        db.Execute("INSERT INTO dbo.RepairWorkParts(RepairWorkId,PartName,Quantity) VALUES(@p0,@p1,@p2)", workId, part.Name.Trim(), part.Quantity);
                }
                string eventDetail=record.ContactResult==null?record.Detail:
                    record.ContactMethod+" — "+record.ContactResult+"; quote #"+state.QuoteVersion+" ("+state.Minimum+"–"+state.Maximum+" AED). "+record.Detail;
                Event(db, deviceId, issueId, record.Event, eventDetail);
                Recalculate(db, deviceId);
                return true;
            });
        }
        private static void Recalculate(SqlSession db, int deviceId)
        {
            db.Execute(@"UPDATE dbo.Devices SET Status = CASE
                WHEN EXISTS(SELECT 1 FROM dbo.RepairIssues WHERE DeviceId=@p0 AND Status='In Repair') THEN 'In Repair'
                WHEN EXISTS(SELECT 1 FROM dbo.RepairIssues WHERE DeviceId=@p0 AND Status='Waiting Approval') THEN 'Waiting Approval'
                WHEN EXISTS(SELECT 1 FROM dbo.RepairIssues WHERE DeviceId=@p0 AND Status='Approved') THEN 'Approved'
                ELSE 'Diagnosis' END WHERE DeviceId=@p0", deviceId);
        }
        public void AddIssue(int deviceId, string problem)
        {
            Write(deviceId, db => {
                int id = (int)db.Scalar("INSERT INTO dbo.RepairIssues(DeviceId,ReportedProblem,Status) VALUES(@p0,@p1,'Reported'); SELECT CAST(SCOPE_IDENTITY() AS int);", deviceId, problem);
                Event(db, deviceId, id, "Issue added", problem);
                Recalculate(db, deviceId);
                return true;
            });
        }
        public void Ready(int deviceId,Action<string,IList<WorkflowIssue>> validate)
        {
            Write(deviceId, db => {
                var issues=new List<WorkflowIssue>();
                foreach(DataRow row in db.Query("SELECT * FROM dbo.RepairIssues WHERE DeviceId=@p0",deviceId).Rows)issues.Add(Map(row));
                validate((string)db.Scalar("SELECT Status FROM dbo.Devices WHERE DeviceId=@p0",deviceId),issues);
                db.Execute("UPDATE dbo.Devices SET Status='Ready for Pickup' WHERE DeviceId=@p0", deviceId);
                db.Execute("UPDATE dbo.TechnicianAssignments SET UnassignedAt=SYSDATETIME(),TransferReason='Returned to reception' WHERE DeviceId=@p0 AND UnassignedAt IS NULL", deviceId);
                Event(db, deviceId, null, "Ready for pickup", "Returned to reception; all issues resolved.");
                return true;
            });
        }
        public decimal AmountDue(int deviceId)
        { return Read(db => (decimal)db.Scalar("SELECT COALESCE(SUM(FinalPrice),0) FROM dbo.RepairIssues WHERE DeviceId=@p0 AND Status='Completed'", deviceId)); }
        public void Deliver(int deviceId, string method, decimal expectedAmount,Action<string,decimal,string,decimal> validate)
        {
            Write(deviceId, db => {
                string status=(string)db.Scalar("SELECT Status FROM dbo.Devices WHERE DeviceId=@p0",deviceId);
                decimal amount=(decimal)db.Scalar("SELECT COALESCE(SUM(FinalPrice),0) FROM dbo.RepairIssues WHERE DeviceId=@p0 AND Status='Completed'",deviceId);
                validate(status,amount,method,expectedAmount);
                db.Execute("INSERT INTO dbo.DeviceDeliveries(DeviceId,Amount,PaymentMethod) VALUES(@p0,@p1,@p2)", deviceId, amount, method);
                db.Execute("UPDATE dbo.Devices SET Status='Delivered' WHERE DeviceId=@p0", deviceId);
                db.Execute("UPDATE dbo.TechnicianAssignments SET UnassignedAt=SYSDATETIME(),TransferReason='Delivered to customer' WHERE DeviceId=@p0 AND UnassignedAt IS NULL", deviceId);
                db.Execute(@"UPDATE o SET Status='Closed' FROM dbo.RepairOrders o INNER JOIN dbo.Devices d ON o.RepairOrderId=d.RepairOrderId
                    WHERE d.DeviceId=@p0 AND NOT EXISTS(SELECT 1 FROM dbo.Devices x WHERE x.RepairOrderId=o.RepairOrderId AND x.Status<>'Delivered')", deviceId);
                Event(db, deviceId, null, "Delivered", "Payment recorded: " + amount.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " AED / " + method);
                return true;
            });
        }
        public DataTable Timeline(int deviceId)
        {
            return Read(db => db.Query(@"SELECT CreatedAt AS [Time], EventType AS Event, RepairIssueId AS Issue, Detail
                FROM dbo.RepairEvents WHERE DeviceId=@p0
                UNION ALL SELECT a.AssignedAt,'Technician assigned',NULL,t.Name FROM dbo.TechnicianAssignments a JOIN dbo.Technicians t ON t.TechnicianId=a.TechnicianId WHERE a.DeviceId=@p0
                UNION ALL SELECT a.UnassignedAt,'Custody ended',NULL,t.Name + COALESCE(' — '+a.TransferReason,'') FROM dbo.TechnicianAssignments a JOIN dbo.Technicians t ON t.TechnicianId=a.TechnicianId WHERE a.DeviceId=@p0 AND a.UnassignedAt IS NOT NULL
                ORDER BY [Time] DESC", deviceId));
        }
        public DataTable Work(int deviceId)
        { return Read(db => db.Query(@"SELECT w.CompletedAt, w.RepairIssueId AS Issue, t.Name AS Technician,w.WorkDone,w.FinalPrice,p.PartName,p.Quantity FROM dbo.RepairWork w JOIN dbo.Technicians t ON t.TechnicianId=w.TechnicianId JOIN dbo.RepairIssues i ON i.RepairIssueId=w.RepairIssueId LEFT JOIN dbo.RepairWorkParts p ON p.RepairWorkId=w.RepairWorkId WHERE i.DeviceId=@p0 ORDER BY w.CompletedAt DESC", deviceId)); }
        public DataTable IntakeDetails(int deviceId)
        { return Read(db => db.Query(@"SELECT 'Condition' AS Kind,c.Name AS Detail FROM dbo.DeviceIntakeConditions d JOIN dbo.IntakeConditions c ON c.IntakeConditionId=d.IntakeConditionId WHERE d.DeviceId=@p0
                UNION ALL SELECT 'Condition notes',IntakeConditionNotes FROM dbo.Devices WHERE DeviceId=@p0 AND IntakeConditionNotes IS NOT NULL
                UNION ALL SELECT 'Accessory',COALESCE(a.Name,d.OtherDescription) FROM dbo.DeviceAccessories d LEFT JOIN dbo.Accessories a ON a.AccessoryId=d.AccessoryId WHERE d.DeviceId=@p0", deviceId)); }
        public DataTable CustomerHistory(int customerId)
        { return Read(db => db.Query(@"SELECT o.RepairOrderId AS [Order],o.CreatedAt,o.Status,d.Brand+' '+d.Model AS Device,d.Status AS DeviceStatus FROM dbo.RepairOrders o JOIN dbo.Devices d ON d.RepairOrderId=o.RepairOrderId WHERE o.CustomerId=@p0 ORDER BY o.RepairOrderId DESC", customerId)); }
    }
}


