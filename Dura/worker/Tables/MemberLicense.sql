CREATE TABLE [worker].[MemberLicense] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [FinYearId]        INT            NOT NULL,
    [MemberId]         INT            NOT NULL,
    [LicenseNo]        NVARCHAR (50)  NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    CONSTRAINT [PK_MemberLicense] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MemberLicense_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_MemberLicense_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_MemberLicense_FinYear_Id] FOREIGN KEY ([FinYearId]) REFERENCES [meta].[FinYear] ([Id]),
    CONSTRAINT [FK_MemberLicense_Member_Id] FOREIGN KEY ([MemberId]) REFERENCES [worker].[Member] ([Id]),
    CONSTRAINT [FK_MemberLicense_FinYear_MemberId_LicenseNo] UNIQUE NONCLUSTERED ([FinYearId] ASC, [MemberId] ASC, [LicenseNo] ASC)
);

