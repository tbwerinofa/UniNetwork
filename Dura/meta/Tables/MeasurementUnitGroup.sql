CREATE TABLE [meta].[MeasurementUnitGroup] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_MeasurementUnitGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MeasurementUnitGroup_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_MeasurementUnitGroup_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_MeasurementUnitGroup_CreatedUserId]
    ON [meta].[MeasurementUnitGroup]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MeasurementUnitGroup_Name]
    ON [meta].[MeasurementUnitGroup]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MeasurementUnitGroup_UpdatedUserId]
    ON [meta].[MeasurementUnitGroup]([UpdatedUserId] ASC);

