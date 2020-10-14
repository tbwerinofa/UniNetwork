CREATE TABLE [dbo].[Address] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Line1]            NVARCHAR (100) NOT NULL,
    [Line2]            NVARCHAR (100) NULL,
    [Code]             NVARCHAR (20)  NULL,
    [SuburbId]         INT            NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Address_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Address_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Address_Suburb_Id] FOREIGN KEY ([SuburbId]) REFERENCES [gis].[Suburb] ([Id])
);

