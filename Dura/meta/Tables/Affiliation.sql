CREATE TABLE [meta].[Affiliation] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Discriminator]    NVARCHAR (4)   NOT NULL,
    CONSTRAINT [PK_Affiliation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Affiliation_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Affiliation_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Affiliation_CreatedUserId]
    ON [meta].[Affiliation]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Affiliation_Discriminator]
    ON [meta].[Affiliation]([Discriminator] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Affiliation_Name]
    ON [meta].[Affiliation]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Affiliation_UpdatedUserId]
    ON [meta].[Affiliation]([UpdatedUserId] ASC);

