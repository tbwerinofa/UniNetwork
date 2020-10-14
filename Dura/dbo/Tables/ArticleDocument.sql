CREATE TABLE [dbo].[ArticleDocument] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [ArticleId]        INT            NOT NULL,
    [DocumentId]       INT            NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_ArticleDocument] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ArticleDocument_Article_Id] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Article] ([Id]),
    CONSTRAINT [FK_ArticleDocument_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_ArticleDocument_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_ArticleDocument_Document_Id] FOREIGN KEY ([DocumentId]) REFERENCES [meta].[Document] ([Id]),
    CONSTRAINT [UQ_ArticleDocument_ArticleId_DocumentId] UNIQUE NONCLUSTERED ([ArticleId] ASC, [DocumentId] ASC)
);

