CREATE TABLE [dbo].[ApplicationRoleMessageTemplate] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationRoleId] NVARCHAR (450) NOT NULL,
    [MessageTemplateId] INT            NOT NULL,
    [IsActive]          BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp]  DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]  DATETIME2 (7)  NULL,
    [CreatedUserId]     NVARCHAR (450) NOT NULL,
    [UpdatedUserId]     NVARCHAR (450) NULL,
    CONSTRAINT [PK_ApplicationRoleMessageTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApplicationRoleMessageTemplate_AspNetRoles_ApplicationRoleId] FOREIGN KEY ([ApplicationRoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]),
    CONSTRAINT [FK_ApplicationRoleMessageTemplate_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_ApplicationRoleMessageTemplate_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_ApplicationRoleMessageTemplate_MessageTemplate_Id] FOREIGN KEY ([MessageTemplateId]) REFERENCES [emailer].[MessageTemplate] ([Id])
);

