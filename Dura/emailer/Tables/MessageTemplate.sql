CREATE TABLE [emailer].[MessageTemplate] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]              BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp]      DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]      DATETIME2 (7)  NULL,
    [CreatedUserId]         NVARCHAR (450) NOT NULL,
    [UpdatedUserId]         NVARCHAR (450) NULL,
    [Name]                  NVARCHAR (50)  NOT NULL,
    [BccEmailAddresses]     NVARCHAR (MAX) NULL,
    [Subject]               NVARCHAR (MAX) NULL,
    [Body]                  NVARCHAR (MAX) NOT NULL,
    [DelayBeforeSend]       INT            NULL,
    [EmailAccountId]        INT            NOT NULL,
    [LimitedToApplications] BIT            NOT NULL,
    [FromAddress]           NVARCHAR (MAX) NOT NULL,
    [DelayHours]            INT            NOT NULL,
    CONSTRAINT [PK_MessageTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MessageTemplate_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MessageTemplate_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_MessageTemplate_EmailAccount_EmailAccountId] FOREIGN KEY ([EmailAccountId]) REFERENCES [emailer].[EmailAccount] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_MessageTemplate_CreatedUserId]
    ON [emailer].[MessageTemplate]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MessageTemplate_EmailAccountId]
    ON [emailer].[MessageTemplate]([EmailAccountId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MessageTemplate_Name_EmailAccountId]
    ON [emailer].[MessageTemplate]([Name] ASC, [EmailAccountId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MessageTemplate_UpdatedUserId]
    ON [emailer].[MessageTemplate]([UpdatedUserId] ASC);

