CREATE TABLE [workflow].[WorkFlowType] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Discriminator]    NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_WorkFlowType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkFlowType_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_WorkFlowType_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_WorkFlowType_CreatedUserId]
    ON [workflow].[WorkFlowType]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_WorkFlowType_Name]
    ON [workflow].[WorkFlowType]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_WorkFlowType_UpdatedUserId]
    ON [workflow].[WorkFlowType]([UpdatedUserId] ASC);

