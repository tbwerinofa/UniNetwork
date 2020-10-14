CREATE TYPE [activity].[ut_Distance] AS TABLE (
    [Ordinal]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50)  NOT NULL,
    [Measurement]       INT            NOT NULL,
    [MeasurementUnitId] INT            NOT NULL,
    [Discriminator]     NCHAR (4)      NOT NULL,
    [CurrentUserId]     NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Ordinal] ASC));

