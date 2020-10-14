CREATE TABLE [activity].[Distance] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50)  NOT NULL,
    [Measurement]       INT            NOT NULL,
    [MeasurementUnitId] INT            NOT NULL,
    [Discriminator]     NCHAR (4)      NOT NULL,
    [IsActive]          BIT            DEFAULT ((1)) NOT NULL,
    [CreatedUserId]     NVARCHAR (450) NOT NULL,
    [UpdatedUserId]     NVARCHAR (450) NULL,
    [CreatedTimestamp]  DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]  DATETIME2 (7)  NULL,
    CONSTRAINT [PK_activity.Distance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_activity.Distance_Organisation.OrganisationId] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [activity].[MeasurementUnit] ([Id]),
    CONSTRAINT [FK_activity.Distance_Security.User_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_activity.Distance_Security.User_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UQ_activity.Distance_Discriminator] UNIQUE NONCLUSTERED ([Discriminator] ASC),
    CONSTRAINT [UQ_activity.Distance_Measurement_MeasurementUnitId] UNIQUE NONCLUSTERED ([Measurement] ASC, [MeasurementUnitId] ASC),
    CONSTRAINT [UQ_activity.Distance_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

