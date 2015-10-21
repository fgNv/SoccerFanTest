CREATE TABLE [dbo].[Dependent] (
    [Id]        INT         IDENTITY (1, 1) NOT NULL,
    [FirstName] NCHAR (100) NOT NULL,
    [LastName]  NCHAR (100) NOT NULL,
    [CPF]       NCHAR (20)  NOT NULL,
    [Email]     NCHAR (50)  NOT NULL,
    [TitularId] INT         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([TitularId]) REFERENCES [dbo].[Titular] ([Id])
);

