CREATE TABLE [worker].[MemberMapping] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [MemberId]         INT            NOT NULL,
    [RelationMemberId] INT            NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    CONSTRAINT [PK_MemberMapping] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MemberMapping_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_MemberMapping_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_MemberMapping_Member_Id] FOREIGN KEY ([MemberId]) REFERENCES [worker].[Member] ([Id]),
    CONSTRAINT [FK_MemberMapping_MemberRelation_Id] FOREIGN KEY ([RelationMemberId]) REFERENCES [worker].[Member] ([Id]),
    CONSTRAINT [UQ_MemberMapping_Member_Id] UNIQUE NONCLUSTERED ([MemberId] ASC, [RelationMemberId] ASC)
);

