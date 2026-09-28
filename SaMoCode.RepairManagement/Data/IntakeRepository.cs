using System;
using System.Collections.Generic;
using System.Data;
using SaMoCode.RepairManagement.Models;

namespace SaMoCode.RepairManagement.Data
{
    public class IntakeRepository
    {
        public DataTable Conditions() { return Lookup("SELECT IntakeConditionId AS Id, Name FROM dbo.IntakeConditions ORDER BY Name"); }
        public DataTable Accessories() { return Lookup("SELECT AccessoryId AS Id, Name FROM dbo.Accessories ORDER BY Name"); }
        private static DataTable Lookup(string sql)
        {
            using (var c = DatabaseConnection.CreateConnection())
            { c.Open(); return new SqlSession(c).Query(sql); }
        }
        public int Create(int customerId, IList<IntakeDevice> devices, string notes)
        {
            using (var c = DatabaseConnection.CreateConnection())
            {
                c.Open();
                using (var tx = c.BeginTransaction())
                {
                    var db = new SqlSession(c, tx);
                    int order = (int)db.Scalar(@"INSERT INTO dbo.RepairOrders(CustomerId,CreatedAt,Status,Notes)
                        VALUES(@p0,SYSDATETIME(),'Open',@p1); SELECT CAST(SCOPE_IDENTITY() AS int);", customerId, notes);
                    foreach (var device in devices)
                    {
                        int id = (int)db.Scalar(@"INSERT INTO dbo.Devices(RepairOrderId,Brand,Model,Color,SerialNumber,Status,IntakeConditionNotes)
                            VALUES(@p0,@p1,@p2,@p3,@p4,'Received',@p5); SELECT CAST(SCOPE_IDENTITY() AS int);",
                            order, device.Brand.Trim(), device.Model.Trim(), device.Color, device.SerialNumber, device.ConditionNotes);
                        foreach (string problem in device.Problems)
                            db.Execute("INSERT INTO dbo.RepairIssues(DeviceId,ReportedProblem,Status) VALUES(@p0,@p1,'Reported')", id, problem.Trim());
                        foreach (int condition in device.ConditionIds)
                            db.Execute("INSERT INTO dbo.DeviceIntakeConditions(DeviceId,IntakeConditionId) VALUES(@p0,@p1)", id, condition);
                        foreach (int accessory in device.AccessoryIds)
                            db.Execute("INSERT INTO dbo.DeviceAccessories(DeviceId,AccessoryId) VALUES(@p0,@p1)", id, accessory);
                        if (!string.IsNullOrWhiteSpace(device.OtherAccessory))
                            db.Execute("INSERT INTO dbo.DeviceAccessories(DeviceId,OtherDescription) VALUES(@p0,@p1)", id, device.OtherAccessory.Trim());
                        db.Execute("INSERT INTO dbo.RepairEvents(DeviceId,EventType,Detail) VALUES(@p0,'Received',@p1)", id, "Device received: " + device.Brand + " " + device.Model);
                    }
                    tx.Commit();
                    return order;
                }
            }
        }
    }
}
