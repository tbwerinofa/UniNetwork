CREATE TABLE [meta].[ShopStewardSetting] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [ValidYears]       INT            NOT NULL,
    [StartDate]        DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_ShopStewardSetting] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ShopStewardSetting_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_ShopStewardSetting_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ShopStewardSetting_CreatedUserId]
    ON [meta].[ShopStewardSetting]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ShopStewardSetting_StartDate]
    ON [meta].[ShopStewardSetting]([StartDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ShopStewardSetting_UpdatedUserId]
    ON [meta].[ShopStewardSetting]([UpdatedUserId] ASC);

