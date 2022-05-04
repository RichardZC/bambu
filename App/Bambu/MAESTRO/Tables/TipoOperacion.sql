CREATE TABLE [MAESTRO].[TipoOperacion] (
    [TipoOperacionId] INT          NOT NULL,
    [Codigo]          CHAR (3)     NOT NULL,
    [Denominacion]    VARCHAR (50) NOT NULL,
    [IndEntrada]      BIT          DEFAULT ((0)) NOT NULL,
    [IndCajaDiario]   BIT          DEFAULT ((0)) NOT NULL,
    [IndBoveda]       BIT          DEFAULT ((0)) NOT NULL
);

