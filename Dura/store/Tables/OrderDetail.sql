CREATE TABLE [store].[OrderDetail] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [QuoteId]          INT             NOT NULL,
    [ProductSizeId]    INT             NOT NULL,
    [Quantity]         INT             NOT NULL,
    [UnitPrice]        DECIMAL (18, 2) NOT NULL,
    [IsActive]         BIT             DEFAULT ((1)) NOT NULL,
    [CreatedUserId]    NVARCHAR (450)  NOT NULL,
    [UpdatedUserId]    NVARCHAR (450)  NULL,
    [CreatedTimestamp] DATETIME2 (7)   DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)   NULL,
    CONSTRAINT [PK_store.OrderDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_store.OrderDetail_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.OrderDetail_AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.OrderDetail_ProductSize_Id] FOREIGN KEY ([ProductSizeId]) REFERENCES [store].[ProductSize] ([Id]),
    CONSTRAINT [UQ_store.OrderDetail_QuoteId_ProductSizeId] UNIQUE NONCLUSTERED ([QuoteId] ASC, [ProductSizeId] ASC)
);

