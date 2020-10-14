CREATE TABLE [dbo].[OrganisationType] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]          BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp]  DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]  DATETIME2 (7)  NULL,
    [CreatedUserId]     NVARCHAR (450) NOT NULL,
    [UpdatedUserId]     NVARCHAR (450) NULL,
    [Name]              NVARCHAR (50)  NOT NULL,
    [ApplicationUserId] NVARCHAR (450) NULL,
    CONSTRAINT [PK_OrganisationType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrganisationType_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_OrganisationType_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_OrganisationType_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

