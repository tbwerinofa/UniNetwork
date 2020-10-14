CREATE TABLE [meta].[MenuSection] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (50)  NULL,
    [Ordinal]          INT            NOT NULL,
    [MenuGroupId]      INT            NOT NULL,
    CONSTRAINT [PK_MenuSection] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MenuSection_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_MenuSection_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_MenuSection_MenuGroup_MenuGroupId] FOREIGN KEY ([MenuGroupId]) REFERENCES [meta].[MenuGroup] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_MenuSection_CreatedUserId]
    ON [meta].[MenuSection]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MenuSection_MenuGroupId]
    ON [meta].[MenuSection]([MenuGroupId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MenuSection_Name_MenuGroupId]
    ON [meta].[MenuSection]([Name] ASC, [MenuGroupId] ASC) WHERE ([Name] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_MenuSection_UpdatedUserId]
    ON [meta].[MenuSection]([UpdatedUserId] ASC);

