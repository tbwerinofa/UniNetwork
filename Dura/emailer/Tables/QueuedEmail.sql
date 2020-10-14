CREATE TABLE [emailer].[QueuedEmail] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [IsActive]           BIT             DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp]   DATETIME2 (7)   DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]   DATETIME2 (7)   NULL,
    [CreatedUserId]      NVARCHAR (450)  NOT NULL,
    [UpdatedUserId]      NVARCHAR (450)  NULL,
    [Priority]           INT             NOT NULL,
    [From]               NVARCHAR (MAX)  NOT NULL,
    [FromName]           NVARCHAR (50)   NULL,
    [To]                 NVARCHAR (MAX)  NOT NULL,
    [ToName]             NVARCHAR (MAX)  NOT NULL,
    [ReplyTo]            NVARCHAR (MAX)  NULL,
    [ReplyToName]        NVARCHAR (MAX)  NULL,
    [CC]                 NVARCHAR (MAX)  NULL,
    [BCC]                NVARCHAR (MAX)  NULL,
    [Subject]            NVARCHAR (MAX)  NOT NULL,
    [Body]               NVARCHAR (MAX)  NOT NULL,
    [AttachmentFilePath] NVARCHAR (MAX)  NULL,
    [AttachmentFileName] NVARCHAR (MAX)  NULL,
    [DontSendBeforeDate] DATETIME2 (7)   NULL,
    [SentTries]          INT             NOT NULL,
    [SentOn]             DATETIME2 (7)   NULL,
    [EmailAccountId]     INT             NOT NULL,
    [FileByte]           VARBINARY (MAX) NULL,
    CONSTRAINT [PK_QueuedEmail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_QueuedEmail_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_QueuedEmail_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_QueuedEmail_EmailAccount_EmailAccountId] FOREIGN KEY ([EmailAccountId]) REFERENCES [emailer].[EmailAccount] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_QueuedEmail_CreatedUserId]
    ON [emailer].[QueuedEmail]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QueuedEmail_EmailAccountId]
    ON [emailer].[QueuedEmail]([EmailAccountId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QueuedEmail_UpdatedUserId]
    ON [emailer].[QueuedEmail]([UpdatedUserId] ASC);

