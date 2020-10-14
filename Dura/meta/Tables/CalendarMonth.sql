CREATE TABLE [meta].[CalendarMonth] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Ordinal]          INT            NOT NULL,
    CONSTRAINT [PK_CalendarMonth] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CalendarMonth_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_CalendarMonth_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CalendarMonth_CreatedUserId]
    ON [meta].[CalendarMonth]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_CalendarMonth_Name]
    ON [meta].[CalendarMonth]([Name] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_CalendarMonth_Ordinal]
    ON [meta].[CalendarMonth]([Ordinal] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CalendarMonth_UpdatedUserId]
    ON [meta].[CalendarMonth]([UpdatedUserId] ASC);

