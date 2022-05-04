CREATE TABLE [CAJA].[MovimientoCaja] (
    [MovimientoCajaId] INT             IDENTITY (1, 1) NOT NULL,
    [CajaDiarioId]     INT             NOT NULL,
    [Operacion]        CHAR (3)        NOT NULL,
    [ImportePago]      DECIMAL (16, 2) DEFAULT ((0)) NOT NULL,
    [PersonaId]        INT             NULL,
    [Descripcion]      VARCHAR (MAX)   NULL,
    [IndEntrada]       BIT             DEFAULT ((0)) NOT NULL,
    [Estado]           BIT             DEFAULT ((0)) NOT NULL,
    [UsuarioRegId]     INT             NOT NULL,
    [FechaReg]         DATETIME        NOT NULL,
    PRIMARY KEY CLUSTERED ([MovimientoCajaId] ASC),
    FOREIGN KEY ([CajaDiarioId]) REFERENCES [CAJA].[CajaDiario] ([CajaDiarioId]),
    CONSTRAINT [FK__Movimient__Perso__4E88ABD4] FOREIGN KEY ([PersonaId]) REFERENCES [MAESTRO].[Persona] ([PersonaId])
);

