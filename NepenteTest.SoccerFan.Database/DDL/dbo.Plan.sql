CREATE TABLE [dbo].[Plan] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]  VARCHAR (150) NOT NULL,
    [Price] FLOAT (53)    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

