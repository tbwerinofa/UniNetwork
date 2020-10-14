CREATE TABLE [finance].[Quote] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [QuoteNo]          NVARCHAR (50)  NULL,
    [QuoteStatusId]    INT            NOT NULL,
    [FinYearId]        INT            NOT NULL,
    [QuoteUserId]      NVARCHAR (450) NOT NULL,
    [PaymentDate]      DATETIME2 (7)  NULL,
    [PaymentReference] NVARCHAR (250) NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_finance.Quote] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_finance.Quote_finance.AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.Quote_finance.AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.Quote_finance.AspNetUsers_UserId] FOREIGN KEY ([QuoteUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.Quote_finance.Finyear_Id] FOREIGN KEY ([FinYearId]) REFERENCES [meta].[FinYear] ([Id]),
    CONSTRAINT [FK_finance.Quote_finance.QuoteStatus_QuoteStatusId] FOREIGN KEY ([QuoteStatusId]) REFERENCES [finance].[QuoteStatus] ([Id]),
    CONSTRAINT [UQ_finance.QuoteNo] UNIQUE NONCLUSTERED ([QuoteNo] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_QuoteStatusId]
    ON [finance].[Quote]([Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QuoteNo]
    ON [finance].[Quote]([QuoteNo] ASC);

