CREATE TABLE [store].[FeaturedImage] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [ProductImageId]     INT            NOT NULL,
    [FeaturedCategoryId] INT            NOT NULL,
    [CreatedUserId]      NVARCHAR (450) NOT NULL,
    [UpdatedUserId]      NVARCHAR (450) NULL,
    [CreatedTimestamp]   DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]   DATETIME2 (7)  NULL,
    [IsActive]           BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_store.FeaturedImage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_store.FeaturedImage_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.FeaturedImage_AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.FeaturedImage_store.FeaturedCategory_FeaturedCategoryId] FOREIGN KEY ([FeaturedCategoryId]) REFERENCES [store].[FeaturedCategory] ([Id]),
    CONSTRAINT [FK_store.FeaturedImage_store.ProductImage_ProductImageId] FOREIGN KEY ([ProductImageId]) REFERENCES [store].[ProductImage] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ_store.FeaturedImage_ProductImageID_FeaturedImageId] UNIQUE NONCLUSTERED ([ProductImageId] ASC, [FeaturedCategoryId] ASC)
);

