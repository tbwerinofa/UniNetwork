CREATE TABLE [store].[Size] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [ShortName]        NVARCHAR (3)   NULL,
    [Ordinal]          INT            NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_store.Size] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_store.Size_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.Size_AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UQ_store.Size_Name] UNIQUE NONCLUSTERED ([Name] ASC),
    CONSTRAINT [UQ_store.Size_ShortName] UNIQUE NONCLUSTERED ([ShortName] ASC)
);

