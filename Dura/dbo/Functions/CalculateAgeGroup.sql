CREATE FUNCTION [dbo].[CalculateAgeGroup]
(
	@DateOfBirth Date
)
RETURNS INT

AS
BEGIN
	DECLARE @AgeGroupId INT;
    DECLARE @Year INT = DATEDIFF(Year,@DateOfBirth,GetDate())
	SELECT @AgeGroupId = q.Id
	FROM activity.[AgeGroup] q
	WHERE @Year BETWEEN q.MinValue AND q.MaxValue

	RETURN @AgeGroupId;
END