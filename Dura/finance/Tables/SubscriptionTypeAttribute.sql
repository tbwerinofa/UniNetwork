CREATE TABLE [finance].[SubscriptionTypeAttribute] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [SubscriptionTypeId] INT            NOT NULL,
    [Name]               NVARCHAR (50)  NOT NULL,
    [CreatedUserId]      NVARCHAR (450) NOT NULL,
    [UpdatedUserId]      NVARCHAR (450) NULL,
    [CreatedTimestamp]   DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]   DATETIME2 (7)  NULL,
    [IsActive]           BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_finance.SubscriptionTypeAttribute] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_finance.SubscriptionTypeAttribute_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.SubscriptionTypeAttribute_AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.SubscriptionTypeAttribute_SubscriptionType_SubscriptionTypeId] FOREIGN KEY ([SubscriptionTypeId]) REFERENCES [finance].[SubscriptionType] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ_finance.SubscriptionTypeAttribute_SubscriptionTypeId] UNIQUE NONCLUSTERED ([SubscriptionTypeId] ASC, [Name] ASC)
);

