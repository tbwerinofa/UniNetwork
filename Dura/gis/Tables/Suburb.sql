CREATE TABLE [gis].[Suburb] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50)   NOT NULL,
    [BoxCode]          NVARCHAR (4)    NOT NULL,
    [StreetCode]       NVARCHAR (4)    NOT NULL,
    [PostCode]         NVARCHAR (4)    NOT NULL,
    [Latitude]         NUMERIC (38, 8) NULL,
    [Longitude]        NUMERIC (38, 8) NULL,
    [TownId]           INT             NOT NULL,
    [CreatedUserId]    NVARCHAR (450)  NOT NULL,
    [UpdatedUserId]    NVARCHAR (450)  NULL,
    [IsActive]         BIT             DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)   DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)   NULL,
    CONSTRAINT [PK_gis_Suburb] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_gis_Suburb_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_gis_Suburb_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_gis_Suburb_Town_Id] FOREIGN KEY ([TownId]) REFERENCES [gis].[Town] ([Id]),
    CONSTRAINT [UQ_gis_Suburb_Name_TownId] UNIQUE NONCLUSTERED ([Name] ASC, [TownId] ASC)
);

