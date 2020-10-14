CREATE TABLE [meta].[PlantLevel] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Discriminator]    NVARCHAR (4)   NOT NULL,
    CONSTRAINT [PK_PlantLevel] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PlantLevel_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_PlantLevel_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_PlantLevel_CreatedUserId]
    ON [meta].[PlantLevel]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PlantLevel_Discriminator]
    ON [meta].[PlantLevel]([Discriminator] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PlantLevel_Name]
    ON [meta].[PlantLevel]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PlantLevel_UpdatedUserId]
    ON [meta].[PlantLevel]([UpdatedUserId] ASC);

