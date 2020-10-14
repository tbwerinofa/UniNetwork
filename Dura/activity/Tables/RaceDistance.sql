CREATE TABLE [activity].[RaceDistance] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [RaceId]           INT            NOT NULL,
    [DistanceId]       INT            NOT NULL,
    [EventDate]        DATETIME2 (7)  NOT NULL,
    [IsSyncronised]    BIT            DEFAULT ((0)) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_activity.RaceDistance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_activity.RaceDistance_FinYear.Id] FOREIGN KEY ([DistanceId]) REFERENCES [activity].[Distance] ([Id]),
    CONSTRAINT [FK_activity.RaceDistance_Race_Id] FOREIGN KEY ([RaceId]) REFERENCES [activity].[Race] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_activity.RaceDistance_Security.User_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_activity.RaceDistance_Security.User_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UQ_activity.RaceDistance_Name] UNIQUE NONCLUSTERED ([RaceId] ASC, [DistanceId] ASC)
);

