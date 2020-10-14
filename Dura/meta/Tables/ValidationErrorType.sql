CREATE TABLE [meta].[ValidationErrorType] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Discriminator]    NVARCHAR (450) NOT NULL,
    [Description]      NVARCHAR (250) NULL,
    CONSTRAINT [PK_ValidationErrorType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ValidationErrorType_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_ValidationErrorType_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ValidationErrorType_CreatedUserId]
    ON [meta].[ValidationErrorType]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ValidationErrorType_Discriminator]
    ON [meta].[ValidationErrorType]([Discriminator] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ValidationErrorType_Name]
    ON [meta].[ValidationErrorType]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ValidationErrorType_UpdatedUserId]
    ON [meta].[ValidationErrorType]([UpdatedUserId] ASC);

