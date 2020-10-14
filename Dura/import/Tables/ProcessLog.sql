CREATE TABLE [import].[ProcessLog] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [DocumentId]         INT             NULL,
    [ParentProcessLogId] INT             NULL,
    [ProcessID]          INT             NOT NULL,
    [Result]             NVARCHAR (2000) NOT NULL,
    [DateStart]          DATETIME        CONSTRAINT [DF_impProcesslog_DateStart] DEFAULT (getdate()) NOT NULL,
    [DateEnd]            DATETIME        NULL,
    [ErrorBit]           BIT             NULL,
    [CreatedUserId]      NVARCHAR (450)  NOT NULL,
    [UpdatedUserId]      NVARCHAR (450)  NULL,
    [CreatedDateTime]    DATETIME        CONSTRAINT [DF_impProcesslog_DateAdded] DEFAULT (getdate()) NOT NULL,
    [UpdatedDateTime]    DATETIME        NULL,
    [IsActive]           BIT             CONSTRAINT [DF_importProcesslog_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [FK_import.ProcessLog_Created] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_import.ProcessLog_Document] FOREIGN KEY ([DocumentId]) REFERENCES [meta].[Document] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_import.ProcessLog_Updated] FOREIGN KEY ([UpdatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

