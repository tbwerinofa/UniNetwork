CREATE TABLE [activity].[RaceOrganisation] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [RaceId]           INT            NOT NULL,
    [OrganisationId]   INT            NOT NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_activity.RaceOrganisation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_activity.RaceOrganisation_Organisation_Id] FOREIGN KEY ([OrganisationId]) REFERENCES [dbo].[Organisation] ([Id]),
    CONSTRAINT [FK_activity.RaceOrganisation_Race_Id] FOREIGN KEY ([RaceId]) REFERENCES [activity].[Race] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_activity.RaceOrganisation_Security.User_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_activity.RaceOrganisation_Security.User_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UQ_activity.RaceOrganisation_Name] UNIQUE NONCLUSTERED ([RaceId] ASC, [OrganisationId] ASC)
);

