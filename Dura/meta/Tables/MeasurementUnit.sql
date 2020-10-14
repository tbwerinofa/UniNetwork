CREATE TABLE [meta].[MeasurementUnit] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]               BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp]       DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]       DATETIME2 (7)  NULL,
    [CreatedUserId]          NVARCHAR (450) NOT NULL,
    [UpdatedUserId]          NVARCHAR (450) NULL,
    [Name]                   NVARCHAR (450) NOT NULL,
    [Ordinal]                INT            NOT NULL,
    [MeasurementUnitGroupId] INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MeasurementUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MeasurementUnit_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_MeasurementUnit_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_MeasurementUnit_MeasurementUnitGroup_MeasurementUnitGroupId] FOREIGN KEY ([MeasurementUnitGroupId]) REFERENCES [meta].[MeasurementUnitGroup] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_MeasurementUnit_CreatedUserId]
    ON [meta].[MeasurementUnit]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MeasurementUnit_MeasurementUnitGroupId]
    ON [meta].[MeasurementUnit]([MeasurementUnitGroupId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MeasurementUnit_Name]
    ON [meta].[MeasurementUnit]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MeasurementUnit_UpdatedUserId]
    ON [meta].[MeasurementUnit]([UpdatedUserId] ASC);

