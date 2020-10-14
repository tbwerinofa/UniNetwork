CREATE PROCEDURE [activity].[usp_RaceDistance_TransformDataUsingTableUDT]
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
   DECLARE  @Table_utRaceDistance [activity].[ut_RaceDistance]

    /**********************************************************************************/
        PRINT dbo.fnSys_LogProcAction(@@PROCID, 'Insert RaceResultImport into temp table')
        /**********************************************************************************/
    INSERT INTO @Table_utRaceDistance(
            [RaceId]
        ,[DistanceId]
        ,[EventDate]
        ,[CurrentUserId])
    SELECT DISTINCT
         rac.Id
         ,dis.Id
         ,tem.[EventDate]
         ,tem.CurrentUserId
      FROM @table tem
      JOIN activity.[RaceDefinition] def on  def.[Name] = tem.RaceDefinition
     JOIN activity.[Race] rac on  def.[Id] = rac.[RaceDefinitionId]
     JOIN [activity].[Distance] dis on  dis.[Discriminator] = tem.[Distance]
     LEFT JOIN activity.[RaceDistance] trg on  trg.[DistanceId] = dis.[Id] AND rac.Id = trg.RaceId
     WHERE trg.Id IS NULL

    /*****************************************************/
    PRINT dbo.fnSys_LogProcAction(@@PROCID, 'Merge and Insert RaceDistance')
    /*****************************************************/
EXEC [activity].[usp_RaceDistance_MergeDataUsingTableUDT] @table = @Table_utRaceDistance

END