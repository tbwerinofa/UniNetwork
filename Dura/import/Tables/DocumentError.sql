CREATE TABLE [import].[DocumentError] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [DocumentId]       INT            NOT NULL,
    [Description]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_DocumentError] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DocumentError_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_DocumentError_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_DocumentError_Document_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [meta].[Document] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DocumentError_CreatedUserId]
    ON [import].[DocumentError]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DocumentError_DocumentId]
    ON [import].[DocumentError]([DocumentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DocumentError_UpdatedUserId]
    ON [import].[DocumentError]([UpdatedUserId] ASC);

