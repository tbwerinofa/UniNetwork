CREATE TABLE [accolade].[AwardTrophyAudit] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [AwardTrophyId]    INT            NOT NULL,
    [FinYearId]        INT            NOT NULL,
    [StartDate]        DATETIME2 (7)  NOT NULL,
    [EndDate]          DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    CONSTRAINT [PK_AwardTrophyAudit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AwardTrophyAudit_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_AwardTrophyAudit_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_AwardTrophyAudit_Award_Id] FOREIGN KEY ([AwardTrophyId]) REFERENCES [accolade].[AwardTrophy] ([Id]),
    CONSTRAINT [FK_AwardTrophyAudit_Trophy_Id] FOREIGN KEY ([FinYearId]) REFERENCES [meta].[FinYear] ([Id])
);

