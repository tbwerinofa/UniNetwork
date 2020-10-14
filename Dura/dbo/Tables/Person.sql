CREATE TABLE [dbo].[Person] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]        NVARCHAR (MAX) NOT NULL,
    [Surname]          NVARCHAR (MAX) NOT NULL,
    [Initials]         NVARCHAR (MAX) NULL,
    [OtherName]        NVARCHAR (MAX) NULL,
    [IDNumber]         NVARCHAR (13)  NULL,
    [ContactNo]        NVARCHAR (MAX) NULL,
    [Email]            NVARCHAR (MAX) NULL,
    [BirthDate]        DATETIME       NOT NULL,
    [TitleId]          INT            NULL,
    [AgeGroupId]       AS             ([dbo].[CalculateAgeGroup]([BirthDate])),
    [DocumentId]       INT            NULL,
    [GenderId]         INT            NOT NULL,
    [IDTypeId]         INT            NOT NULL,
    [CountryId]        INT            NOT NULL,
    [AddressId]        INT            NULL,
    [PersonGuid]       NVARCHAR (250) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Person_Address_Id] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([Id]),
    CONSTRAINT [FK_Person_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Person_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Person_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [gis].[Country] ([Id]),
    CONSTRAINT [FK_Person_Document_Id] FOREIGN KEY ([DocumentId]) REFERENCES [meta].[Document] ([Id]),
    CONSTRAINT [FK_Person_Gender_GenderId] FOREIGN KEY ([GenderId]) REFERENCES [meta].[Gender] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Person_IDType_Id] FOREIGN KEY ([IDTypeId]) REFERENCES [meta].[IDType] ([Id]),
    CONSTRAINT [FK_Person_Title_TitleId] FOREIGN KEY ([TitleId]) REFERENCES [meta].[Title] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ_Person_IDTYpe_Country_IDNumber] UNIQUE NONCLUSTERED ([IDTypeId] ASC, [IDNumber] ASC, [CountryId] ASC),
    CONSTRAINT [UQ_Person_PersonGuid] UNIQUE NONCLUSTERED ([PersonGuid] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Person_CreatedUserId]
    ON [dbo].[Person]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Person_GenderId]
    ON [dbo].[Person]([GenderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Person_TitleId]
    ON [dbo].[Person]([TitleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Person_UpdatedUserId]
    ON [dbo].[Person]([UpdatedUserId] ASC);

