CREATE TABLE [MAESTRO].[RolMenu] (
    [RolMenuId] INT IDENTITY (1, 1) NOT NULL,
    [RolId]     INT NULL,
    [MenuId]    INT NULL,
    PRIMARY KEY CLUSTERED ([RolMenuId] ASC),
    FOREIGN KEY ([MenuId]) REFERENCES [MAESTRO].[Menu] ([MenuId]),
    FOREIGN KEY ([RolId]) REFERENCES [MAESTRO].[Rol] ([RolId])
);

