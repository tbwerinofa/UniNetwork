CREATE TYPE [activity].[ut_RaceDefinition] AS TABLE (
    [Ordinal]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (250) NOT NULL,
    [ProvinceId]    INT            NOT NULL,
    [DiscplineId]   INT            NOT NULL,
    [RaceTypeId]    INT            NOT NULL,
    [CurrentUserId] NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Ordinal] ASC));

