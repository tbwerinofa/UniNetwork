CREATE TABLE [accolade].[Winner] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [FinYearId]        INT            NOT NULL,
    [CalendarMonthId]  INT            NULL,
    [MemberId]         INT            NOT NULL,
    [AwardId]          INT            NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    CONSTRAINT [PK_Winner] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Winner_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Winner_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Winner_AwardTrophyAudit_Id] FOREIGN KEY ([AwardId]) REFERENCES [accolade].[Award] ([Id]),
    CONSTRAINT [FK_Winner_CalendaMonth_Id] FOREIGN KEY ([CalendarMonthId]) REFERENCES [meta].[CalendarMonth] ([Id]),
    CONSTRAINT [FK_Winner_FinYear_Id] FOREIGN KEY ([FinYearId]) REFERENCES [meta].[FinYear] ([Id])
);

