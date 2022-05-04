CREATE TABLE [CAJA].[BovedaMov] (
    [MovimientoBovedaId] INT             IDENTITY (1, 1) NOT NULL,
    [BovedaId]           INT             NOT NULL,
    [CodOperacion]       CHAR (3)        NOT NULL,
    [Glosa]              VARCHAR (MAX)   NULL,
    [Importe]            DECIMAL (16, 2) NOT NULL,
    [IndEntrada]         BIT             NOT NULL,
    [Estado]             BIT             NOT NULL,
    [CajaDiarioId]       INT             NULL,
    [UsuarioRegId]       INT             NOT NULL,
    [FechaReg]           DATETIME        NOT NULL,
    PRIMARY KEY CLUSTERED ([MovimientoBovedaId] ASC),
    FOREIGN KEY ([BovedaId]) REFERENCES [CAJA].[Boveda] ([BovedaId])
);

