CREATE TABLE [MAESTRO].[MiembroFoto] (
    [MiembroFotoId] INT           IDENTITY (1, 1) NOT NULL,
    [MiembroId]     INT           NOT NULL,
    [Foto]          VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([MiembroFotoId] ASC),
    FOREIGN KEY ([MiembroId]) REFERENCES [MAESTRO].[Miembro] ([MiembroId])
);

