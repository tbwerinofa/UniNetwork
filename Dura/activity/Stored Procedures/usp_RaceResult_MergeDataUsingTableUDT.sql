CREATE PROCEDURE [activity].[usp_RaceResult_MergeDataUsingTableUDT]
  @table [activity].[ut_RaceResult] READONLY
AS
BEGIN
    BEGIN TRY
        MERGE [activity].[RaceResult] T
        USING @table S ON T.MemberId = S.MemberId AND T.[RaceDistanceId] = S.[RaceDistanceId]
        WHEN NOT MATCHED BY TARGET THEN INSERT
        (
            [RaceDistanceId]
            ,[AgeGroupId]
            ,[MemberId]
            ,[TimeTaken]
            ,[Position]
            ,[CreatedUserId]
            ,[IsActive]
        )
        VALUES
        (
             S.[RaceDistanceId]
            ,S.[AgeGroupId]
            ,S.[MemberId]
            ,S.[TimeTaken]
            ,S.[Position]
            ,S.[CurrentUserId]
            ,1
        )
        WHEN MATCHED
        THEN UPDATE SET
         T.[RaceDistanceId]  = S.[RaceDistanceId]
        ,T.[AgeGroupId]  = S.[AgeGroupId]
        ,T.[MemberId]  = S.[MemberId]
        ,T.[TimeTaken]  = S.[TimeTaken]
        ,T.[Position]  = S.[Position]
        ,T.[CreatedUserId] =  S.[CurrentUserId];

    END TRY
    BEGIN CATCH
        DECLARE @nvcError NVARCHAR(2000)
        SET @nvcError = dbo.fnSys_LogProcAction(@@PROCID, ERROR_MESSAGE())
        PRINT @nvcError
        RAISERROR(@nvcError, 18, 1)
    END CATCH
END