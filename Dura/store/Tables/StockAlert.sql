CREATE TABLE [store].[StockAlert] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [ProductId]        INT            NOT NULL,
    [ProductSizeId]    INT            NULL,
    [UserId]           NVARCHAR (450) NULL,
    [Email]            NVARCHAR (200) NULL,
    [IsAlertSent]      BIT            DEFAULT ((0)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_store.StockAlert] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_store.StockAlert_AspNetUsers_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.StockAlert_store.Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [store].[Product] ([Id]),
    CONSTRAINT [FK_store.StockAlert_store.Size_SizeId] FOREIGN KEY ([ProductSizeId]) REFERENCES [store].[ProductSize] ([Id])
);

