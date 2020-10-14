CREATE TABLE [dbo].[DocumentType] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Discriminator]    NVARCHAR (4)   NOT NULL,
    CONSTRAINT [PK_DocumentType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DocumentType_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_DocumentType_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_DocumentType_CreatedUserId]
    ON [dbo].[DocumentType]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_DocumentType_Name]
    ON [dbo].[DocumentType]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DocumentType_UpdatedUserId]
    ON [dbo].[DocumentType]([UpdatedUserId] ASC);

