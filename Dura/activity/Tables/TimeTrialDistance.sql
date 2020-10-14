CREATE TABLE [activity].[TimeTrialDistance] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [TimeTrialId]      INT            NOT NULL,
    [DistanceId]       INT            NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_activity.TimeTrialDistance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_activity.TimeTrialDistance_Distance.Id] FOREIGN KEY ([DistanceId]) REFERENCES [activity].[Distance] ([Id]),
    CONSTRAINT [FK_activity.TimeTrialDistance_Security.User_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_activity.TimeTrialDistance_Security.User_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_activity.TimeTrialDistance_TimeTrial_Id] FOREIGN KEY ([TimeTrialId]) REFERENCES [activity].[TimeTrial] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ_activity.TimeTrialDistance_Name] UNIQUE NONCLUSTERED ([TimeTrialId] ASC, [DistanceId] ASC)
);

