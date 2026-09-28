SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF COL_LENGTH('dbo.RepairIssues', 'ApprovalStatus') IS NULL
    ALTER TABLE dbo.RepairIssues ADD ApprovalStatus nvarchar(30) NOT NULL CONSTRAINT DF_Issue_Approval DEFAULT 'Pending';
IF COL_LENGTH('dbo.RepairIssues', 'QuoteVersion') IS NULL
    ALTER TABLE dbo.RepairIssues ADD QuoteVersion int NOT NULL CONSTRAINT DF_Issue_QuoteVersion DEFAULT 0;
IF COL_LENGTH('dbo.CustomerContacts', 'QuotedMin') IS NULL
    ALTER TABLE dbo.CustomerContacts ADD QuotedMin decimal(10,2) NULL, QuotedMax decimal(10,2) NULL, QuoteVersion int NULL;
IF OBJECT_ID('dbo.RepairEvents') IS NULL
BEGIN
    CREATE TABLE dbo.RepairEvents (
        RepairEventId int IDENTITY PRIMARY KEY,
        DeviceId int NOT NULL REFERENCES dbo.Devices(DeviceId),
        RepairIssueId int NULL REFERENCES dbo.RepairIssues(RepairIssueId),
        EventType nvarchar(50) NOT NULL,
        Detail nvarchar(2000) NOT NULL,
        CreatedAt datetime2 NOT NULL DEFAULT SYSDATETIME()
    );
    CREATE INDEX IX_RepairEvents_Device ON dbo.RepairEvents(DeviceId, CreatedAt);
END;
IF OBJECT_ID('dbo.RepairWork') IS NULL
    CREATE TABLE dbo.RepairWork (
        RepairWorkId int IDENTITY PRIMARY KEY,
        RepairIssueId int NOT NULL REFERENCES dbo.RepairIssues(RepairIssueId),
        TechnicianId int NOT NULL REFERENCES dbo.Technicians(TechnicianId),
        WorkDone nvarchar(500) NOT NULL,
        FinalPrice decimal(10,2) NOT NULL CHECK (FinalPrice >= 0),
        CompletedAt datetime2 NOT NULL DEFAULT SYSDATETIME()
    );
IF OBJECT_ID('dbo.RepairWorkParts') IS NULL
    CREATE TABLE dbo.RepairWorkParts (
        RepairWorkPartId int IDENTITY PRIMARY KEY,
        RepairWorkId int NOT NULL REFERENCES dbo.RepairWork(RepairWorkId),
        PartName nvarchar(200) NOT NULL,
        Quantity decimal(10,2) NOT NULL CHECK (Quantity > 0)
    );
IF OBJECT_ID('dbo.DeviceDeliveries') IS NULL
    CREATE TABLE dbo.DeviceDeliveries (
        DeviceId int NOT NULL PRIMARY KEY REFERENCES dbo.Devices(DeviceId),
        Amount decimal(10,2) NOT NULL CHECK (Amount >= 0),
        PaymentMethod nvarchar(30) NOT NULL CHECK (PaymentMethod IN ('Cash', 'Visa', 'No charge')),
        DeliveredAt datetime2 NOT NULL DEFAULT SYSDATETIME()
    );
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.TechnicianAssignments') AND name = 'UX_TechnicianAssignments_CurrentDevice')
    CREATE UNIQUE INDEX UX_TechnicianAssignments_CurrentDevice ON dbo.TechnicianAssignments(DeviceId) WHERE UnassignedAt IS NULL;
COMMIT TRANSACTION;
