CREATE TABLE [gis].[Country] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (100) NOT NULL,
    [Code]             NVARCHAR (MAX) NULL,
    [GlobalRegionId]   INT            NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Country_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Country_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Country_GlobalRegion_GlobalRegionId] FOREIGN KEY ([GlobalRegionId]) REFERENCES [gis].[GlobalRegion] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Country_CreatedUserId]
    ON [gis].[Country]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Country_GlobalRegionId]
    ON [gis].[Country]([GlobalRegionId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Country_Name]
    ON [gis].[Country]([Name] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Country_Name_GlobalRegionId]
    ON [gis].[Country]([Name] ASC, [GlobalRegionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Country_UpdatedUserId]
    ON [gis].[Country]([UpdatedUserId] ASC);

