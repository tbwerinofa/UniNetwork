CREATE TYPE [dbo].[ut_Basic] AS TABLE (
    [Ordinal]       INT            IDENTITY (1, 1) NOT NULL,
    [ParentId]      INT            NULL,
    [Name]          NVARCHAR (500) NULL,
    [Description]   NVARCHAR (500) NULL,
    [Discriminator] NVARCHAR (10)  NULL,
    [CurrentUserId] NVARCHAR (450) NULL,
    PRIMARY KEY CLUSTERED ([Ordinal] ASC));

