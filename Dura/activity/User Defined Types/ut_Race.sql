CREATE TYPE [activity].[ut_Race] AS TABLE (
    [Ordinal]          INT            IDENTITY (1, 1) NOT NULL,
    [RaceDefinitionId] INT            NOT NULL,
    [Theme]            NVARCHAR (250) NULL,
    [FinYearId]        INT            NOT NULL,
    [CurrentUserId]    NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Ordinal] ASC));

