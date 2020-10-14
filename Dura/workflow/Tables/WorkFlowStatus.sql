CREATE TABLE [workflow].[WorkFlowStatus] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Discriminator]    NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_WorkFlowStatus] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkFlowStatus_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_WorkFlowStatus_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_WorkFlowStatus_CreatedUserId]
    ON [workflow].[WorkFlowStatus]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_WorkFlowStatus_Name]
    ON [workflow].[WorkFlowStatus]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_WorkFlowStatus_UpdatedUserId]
    ON [workflow].[WorkFlowStatus]([UpdatedUserId] ASC);

