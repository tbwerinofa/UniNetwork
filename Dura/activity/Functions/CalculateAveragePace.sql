CREATE FUNCTION [activity].[CalculateAveragePace]
(
	@RaceDistanceId   INT,
    @TimeTaken  TIME(7)
)
RETURNS TIME(7)

AS
BEGIN
	DECLARE @AveragePace TIME(7)

	SELECT @AveragePace = CONVERT(TIME,DATEADD(ms, (DATEDIFF(second,0,cast(@TimeTaken as datetime))/ CONVERT(float, dis.Measurement)) * 1000, 0))
    FROM activity.RaceDistance rce WITH(nolock)
    JOIN activity.Distance dis  WITH(nolock) ON rce.DistanceId = dis.Id
    WHERe rce.Id = @RaceDistanceId

	RETURN @AveragePace;
END