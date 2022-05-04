CREATE TABLE [MAESTRO].[UsuarioRol] (
    [UsuarioRolId] INT IDENTITY (1, 1) NOT NULL,
    [UsuarioId]    INT NULL,
    [RolId]        INT NULL,
    FOREIGN KEY ([RolId]) REFERENCES [MAESTRO].[Rol] ([RolId]),
    FOREIGN KEY ([UsuarioId]) REFERENCES [MAESTRO].[Usuario] ([UsuarioId])
);

