CREATE TABLE [meta].[IDType] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    CONSTRAINT [PK_IDType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IDType_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_IDType_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_IDType_CreatedUserId]
    ON [meta].[IDType]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_IDType_Name]
    ON [meta].[IDType]([Name] ASC) WHERE ([Name] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_IDType_UpdatedUserId]
    ON [meta].[IDType]([UpdatedUserId] ASC);

