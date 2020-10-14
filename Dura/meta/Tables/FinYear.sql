CREATE TABLE [meta].[FinYear] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             INT            NOT NULL,
    [StartDate]        DATETIME2 (7)  NOT NULL,
    [EndDate]          DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_FinYear] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FinYear_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_FinYear_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FinYear_CreatedUserId]
    ON [meta].[FinYear]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_FinYear_EndDate]
    ON [meta].[FinYear]([EndDate] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_FinYear_Name]
    ON [meta].[FinYear]([Name] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_FinYear_StartDate]
    ON [meta].[FinYear]([StartDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FinYear_UpdatedUserId]
    ON [meta].[FinYear]([UpdatedUserId] ASC);

