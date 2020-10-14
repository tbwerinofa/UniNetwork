CREATE TABLE [meta].[MenuGroup] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Icon]             NVARCHAR (MAX) NOT NULL,
    [Ordinal]          INT            NOT NULL,
    CONSTRAINT [PK_MenuGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MenuGroup_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_MenuGroup_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_MenuGroup_CreatedUserId]
    ON [meta].[MenuGroup]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MenuGroup_Name]
    ON [meta].[MenuGroup]([Name] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MenuGroup_Ordinal]
    ON [meta].[MenuGroup]([Ordinal] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MenuGroup_UpdatedUserId]
    ON [meta].[MenuGroup]([UpdatedUserId] ASC);

