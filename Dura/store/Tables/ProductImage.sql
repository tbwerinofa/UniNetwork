CREATE TABLE [store].[ProductImage] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [DocumentId]       INT            NOT NULL,
    [ProductId]        INT            NOT NULL,
    [Ordinal]          INT            NOT NULL,
    [IsFeatured]       BIT            NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_store.ProductImage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_store.ProductImage_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.ProductImage_AspNetUsers_Updated] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.ProductImage_store.Document_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [meta].[Document] ([Id]),
    CONSTRAINT [FK_store.ProductImage_store.Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [store].[Product] ([Id]),
    CONSTRAINT [UQ_store.ProductImage_ProductId_DocumentId] UNIQUE NONCLUSTERED ([ProductId] ASC, [DocumentId] ASC)
);

