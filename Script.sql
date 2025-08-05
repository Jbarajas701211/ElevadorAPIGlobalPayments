-- Verificar y crear la base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'DBPrueba')
BEGIN
    CREATE DATABASE DBPrueba
END
GO

-- Usar la base de datos
USE [MiBaseDeDatos]
GO

-- Verificar y crear la tabla Usuario si no existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
   CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Correo] [varchar](70) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[EsBloqueado] [bit] NOT NULL
) ON [PRIMARY]
END

-- Verificar y crear la tabla UsuarioIntento si no existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'UsuarioIntento' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
  CREATE TABLE [dbo].[UsuarioIntento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[Intentos] [int] NULL,
	[Bloqueado] [bit] NULL,
	[FechaBloqueo] [datetime] NULL
) ON [PRIMARY]
END
