CREATE PROCEDURE [activity].[usp_RaceDefinition_TransformDataUsingTableUDT]
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
   DECLARE  @Table_utRaceDefinition [activity].[ut_RaceDefinition]
   DECLARE @ProvinceId INT = (SELECT Top 1 Id FROM gis.Province Where Name ='Western Cape')
   DECLARE @OrganisationTypeId INT = (SELECT Top 1 Id FROM dbo.[OrganisationType] Where Name ='Sports Club')
    /**********************************************************************************/
        PRINT dbo.fnSys_LogProcAction(@@PROCID, 'Insert RaceResultImport into temp table')
        /**********************************************************************************/
    INSERT INTO @Table_utRaceDefinition(
        [Name]
        ,[ProvinceId]
        ,[DiscplineId]
        ,[RaceTypeId]
        ,[CurrentUserId])
    SELECT DISTINCT
         tem.RaceDefinition
		 ,pro.Id
         ,dis.Id
         ,typ.Id
         ,tem.CurrentUserId
      FROM @table tem
     JOIN [gis].[Province] pro on  pro.[Name] = tem.Province
     JOIN [activity].[Discpline] dis on  dis.[Name] = tem.Discpline
     JOIN [activity].[RaceType] typ on  typ.[Name] = tem.RaceType
     LEFT JOIN activity.[RaceDefinition] def on  def.[Name] = tem.RaceDefinition
     WHERE def.Id IS NULL

    /*****************************************************/
    PRINT dbo.fnSys_LogProcAction(@@PROCID, 'Merge and Insert RaceResultImport')
    /*****************************************************/
EXEC [activity].[usp_RaceDefinition_MergeDataUsingTableUDT] @table = @Table_utRaceDefinition

END