CREATE TABLE [MAESTRO].[MiembroPago] (
    [MiembroPagoId] INT             IDENTITY (1, 1) NOT NULL,
    [MiembroId]     INT             NOT NULL,
    [Fecha]         DATE            NOT NULL,
    [Importe]       DECIMAL (15, 2) NOT NULL,
    [IndPago]       BIT             DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([MiembroPagoId] ASC),
    FOREIGN KEY ([MiembroId]) REFERENCES [MAESTRO].[Miembro] ([MiembroId])
);

