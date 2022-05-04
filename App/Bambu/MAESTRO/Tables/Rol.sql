CREATE TABLE [MAESTRO].[Rol] (
    [RolId]        INT           IDENTITY (1, 1) NOT NULL,
    [Denominacion] VARCHAR (255) NULL,
    [Estado]       BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([RolId] ASC)
);

