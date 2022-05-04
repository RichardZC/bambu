CREATE TABLE [MAESTRO].[Miembro] (
    [MiembroId] INT      IDENTITY (1, 1) NOT NULL,
    [GrupoId]   INT      NOT NULL,
    [NivelId]   INT      NOT NULL,
    [PersonaId] INT      NOT NULL,
    [Fecha]     DATETIME NOT NULL,
    [EstadoId]  INT      DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([MiembroId] ASC),
    FOREIGN KEY ([GrupoId]) REFERENCES [MAESTRO].[Grupo] ([GrupoId]),
    FOREIGN KEY ([PersonaId]) REFERENCES [MAESTRO].[Persona] ([PersonaId])
);

