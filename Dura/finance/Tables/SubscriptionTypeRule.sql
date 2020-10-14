CREATE TABLE [finance].[SubscriptionTypeRule] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [SubscriptionTypeId] INT             NOT NULL,
    [AmountRand]         NUMERIC (18, 2) NOT NULL,
    [AgeGroupId]         INT             NULL,
    [ActiveMonths]       INT             NULL,
    [HasQuantity]        BIT             NOT NULL,
    [HasRelations]       BIT             NOT NULL,
    [CreatedUserId]      NVARCHAR (450)  NOT NULL,
    [UpdatedUserId]      NVARCHAR (450)  NULL,
    [CreatedTimestamp]   DATETIME2 (7)   DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]   DATETIME2 (7)   NULL,
    [IsActive]           BIT             DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_finance.SubscriptionTypeRule] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_finance.SubscriptionTypeRule_AgeGroup_Id] FOREIGN KEY ([AgeGroupId]) REFERENCES [activity].[AgeGroup] ([Id]),
    CONSTRAINT [FK_finance.SubscriptionTypeRule_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.SubscriptionTypeRule_AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.SubscriptionTypeRule_SubscriptionType_SubscriptionTypeId] FOREIGN KEY ([SubscriptionTypeId]) REFERENCES [finance].[SubscriptionType] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ_finance.SubscriptionTypeRule_SubscriptionTypeId] UNIQUE NONCLUSTERED ([SubscriptionTypeId] ASC, [AgeGroupId] ASC)
);

