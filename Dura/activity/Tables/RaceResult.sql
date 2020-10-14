CREATE TABLE [activity].[RaceResult] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [RaceDistanceId]   INT            NOT NULL,
    [MemberId]         INT            NOT NULL,
    [TimeTaken]        TIME (7)       NULL,
    [Position]         INT            NULL,
    [AgeGroupId]       INT            NOT NULL,
    [Discriminator]    INT            NULL,
    [AveragePace]      AS             ([activity].[CalculateAveragePace]([RaceDistanceId],[TimeTaken])),
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_activity.RaceResult] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_activity.AgeGroup_Id] FOREIGN KEY ([AgeGroupId]) REFERENCES [activity].[AgeGroup] ([Id]),
    CONSTRAINT [FK_activity.RaceResult_Member.Id] FOREIGN KEY ([MemberId]) REFERENCES [worker].[Member] ([Id]),
    CONSTRAINT [FK_activity.RaceResult_RaceDistance.Id] FOREIGN KEY ([RaceDistanceId]) REFERENCES [activity].[RaceDistance] ([Id]),
    CONSTRAINT [FK_activity.RaceResult_Security.User_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_activity.RaceResult_Security.User_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UQ_activity.RaceResult_Name] UNIQUE NONCLUSTERED ([RaceDistanceId] ASC, [MemberId] ASC)
);

