CREATE PROCEDURE [accolade].[web_GetTrophyWinners]

AS
BEGIN

SELECT   TrophyId
        ,Trophy
        ,[Description]
        ,[DocumentId]
        ,Award
        ,Gender
        ,MemberNo
        ,[FullName]
        , [DocumentTypeId]
        ,[DocumentData]
        , [DocumentNameGuid]
        ,DocumentName
        ,[FinYear]
        ,Ordinal
  FROM  [accolade].[vwTrophy]
END