CREATE TABLE [finance].[SubscriptionHistory] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [SubscriptionId]   INT            NOT NULL,
    [QuoteDetailId]    INT            NOT NULL,
    [MemberId]         INT            NOT NULL,
    [StartDate]        DATETIME2 (7)  NOT NULL,
    [EndDate]          DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_finance.SubscriptionHistory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_finance.SubscriptionHistory_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.SubscriptionHistory_AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.SubscriptionHistory_finance.QuoteDetail_Id] FOREIGN KEY ([QuoteDetailId]) REFERENCES [finance].[QuoteDetail] ([Id]),
    CONSTRAINT [FK_finance.SubscriptionHistory_finance.Subscription_Id] FOREIGN KEY ([SubscriptionId]) REFERENCES [finance].[Subscription] ([Id]),
    CONSTRAINT [FK_finance.SubscriptionHistory_Member_Id] FOREIGN KEY ([MemberId]) REFERENCES [worker].[Member] ([Id])
);

