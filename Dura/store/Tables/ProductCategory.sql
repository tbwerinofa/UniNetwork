CREATE TABLE [store].[ProductCategory] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Description]      NVARCHAR (500) NULL,
    [Ordinal]          INT            NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_store.ProductCategory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_store.ProductCategory_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.ProductCategory_AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UQ_store.ProductCategory_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

