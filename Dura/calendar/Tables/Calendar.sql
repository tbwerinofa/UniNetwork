CREATE TABLE [calendar].[Calendar] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [ScheduleDate]     DATETIME2 (7)   NOT NULL,
    [RevisedDate]      DATETIME2 (7)   NULL,
    [StartTime]        TIME (7)        NOT NULL,
    [Notes]            NVARCHAR (2000) NULL,
    [FinYearId]        INT             NOT NULL,
    [EventId]          INT             NOT NULL,
    [VenueId]          INT             NOT NULL,
    [IsActive]         BIT             DEFAULT ((1)) NOT NULL,
    [CreatedUserId]    NVARCHAR (450)  NOT NULL,
    [UpdatedUserId]    NVARCHAR (450)  NULL,
    [CreatedTimestamp] DATETIME2 (7)   DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)   NULL,
    CONSTRAINT [PK_Calendar] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Calendar_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Calendar_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Calendar_Event_Id] FOREIGN KEY ([EventId]) REFERENCES [calendar].[Event] ([Id]),
    CONSTRAINT [FK_Calendar_Frequency_Id] FOREIGN KEY ([FinYearId]) REFERENCES [meta].[FinYear] ([Id]),
    CONSTRAINT [FK_Calendar_Venue_Id] FOREIGN KEY ([VenueId]) REFERENCES [meta].[Venue] ([Id]),
    CONSTRAINT [UQ_Calendar_EventId_ScheduleDate] UNIQUE NONCLUSTERED ([EventId] ASC, [ScheduleDate] ASC, [StartTime] ASC)
);

