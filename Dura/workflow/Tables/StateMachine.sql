CREATE TABLE [workflow].[StateMachine] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]               BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp]       DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]       DATETIME2 (7)  NULL,
    [CreatedUserId]          NVARCHAR (450) NOT NULL,
    [UpdatedUserId]          NVARCHAR (450) NULL,
    [WorkFlowStatusId]       INT            NOT NULL,
    [WorkFlowTypeId]         INT            NOT NULL,
    [NextStateMachineID]     INT            NULL,
    [PreviousStateMachineID] INT            NULL,
    [PrevStateMachineId]     INT            NULL,
    CONSTRAINT [PK_StateMachine] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StateMachine_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_StateMachine_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_StateMachine_StateMachine_NextStateMachineID] FOREIGN KEY ([NextStateMachineID]) REFERENCES [workflow].[StateMachine] ([Id]),
    CONSTRAINT [FK_StateMachine_StateMachine_PrevStateMachineId] FOREIGN KEY ([PrevStateMachineId]) REFERENCES [workflow].[StateMachine] ([Id]),
    CONSTRAINT [FK_StateMachine_WorkFlowStatus_WorkFlowStatusId] FOREIGN KEY ([WorkFlowStatusId]) REFERENCES [workflow].[WorkFlowStatus] ([Id]),
    CONSTRAINT [FK_StateMachine_WorkFlowType_WorkFlowTypeId] FOREIGN KEY ([WorkFlowTypeId]) REFERENCES [workflow].[WorkFlowType] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_StateMachine_CreatedUserId]
    ON [workflow].[StateMachine]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StateMachine_NextStateMachineID]
    ON [workflow].[StateMachine]([NextStateMachineID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StateMachine_PrevStateMachineId]
    ON [workflow].[StateMachine]([PrevStateMachineId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StateMachine_UpdatedUserId]
    ON [workflow].[StateMachine]([UpdatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StateMachine_WorkFlowStatusId]
    ON [workflow].[StateMachine]([WorkFlowStatusId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_StateMachine_WorkFlowTypeId_WorkFlowStatusId]
    ON [workflow].[StateMachine]([WorkFlowTypeId] ASC, [WorkFlowStatusId] ASC);

