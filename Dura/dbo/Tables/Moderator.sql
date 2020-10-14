CREATE TABLE [dbo].[Moderator] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [CalendarId]       INT            NOT NULL,
    [MemberId]         INT            NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    CONSTRAINT [PK_Moderator] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Moderator_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Moderator_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Moderator_Calendar_Id] FOREIGN KEY ([CalendarId]) REFERENCES [calendar].[Calendar] ([Id]),
    CONSTRAINT [FK_Moderator_Member_Id] FOREIGN KEY ([MemberId]) REFERENCES [worker].[Member] ([Id]),
    CONSTRAINT [UQ_Moderator_CalendarId_MemberId] UNIQUE NONCLUSTERED ([CalendarId] ASC, [MemberId] ASC)
);

