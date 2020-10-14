CREATE TABLE [finance].[QuoteStatus] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50)  NOT NULL,
    [Discriminator]     NVARCHAR (4)   NOT NULL,
    [Description]       NVARCHAR (50)  NOT NULL,
    [RequiresPayment]   BIT            NOT NULL,
    [MessageTemplateId] INT            NOT NULL,
    [CreatedUserId]     NVARCHAR (450) NOT NULL,
    [UpdatedUserId]     NVARCHAR (450) NULL,
    [CreatedTimestamp]  DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]  DATETIME2 (7)  NULL,
    [IsActive]          BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_finance.QuoteStatus] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_finance.QuoteStatus_finance.AspNetUsers_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.QuoteStatus_finance.AspNetUsers_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_finance.QuoteStatus_finance.MessageTemplate_Id] FOREIGN KEY ([MessageTemplateId]) REFERENCES [emailer].[MessageTemplate] ([Id]),
    CONSTRAINT [UQ_finance.QuoteStatus_Name] UNIQUE NONCLUSTERED ([Name] ASC),
    CONSTRAINT [UQ_finance.QuoteStatus_Reference] UNIQUE NONCLUSTERED ([Discriminator] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Name]
    ON [finance].[QuoteStatus]([Name] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_QuoteStatus_Name]
    ON [finance].[QuoteStatus]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Discriminator]
    ON [finance].[QuoteStatus]([Discriminator] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_QuoteStatus_Discriminator]
    ON [finance].[QuoteStatus]([Discriminator] ASC);

