USE [master]
GO
/****** Object:  Database [CiclosAutoclaves]    Script Date: 03/11/2020 17:16:15 ******/
CREATE DATABASE [CiclosAutoclaves]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CiclosAutoclaves', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CiclosAutoclaves.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CiclosAutoclaves_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CiclosAutoclaves_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CiclosAutoclaves] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CiclosAutoclaves].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CiclosAutoclaves] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET ARITHABORT OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CiclosAutoclaves] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CiclosAutoclaves] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CiclosAutoclaves] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CiclosAutoclaves] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CiclosAutoclaves] SET  MULTI_USER 
GO
ALTER DATABASE [CiclosAutoclaves] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CiclosAutoclaves] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CiclosAutoclaves] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CiclosAutoclaves] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CiclosAutoclaves] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CiclosAutoclaves] SET QUERY_STORE = OFF
GO
USE [CiclosAutoclaves]
GO
/****** Object:  Table [dbo].[CiclosAutoclaves]    Script Date: 03/11/2020 17:16:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CiclosAutoclaves](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdAutoclave] [varchar](100) NULL,
	[IdSeccion] [varchar](100) NULL,
	[TInicio] [varchar](100) NULL,
	[NumeroCiclo] [varchar](100) NULL,
	[Programa] [varchar](100) NULL,
	[Modelo] [varchar](100) NULL,
	[Programador] [varchar](100) NULL,
	[Operador] [varchar](100) NULL,
	[CodigoProducto] [varchar](100) NULL,
	[Lote] [varchar](100) NULL,
	[Notas] [varchar](100) NULL,
	[IdUsuario] [varchar](100) NULL,
	[Fase1] [varchar](100) NULL,
	[DuracionTotalF1] [varchar](100) NULL,
	[Fase2] [varchar](100) NULL,
	[DuracionTotalF2] [varchar](100) NULL,
	[Fase3] [varchar](100) NULL,
	[DuracionTotalF3] [varchar](100) NULL,
	[Fase4] [varchar](100) NULL,
	[DuracionTotalF4] [varchar](100) NULL,
	[Fase5] [varchar](100) NULL,
	[DuracionTotalF5] [varchar](100) NULL,
	[TIF5] [varchar](100) NULL,
	[TISubF5] [varchar](100) NULL,
	[TFF5] [varchar](100) NULL,
	[TFSubF5] [varchar](100) NULL,
	[Fase6] [varchar](100) NULL,
	[DuracionTotalF6] [varchar](100) NULL,
	[TIF6] [varchar](100) NULL,
	[TISubF6] [varchar](100) NULL,
	[TFF6] [varchar](100) NULL,
	[TFSubF6] [varchar](100) NULL,
	[Fase7] [varchar](100) NULL,
	[DuracionTotalF7] [varchar](100) NULL,
	[TIF7] [varchar](100) NULL,
	[TISubF7] [varchar](100) NULL,
	[Fase8] [varchar](100) NULL,
	[DuracionTotalF8] [varchar](100) NULL,
	[TIF8] [varchar](100) NULL,
	[TISubF8] [varchar](100) NULL,
	[TFF8] [varchar](100) NULL,
	[Fase9] [varchar](100) NULL,
	[DuracionTotalF9] [varchar](100) NULL,
	[TIF9] [varchar](100) NULL,
	[TISubF9] [varchar](100) NULL,
	[TFF9] [varchar](100) NULL,
	[Fase10] [varchar](100) NULL,
	[DuracionTotalF10] [varchar](100) NULL,
	[Fase11] [varchar](100) NULL,
	[DuracionTotalF11] [varchar](100) NULL,
	[Fase12] [varchar](100) NULL,
	[DuracionTotalF12] [varchar](100) NULL,
	[Fase13] [varchar](100) NULL,
	[TFF13] [varchar](100) NULL,
	[TFSubF13] [varchar](100) NULL,
	[HoraInicio] [varchar](100) NULL,
	[HoraFin] [varchar](100) NULL,
	[EsterilizacionN] [varchar](100) NULL,
	[TMinima] [varchar](100) NULL,
	[TMaxima] [varchar](100) NULL,
	[DuracionTotal] [varchar](100) NULL,
	[FtzMin] [varchar](100) NULL,
	[FtzMax] [varchar](100) NULL,
	[DifMaxMin] [varchar](100) NULL,
	[AperturaPuerta] [varchar](100) NULL,
	[TiempoCiclo] [varchar](100) NULL,
	[ErrorCiclo] [varchar](max) NULL,
	[FechaRegistro] [datetime] NULL,
 CONSTRAINT [PK_Ciclos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CiclosSabiDos]    Script Date: 03/11/2020 17:16:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CiclosSabiDos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdAutoclave] [varchar](100) NULL,
	[IdSeccion] [varchar](100) NULL,
	[TInicio] [varchar](100) NULL,
	[NumeroCiclo] [varchar](100) NULL,
	[Programa] [varchar](100) NULL,
	[Modelo] [varchar](100) NULL,
	[Programador] [varchar](100) NULL,
	[Operador] [varchar](100) NULL,
	[CodigoProducto] [varchar](100) NULL,
	[Lote] [varchar](100) NULL,
	[Notas] [varchar](100) NULL,
	[IdUsuario] [varchar](100) NULL,
	[Fase1] [varchar](100) NULL,
	[DuracionTotalF1] [varchar](100) NULL,
	[Fase2] [varchar](100) NULL,
	[DuracionTotalF2] [varchar](100) NULL,
	[TIF2] [varchar](100) NULL,
	[TISubF2] [varchar](100) NULL,
	[TFF2] [varchar](100) NULL,
	[TFSubF2] [varchar](100) NULL,
	[Fase3] [varchar](100) NULL,
	[DuracionTotalF3] [varchar](100) NULL,
	[TIF3] [varchar](100) NULL,
	[TISubF3] [varchar](100) NULL,
	[TFF3] [varchar](100) NULL,
	[TFSubF3] [varchar](100) NULL,
	[Fase4] [varchar](100) NULL,
	[DuracionTotalF4] [varchar](100) NULL,
	[TIF4] [varchar](100) NULL,
	[TISubF4] [varchar](100) NULL,
	[Fase5] [varchar](100) NULL,
	[DuracionTotalF5] [varchar](100) NULL,
	[Fase6] [varchar](100) NULL,
	[DuracionTotalF6] [varchar](100) NULL,
	[Fase7A] [varchar](100) NULL,
	[DuracionTotalF7A] [varchar](100) NULL,
	[Fase8A] [varchar](100) NULL,
	[DuracionTotalF8A] [varchar](100) NULL,
	[Fase7B] [varchar](100) NULL,
	[DuracionTotalF7B] [varchar](100) NULL,
	[Fase8B] [varchar](100) NULL,
	[DuracionTotalF8B] [varchar](100) NULL,
	[Fase9] [varchar](100) NULL,
	[DuracionTotalF9] [varchar](100) NULL,
	[TIF9] [varchar](100) NULL,
	[TISubF9] [varchar](100) NULL,
	[TFF9] [varchar](100) NULL,
	[Fase10] [varchar](100) NULL,
	[DuracionTotalF10] [varchar](100) NULL,
	[Fase11] [varchar](100) NULL,
	[DuracionTotalF11] [varchar](100) NULL,
	[Fase12] [varchar](100) NULL,
	[TIF12] [varchar](100) NULL,
	[TISubF12] [varchar](100) NULL,
	[HoraInicio] [varchar](100) NULL,
	[HoraFin] [varchar](100) NULL,
	[EsterilizacionN] [varchar](100) NULL,
	[TMinima] [varchar](100) NULL,
	[TMaxima] [varchar](100) NULL,
	[DuracionTotal] [varchar](100) NULL,
	[FtzMin] [varchar](100) NULL,
	[FtzMax] [varchar](100) NULL,
	[DifMaxMin] [varchar](100) NULL,
	[AperturaPuerta] [varchar](100) NULL,
	[TiempoCiclo] [varchar](100) NULL,
	[ErrorCiclo] [varchar](max) NULL,
	[FechaRegistro] [datetime] NULL,
 CONSTRAINT [PK_CiclosSabiDos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaestroAutoclave]    Script Date: 03/11/2020 17:16:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaestroAutoclave](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Matricula] [varchar](50) NULL,
	[Nombre] [varchar](50) NULL,
	[Version] [int] NOT NULL,
	[IP] [varchar](50) NULL,
	[Seccion] [varchar](50) NULL,
	[Estado] [bit] NULL,
	[UltimoCiclo] [varchar](100) NULL,
	[RutaSalida] [varchar](300) NULL,
 CONSTRAINT [PK_MaestraAutoclave] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [CiclosAutoclaves] SET  READ_WRITE 
GO
