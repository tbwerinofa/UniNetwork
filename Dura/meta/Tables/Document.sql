CREATE TABLE [meta].[Document] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [DocumentTypeId]   INT             NOT NULL,
    [DocumentData]     VARBINARY (MAX) NULL,
    [DocumentNameGuid] NVARCHAR (250)  NOT NULL,
    [Name]             NVARCHAR (500)  NOT NULL,
    [Comments]         NVARCHAR (MAX)  NULL,
    [IsActive]         BIT             DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)   DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)   NULL,
    [CreatedUserId]    NVARCHAR (450)  NOT NULL,
    [UpdatedUserId]    NVARCHAR (450)  NULL,
    CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Document_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Document_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Document_DocumentType_DocumentTypeId] FOREIGN KEY ([DocumentTypeId]) REFERENCES [dbo].[DocumentType] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Document_CreatedUserId]
    ON [meta].[Document]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Document_DocumentTypeId]
    ON [meta].[Document]([DocumentTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Document_UpdatedUserId]
    ON [meta].[Document]([UpdatedUserId] ASC);

