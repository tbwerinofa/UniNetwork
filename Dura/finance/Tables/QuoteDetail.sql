CREATE TABLE [finance].[QuoteDetail] (
    [Id]                          INT            IDENTITY (1, 1) NOT NULL,
    [QuoteId]                     INT            NOT NULL,
    [SubscriptionTypeRuleAuditId] INT            NOT NULL,
    [Quantity]                    INT            NOT NULL,
    [ItemNo]                      INT            NOT NULL,
    [CreatedUserId]               NVARCHAR (450) NOT NULL,
    [UpdatedUserId]               NVARCHAR (450) NULL,
    [CreatedTimestamp]            DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]            DATETIME2 (7)  NULL,
    [IsActive]                    BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_fiance.QuoteDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_fiance.QuoteDetail_dbo.AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_fiance.QuoteDetail_dbo.AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_fiance.QuoteDetail_dbo.Quote_Id] FOREIGN KEY ([QuoteId]) REFERENCES [finance].[Quote] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_fiance.QuoteDetail_dbo.SubscriptionTypeRuleAudit_Id] FOREIGN KEY ([SubscriptionTypeRuleAuditId]) REFERENCES [finance].[SubscriptionTypeRuleAudit] ([Id]),
    CONSTRAINT [UQ_fiance.QuoteDetail_QuoteID_SubscriptionTypeId] UNIQUE NONCLUSTERED ([QuoteId] ASC, [SubscriptionTypeRuleAuditId] ASC, [ItemNo] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_QuoteId]
    ON [finance].[QuoteDetail]([QuoteId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DesignId]
    ON [finance].[QuoteDetail]([Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QuoteDetailId]
    ON [finance].[QuoteDetail]([Id] ASC);

