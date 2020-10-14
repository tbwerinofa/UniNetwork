﻿CREATE FUNCTION [dbo].[CalculateAmountExVat]
(
	@Amount NUMERIC(18,2)
)
RETURNS NUMERIC(18,2)

AS
BEGIN
	DECLARE @VatAmount NUMERIC(18,2);

	SELECT @VatAmount = @Amount /1.15
	RETURN @VatAmount;
END