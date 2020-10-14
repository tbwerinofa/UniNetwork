CREATE TABLE [activity].[Race] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Theme]            NVARCHAR (250) NULL,
    [FinYearId]        INT            NOT NULL,
    [RaceDefinitionId] INT            NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_activity.Race] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Race_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.Race_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.Race_FinYear.Id] FOREIGN KEY ([FinYearId]) REFERENCES [meta].[FinYear] ([Id]),
    CONSTRAINT [UQ_activity.Race_FinYearId_RaceDefinitionId] UNIQUE NONCLUSTERED ([FinYearId] ASC, [RaceDefinitionId] ASC)
);

