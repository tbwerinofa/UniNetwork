CREATE TYPE [dbo].[ut_Remittance] AS TABLE (
    [Ordinal]             INT             NULL,
    [RemittanceSummaryId] INT             NOT NULL,
    [EmployeeId]          INT             NOT NULL,
    [RowNumber]           INT             NOT NULL,
    [IsVerified]          BIT             NOT NULL,
    [Amount]              DECIMAL (18, 2) NOT NULL,
    [CurrentUserId]       NVARCHAR (450)  NOT NULL);

