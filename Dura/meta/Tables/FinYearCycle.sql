CREATE TABLE [meta].[FinYearCycle] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [StartDate]        DATETIME2 (7)  NOT NULL,
    [EndDate]          DATETIME2 (7)  NOT NULL,
    [FinYearId]        INT            NOT NULL,
    [CycleId]          INT            NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_dbo.FinYearCycle] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.FinYearCycle_Cycle_Id] FOREIGN KEY ([CycleId]) REFERENCES [meta].[Cycle] ([Id]),
    CONSTRAINT [FK_dbo.FinYearCycle_FinYear.FinYearId] FOREIGN KEY ([FinYearId]) REFERENCES [meta].[FinYear] ([Id]),
    CONSTRAINT [FK_dbo.FinYearCycle_Security.User_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.FinYearCycle_Security.User_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UQ_dbo.FinYearCycle_EndDate] UNIQUE NONCLUSTERED ([EndDate] ASC),
    CONSTRAINT [UQ_dbo.FinYearCycle_Name] UNIQUE NONCLUSTERED ([FinYearId] ASC, [CycleId] ASC),
    CONSTRAINT [UQ_dbo.FinYearCycle_StartDate] UNIQUE NONCLUSTERED ([StartDate] ASC)
);

