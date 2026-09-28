-- Run against the existing SaMoCodeRepairManagement database.
-- No history is deleted: conflicting existing assignments must be reviewed first.
SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF EXISTS (
    SELECT DeviceId FROM dbo.TechnicianAssignments
    WHERE UnassignedAt IS NULL
    GROUP BY DeviceId HAVING COUNT(*) > 1
)
BEGIN
    ROLLBACK TRANSACTION;
    THROW 50002, 'Multiple current assignments exist. Review them before applying this migration.', 1;
END;
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE object_id = OBJECT_ID('dbo.TechnicianAssignments')
      AND name = 'UX_TechnicianAssignments_CurrentDevice'
)
    CREATE UNIQUE INDEX UX_TechnicianAssignments_CurrentDevice
        ON dbo.TechnicianAssignments(DeviceId)
        WHERE UnassignedAt IS NULL;
COMMIT TRANSACTION;
