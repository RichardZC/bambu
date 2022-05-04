CREATE TABLE [MAESTRO].[Usuario] (
    [UsuarioId] INT           NOT NULL,
    [PersonaId] INT           NOT NULL,
    [Nombre]    NVARCHAR (50) NOT NULL,
    [Clave]     NVARCHAR (50) NOT NULL,
    [Estado]    BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([UsuarioId] ASC),
    CONSTRAINT [FK__Usuario__Persona__286302EC] FOREIGN KEY ([PersonaId]) REFERENCES [MAESTRO].[Persona] ([PersonaId])
);

