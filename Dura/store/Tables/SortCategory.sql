CREATE TABLE [store].[SortCategory] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Discriminator]    NVARCHAR (4)   NOT NULL,
    [Ordinal]          INT            NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_store.SortCategory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_store.SortCategory_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.SortCategory_AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UQ_store.SortCategory_Discriminator] UNIQUE NONCLUSTERED ([Discriminator] ASC),
    CONSTRAINT [UQ_store.SortCategory_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

