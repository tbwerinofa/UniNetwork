CREATE TABLE [dbo].[AspNetRoles] (
    [Id]                   NVARCHAR (450) NOT NULL,
    [Name]                 NVARCHAR (256) NULL,
    [NormalizedName]       NVARCHAR (256) NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX) NULL,
    [TenantId] [nvarchar](64) NOT NULL,
    --[Discriminator]        NVARCHAR (MAX) NOT NULL,
    --[IsCorporateUnitLevel] BIT            NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([NormalizedName] ASC) WHERE ([NormalizedName] IS NOT NULL);

