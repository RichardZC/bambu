CREATE TABLE [CAJA].[Boveda] (
    [BovedaId]          INT             IDENTITY (1, 1) NOT NULL,
    [SaldoInicial]      DECIMAL (16, 2) NOT NULL,
    [Entradas]          DECIMAL (16, 2) NOT NULL,
    [Salidas]           DECIMAL (16, 2) NOT NULL,
    [SaldoFinal]        DECIMAL (16, 2) NOT NULL,
    [FechaIniOperacion] DATETIME        NOT NULL,
    [FechaFinOperacion] DATETIME        NULL,
    [IndCierre]         BIT             NOT NULL,
    [IndTemporal]       BIT             NOT NULL,
    PRIMARY KEY CLUSTERED ([BovedaId] ASC)
);

