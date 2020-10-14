CREATE TABLE [meta].[SystemDocument] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [DocumentId]       INT            NOT NULL,
    [Ordinal]          INT            NOT NULL,
    [FinYearId]        INT            NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_store.SystemDocument] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_store.SystemDocument_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.SystemDocument_AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_store.SystemDocument_meta.Document_Id] FOREIGN KEY ([DocumentId]) REFERENCES [meta].[Document] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_store.SystemDocument_meta.FinYear_Id] FOREIGN KEY ([FinYearId]) REFERENCES [meta].[FinYear] ([Id]),
    CONSTRAINT [UQ_store.SystemDocument_Name] UNIQUE NONCLUSTERED ([DocumentId] ASC, [FinYearId] ASC, [Ordinal] ASC)
);

