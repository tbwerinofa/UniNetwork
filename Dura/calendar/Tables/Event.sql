CREATE TABLE [calendar].[Event] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (50)  NOT NULL,
    [RequiresRsvp]         BIT            NOT NULL,
    [RequiresSubscription] BIT            NOT NULL,
    [FrequencyId]          INT            NOT NULL,
    [EventTypeId]          INT            NOT NULL,
    [IsActive]             BIT            DEFAULT ((1)) NOT NULL,
    [CreatedUserId]        NVARCHAR (450) NOT NULL,
    [UpdatedUserId]        NVARCHAR (450) NULL,
    [CreatedTimestamp]     DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp]     DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Event_AspNetUsers_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Event_AspNetUsers_UpdatedUserId] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Event_EventType_Id] FOREIGN KEY ([EventTypeId]) REFERENCES [meta].[EventType] ([Id]),
    CONSTRAINT [FK_Event_Frequency_Id] FOREIGN KEY ([FrequencyId]) REFERENCES [meta].[Frequency] ([Id]),
    CONSTRAINT [UQ_Event_IssueNo] UNIQUE NONCLUSTERED ([Name] ASC, [EventTypeId] ASC)
);

