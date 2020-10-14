CREATE VIEW [workflow].[vwWorkFlow]
	AS 
	SELECT wrk.[Name] AS [WorkFlow],
	stt.WorkFlowTypeId,
	stt.Id AS StateMachineId,
	stt.NextStateMachineId,
	stt.PreviousStateMachineId,
	wfs.[Name] AS [State],
	wfs.[Discriminator] AS StateDiscr,
    wrk.Discriminator AS WorkFlowTypDisr,
	nfs.[Name] AS [NextState],
	plo.[Name] AS [PreviousState]
FROM [workflow].[WorkFlowType] wrk WITH(NOLOCK)
JOIN [workflow].[StateMachine] stt WITH(NOLOCK) ON stt.WorkFlowTypeId = wrk.[Id]
JOIN [workflow].[WorkFlowStatus] wfs WITH(NOLOCK) ON stt.[WorkFlowStatusId] = wfs.[Id]
LEFT JOIN [workflow].[StateMachine] nss WITH(NOLOCK) ON stt.NextStateMachineId = nss.[Id]
LEFT JOIN [workflow].[WorkFlowStatus] nfs WITH(NOLOCK) ON nss.[WorkFlowStatusId] = nfs.[Id]
LEFT JOIN [workflow].[StateMachine] spp WITH(NOLOCK) ON stt.[PreviousStateMachineId] = spp.[Id]
LEFT JOIN [workflow].[WorkFlowStatus] plo WITH(NOLOCK) ON spp.[WorkFlowStatusId] = plo.[Id]