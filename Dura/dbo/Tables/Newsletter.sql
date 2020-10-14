CREATE TABLE [dbo].[Newsletter] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IssueNo]          INT            NOT NULL,
    [ArticleId]        INT            NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Newsletter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Newsletter_Article_Id] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Article] ([Id]),
    CONSTRAINT [FK_Newsletter_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Newsletter_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UQ_Newsletter_FinYearId_CalendarMonthId] UNIQUE NONCLUSTERED ([IssueNo] ASC, [ArticleId] ASC),
    CONSTRAINT [UQ_Newsletter_IssueNo] UNIQUE NONCLUSTERED ([IssueNo] ASC)
);

