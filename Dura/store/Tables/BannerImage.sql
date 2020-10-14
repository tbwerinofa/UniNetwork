CREATE TABLE [store].[BannerImage] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [DocumentId]       INT            NOT NULL,
    [Ordinal]          INT            NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_store.BannerImage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_store.BannerImage_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.BannerImage_AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.BannerImage_store.Document_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [meta].[Document] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ_store.BannerImage_Name] UNIQUE NONCLUSTERED ([DocumentId] ASC)
);

