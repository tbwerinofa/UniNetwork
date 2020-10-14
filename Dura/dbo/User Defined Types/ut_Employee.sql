CREATE TYPE [dbo].[ut_Employee] AS TABLE (
    [Ordinal]              INT            IDENTITY (1, 1) NOT NULL,
    [CorporateUnitId]      INT            NOT NULL,
    [PersonId]             INT            NOT NULL,
    [EmployeeNo]           NVARCHAR (50)  NULL,
    [IsPermanent]          BIT            NULL,
    [CorporateUnitTradeId] INT            NOT NULL,
    [EmploymentDate]       DATETIME       NOT NULL,
    [EmploymentStatusId]   INT            NULL,
    [TerminationDate]      DATETIME       NULL,
    [CurrentUserId]        NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Ordinal] ASC));

