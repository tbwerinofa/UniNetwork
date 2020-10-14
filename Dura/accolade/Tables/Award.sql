CREATE TABLE [accolade].[Award] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (250) NOT NULL,
    [FrequencyId]      INT            NOT NULL,
    [GenderId]         INT            NULL,
    [Ordinal]          INT            NULL,
    [HasTrophy]        BIT            NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    CONSTRAINT [PK_Award] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Award_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Award_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Award_Frequency_Id] FOREIGN KEY ([FrequencyId]) REFERENCES [meta].[Frequency] ([Id]),
    CONSTRAINT [FK_Award_Gender_Id] FOREIGN KEY ([GenderId]) REFERENCES [meta].[Gender] ([Id]),
    CONSTRAINT [UQ_Award_Name_Gender_Ordinal] UNIQUE NONCLUSTERED ([Name] ASC, [GenderId] ASC, [Ordinal] ASC)
);

