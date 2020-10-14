CREATE TYPE [activity].[ut_RaceResult] AS TABLE (
    [Ordinal]        INT            IDENTITY (1, 1) NOT NULL,
    [RaceDistanceId] INT            NOT NULL,
    [MemberId]       INT            NOT NULL,
    [TimeTaken]      TIME (7)       NULL,
    [Position]       INT            NULL,
    [AgeGroupId]     INT            NOT NULL,
    [Discriminator]  INT            NOT NULL,
    [CurrentUserId]  NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Ordinal] ASC));

