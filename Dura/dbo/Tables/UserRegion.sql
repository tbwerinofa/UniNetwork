CREATE TABLE [dbo].[UserRegion] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]          BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp]  DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]  DATETIME2 (7)  NULL,
    [CreatedUserId]     NVARCHAR (450) NOT NULL,
    [UpdatedUserId]     NVARCHAR (450) NULL,
    [ApplicationUserId] NVARCHAR (450) NOT NULL,
    [RegionId]          INT            NOT NULL,
    CONSTRAINT [PK_UserRegion] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserRegion_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_UserRegion_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_UserRegion_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_UserRegion_Region_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [gis].[Province] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserRegion_ApplicationUserId_RegionId]
    ON [dbo].[UserRegion]([ApplicationUserId] ASC, [RegionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserRegion_CreatedUserId]
    ON [dbo].[UserRegion]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserRegion_RegionId]
    ON [dbo].[UserRegion]([RegionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserRegion_UpdatedUserId]
    ON [dbo].[UserRegion]([UpdatedUserId] ASC);

