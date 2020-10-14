CREATE TABLE [gis].[City] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (250) NOT NULL,
    [Code]             NVARCHAR (MAX) NULL,
    [ProvinceId]       INT            NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_City_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_City_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_City_Province_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [gis].[Province] ([Id]),
    CONSTRAINT [UQ_gis_City_Name_ProvinceId] UNIQUE NONCLUSTERED ([Name] ASC, [ProvinceId] ASC)
);

