CREATE TABLE [dbo].[StateMachineRolePermission] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]          BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp]  DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]  DATETIME2 (7)  NULL,
    [CreatedUserId]     NVARCHAR (450) NOT NULL,
    [UpdatedUserId]     NVARCHAR (450) NULL,
    [ApplicationRoleId] NVARCHAR (450) NOT NULL,
    [StateMachineId]    INT            NOT NULL,
    [WorkFlowStatusId]  INT            NULL,
    CONSTRAINT [PK_StateMachineRolePermission] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StateMachineRolePermission_AspNetRoles_ApplicationRoleId] FOREIGN KEY ([ApplicationRoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]),
    CONSTRAINT [FK_StateMachineRolePermission_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_StateMachineRolePermission_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_StateMachineRolePermission_StateMachine_StateMachineId] FOREIGN KEY ([StateMachineId]) REFERENCES [workflow].[StateMachine] ([Id]),
    CONSTRAINT [FK_StateMachineRolePermission_WorkFlowStatus_WorkFlowStatusId] FOREIGN KEY ([WorkFlowStatusId]) REFERENCES [workflow].[WorkFlowStatus] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_StateMachineRolePermission_ApplicationRoleId_StateMachineId]
    ON [dbo].[StateMachineRolePermission]([ApplicationRoleId] ASC, [StateMachineId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StateMachineRolePermission_CreatedUserId]
    ON [dbo].[StateMachineRolePermission]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StateMachineRolePermission_StateMachineId]
    ON [dbo].[StateMachineRolePermission]([StateMachineId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StateMachineRolePermission_UpdatedUserId]
    ON [dbo].[StateMachineRolePermission]([UpdatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StateMachineRolePermission_WorkFlowStatusId]
    ON [dbo].[StateMachineRolePermission]([WorkFlowStatusId] ASC);

