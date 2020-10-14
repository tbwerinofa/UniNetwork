CREATE TABLE [security].[Control] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Type]             NVARCHAR (50)  NOT NULL,
    [Value]            NVARCHAR (50)  NOT NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  NULL,
    [CreatedUserId]    NVARCHAR (450) NOT NULL,
    [UpdatedUserId]    NVARCHAR (450) NULL,
    CONSTRAINT [PK_Security_Control] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employee_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Employee_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UQ_Security_Control_Type] UNIQUE NONCLUSTERED ([Type] ASC)
);

