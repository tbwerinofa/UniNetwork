CREATE TABLE [meta].[Frequency] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Discriminator]    NCHAR (4)      NULL,
    [Recurrence]       INT            NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    CONSTRAINT [PK_Frequency] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Frequency_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Frequency_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UQ_Frequency_Discriminator] UNIQUE NONCLUSTERED ([Discriminator] ASC),
    CONSTRAINT [UQ_Frequency_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

