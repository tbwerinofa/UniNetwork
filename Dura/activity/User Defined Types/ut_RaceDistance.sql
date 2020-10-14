CREATE TYPE [activity].[ut_RaceDistance] AS TABLE (
    [Ordinal]       INT            IDENTITY (1, 1) NOT NULL,
    [RaceId]        INT            NOT NULL,
    [DistanceId]    INT            NOT NULL,
    [EventDate]     DATETIME2 (7)  NOT NULL,
    [CurrentUserId] NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Ordinal] ASC));

