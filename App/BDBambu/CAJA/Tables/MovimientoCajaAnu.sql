CREATE TABLE [CAJA].[MovimientoCajaAnu] (
    [MovimientoCajaAnuId] INT           IDENTITY (1, 1) NOT NULL,
    [MovimientoCajaId]    INT           NULL,
    [Observacion]         VARCHAR (MAX) NULL,
    [UsuarioRegId]        INT           NULL,
    [FechaReg]            DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([MovimientoCajaAnuId] ASC),
    FOREIGN KEY ([MovimientoCajaId]) REFERENCES [CAJA].[MovimientoCaja] ([MovimientoCajaId])
);

