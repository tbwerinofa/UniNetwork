CREATE TABLE [store].[Item] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [ProductSizeId]    INT            NOT NULL,
    [Quantity]         INT            NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_store.Item] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_store.Item_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.Item_AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.Item_store.ProductSize_ProductSize_ProductSizeId] FOREIGN KEY ([ProductSizeId]) REFERENCES [store].[ProductSize] ([Id]),
    CONSTRAINT [UQ_store.Item_ProductSizeId] UNIQUE NONCLUSTERED ([ProductSizeId] ASC)
);

