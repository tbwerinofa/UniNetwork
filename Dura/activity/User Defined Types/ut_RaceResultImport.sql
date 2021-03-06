﻿CREATE TYPE [activity].[ut_RaceResultImport] AS TABLE (
    [Ordinal]        INT            IDENTITY (1, 1) NOT NULL,
    [RaceDefinition] NVARCHAR (100) NOT NULL,
    [Theme]          NVARCHAR (250) NULL,
    [RaceType]       NVARCHAR (100) NOT NULL,
    [Discpline]      NVARCHAR (100) NOT NULL,
    [Province]       NVARCHAR (100) NOT NULL,
    [FinYear]        INT            NOT NULL,
    [EventDate]      TIME (7)       NULL,
    [Distance]       NVARCHAR (100) NULL,
    [AgeGroup]       NVARCHAR (100) NOT NULL,
    [Organisation]   NVARCHAR (100) NOT NULL,
    [Position]       INT            NOT NULL,
    [TimeTaken]      TIME (7)       NULL,
    [FirstName]      NVARCHAR (100) NOT NULL,
    [Surname]        NVARCHAR (100) NOT NULL,
    [Discriminator]  INT            NOT NULL,
    [CurrentUserId]  NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Ordinal] ASC));

