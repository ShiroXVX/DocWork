USE [master]
GO
/****** Object:  Database [DipDocument]    Script Date: 29.07.2024 16:52:56 ******/
CREATE DATABASE [DipDocument]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DipDocument', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\DipDocument.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DipDocument_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\DipDocument_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [DipDocument] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DipDocument].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DipDocument] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DipDocument] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DipDocument] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DipDocument] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DipDocument] SET ARITHABORT OFF 
GO
ALTER DATABASE [DipDocument] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DipDocument] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DipDocument] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DipDocument] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DipDocument] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DipDocument] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DipDocument] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DipDocument] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DipDocument] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DipDocument] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DipDocument] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DipDocument] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DipDocument] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DipDocument] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DipDocument] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DipDocument] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DipDocument] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DipDocument] SET RECOVERY FULL 
GO
ALTER DATABASE [DipDocument] SET  MULTI_USER 
GO
ALTER DATABASE [DipDocument] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DipDocument] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DipDocument] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DipDocument] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DipDocument] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DipDocument', N'ON'
GO
ALTER DATABASE [DipDocument] SET QUERY_STORE = OFF
GO
USE [DipDocument]
GO
/****** Object:  Table [dbo].[Вид_доступа]    Script Date: 29.07.2024 16:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Вид_доступа](
	[Код_доступа] [int] NOT NULL,
	[Описание] [varchar](100) NOT NULL,
 CONSTRAINT [Вид_доступа_PK] PRIMARY KEY CLUSTERED 
(
	[Код_доступа] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Документ]    Script Date: 29.07.2024 16:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Документ](
	[Код_документа] [int] NOT NULL,
	[Код_типа] [int] NOT NULL,
	[Документ] [varbinary](max) NOT NULL,
	[Дата_добавления] [datetime] NULL,
	[Название_документа] [varchar](150) NULL,
	[Код_раздела] [int] NOT NULL,
 CONSTRAINT [Документ_PK] PRIMARY KEY CLUSTERED 
(
	[Код_документа] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Должность]    Script Date: 29.07.2024 16:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Должность](
	[Код_должности] [int] NOT NULL,
	[Наименование_должности] [varchar](50) NOT NULL,
 CONSTRAINT [Должность_PK] PRIMARY KEY CLUSTERED 
(
	[Код_должности] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Дополнительный_доступ]    Script Date: 29.07.2024 16:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Дополнительный_доступ](
	[Код_доступа] [int] NOT NULL,
	[Код_профиля] [int] NOT NULL,
	[Код_документа] [int] NOT NULL,
	[Дата_выдачи_доступа] [datetime] NULL,
 CONSTRAINT [Дополнительный_доступ_PK] PRIMARY KEY CLUSTERED 
(
	[Код_доступа] ASC,
	[Код_профиля] ASC,
	[Код_документа] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Отдел]    Script Date: 29.07.2024 16:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Отдел](
	[Код_отдела] [int] NOT NULL,
	[Название_отдела] [varchar](150) NOT NULL,
 CONSTRAINT [Отдел_PK] PRIMARY KEY CLUSTERED 
(
	[Код_отдела] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Превью]    Script Date: 29.07.2024 16:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Превью](
	[Код_превью] [int] IDENTITY(1,1) NOT NULL,
	[Код_документа] [int] NOT NULL,
	[Превью] [varbinary](max) NOT NULL,
 CONSTRAINT [Превью_PK] PRIMARY KEY CLUSTERED 
(
	[Код_превью] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Профиль]    Script Date: 29.07.2024 16:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Профиль](
	[Код_профиля] [int] IDENTITY(1,1) NOT NULL,
	[Код_сотрудника] [int] NOT NULL,
	[Логин] [varchar](50) NOT NULL,
	[Пароль] [varchar](50) NOT NULL,
	[Код_роли] [int] NOT NULL,
	[Путь_загрузки] [varchar](100) NULL,
 CONSTRAINT [Профиль_PK] PRIMARY KEY CLUSTERED 
(
	[Код_профиля] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Раздел]    Script Date: 29.07.2024 16:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Раздел](
	[Код_раздела] [int] NOT NULL,
	[Название_раздела] [varchar](100) NOT NULL,
 CONSTRAINT [Раздел_PK] PRIMARY KEY CLUSTERED 
(
	[Код_раздела] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Роль]    Script Date: 29.07.2024 16:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Роль](
	[Код_роли] [int] NOT NULL,
	[Наименование_роли] [varchar](50) NOT NULL,
 CONSTRAINT [Роль_PK] PRIMARY KEY CLUSTERED 
(
	[Код_роли] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Сотрудник]    Script Date: 29.07.2024 16:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Сотрудник](
	[Код_сотрудника] [int] IDENTITY(1,1) NOT NULL,
	[Код_отдела] [int] NOT NULL,
	[Фамилия] [varchar](50) NOT NULL,
	[Имя] [varchar](50) NOT NULL,
	[Отчество] [varchar](50) NULL,
	[Контактный_номер] [varchar](12) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Код_должности] [int] NOT NULL,
 CONSTRAINT [Сотрудник_PK] PRIMARY KEY CLUSTERED 
(
	[Код_сотрудника] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Список_тегов]    Script Date: 29.07.2024 16:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Список_тегов](
	[Код_тега] [int] IDENTITY(1,1) NOT NULL,
	[Наименование_тега] [varchar](50) NOT NULL,
 CONSTRAINT [Список_тегов_PK] PRIMARY KEY CLUSTERED 
(
	[Код_тега] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Тег_файла]    Script Date: 29.07.2024 16:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Тег_файла](
	[Код_документа] [int] NOT NULL,
	[Код_тега] [int] NOT NULL,
	[Доп_описание] [varchar](50) NULL,
 CONSTRAINT [Тег_файла_PK] PRIMARY KEY CLUSTERED 
(
	[Код_документа] ASC,
	[Код_тега] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Тип_документа]    Script Date: 29.07.2024 16:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Тип_документа](
	[Код_типа] [int] NOT NULL,
	[Название_типа] [varchar](10) NOT NULL,
	[Значёк] [varbinary](max) NULL,
 CONSTRAINT [Тип_документа_PK] PRIMARY KEY CLUSTERED 
(
	[Код_типа] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Документ]  WITH CHECK ADD  CONSTRAINT [Документ_Раздел_FK] FOREIGN KEY([Код_раздела])
REFERENCES [dbo].[Раздел] ([Код_раздела])
GO
ALTER TABLE [dbo].[Документ] CHECK CONSTRAINT [Документ_Раздел_FK]
GO
ALTER TABLE [dbo].[Документ]  WITH CHECK ADD  CONSTRAINT [Документ_Тип_документа_FK] FOREIGN KEY([Код_типа])
REFERENCES [dbo].[Тип_документа] ([Код_типа])
GO
ALTER TABLE [dbo].[Документ] CHECK CONSTRAINT [Документ_Тип_документа_FK]
GO
ALTER TABLE [dbo].[Дополнительный_доступ]  WITH CHECK ADD  CONSTRAINT [Дополнительный_доступ_Вид_доступа_FK] FOREIGN KEY([Код_доступа])
REFERENCES [dbo].[Вид_доступа] ([Код_доступа])
GO
ALTER TABLE [dbo].[Дополнительный_доступ] CHECK CONSTRAINT [Дополнительный_доступ_Вид_доступа_FK]
GO
ALTER TABLE [dbo].[Дополнительный_доступ]  WITH CHECK ADD  CONSTRAINT [Дополнительный_доступ_Документ_FK] FOREIGN KEY([Код_документа])
REFERENCES [dbo].[Документ] ([Код_документа])
GO
ALTER TABLE [dbo].[Дополнительный_доступ] CHECK CONSTRAINT [Дополнительный_доступ_Документ_FK]
GO
ALTER TABLE [dbo].[Дополнительный_доступ]  WITH CHECK ADD  CONSTRAINT [Дополнительный_доступ_Профиль_FK] FOREIGN KEY([Код_профиля])
REFERENCES [dbo].[Профиль] ([Код_профиля])
GO
ALTER TABLE [dbo].[Дополнительный_доступ] CHECK CONSTRAINT [Дополнительный_доступ_Профиль_FK]
GO
ALTER TABLE [dbo].[Превью]  WITH CHECK ADD  CONSTRAINT [Превью_Документ_FK] FOREIGN KEY([Код_документа])
REFERENCES [dbo].[Документ] ([Код_документа])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Превью] CHECK CONSTRAINT [Превью_Документ_FK]
GO
ALTER TABLE [dbo].[Профиль]  WITH CHECK ADD  CONSTRAINT [Профиль_Роль_FK] FOREIGN KEY([Код_роли])
REFERENCES [dbo].[Роль] ([Код_роли])
GO
ALTER TABLE [dbo].[Профиль] CHECK CONSTRAINT [Профиль_Роль_FK]
GO
ALTER TABLE [dbo].[Профиль]  WITH CHECK ADD  CONSTRAINT [Профиль_Сотрудник_FK] FOREIGN KEY([Код_сотрудника])
REFERENCES [dbo].[Сотрудник] ([Код_сотрудника])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Профиль] CHECK CONSTRAINT [Профиль_Сотрудник_FK]
GO
ALTER TABLE [dbo].[Сотрудник]  WITH CHECK ADD  CONSTRAINT [Сотрудник_Должность_FK] FOREIGN KEY([Код_должности])
REFERENCES [dbo].[Должность] ([Код_должности])
GO
ALTER TABLE [dbo].[Сотрудник] CHECK CONSTRAINT [Сотрудник_Должность_FK]
GO
ALTER TABLE [dbo].[Сотрудник]  WITH CHECK ADD  CONSTRAINT [Сотрудник_Отдел_FK] FOREIGN KEY([Код_отдела])
REFERENCES [dbo].[Отдел] ([Код_отдела])
GO
ALTER TABLE [dbo].[Сотрудник] CHECK CONSTRAINT [Сотрудник_Отдел_FK]
GO
ALTER TABLE [dbo].[Тег_файла]  WITH CHECK ADD  CONSTRAINT [Тег_файла_Документ_FK] FOREIGN KEY([Код_документа])
REFERENCES [dbo].[Документ] ([Код_документа])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Тег_файла] CHECK CONSTRAINT [Тег_файла_Документ_FK]
GO
ALTER TABLE [dbo].[Тег_файла]  WITH CHECK ADD  CONSTRAINT [Тег_файла_Список_тегов_FK] FOREIGN KEY([Код_тега])
REFERENCES [dbo].[Список_тегов] ([Код_тега])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Тег_файла] CHECK CONSTRAINT [Тег_файла_Список_тегов_FK]
GO
USE [master]
GO
ALTER DATABASE [DipDocument] SET  READ_WRITE 
GO
