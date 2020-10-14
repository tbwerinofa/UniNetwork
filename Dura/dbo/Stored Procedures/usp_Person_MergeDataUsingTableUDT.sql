CREATE PROCEDURE [dbo].[usp_Person_MergeDataUsingTableUDT]
  @table [dbo].[ut_Person] READONLY
AS
BEGIN
    BEGIN TRY
        MERGE [dbo].[Person] T
        USING @table S ON T.[IDNumber] = S.[IDNumber] AND T.[IDTypeId] = S.[IDTypeId] AND T.[CountryId] = S.[CountryId]
        WHEN NOT MATCHED BY TARGET THEN INSERT
        (
            [TitleId]
            ,[GenderId]
            ,[FirstName]
            ,[Surname]
            ,[Initials]
            ,[OtherName]
            ,[IDNumber]
            ,[IDTypeId]
            ,[ContactNo]
            ,[Email]
            ,[CountryId]
            ,[CreatedUserId]
            ,[BirthDate]
            ,[IsActive]
        )
        VALUES
        (

             S.[TitleId]
            ,S.[GenderId]
            ,S.[FirstName]
            ,S.[Surname]
            ,S.[Initials]
            ,S.[OtherName]
            ,S.[IDNumber]
            ,S.[IDTypeId]
            ,S.[ContactNo]
            ,S.[Email]
            ,S.[CountryId]
            ,S.[CurrentUserId]
            ,S.[BirthDate]
            ,1
        )
        WHEN MATCHED
        THEN UPDATE SET
             T.[TitleId]= S.[TitleId]
            ,T.[GenderId]= S.[GenderId]
            ,T.[FirstName]= S.[FirstName]
            ,T.[Surname]= S.[Surname]
            ,T.[Initials]= S.[Initials]
            ,T.[OtherName]= S.[OtherName]
            ,T.[IDTypeId]= S.[IDTypeId]
            ,T.[ContactNo]= S.[ContactNo]
            ,T.[Email]= S.[Email]
            ,T.[CountryId]= S.[CountryId]
           ,T.[CreatedUserId] =  S.[CurrentUserId];

    END TRY
    BEGIN CATCH
        DECLARE @nvcError NVARCHAR(2000)
        SET @nvcError = dbo.fnSys_LogProcAction(@@PROCID, ERROR_MESSAGE())
        PRINT @nvcError
        RAISERROR(@nvcError, 18, 1)
    END CATCH
END