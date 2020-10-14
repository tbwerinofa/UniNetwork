CREATE PROCEDURE [activity].[usp_RaceDistance_MergeDataUsingTableUDT]
  @table [activity].[ut_RaceDistance] READONLY
AS
BEGIN
    BEGIN TRY
        MERGE [activity].[RaceDistance] T
        USING @table S ON T.[RaceId] = S.[RaceId] AND T.[DistanceId] = S.[DistanceId]
        WHEN NOT MATCHED BY TARGET THEN INSERT
        (
             [RaceId]
            ,[DistanceId]
            ,[EventDate]
            ,[CreatedUserId]
            ,[IsActive]
        )
        VALUES
        (
             S.[RaceId]
            ,S.[DistanceId]
            ,S.[EventDate]
            ,S.[CurrentUserId]
            ,1
        )
        WHEN MATCHED
        THEN UPDATE SET
         T.[RaceId]  = S.[RaceId]
        ,T.[DistanceId]  = S.[DistanceId]
        ,T.[EventDate]  = S.[EventDate]
        ,T.[CreatedUserId] =  S.[CurrentUserId];

    END TRY
    BEGIN CATCH
        DECLARE @nvcError NVARCHAR(2000)
        SET @nvcError = dbo.fnSys_LogProcAction(@@PROCID, ERROR_MESSAGE())
        PRINT @nvcError
        RAISERROR(@nvcError, 18, 1)
    END CATCH
END