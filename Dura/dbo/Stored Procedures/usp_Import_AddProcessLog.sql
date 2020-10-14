
CREATE PROCEDURE [dbo].[usp_Import_AddProcessLog]  @DocumentId				INT
												, @ProcessId				INT
												, @nvcResult				NVARCHAR(2000)
												, @CreatedUserId			NVARCHAR(450) 
												, @ParentProcessLogId	INT			= NULL
												, @ProcessLogId			INT			OUTPUT
as
BEGIN

		/***************************************************************************************************/
	PRINT dbo.fnSys_LogProcAction(@@PROCID, 'Entering Stored Procedure')
	/********************************************************************/
	DECLARE @LocalProcessLogId	INT										--local
	, @sql				NVARCHAR(2000)
	, @paramDEF			NVARCHAR(200) = '@ProcessLogId_OUT INT OUTPUT'
	
	--Trim spaces from Parameters--
	SELECT @nvcResult = LTRIM(RTRIM(@nvcResult))
	-------------------------------		
	
	--<check if this particular record already exists>--
	--none
	--<---------------------------------------------->--
			
	--<Insert if this particular record does not exists>--
	SET @sql = '
				INSERT [import].[ProcessLog] ([DocumentId], [ParentProcessLogId], [ProcessId], [Result],[CreatedUserId])		
				VALUES (' 
				+ ISNULL(CAST(@DocumentId			as nVARCHAR(20)), 'DEFAULT') + ', ' 
				+ ISNULL(CAST(@ParentProcessLogId	as nVARCHAR(20)), 'DEFAULT') + ', ' 
				+ ISNULL(CAST(@ProcessId			as nVARCHAR(20)), 'DEFAULT') + ', ' 
				+ ISNULL('''' + CAST(@nvcResult		as nVARCHAR(2000))	+ '''', 'DEFAULT') + ', ' 
				+  '''' + @CreatedUserId + ''''
				+ ')
			SET @ProcessLogId_OUT = SCOPE_IDENTITY()
	'		
	
	IF (@ProcessLogId IS NULL)										--Assign LOCAL_VVVV_
		EXEC sp_executesql @stmt = @sql, @params = @paramDEF, @ProcessLogId_OUT = @LocalProcessLogId OUTPUT
	--<------------------------------------------------>--			
	SET @ProcessLogID = 	@LocalProcessLogId									--Assign OUTPUT
END