CREATE TABLE [dbo].[Address] (
    [Id]           INT         IDENTITY (1, 1) NOT NULL,
    [Street]       NCHAR (150) NOT NULL,
    [Number]       INT         NOT NULL,
    [PostCode]     NCHAR (15)  NOT NULL,
    [City]         NCHAR (100) NOT NULL,
    [State]        NCHAR (2)   NOT NULL,
    [Neighborhood] NCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

