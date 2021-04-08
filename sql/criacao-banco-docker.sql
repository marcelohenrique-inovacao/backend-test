USE [master]
GO
/****** Object:  Database [BackEndTestDocker]    Script Date: 08/04/2021 14:36:16 ******/
CREATE DATABASE [BackEndTestDocker]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BackEndTestDocker', FILENAME = N'/var/opt/mssql/data/BackEndTestDocker.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BackEndTestDocker_log', FILENAME = N'/var/opt/mssql/data/BackEndTestDocker_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BackEndTestDocker] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BackEndTestDocker].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BackEndTestDocker] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET ARITHABORT OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BackEndTestDocker] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BackEndTestDocker] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BackEndTestDocker] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BackEndTestDocker] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BackEndTestDocker] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET RECOVERY FULL 
GO
ALTER DATABASE [BackEndTestDocker] SET  MULTI_USER 
GO
ALTER DATABASE [BackEndTestDocker] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BackEndTestDocker] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BackEndTestDocker] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BackEndTestDocker] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BackEndTestDocker] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BackEndTestDocker', N'ON'
GO
ALTER DATABASE [BackEndTestDocker] SET QUERY_STORE = OFF
GO
USE [BackEndTestDocker]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 08/04/2021 14:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Aplicativo]    Script Date: 08/04/2021 14:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aplicativo](
	[Id] [uniqueidentifier] NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[DataLancamento] [smalldatetime] NOT NULL,
	[Plataforma] [tinyint] NOT NULL,
	[Id_DesenvolvedorResponsavel] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Aplicativo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Desenvolvedor]    Script Date: 08/04/2021 14:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Desenvolvedor](
	[Id] [uniqueidentifier] NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[CPF] [char](11) NULL,
	[Email] [varchar](100) NULL,
 CONSTRAINT [PK_Desenvolvedor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DesenvolvedorAplicativo]    Script Date: 08/04/2021 14:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DesenvolvedorAplicativo](
	[Id] [uniqueidentifier] NOT NULL,
	[Id_Desenvolvedor] [uniqueidentifier] NOT NULL,
	[Id_Aplicativo] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_DesenvolvedorAplicativo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
 
Use BackEndTestDocker
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210325174213_Initial', N'5.0.4')
GO
INSERT [dbo].[Desenvolvedor] ([Id], [Nome], [CPF], [Email]) VALUES (N'408d3aa6-b39f-473e-a275-4f54303bbb7f', N'Junior', N'95312167037', N'junior@gmail.com')
GO
INSERT [dbo].[Desenvolvedor] ([Id], [Nome], [CPF], [Email]) VALUES (N'2a7e6458-98fd-4415-a7b7-7c11fc6270fa', N'Paulo Henrique', N'08272217627', N'paulo@gmail.com')
GO
INSERT [dbo].[Desenvolvedor] ([Id], [Nome], [CPF], [Email]) VALUES (N'bda93dbb-4ca2-48a6-8637-a176eb4a6098', N'Paulo', N'64188680059', N'paulo@gmail.com')
GO
INSERT [dbo].[Desenvolvedor] ([Id], [Nome], [CPF], [Email]) VALUES (N'1e5c0a5f-7f5e-4eb0-a4a7-bd6a35bb053e', N'Mauro', N'37747423080', N'mauro@gmail.com')
GO
INSERT [dbo].[Desenvolvedor] ([Id], [Nome], [CPF], [Email]) VALUES (N'e69f58f3-5465-4619-8f9b-cbd0c13d93b0', N'Teste', N'11111111111', N'teste@teste')
GO
INSERT [dbo].[Desenvolvedor] ([Id], [Nome], [CPF], [Email]) VALUES (N'9a82f9ca-0bc5-4e11-a417-e2bf8b7308ef', N'Marcelo', N'08272217627', N'marcelo@gmail.com')
GO
INSERT [dbo].[Aplicativo] ([Id], [Nome], [DataLancamento], [Plataforma], [Id_DesenvolvedorResponsavel]) VALUES (N'7ebb7935-ed98-444a-9247-4390d16ed615', N'Google', CAST(N'2021-05-12T00:00:00' AS SmallDateTime), 2, NULL)
GO
INSERT [dbo].[Aplicativo] ([Id], [Nome], [DataLancamento], [Plataforma], [Id_DesenvolvedorResponsavel]) VALUES (N'277ebfc8-a657-4714-bd3b-45645ff2a82e', N'Bradesco', CAST(N'2021-10-29T00:00:00' AS SmallDateTime), 1, N'9a82f9ca-0bc5-4e11-a417-e2bf8b7308ef')
GO
INSERT [dbo].[Aplicativo] ([Id], [Nome], [DataLancamento], [Plataforma], [Id_DesenvolvedorResponsavel]) VALUES (N'9c8d30a6-586e-418d-98bb-7eedb1f612b8', N'Bradesco1', CAST(N'2021-10-29T00:00:00' AS SmallDateTime), 1, NULL)
GO
INSERT [dbo].[Aplicativo] ([Id], [Nome], [DataLancamento], [Plataforma], [Id_DesenvolvedorResponsavel]) VALUES (N'790687bb-682c-480d-811e-c999790c298f', N'Facebook', CAST(N'2021-03-11T00:00:00' AS SmallDateTime), 1, N'e69f58f3-5465-4619-8f9b-cbd0c13d93b0')
GO
INSERT [dbo].[DesenvolvedorAplicativo] ([Id], [Id_Desenvolvedor], [Id_Aplicativo]) VALUES (N'a3e2d4fb-9e97-401c-847c-23fb5b3f684a', N'408d3aa6-b39f-473e-a275-4f54303bbb7f', N'790687bb-682c-480d-811e-c999790c298f')
GO
INSERT [dbo].[DesenvolvedorAplicativo] ([Id], [Id_Desenvolvedor], [Id_Aplicativo]) VALUES (N'd06ef029-a104-4f8c-94b6-2209c67a17ea', N'9a82f9ca-0bc5-4e11-a417-e2bf8b7308ef', N'7ebb7935-ed98-444a-9247-4390d16ed615')
GO
INSERT [dbo].[DesenvolvedorAplicativo] ([Id], [Id_Desenvolvedor], [Id_Aplicativo]) VALUES (N'5ed2e9fe-4a41-4b0b-b860-77522d8638fc', N'9a82f9ca-0bc5-4e11-a417-e2bf8b7308ef', N'790687bb-682c-480d-811e-c999790c298f')
GO
 
/****** Object:  Index [Idx_Id_DesenvolvedorResponsavel]    Script Date: 08/04/2021 14:36:17 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Idx_Id_DesenvolvedorResponsavel] ON [dbo].[Aplicativo]
(
	[Id_DesenvolvedorResponsavel] ASC
)
WHERE ([Id_DesenvolvedorResponsavel] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Idx_Id_Aplicativo]    Script Date: 08/04/2021 14:36:17 ******/
CREATE NONCLUSTERED INDEX [Idx_Id_Aplicativo] ON [dbo].[DesenvolvedorAplicativo]
(
	[Id_Aplicativo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Idx_Id_Desenvolvedor]    Script Date: 08/04/2021 14:36:17 ******/
CREATE NONCLUSTERED INDEX [Idx_Id_Desenvolvedor] ON [dbo].[DesenvolvedorAplicativo]
(
	[Id_Desenvolvedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Idx_Id_Desenvolvedor_Id_Aplicativo]    Script Date: 08/04/2021 14:36:17 ******/
CREATE NONCLUSTERED INDEX [Idx_Id_Desenvolvedor_Id_Aplicativo] ON [dbo].[DesenvolvedorAplicativo]
(
	[Id_Desenvolvedor] ASC,
	[Id_Aplicativo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Aplicativo]  WITH CHECK ADD  CONSTRAINT [Id_DesenvolvedorResponsavel] FOREIGN KEY([Id_DesenvolvedorResponsavel])
REFERENCES [dbo].[Desenvolvedor] ([Id])
GO
ALTER TABLE [dbo].[Aplicativo] CHECK CONSTRAINT [Id_DesenvolvedorResponsavel]
GO
ALTER TABLE [dbo].[DesenvolvedorAplicativo]  WITH CHECK ADD  CONSTRAINT [Id_Aplicativo] FOREIGN KEY([Id_Aplicativo])
REFERENCES [dbo].[Aplicativo] ([Id])
GO
ALTER TABLE [dbo].[DesenvolvedorAplicativo] CHECK CONSTRAINT [Id_Aplicativo]
GO
ALTER TABLE [dbo].[DesenvolvedorAplicativo]  WITH CHECK ADD  CONSTRAINT [Id_Desenvolvedor] FOREIGN KEY([Id_Desenvolvedor])
REFERENCES [dbo].[Desenvolvedor] ([Id])
GO
ALTER TABLE [dbo].[DesenvolvedorAplicativo] CHECK CONSTRAINT [Id_Desenvolvedor]
GO
USE [master]
GO
ALTER DATABASE [BackEndTestDocker] SET  READ_WRITE 
GO
