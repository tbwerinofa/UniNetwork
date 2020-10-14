CREATE TABLE [gis].[GlobalRegion] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_GlobalRegion] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlobalRegion_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_GlobalRegion_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_GlobalRegion_CreatedUserId]
    ON [gis].[GlobalRegion]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_GlobalRegion_Name]
    ON [gis].[GlobalRegion]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GlobalRegion_UpdatedUserId]
    ON [gis].[GlobalRegion]([UpdatedUserId] ASC);

