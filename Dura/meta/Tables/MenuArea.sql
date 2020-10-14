CREATE TABLE [meta].[MenuArea] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (50)  NULL,
    [DefaultMenuID]    INT            NULL,
    [Discriminator]    NVARCHAR (450) NOT NULL,
    [Navigable]        BIT            NOT NULL,
    [Ordinal]          INT            NOT NULL,
    CONSTRAINT [PK_MenuArea] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MenuArea_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_MenuArea_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_MenuArea_Menu_DefaultMenuID] FOREIGN KEY ([DefaultMenuID]) REFERENCES [meta].[Menu] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_MenuArea_CreatedUserId]
    ON [meta].[MenuArea]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MenuArea_DefaultMenuID]
    ON [meta].[MenuArea]([DefaultMenuID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MenuArea_Discriminator]
    ON [meta].[MenuArea]([Discriminator] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MenuArea_Name]
    ON [meta].[MenuArea]([Name] ASC) WHERE ([Name] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_MenuArea_UpdatedUserId]
    ON [meta].[MenuArea]([UpdatedUserId] ASC);

