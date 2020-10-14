CREATE TABLE [dbo].[Article] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (100) NOT NULL,
    [Author]           NVARCHAR (100) NULL,
    [Body]             NVARCHAR (MAX) NOT NULL,
    [PublishDate]      DATETIME2 (7)  NOT NULL,
    [FinYearId]        INT            NOT NULL,
    [CalendarMonthId]  INT            NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Article_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Article_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Article_CalendarMonth_Id] FOREIGN KEY ([CalendarMonthId]) REFERENCES [meta].[CalendarMonth] ([Id]),
    CONSTRAINT [FK_Article_FinYear_Id] FOREIGN KEY ([FinYearId]) REFERENCES [meta].[FinYear] ([Id]),
    CONSTRAINT [UQ_Article_FinYearId_CalendarMonthId] UNIQUE NONCLUSTERED ([FinYearId] ASC, [CalendarMonthId] ASC, [PublishDate] ASC, [Name] ASC)
);

