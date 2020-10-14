CREATE TABLE [store].[Product] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50)   NOT NULL,
    [Description]       NVARCHAR (500)  NULL,
    [ProductCategoryId] INT             NOT NULL,
    [Price]             DECIMAL (18, 2) NOT NULL,
    [Ordinal]           INT             NOT NULL,
    [Vat]               AS              ([Price]*(0.15)),
    [Hits]              INT             NOT NULL,
    [Sold]              INT             NOT NULL,
    [CreatedUserId]     NVARCHAR (450)  NOT NULL,
    [UpdatedUserId]     NVARCHAR (450)  NULL,
    [CreatedTimestamp]  DATETIME2 (7)   DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]  DATETIME2 (7)   NULL,
    [IsActive]          BIT             DEFAULT ((1)) NOT NULL,
    [IsMain]            BIT             DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_store.Product] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_store.Product_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.Product_AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.Product_store.ProductCategory_ProductCategoryId] FOREIGN KEY ([ProductCategoryId]) REFERENCES [store].[ProductCategory] ([Id]),
    CONSTRAINT [UQ_store.Product_ProductCategoryId_Name] UNIQUE NONCLUSTERED ([ProductCategoryId] ASC, [Name] ASC)
);

