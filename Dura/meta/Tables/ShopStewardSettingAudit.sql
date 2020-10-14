CREATE TABLE [meta].[ShopStewardSettingAudit] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]             BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp]     DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]     DATETIME2 (7)  NULL,
    [CreatedUserId]        NVARCHAR (450) NOT NULL,
    [UpdatedUserId]        NVARCHAR (450) NULL,
    [ShopStewardSettingId] INT            NOT NULL,
    [ValidYears]           INT            NOT NULL,
    [StartDate]            DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_ShopStewardSettingAudit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ShopStewardSettingAudit_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_ShopStewardSettingAudit_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_ShopStewardSettingAudit_ShopStewardSetting_ShopStewardSettingId] FOREIGN KEY ([ShopStewardSettingId]) REFERENCES [meta].[ShopStewardSetting] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ShopStewardSettingAudit_CreatedUserId]
    ON [meta].[ShopStewardSettingAudit]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ShopStewardSettingAudit_ShopStewardSettingId_StartDate]
    ON [meta].[ShopStewardSettingAudit]([ShopStewardSettingId] ASC, [StartDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ShopStewardSettingAudit_UpdatedUserId]
    ON [meta].[ShopStewardSettingAudit]([UpdatedUserId] ASC);

