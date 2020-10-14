CREATE FUNCTION [dbo].[Ufn_CamelCase](@Str VARCHAR(MAX))
RETURNS VARCHAR(MAX) 
WITH SCHEMABINDING
AS
BEGIN
	DECLARE @Result VARCHAR(MAX)
	IF @Str IS NOT NULL
		BEGIN
			SET @Str = LOWER(@Str) + ' '
			SET @Result = ''

			WHILE 1=1
			BEGIN
				IF PATINDEX('% %',@Str) = 0 BREAK
				SET @Result = @Result+UPPER(LEFT(@Str,1))+SUBSTRING(@Str,2,CHARINDEX(' ',@Str)-1)
				SET @Str = SUBSTRING(@Str,CHARINDEX(' ',@Str)+1,LEN(@Str))
			END
			SET @Result = LEFT(@Result,LEN(@Result))
		END
	RETURN @Result
END