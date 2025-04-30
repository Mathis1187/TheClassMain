CREATE TABLE [dbo].[Categories] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]    VARCHAR (255) NULL,
    [Favorit] BIT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Factures] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (255) NULL,
    [Description] VARCHAR (255) NULL,
    [Montant]     DECIMAL (18)  NULL,
    [Date]        DATETIME      NULL,
    [CategorieId] INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CategorieId]) REFERENCES [dbo].[Categories] ([Id])
);