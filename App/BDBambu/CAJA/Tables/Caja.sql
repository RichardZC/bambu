CREATE TABLE [CAJA].[Caja] (
    [CajaId]       INT           IDENTITY (1, 1) NOT NULL,
    [Denominacion] VARCHAR (100) NOT NULL,
    [Estado]       BIT           NOT NULL,
    [IndAbierto]   BIT           NOT NULL,
    [CajeroId]     INT           NULL,
    PRIMARY KEY CLUSTERED ([CajaId] ASC)
);

