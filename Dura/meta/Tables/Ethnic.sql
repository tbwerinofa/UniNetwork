CREATE TABLE [meta].[Ethnic] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Ethnic] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Ethnic_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Ethnic_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Ethnic_CreatedUserId]
    ON [meta].[Ethnic]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Ethnic_Name]
    ON [meta].[Ethnic]([Name] ASC) WHERE ([Name] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_Ethnic_UpdatedUserId]
    ON [meta].[Ethnic]([UpdatedUserId] ASC);

