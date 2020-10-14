CREATE TABLE [activity].[TrainingPlanRaceDefinition] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [TrainingPlanId]   INT            NOT NULL,
    [RaceDefinitionId] INT            NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_activity.TrainingPlanRaceDefinition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_activity.TrainingPlanRaceDefinition_Security.User_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_activity.TrainingPlanRaceDefinition_Security.User_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_activity.TrainingPlanRaceDefinition_TrainingPlan_Id] FOREIGN KEY ([TrainingPlanId]) REFERENCES [activity].[TrainingPlan] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ_activity.TrainingPlanRaceDefinition_TrainingPlanID_RaceDefinitionId] UNIQUE NONCLUSTERED ([TrainingPlanId] ASC, [RaceDefinitionId] ASC)
);

