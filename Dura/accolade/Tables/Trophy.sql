CREATE TABLE [accolade].[Trophy] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Description]      NVARCHAR (MAX) NULL,
    [DocumentId]       INT            NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    CONSTRAINT [PK_Trophy] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Trophy_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Trophy_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Trophy_Document_Id] FOREIGN KEY ([DocumentId]) REFERENCES [meta].[Document] ([Id]),
    CONSTRAINT [FK_Trophy_AspNetUsers_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

