CREATE TABLE [meta].[Title] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]          BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp]  DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]  DATETIME2 (7)  NULL,
    [CreatedUserId]     NVARCHAR (450) NOT NULL,
    [UpdatedUserId]     NVARCHAR (450) NULL,
    [Name]              NVARCHAR (50)  NULL,
    [ApplicationUserId] NVARCHAR (450) NULL,
    CONSTRAINT [PK_Title] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Title_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Title_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Title_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Title_ApplicationUserId]
    ON [meta].[Title]([ApplicationUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Title_CreatedUserId]
    ON [meta].[Title]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Title_Name]
    ON [meta].[Title]([Name] ASC) WHERE ([Name] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_Title_UpdatedUserId]
    ON [meta].[Title]([UpdatedUserId] ASC);

