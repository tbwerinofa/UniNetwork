CREATE PROCEDURE [activity].[usp_Race_TransformDataUsingTableUDT]
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
   DECLARE  @Table_utRace [activity].[ut_Race]

    /**********************************************************************************/
        PRINT dbo.fnSys_LogProcAction(@@PROCID, 'Insert RaceResultImport into temp table')
        /**********************************************************************************/
    INSERT INTO @Table_utRace(
            [RaceDefinitionId]
        ,[FinYearId]
        ,[Theme]
        ,[CurrentUserId])
    SELECT DISTINCT
         def.Id
         ,fin.Id
         ,tem.[Theme]
         ,tem.CurrentUserId
      FROM @table tem
      JOIN activity.[RaceDefinition] def on  def.[Name] = tem.RaceDefinition
     JOIN meta.[FinYear] fin on  fin.[Name] = tem.[FinYear]
     LEFT JOIN activity.[Race] trg on  trg.RaceDefinitionId = def.Id AND trg.FinYearId = fin.Id
     WHERE trg.Id IS NULL

    /*****************************************************/
    PRINT dbo.fnSys_LogProcAction(@@PROCID, 'Merge and Insert Race')
    /*****************************************************/
EXEC [activity].[usp_Race_MergeDataUsingTableUDT] @table = @Table_utRace

END