CREATE VIEW [accolade].[vwTrophy]

AS
SELECT  src.[Id] AS TrophyId
        ,src.[Name] AS Trophy
        ,[Description]
        ,src.[DocumentId]
        ,daw.[Name] AS Award
        ,gen.Name AS Gender
        ,mem.MemberNo
        ,per.FirstName + ' ' + per.Surname AS FullName
        , [DocumentTypeId]
        ,[DocumentData]
        , [DocumentNameGuid]
        ,doc.[Name]AS DocumentName
        ,fin.[Name] AS [FinYear]
        ,daw.Ordinal
  FROM [accolade].[Trophy] src
	JOIN [accolade].AwardTrophy awt WITH(NOLOCK) ON src.Id = awt.TrophyId
	JOIN [accolade].Award daw WITH(NOLOCK) ON daw.Id = awt.AwardId
	JOIN [accolade].AwardTrophyAudit ata WITH(NOLOCK) ON ata.AwardTrophyId = awt.Id AND ata.IsActive =1
	JOIN [accolade].Winner win WITH(NOLOCK) ON win.AwardId = awt.AwardId AND ata.FinYearId =win.FinYearId
    JOIN [meta].FinYear fin WITH(NOLOCK) ON fin.Id = win.FinYearId 
	LEFT JOIN [worker].Member mem WITH(NOLOCK) ON mem.Id = win.MemberId
	LEFT JOIN [dbo].Person per WITH(NOLOCK) ON per.Id = mem.PersonId
	LEFT JOIN [meta].Gender gen WITH(NOLOCK) ON gen.Id = per.GenderId
    LEFT JOIN [meta].Document doc  WITH(NOLOCK) ON doc.Id = src.DocumentId