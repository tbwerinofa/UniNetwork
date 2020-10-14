CREATE PROCEDURE [activity].[usp_ProcessRaceResultImportStaging]
AS
  BEGIN
    /*************************************************************************/
        PRINT dbo.fnSys_LogProcAction(@@PROCID, 'Begin Processing Race Result ImportStaging File')
        PRINT dbo.fnSys_LogProcAction(@@PROCID, 'Declare and initialise variables')
        /*************************************************************************/
        DECLARE @bitSuccess    BIT
        DECLARE @nvcError      NVARCHAR(2000)
        DECLARE @DocumentId    INT
        DECLARE @ImportId      INT
        DECLARE @ProcessId     INT =@@PROCID
        DECLARE @ProcessLogId  INT
        DECLARE @CreatedUserId NVARCHAR(450)



        /********************************************************************************/
        PRINT dbo.fnSys_LogProcAction(@@PROCID, 'Insert Into Race Result ImportStaging table')
        /********************************************************************************/
        DECLARE @TABLE_utImp_RaceResultImportStagingFl [activity].[ut_RaceResultImport]
        DECLARE @RaceResultImportId INT = 0
        DECLARE @ReadyStatusMachineId INT = (SELECT TOP 1 StateMachineId FROM [workflow].[vwWorkFlow] WHERE StateDiscr = 'PenPro' AND WorkFlowTypDisr ='DcIm')
        DECLARE @ProcessedStatusMachineId INT = (SELECT TOP 1 StateMachineId FROM [workflow].[vwWorkFlow] WHERE StateDiscr = 'Processed' AND WorkFlowTypDisr ='DcIm')
          DECLARE @FailedStatusMachineId INT = (SELECT TOP 1 StateMachineId FROM [workflow].[vwWorkFlow] WHERE StateDiscr = 'Failed' AND WorkFlowTypDisr ='DcIm')

          /********************************************************************************/
        PRINT dbo.fnSys_LogProcAction(@@PROCID, 'Log this process for the import history')
        /********************************************************************************/

        

        EXEC [usp_Import_AddProcessLog]   @DocumentId             = @DocumentId,          --This current import
                                          @ProcessId         = @@PROCID ,         --This current process
                                          @nvcResult            = 'Started -import usp_ProcessRaceResultImportStaging',
                                          @CreatedUserId        = @CreatedUserId,
                                          @ParentProcessLogId   = null,              --The calling process
                                          @ProcessLogId         = @ProcessLogID OUTPUT	

        --BEGIN TRY


        INSERT INTO  @TABLE_utImp_RaceResultImportStagingFl([RaceDefinition],[RaceType],[Discpline],[Province],[FinYear],[EventDate],[Distance],[AgeGroup],[Organisation],[Position],[TimeTaken],[FirstName],[Surname],[Discriminator],[CurrentUserId])
        SELECT      cte.[RaceDefinition],[RaceType],[Discpline],[Province],[FinYear],[EventDate],[Distance],[AgeGroup],[Organisation],[Position],[TimeTaken],[dbo].[Ufn_CamelCase]([FirstName]),[dbo].[Ufn_CamelCase]([Surname]),[Discriminator],CreatedUserId
        FROM activity.RaceResultImport cte

        EXEC [activity].[usp_RaceDefinition_TransformDataUsingTableUDT] @table= @TABLE_utImp_RaceResultImportStagingFl
        EXEC [activity].[usp_Race_TransformDataUsingTableUDT] @table= @TABLE_utImp_RaceResultImportStagingFl
        EXEC [activity].[usp_RaceDistance_TransformDataUsingTableUDT] @table= @TABLE_utImp_RaceResultImportStagingFl
        EXEC [activity].[usp_RaceResult_TransformDataUsingTableUDT] @table= @TABLE_utImp_RaceResultImportStagingFl
   

        --END TRY
        --BEGIN CATCH
        --    SET @nvcError = dbo.fnSys_LogProcAction(@@PROCID, ERROR_MESSAGE())		
        --    PRINT @nvcError
        --    EXEC [import].[usp_ImportProcessLog_UpdateResult]	  @ProcessLogId		= @ProcessLogId		
        --                                        , @Result			= @nvcError
        --                                        , @UpdatedUserId		= @CreatedUserId
        --END CATCH
    
        --IF (@nvcError IS NULL)
        --BEGIN
        --    SET @bitSuccess = 1
        --            EXEC [import].[usp_ImportProcessLog_UpdateResult] @ProcessLogId = @ProcessLogId		
        --                                        , @Result =  @nvcError
        --                                        , @UpdatedUserId= @CreatedUserId
        --END
        --ELSE
        --    BEGIN
        --        PRINT @nvcError
        --        RAISERROR(@nvcError, 18, 1)
        --    END

            SELECT @bitSuccess
    END