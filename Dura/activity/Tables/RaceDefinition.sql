CREATE TABLE [activity].[RaceDefinition] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (250) NOT NULL,
    [ProvinceId]       INT            NOT NULL,
    [DiscplineId]      INT            NOT NULL,
    [RaceTypeId]       INT            NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_dbo.RaceDefinition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.RaceDefinition_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.RaceDefinition_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.RaceDefinition_Displine.Id] FOREIGN KEY ([DiscplineId]) REFERENCES [activity].[Discpline] ([Id]),
    CONSTRAINT [FK_dbo.RaceDefinition_Province.Id] FOREIGN KEY ([ProvinceId]) REFERENCES [gis].[Province] ([Id]),
    CONSTRAINT [FK_dbo.RaceDefinition_RaceType.Id] FOREIGN KEY ([RaceTypeId]) REFERENCES [activity].[RaceType] ([Id]),
    CONSTRAINT [UQ_dbo.RaceDefinition_ProvinceId_Name] UNIQUE NONCLUSTERED ([ProvinceId] ASC, [Name] ASC)
);

