CREATE TABLE [accolade].[AwardTrophy] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [AwardId]          INT            NOT NULL,
    [TrophyId]         INT            NOT NULL,
    [FinYearId]        INT            NOT NULL,
    [StartDate]        DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    CONSTRAINT [PK_AwardTrophy] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AwardTrophy_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_AwardTrophy_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_AwardTrophy_Award_Id] FOREIGN KEY ([AwardId]) REFERENCES [accolade].[Award] ([Id]),
    CONSTRAINT [FK_AwardTrophy_Trophy_Id] FOREIGN KEY ([TrophyId]) REFERENCES [accolade].[Trophy] ([Id]),
    CONSTRAINT [UQ_AwardTrophy_Award_Id] UNIQUE NONCLUSTERED ([AwardId] ASC, [TrophyId] ASC)
);

