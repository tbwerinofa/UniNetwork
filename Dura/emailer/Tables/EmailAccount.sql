CREATE TABLE [emailer].[EmailAccount] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]              BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp]      DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]      DATETIME2 (7)  NULL,
    [CreatedUserId]         NVARCHAR (450) NOT NULL,
    [UpdatedUserId]         NVARCHAR (450) NULL,
    [Email]                 NVARCHAR (MAX) NOT NULL,
    [DisplayName]           NVARCHAR (50)  NOT NULL,
    [Host]                  NVARCHAR (MAX) NOT NULL,
    [Port]                  INT            NOT NULL,
    [Username]              NVARCHAR (MAX) NULL,
    [Password]              NVARCHAR (MAX) NULL,
    [EnableSsl]             BIT            NOT NULL,
    [UseDefaultCredentials] BIT            NOT NULL,
    CONSTRAINT [PK_EmailAccount] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EmailAccount_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EmailAccount_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_EmailAccount_CreatedUserId]
    ON [emailer].[EmailAccount]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_EmailAccount_DisplayName]
    ON [emailer].[EmailAccount]([DisplayName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EmailAccount_UpdatedUserId]
    ON [emailer].[EmailAccount]([UpdatedUserId] ASC);

