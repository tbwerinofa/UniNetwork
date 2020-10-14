CREATE TABLE [meta].[Gender] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Discriminator]    NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Gender_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Gender_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Gender_CreatedUserId]
    ON [meta].[Gender]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Gender_Discriminator]
    ON [meta].[Gender]([Discriminator] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Gender_Name]
    ON [meta].[Gender]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Gender_UpdatedUserId]
    ON [meta].[Gender]([UpdatedUserId] ASC);

