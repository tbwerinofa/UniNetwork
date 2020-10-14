CREATE TABLE [finance].[SubscriptionTypeRuleAudit] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [SubscriptionTypeRuleId] INT             NOT NULL,
    [AmountRand]             NUMERIC (18, 2) NOT NULL,
    [AgeGroupId]             INT             NULL,
    [ActiveMonths]           INT             NULL,
    [HasQuantity]            BIT             NOT NULL,
    [HasRelations]           BIT             NOT NULL,
    [CreatedUserId]          NVARCHAR (450)  NOT NULL,
    [UpdatedUserId]          NVARCHAR (450)  NULL,
    [CreatedTimestamp]       DATETIME2 (7)   DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]       DATETIME2 (7)   NULL,
    [IsActive]               BIT             DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_finance.SubscriptionTypeRuleAudit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_finance.SubscriptionTypeRuleAudit_AgeGroup_Id] FOREIGN KEY ([AgeGroupId]) REFERENCES [activity].[AgeGroup] ([Id]),
    CONSTRAINT [FK_finance.SubscriptionTypeRuleAudit_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.SubscriptionTypeRuleAudit_AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.SubscriptionTypeRuleAudit_SubscriptionType_SubscriptionTypeId] FOREIGN KEY ([SubscriptionTypeRuleId]) REFERENCES [finance].[SubscriptionTypeRule] ([Id]) ON DELETE CASCADE
);

