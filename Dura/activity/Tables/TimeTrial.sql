CREATE TABLE [activity].[TimeTrial] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [CalendarId]       INT            NOT NULL,
    [RaceTypeId]       INT            NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    CONSTRAINT [PK_TimeTrial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TimeTrial_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_TimeTrial_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_TimeTrial_Calendar_Id] FOREIGN KEY ([CalendarId]) REFERENCES [calendar].[Calendar] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TimeTrial_RaceType_Id] FOREIGN KEY ([RaceTypeId]) REFERENCES [activity].[RaceType] ([Id]),
    CONSTRAINT [UQ_TimeTrial_CalendarId_MemberId] UNIQUE NONCLUSTERED ([CalendarId] ASC, [RaceTypeId] ASC)
);

