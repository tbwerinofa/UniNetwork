CREATE TABLE [dbo].[Organisation] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (250) NOT NULL,
    [Abbreviation]       NVARCHAR (10)  NULL,
    [ProvinceId]         INT            NOT NULL,
    [OrganisationTypeId] INT            NOT NULL,
    [IsActive]           BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp]   DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]   DATETIME2 (7)  NULL,
    [CreatedUserId]      NVARCHAR (450) NOT NULL,
    [UpdatedUserId]      NVARCHAR (450) NULL,
    CONSTRAINT [PK_Organisation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo_Organisation_OrganisationTypeId] FOREIGN KEY ([OrganisationTypeId]) REFERENCES [dbo].[OrganisationType] ([Id]),
    CONSTRAINT [FK_dbo_Organisation_Province_Id] FOREIGN KEY ([ProvinceId]) REFERENCES [gis].[Province] ([Id]),
    CONSTRAINT [UQ_dbo.Organisation_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

