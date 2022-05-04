CREATE TABLE [MAESTRO].[Mensaje] (
    [MensajeId] INT      IDENTITY (1, 1) NOT NULL,
    [MiembroId] INT      NOT NULL,
    [Fecha]     DATETIME NOT NULL,
    [Nota]      TEXT     NOT NULL,
    [NivelId]   INT      NOT NULL,
    CONSTRAINT [PK__Nota__EF36CC1AE2DF1D5A] PRIMARY KEY CLUSTERED ([MensajeId] ASC),
    CONSTRAINT [FK_Mensaje_Miembro] FOREIGN KEY ([MiembroId]) REFERENCES [MAESTRO].[Miembro] ([MiembroId])
);

