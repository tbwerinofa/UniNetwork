CREATE TABLE [worker].[Member] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [CreatedUserId]        NVARCHAR (450) NOT NULL,
    [UpdatedUserId]        NVARCHAR (450) NULL,
    [PersonId]             INT            NOT NULL,
    [MemberNo]             NVARCHAR (50)  NOT NULL,
    [EmmergencyContact1]   NVARCHAR (250) NOT NULL,
    [EmmergencyContactNo1] NVARCHAR (250) NOT NULL,
    [EmmergencyContact2]   NVARCHAR (250) NOT NULL,
    [EmmergencyContactNo2] NVARCHAR (250) NOT NULL,
    [MedicalAidName]       NVARCHAR (100) NULL,
    [MedicalAidNumber]     NVARCHAR (100) NULL,
    [Occupation]           NVARCHAR (250) NULL,
    [Company]              NVARCHAR (250) NULL,
    [WorkTelephone]        NVARCHAR (250) NULL,
    [HomeTelephone]        NVARCHAR (250) NULL,
    [IsActive]             BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp]     DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]     DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Member_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Member_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Member_Person_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Member_CreatedUserId]
    ON [worker].[Member]([CreatedUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Member_MemberNo]
    ON [worker].[Member]([MemberNo] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Member_PersonId]
    ON [worker].[Member]([PersonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Member_UpdatedUserId]
    ON [worker].[Member]([UpdatedUserId] ASC);

