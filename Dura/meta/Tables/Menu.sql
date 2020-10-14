CREATE TABLE [meta].[Menu] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Controller]       NVARCHAR (50)  NOT NULL,
    [ActionResult]     NVARCHAR (50)  NOT NULL,
    [Icon]             NVARCHAR (MAX) NULL,
    [Parameter]        NVARCHAR (MAX) NULL,
    [MenuAreaId]       INT            NOT NULL,
    [MenuSectionId]    INT            NOT NULL,
    [Ordinal]          INT            NOT NULL,
    [HasArea]          BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Menu_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Menu_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Menu_MenuArea_MenuAreaId] FOREIGN KEY ([MenuAreaId]) REFERENCES [meta].[MenuArea] ([Id]),
    CONSTRAINT [FK_Menu_MenuSection_MenuSectionId] FOREIGN KEY ([MenuSectionId]) REFERENCES [meta].[MenuSection] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Menu_Controller_ActionResult]
    ON [meta].[Menu]([Controller] ASC, [ActionResult] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Menu_CreatedUserId]
    ON [meta].[Menu]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Menu_MenuAreaId]
    ON [meta].[Menu]([MenuAreaId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Menu_MenuSectionId_Ordinal]
    ON [meta].[Menu]([MenuSectionId] ASC, [Ordinal] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Menu_UpdatedUserId]
    ON [meta].[Menu]([UpdatedUserId] ASC);

