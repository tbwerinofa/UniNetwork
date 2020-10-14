CREATE TABLE [gis].[Province] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [CountryId]        INT            NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Province_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Province_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Province_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [gis].[Country] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Region_CountryId]
    ON [gis].[Province]([CountryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Region_CreatedUserId]
    ON [gis].[Province]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Region_Name_CountryId]
    ON [gis].[Province]([Name] ASC, [CountryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Region_UpdatedUserId]
    ON [gis].[Province]([UpdatedUserId] ASC);

