CREATE FUNCTION [dbo].[fnSys_LogProcAction]
(
	@ProcID INT,
	@Log VARCHAR(MAX)
)
RETURNS VARCHAR(MAX)
AS
BEGIN
	RETURN '[' + CONVERT(VARCHAR(23), GETDATE(), 121) + '] ' + ISNULL(OBJECT_NAME(@ProcID), 'Adhoc query') + ': ' + @Log
END