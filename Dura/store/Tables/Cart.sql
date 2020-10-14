CREATE TABLE [store].[Cart] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [RecordId]         NVARCHAR (MAX) NULL,
    [ProductId]        INT            NOT NULL,
    [ProductSizeId]    INT            NOT NULL,
    [Count]            INT            NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_store.Cart] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_store.Cart_store.Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [store].[Product] ([Id]),
    CONSTRAINT [FK_store.Cart_store.ProductSize_SizeId] FOREIGN KEY ([ProductSizeId]) REFERENCES [store].[ProductSize] ([Id])
);

