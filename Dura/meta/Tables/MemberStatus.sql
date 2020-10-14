CREATE TABLE [meta].[MemberStatus] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    [Name]             NVARCHAR (50)  NULL,
    CONSTRAINT [PK_MemberStatus] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MemberStatus_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_MemberStatus_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_MemberStatus_CreatedUserId]
    ON [meta].[MemberStatus]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MemberStatus_Name]
    ON [meta].[MemberStatus]([Name] ASC) WHERE ([Name] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_MemberStatus_UpdatedUserId]
    ON [meta].[MemberStatus]([UpdatedUserId] ASC);

