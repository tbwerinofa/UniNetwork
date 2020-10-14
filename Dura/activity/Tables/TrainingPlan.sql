CREATE TABLE [activity].[TrainingPlan] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (250) NOT NULL,
    [Objective]        NVARCHAR (500) NOT NULL,
    [EventId]          INT            NOT NULL,
    [FinYearId]        INT            NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_dbo.TrainingPlan] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.TrainingPlan_Event_Id] FOREIGN KEY ([EventId]) REFERENCES [calendar].[Event] ([Id]),
    CONSTRAINT [FK_dbo.TrainingPlan_FinYear_Id] FOREIGN KEY ([FinYearId]) REFERENCES [meta].[FinYear] ([Id]),
    CONSTRAINT [FK_dbo.TrainingPlan_Security.User_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.TrainingPlan_Security.User_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UQ_dbo.TrainingPlan_Name_Version] UNIQUE NONCLUSTERED ([Name] ASC)
);

