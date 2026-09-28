CREATE DATABASE SaMoCodeRepairManagement;
GO

USE SaMoCodeRepairManagement;
GO


CREATE TABLE Customers
(
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(30) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    Notes NVARCHAR(500) NULL
);
GO


CREATE TABLE RepairOrders
(
    RepairOrderId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    Status NVARCHAR(30) NOT NULL DEFAULT 'Open',
    Notes NVARCHAR(500) NULL,

    CONSTRAINT FK_RepairOrders_Customers
        FOREIGN KEY (CustomerId)
        REFERENCES Customers(CustomerId)
);
GO


CREATE TABLE Devices
(
    DeviceId INT IDENTITY(1,1) PRIMARY KEY,
    RepairOrderId INT NOT NULL,

    Brand NVARCHAR(50) NOT NULL,
    Model NVARCHAR(100) NOT NULL,
    Color NVARCHAR(50) NULL,
    SerialNumber NVARCHAR(100) NULL,

    Status NVARCHAR(30) NOT NULL DEFAULT 'Received',

    IntakeConditionNotes NVARCHAR(500) NULL,
    Notes NVARCHAR(500) NULL,

    CONSTRAINT FK_Devices_RepairOrders
        FOREIGN KEY (RepairOrderId)
        REFERENCES RepairOrders(RepairOrderId)
);
GO

CREATE TABLE IntakeConditions
(
    IntakeConditionId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE
);
GO


CREATE TABLE DeviceIntakeConditions
(
    DeviceId INT NOT NULL,
    IntakeConditionId INT NOT NULL,

    CONSTRAINT PK_DeviceIntakeConditions
        PRIMARY KEY (DeviceId, IntakeConditionId),

    CONSTRAINT FK_DeviceIntakeConditions_Devices
        FOREIGN KEY (DeviceId)
        REFERENCES Devices(DeviceId),

    CONSTRAINT FK_DeviceIntakeConditions_IntakeConditions
        FOREIGN KEY (IntakeConditionId)
        REFERENCES IntakeConditions(IntakeConditionId)
);
GO
CREATE TABLE Accessories
(
    AccessoryId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE
);
GO


CREATE TABLE DeviceAccessories
(
    DeviceAccessoryId INT IDENTITY(1,1) PRIMARY KEY,

    DeviceId INT NOT NULL,
    AccessoryId INT NULL,

    OtherDescription NVARCHAR(200) NULL,
    Notes NVARCHAR(300) NULL,

    CONSTRAINT FK_DeviceAccessories_Devices
        FOREIGN KEY (DeviceId)
        REFERENCES Devices(DeviceId),

    CONSTRAINT FK_DeviceAccessories_Accessories
        FOREIGN KEY (AccessoryId)
        REFERENCES Accessories(AccessoryId)
);
GO

CREATE TABLE RepairIssues
(
    RepairIssueId INT IDENTITY(1,1) PRIMARY KEY,

    DeviceId INT NOT NULL,

    ReportedProblem NVARCHAR(500) NOT NULL,
    Diagnosis NVARCHAR(500) NULL,
    WorkDone NVARCHAR(500) NULL,

    Status NVARCHAR(30) NOT NULL DEFAULT 'Reported',

    EstimatedPriceMin DECIMAL(10,2) NULL,
    EstimatedPriceMax DECIMAL(10,2) NULL,
    FinalPrice DECIMAL(10,2) NULL,

    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    Notes NVARCHAR(500) NULL,

    CONSTRAINT FK_RepairIssues_Devices
        FOREIGN KEY (DeviceId)
        REFERENCES Devices(DeviceId)
);
GO


CREATE TABLE CustomerContacts
(
    CustomerContactId INT IDENTITY(1,1) PRIMARY KEY,

    RepairIssueId INT NOT NULL,

    ContactMethod NVARCHAR(30) NOT NULL,
    Result NVARCHAR(30) NOT NULL,

    ContactedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    Notes NVARCHAR(500) NULL,

    CONSTRAINT FK_CustomerContacts_RepairIssues
        FOREIGN KEY (RepairIssueId)
        REFERENCES RepairIssues(RepairIssueId)
);
GO

CREATE TABLE Technicians
(
    TechnicianId INT IDENTITY(1,1) PRIMARY KEY,

    Name NVARCHAR(100) NOT NULL,
    Specialization NVARCHAR(100) NULL,

    IsActive BIT NOT NULL DEFAULT 1
);
GO


CREATE TABLE TechnicianAssignments
(
    TechnicianAssignmentId INT IDENTITY(1,1) PRIMARY KEY,

    DeviceId INT NOT NULL,
    TechnicianId INT NOT NULL,

    AssignedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    UnassignedAt DATETIME2 NULL,

    TransferReason NVARCHAR(300) NULL,
    Notes NVARCHAR(500) NULL,

    CONSTRAINT FK_TechnicianAssignments_Devices
        FOREIGN KEY (DeviceId)
        REFERENCES Devices(DeviceId),

    CONSTRAINT FK_TechnicianAssignments_Technicians
        FOREIGN KEY (TechnicianId)
        REFERENCES Technicians(TechnicianId)
);
GO

CREATE UNIQUE INDEX UX_TechnicianAssignments_CurrentDevice
    ON dbo.TechnicianAssignments(DeviceId)
    WHERE UnassignedAt IS NULL;
GO

INSERT INTO IntakeConditions (Name)
VALUES
('Screen Cracked'),
('Back Glass Broken'),
('Frame Scratched'),
('Water Damage'),
('Device Bent'),
('No Visible Damage');
GO


INSERT INTO Accessories (Name)
VALUES
('Charger'),
('Cable'),
('Case'),
('SIM Card'),
('Box');
GO
