CREATE TABLE [store].[ProductSize] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [ProductId]        INT            NOT NULL,
    [SizeId]           INT            NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_store.ProductSize] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_store.ProductSize_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.ProductSize_AspNetUsers_Updated] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.ProductSize_store.Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [store].[Product] ([Id]),
    CONSTRAINT [FK_store.ProductSize_store.Size_SizeId] FOREIGN KEY ([SizeId]) REFERENCES [store].[Size] ([Id]),
    CONSTRAINT [UQ_store.ProductSize_ProductId_SizeId] UNIQUE NONCLUSTERED ([ProductId] ASC, [SizeId] ASC)
);

