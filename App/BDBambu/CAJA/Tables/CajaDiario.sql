CREATE TABLE [CAJA].[CajaDiario] (
    [CajaDiarioId]      INT             IDENTITY (1, 1) NOT NULL,
    [CajaId]            INT             NOT NULL,
    [UsuarioAsignadoId] INT             NOT NULL,
    [SaldoInicial]      DECIMAL (16, 2) DEFAULT ((0)) NOT NULL,
    [Entradas]          DECIMAL (16, 2) DEFAULT ((0)) NOT NULL,
    [Salidas]           DECIMAL (16, 2) DEFAULT ((0)) NOT NULL,
    [SaldoFinal]        DECIMAL (16, 2) DEFAULT ((0)) NOT NULL,
    [FechaIniOperacion] DATETIME        NOT NULL,
    [FechaFinOperacion] DATETIME        NULL,
    [IndCierre]         BIT             DEFAULT ((0)) NOT NULL,
    [TransBoveda]       BIT             DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([CajaDiarioId] ASC),
    FOREIGN KEY ([CajaId]) REFERENCES [CAJA].[Caja] ([CajaId]),
    FOREIGN KEY ([UsuarioAsignadoId]) REFERENCES [MAESTRO].[Usuario] ([UsuarioId])
);

