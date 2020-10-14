CREATE TABLE [dbo].[ApplicationRoleMenu] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]          BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp]  DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]  DATETIME2 (7)  NULL,
    [CreatedUserId]     NVARCHAR (450) NOT NULL,
    [UpdatedUserId]     NVARCHAR (450) NULL,
    [ApplicationRoleId] NVARCHAR (450) NOT NULL,
    [MenuId]            INT            NOT NULL,
    CONSTRAINT [PK_ApplicationRoleMenu] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApplicationRoleMenu_AspNetRoles_ApplicationRoleId] FOREIGN KEY ([ApplicationRoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]),
    CONSTRAINT [FK_ApplicationRoleMenu_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_ApplicationRoleMenu_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_ApplicationRoleMenu_Menu_MenuId] FOREIGN KEY ([MenuId]) REFERENCES [meta].[Menu] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ApplicationRoleMenu_ApplicationRoleId_MenuId]
    ON [dbo].[ApplicationRoleMenu]([ApplicationRoleId] ASC, [MenuId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationRoleMenu_CreatedUserId]
    ON [dbo].[ApplicationRoleMenu]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationRoleMenu_MenuId]
    ON [dbo].[ApplicationRoleMenu]([MenuId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationRoleMenu_UpdatedUserId]
    ON [dbo].[ApplicationRoleMenu]([UpdatedUserId] ASC);

