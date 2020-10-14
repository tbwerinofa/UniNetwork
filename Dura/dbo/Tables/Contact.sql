CREATE TABLE [dbo].[Contact] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [PersonId]         INT            NOT NULL,
    [ContactNumber]    NVARCHAR (15)  NOT NULL,
    [Fax]              NVARCHAR (MAX) NULL,
    [Email]            NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contact_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Contact_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Contact_Person_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Contact_CreatedUserId]
    ON [dbo].[Contact]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contact_PersonId]
    ON [dbo].[Contact]([PersonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contact_UpdatedUserId]
    ON [dbo].[Contact]([UpdatedUserId] ASC);

