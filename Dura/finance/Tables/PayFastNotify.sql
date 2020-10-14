CREATE TABLE [finance].[PayFastNotify] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [M_payment_id]     INT            NULL,
    [Pf_payment_id]    INT            NOT NULL,
    [Payment_status]   NVARCHAR (250) NULL,
    [Item_name]        NVARCHAR (250) NOT NULL,
    [Item_description] NVARCHAR (250) NULL,
    [Amount_gross]     DECIMAL (18)   NOT NULL,
    [Amount_fee]       DECIMAL (18)   NOT NULL,
    [Amount_net]       DECIMAL (18)   NOT NULL,
    [Custom_int1]      INT            NOT NULL,
    [Custom_str1]      INT            NOT NULL,
    [Name_first]       NVARCHAR (250) NULL,
    [Name_last]        NVARCHAR (250) NULL,
    [Email_address]    NVARCHAR (250) NULL,
    [Isprocessed]      BIT            DEFAULT ((0)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_finance.PayFastNotify] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_finance.PayFastNotify_dbo.Quote_ID] FOREIGN KEY ([Custom_int1]) REFERENCES [finance].[Quote] ([Id])
);

