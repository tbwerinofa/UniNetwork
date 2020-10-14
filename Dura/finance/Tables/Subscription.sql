CREATE TABLE [finance].[Subscription] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [QuoteDetailId]    INT            NOT NULL,
    [MemberId]         INT            NOT NULL,
    [StartDate]        DATETIME2 (7)  NOT NULL,
    [EndDate]          DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_finance.Subscription] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_finance.Subscription_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.Subscription_AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.Subscription_finance.QuoteDetail_Id] FOREIGN KEY ([QuoteDetailId]) REFERENCES [finance].[QuoteDetail] ([Id]),
    CONSTRAINT [FK_finance.Subscription_Member_Id] FOREIGN KEY ([MemberId]) REFERENCES [worker].[Member] ([Id]),
    CONSTRAINT [UQ_finance.Subscription_Name] UNIQUE NONCLUSTERED ([QuoteDetailId] ASC, [MemberId] ASC)
);

