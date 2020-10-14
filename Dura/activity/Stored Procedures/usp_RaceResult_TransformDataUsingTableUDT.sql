CREATE PROCEDURE [activity].[usp_RaceResult_TransformDataUsingTableUDT]
   @table [activity].[ut_RaceResultImport] READONLY
AS

BEGIN

/***************************************************************************************************/
    DECLARE @RowCount INT
    SELECT @RowCount = COUNT(*) FROM @table
    PRINT dbo.fnSys_LogProcAction(@@PROCID, 'Entering; Rows Received ' + CONVERT(VARCHAR(10), @RowCount))
    /***************************************************************************************************/

    /*************************************************************************/
    PRINT dbo.fnSys_LogProcAction(@@PROCID, 'Declare and initialise variables')
    /*************************************************************************/
   DECLARE  @Table_utRaceResult [activity].[ut_RaceResult]

    /**********************************************************************************/
        PRINT dbo.fnSys_LogProcAction(@@PROCID, 'Insert RaceResultImport into temp table')
        /**********************************************************************************/
    INSERT INTO @Table_utRaceResult(
          [RaceDistanceId]
         ,[AgeGroupId]
         ,[MemberId]
         ,[TimeTaken]
         ,[Position]
         ,Discriminator
        ,[CurrentUserId])
    SELECT DISTINCT
         rds.Id
         ,agr.[Id]
         ,mem.Id
         ,tem.[TimeTaken]
         ,tem.[Position]
         ,tem.Discriminator
         ,tem.CurrentUserId
      FROM @table tem
      JOIN activity.[RaceDefinition] def on  def.[Name] = tem.RaceDefinition
     JOIN [meta].[FinYear] fin on  fin.[Name] = tem.[FinYear]
     JOIN activity.[Race] rac on  def.[Id] = rac.[RaceDefinitionId] AND rac.FinYearId =fin.Id
     JOIN [activity].[Distance] dis on  dis.[Discriminator] = tem.[Distance]
     JOIN [activity].[RaceDistance] rds on  rds.DistanceId = dis.Id AND rac.Id = rds.[RaceId]
     JOIN [activity].[AgeGroup] agr on  agr.[Name] = tem.AgeGroup
	 JOIN Person per ON per.FirstName = tem.FirstName and per.Surname = tem.Surname
     JOIN worker.Member mem ON mem.PersonId = per.Id
     LEFT JOIN activity.[RaceResult] trg on  trg.MemberId = mem.[Id] AND rds.Id = trg.RaceDistanceId
     WHERE trg.Id IS NULL

    /*****************************************************/
    PRINT dbo.fnSys_LogProcAction(@@PROCID, 'Merge and Insert RaceResult')
    /*****************************************************/
EXEC [activity].[usp_RaceResult_MergeDataUsingTableUDT] @table = @Table_utRaceResult

END