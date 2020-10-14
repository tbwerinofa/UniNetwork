CREATE PROCEDURE [dbo].[usp_Employee_MergeDataUsingTableUDT]
  @table [dbo].[ut_Employee] READONLY
AS
BEGIN
    BEGIN TRY
        MERGE [worker].[Employee] T
        USING @table S ON T.[PersonId] = S.[PersonId] AND T.EmployeeNo = S.EmployeeNo
        WHEN NOT MATCHED BY TARGET THEN INSERT
        (
            [CorporateUnitId]
            ,[PersonId]
            ,[EmployeeNo]
            ,[IsPermanent]
            ,[CorporateUnitTradeId]
            ,[EmploymentDate]
            ,[EmploymentStatusId]
            ,[TerminationDate]
            ,[CreatedUserId]
            ,[IsActive]
        )
        VALUES
        (

             S.[CorporateUnitId]
            ,S.[PersonId]
            ,S.[EmployeeNo]
            ,S.[IsPermanent]
            ,S.[CorporateUnitTradeId]
            ,S.[EmploymentDate]
            ,S.[EmploymentStatusId]
            ,S.[TerminationDate]
            ,S.[CurrentUserId]
            ,1
        )
        WHEN MATCHED
        THEN UPDATE SET
         T.[CorporateUnitId]  = T.[CorporateUnitId]
        ,T.[PersonId]  = T.[PersonId]
        ,T.[EmployeeNo]  = T.[EmployeeNo]
        ,T.[IsPermanent]  = T.[IsPermanent]
        ,T.[CorporateUnitTradeId]  = T.[CorporateUnitTradeId]
        ,T.[EmploymentDate]  = T.[EmploymentDate]
        ,T.[EmploymentStatusId]  = T.[EmploymentStatusId]
        ,T.[TerminationDate]  = T.[TerminationDate]
           ,T.[CreatedUserId] =  S.[CurrentUserId];

    END TRY
    BEGIN CATCH
        DECLARE @nvcError NVARCHAR(2000)
        SET @nvcError = dbo.fnSys_LogProcAction(@@PROCID, ERROR_MESSAGE())
        PRINT @nvcError
        RAISERROR(@nvcError, 18, 1)
    END CATCH
END