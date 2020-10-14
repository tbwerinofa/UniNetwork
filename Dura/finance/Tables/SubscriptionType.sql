CREATE TABLE [finance].[SubscriptionType] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (100) NOT NULL,
    [Discriminator]    CHAR (4)       NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_finance.SubscriptionType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_finance.SubscriptionType_AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.SubscriptionType_AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UQ_finance.SubscriptionType_Name] UNIQUE NONCLUSTERED ([Name] ASC),
    CONSTRAINT [UQ_finance.SubscriptionType_Reference] UNIQUE NONCLUSTERED ([Discriminator] ASC)
);

