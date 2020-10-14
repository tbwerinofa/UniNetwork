CREATE PROCEDURE [activity].[usp_RaceDefinition_MergeDataUsingTableUDT]
  @table [activity].[ut_RaceDefinition] READONLY
AS
BEGIN
    BEGIN TRY
        MERGE [activity].[RaceDefinition] T
        USING @table S ON T.[Name] = S.[Name] AND T.[ProvinceId] = S.[ProvinceId]
        WHEN NOT MATCHED BY TARGET THEN INSERT
        (
             [Name]
            ,[ProvinceId]
            ,[DiscplineId]
            ,[RaceTypeId]
            ,[CreatedUserId]
            ,[IsActive]
        )
        VALUES
        (

             S.[Name]
            ,S.[ProvinceId]
            ,S.[DiscplineId]
            ,S.[RaceTypeId]
            ,S.[CurrentUserId]
            ,1
        )
        WHEN MATCHED
        THEN UPDATE SET
         T.[Name]  = S.[Name]
        ,T.[ProvinceId]  = S.[ProvinceId]
        ,T.[DiscplineId]  = S.[DiscplineId]
        ,T.[RaceTypeId]  = S.[RaceTypeId]
        ,T.[CreatedUserId] =  S.[CurrentUserId];

    END TRY
    BEGIN CATCH
        DECLARE @nvcError NVARCHAR(2000)
        SET @nvcError = dbo.fnSys_LogProcAction(@@PROCID, ERROR_MESSAGE())
        PRINT @nvcError
        RAISERROR(@nvcError, 18, 1)
    END CATCH
END