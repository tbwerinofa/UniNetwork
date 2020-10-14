CREATE TYPE [import].[ut_RemittanceStagingError] AS TABLE (
    [Ordinal]               INT            NULL,
    [RemittanceStagingId]   INT            NOT NULL,
    [ValidationErrorTypeId] INT            NULL,
    [IsActive]              BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp]      DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]      DATETIME2 (7)  NULL,
    [CurrentUserId]         NVARCHAR (450) NOT NULL);

