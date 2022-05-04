CREATE TABLE [MAESTRO].[Ocupacion] (
    [OcupacionId]  INT           IDENTITY (1, 1) NOT NULL,
    [Denominacion] VARCHAR (200) NULL,
    [Estado]       BIT           NULL,
    PRIMARY KEY CLUSTERED ([OcupacionId] ASC)
);

