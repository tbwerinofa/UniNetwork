CREATE TYPE [dbo].[ut_Person] AS TABLE (
    [Ordinal]       INT            IDENTITY (1, 1) NOT NULL,
    [TitleId]       INT            NULL,
    [GenderId]      INT            NOT NULL,
    [FirstName]     NVARCHAR (MAX) NOT NULL,
    [Surname]       NVARCHAR (MAX) NOT NULL,
    [Initials]      NVARCHAR (MAX) NULL,
    [OtherName]     NVARCHAR (MAX) NULL,
    [IDNumber]      NVARCHAR (13)  NULL,
    [ContactNo]     NVARCHAR (MAX) NULL,
    [Email]         NVARCHAR (MAX) NULL,
    [IDTypeId]      INT            NULL,
    [CountryId]     INT            NULL,
    [BirthDate]     DATETIME       NULL,
    [EthnicId]      INT            NULL,
    [CurrentUserId] NVARCHAR (450) NOT NULL,
    PRIMARY KEY CLUSTERED ([Ordinal] ASC));

