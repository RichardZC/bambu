CREATE TABLE [MAESTRO].[Archivo] (
    [ArchivoId]   INT           IDENTITY (1, 1) NOT NULL,
    [MiembroId]   INT           NOT NULL,
    [Fecha]       DATETIME      NOT NULL,
    [Nombre]      VARCHAR (100) NOT NULL,
    [EvidenciaId] INT           NOT NULL,
    CONSTRAINT [PK_Archivo] PRIMARY KEY CLUSTERED ([ArchivoId] ASC),
    FOREIGN KEY ([MiembroId]) REFERENCES [MAESTRO].[Miembro] ([MiembroId])
);

