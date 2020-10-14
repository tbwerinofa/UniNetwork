CREATE TABLE [gis].[Town] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [CityId]           INT            NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_gis_Town] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_gis_Town_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_gis_Town_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_gis_Town_City_Id] FOREIGN KEY ([CityId]) REFERENCES [gis].[City] ([Id]),
    CONSTRAINT [UQ_gis_Town_Name_CityId] UNIQUE NONCLUSTERED ([Name] ASC, [CityId] ASC)
);

