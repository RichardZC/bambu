CREATE TABLE [MAESTRO].[Grupo] (
    [GrupoId]      INT             IDENTITY (1, 1) NOT NULL,
    [Denominacion] VARCHAR (250)   NOT NULL,
    [FechaInicio]  DATE            NULL,
    [TallerId]     INT             CONSTRAINT [DF_Grupo_TallerId] DEFAULT ((1)) NOT NULL,
    [Costo]        DECIMAL (15, 2) CONSTRAINT [DF_Grupo_Costo] DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([GrupoId] ASC)
);

