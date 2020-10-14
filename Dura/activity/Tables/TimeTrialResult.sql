CREATE TABLE [activity].[TimeTrialResult] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [TimeTrialDistanceId] INT            NOT NULL,
    [MemberId]            INT            NOT NULL,
    [Position]            INT            NULL,
    [TimeTaken]           TIME (7)       NOT NULL,
    [AveragePace]         AS             ([activity].[CalculateAveragePace_TimeTrial]([TimeTrialDistanceId],[TimeTaken])),
    [CreatedUserId]       NVARCHAR (450) NOT NULL,
    [UpdatedUserId]       NVARCHAR (450) NULL,
    [CreatedTimestamp]    DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]    DATETIME2 (7)  NULL,
    [IsActive]            BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_activity.TimeTrialResult] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_activity.TimeTrialResult_Member.Id] FOREIGN KEY ([MemberId]) REFERENCES [worker].[Member] ([Id]),
    CONSTRAINT [FK_activity.TimeTrialResult_Security.User_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_activity.TimeTrialResult_Security.User_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_activity.TimeTrialResult_TimeTrialDistance.Id] FOREIGN KEY ([TimeTrialDistanceId]) REFERENCES [activity].[TimeTrialDistance] ([Id]),
    CONSTRAINT [UQ_activity.TimeTrialResult_Name] UNIQUE NONCLUSTERED ([TimeTrialDistanceId] ASC, [MemberId] ASC)
);

