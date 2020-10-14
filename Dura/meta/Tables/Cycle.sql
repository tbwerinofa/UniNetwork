CREATE TABLE [meta].[Cycle] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             INT            NOT NULL,
    [Description]      NVARCHAR (50)  NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_meta.Cycle] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_meta.Cycle_Security.User_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_meta.Cycle_Security.User_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UQ_meta.Cycle_EndDate] UNIQUE NONCLUSTERED ([Description] ASC),
    CONSTRAINT [UQ_meta.Cycle_Name] UNIQUE NONCLUSTERED ([Name] ASC),
    CONSTRAINT [UQ_meta.Cycle_StartDate] UNIQUE NONCLUSTERED ([Name] ASC)
);

