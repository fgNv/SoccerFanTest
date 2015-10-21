CREATE TABLE [dbo].[Titular] (
    [Id]        INT         IDENTITY (1, 1) NOT NULL,
    [FirstName] NCHAR (100) NOT NULL,
    [LastName]  NCHAR (100) NOT NULL,
    [CPF]       NCHAR (20)  NOT NULL,
    [Phone]     NCHAR (20)  NOT NULL,
    [Email]     NCHAR (40)  NOT NULL,
    [BirthDate] DATE        NOT NULL,
    [AddressId] INT         NOT NULL,
    [PlanId]    INT         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([Id]),
    FOREIGN KEY ([PlanId]) REFERENCES [dbo].[Plan] ([Id])
);

