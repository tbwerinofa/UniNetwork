CREATE TABLE [activity].[AgeGroup] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [MinValue]         INT            NOT NULL,
    [MaxValue]         INT            NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_activity.AgeGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_activity.AgeGroup_Security.User_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_activity.AgeGroup_Security.User_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UQ_activity.AgeGroup_MaxValue] UNIQUE NONCLUSTERED ([MaxValue] ASC),
    CONSTRAINT [UQ_activity.AgeGroup_MinValue] UNIQUE NONCLUSTERED ([MinValue] ASC),
    CONSTRAINT [UQ_activity.AgeGroup_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

