CREATE PROCEDURE [activity].[usp_Race_MergeDataUsingTableUDT]
  @table [activity].[ut_Race] READONLY
AS
BEGIN
    BEGIN TRY
        MERGE [activity].[Race] T
        USING @table S ON T.[RaceDefinitionId] = S.[RaceDefinitionId] AND T.[FinYearId] = S.[FinYearId]
        WHEN NOT MATCHED BY TARGET THEN INSERT
        (
             [RaceDefinitionId]
            ,[FinYearId]
            ,[Theme]
            ,[CreatedUserId]
            ,[IsActive]
        )
        VALUES
        (
             S.[RaceDefinitionId]
            ,S.[FinYearId]
            ,S.[Theme]
            ,S.[CurrentUserId]
            ,1
        )
        WHEN MATCHED
        THEN UPDATE SET
         T.[RaceDefinitionId]  = S.[RaceDefinitionId]
        ,T.[FinYearId]  = S.[FinYearId]
        ,T.[Theme]  = S.[Theme]
        ,T.[CreatedUserId] =  S.[CurrentUserId];

    END TRY
    BEGIN CATCH
        DECLARE @nvcError NVARCHAR(2000)
        SET @nvcError = dbo.fnSys_LogProcAction(@@PROCID, ERROR_MESSAGE())
        PRINT @nvcError
        RAISERROR(@nvcError, 18, 1)
    END CATCH
END