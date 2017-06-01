USE [master]
GO
/****** Object:  Database [DataBaseSadna]    Script Date: 6/1/2017 12:16:50 PM ******/
CREATE DATABASE [DataBaseSadna]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DataBaseSadna', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\DataBaseSadna.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DataBaseSadna_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\DataBaseSadna_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DataBaseSadna] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DataBaseSadna].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DataBaseSadna] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DataBaseSadna] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DataBaseSadna] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DataBaseSadna] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DataBaseSadna] SET ARITHABORT OFF 
GO
ALTER DATABASE [DataBaseSadna] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DataBaseSadna] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DataBaseSadna] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DataBaseSadna] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DataBaseSadna] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DataBaseSadna] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DataBaseSadna] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DataBaseSadna] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DataBaseSadna] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DataBaseSadna] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DataBaseSadna] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DataBaseSadna] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DataBaseSadna] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DataBaseSadna] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DataBaseSadna] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DataBaseSadna] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DataBaseSadna] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DataBaseSadna] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DataBaseSadna] SET  MULTI_USER 
GO
ALTER DATABASE [DataBaseSadna] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DataBaseSadna] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DataBaseSadna] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DataBaseSadna] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DataBaseSadna] SET DELAYED_DURABILITY = DISABLED 
GO
USE [DataBaseSadna]
GO
/****** Object:  Table [dbo].[Card]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Card](
	[Card Value] [int] NOT NULL,
	[Card Shpe] [varchar](10) NOT NULL,
	[Card Real Value] [int] NOT NULL,
 CONSTRAINT [PK_Card] PRIMARY KEY CLUSTERED 
(
	[Card Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Deck]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deck](
	[index] [int] NOT NULL,
	[room Id] [int] NOT NULL,
	[game Id] [int] NOT NULL,
	[card value] [int] NOT NULL,
 CONSTRAINT [PK_Deck] PRIMARY KEY CLUSTERED 
(
	[index] ASC,
	[room Id] ASC,
	[game Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[logId] [int] NOT NULL,
	[msg] [varchar](150) NULL,
 CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED 
(
	[logId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GameMode]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GameMode](
	[Game mode value] [int] NOT NULL,
	[game mode name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_GameMode] PRIMARY KEY CLUSTERED 
(
	[Game mode value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GameRoom]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameRoom](
	[room Id] [int] NOT NULL,
	[game id] [int] NOT NULL,
	[Dealer position] [int] NOT NULL,
	[Max Bet In Round] [int] NOT NULL,
	[Pot count] [int] NOT NULL,
	[Bb] [int] NOT NULL,
	[Sb] [int] NOT NULL,
	[is Active Game] [bit] NOT NULL,
	[curr Player] [int] NOT NULL,
	[Dealer Player] [int] NOT NULL,
	[Bb Player] [int] NOT NULL,
	[SB player] [int] NOT NULL,
	[hand step] [int] NOT NULL,
	[First Player In round] [int] NOT NULL,
	[curr player position] [int] NOT NULL,
	[first player in round position] [int] NOT NULL,
	[last rise in round] [int] NOT NULL,
	[league name] [int] NOT NULL,
 CONSTRAINT [PK_GameRoom] PRIMARY KEY CLUSTERED 
(
	[room Id] ASC,
	[game id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GameRoomPreferance]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameRoomPreferance](
	[room id] [int] NOT NULL,
	[is Spectetor] [bit] NULL,
	[Min player in room] [int] NULL,
	[max player in room] [int] NULL,
	[enter paying money] [int] NULL,
	[starting chip] [int] NULL,
	[Bb] [int] NULL,
	[Sb] [int] NULL,
	[League name] [int] NULL,
	[Game Mode] [int] NULL,
 CONSTRAINT [PK_GameRoomPreferance] PRIMARY KEY CLUSTERED 
(
	[room id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GamesReplays]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GamesReplays](
	[user Id] [int] NOT NULL,
	[room Id] [int] NOT NULL,
	[game Id] [int] NOT NULL,
 CONSTRAINT [PK_GamesReplays] PRIMARY KEY CLUSTERED 
(
	[user Id] ASC,
	[room Id] ASC,
	[game Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HandStep]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HandStep](
	[hand Step value] [int] NOT NULL,
	[hand Step name] [varchar](10) NOT NULL,
 CONSTRAINT [PK_HandStep] PRIMARY KEY CLUSTERED 
(
	[hand Step value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LeagueName]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LeagueName](
	[League Value] [int] NOT NULL,
	[League Name] [varchar](10) NULL,
 CONSTRAINT [PK_LeagueName] PRIMARY KEY CLUSTERED 
(
	[League Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Log]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[LogId] [int] NOT NULL,
	[LogPriority] [int] NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Players]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Players](
	[room Id] [int] NOT NULL,
	[user Id] [int] NOT NULL,
	[is player active] [bit] NOT NULL,
	[player name] [varchar](50) NOT NULL,
	[Total chip] [int] NOT NULL,
	[Round chip bet] [int] NOT NULL,
	[Player action the round] [bit] NOT NULL,
	[first card] [int] NOT NULL,
	[secund card] [int] NOT NULL,
 CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED 
(
	[room Id] ASC,
	[user Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PriorityLogEnum]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PriorityLogEnum](
	[PriorityValue] [int] NOT NULL,
	[ProprityName] [varchar](10) NOT NULL,
 CONSTRAINT [PK_PriorityLogEnum] PRIMARY KEY CLUSTERED 
(
	[PriorityValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Public Cards]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Public Cards](
	[room Id] [int] NOT NULL,
	[card] [int] NOT NULL,
 CONSTRAINT [PK_Public Cards] PRIMARY KEY CLUSTERED 
(
	[room Id] ASC,
	[card] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SpectetorGamesOfUser]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpectetorGamesOfUser](
	[userId] [int] NOT NULL,
	[roomId] [int] NOT NULL,
 CONSTRAINT [PK_SpectetorGamesOfUser] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemLog]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SystemLog](
	[logId] [int] NOT NULL,
	[msg] [varchar](150) NULL,
	[roomId] [int] NOT NULL,
 CONSTRAINT [PK_SystemLog] PRIMARY KEY CLUSTERED 
(
	[logId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[userId] [int] NOT NULL,
	[username] [varchar](50) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[avatar] [varchar](150) NOT NULL,
	[points] [int] NOT NULL,
	[money] [int] NOT NULL,
	[gamesPlayed] [int] NOT NULL,
	[leagueName] [int] NOT NULL,
	[winNum] [int] NOT NULL,
	[HighestCashGainInGame] [int] NOT NULL,
	[TotalProfit] [int] NOT NULL,
	[inActive] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserActiveGames]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserActiveGames](
	[userId] [int] NOT NULL,
	[roomId] [int] NOT NULL,
 CONSTRAINT [PK_UserActiveGames] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserReplaySavedGames]    Script Date: 6/1/2017 12:16:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserReplaySavedGames](
	[userId] [int] NOT NULL,
	[roomId] [int] NOT NULL,
	[gameId] [nchar](10) NULL
) ON [PRIMARY]

GO
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (1, N'Hearts', 1)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (2, N'Hearts', 2)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (3, N'Hearts', 3)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (4, N'Hearts', 4)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (5, N'Hearts', 5)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (6, N'Hearts', 6)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (7, N'Hearts', 7)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (8, N'Hearts', 8)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (9, N'Hearts', 9)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (10, N'Hearts', 10)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (11, N'Hearts', 11)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (12, N'Hearts', 12)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (13, N'Hearts', 13)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (14, N'Diamonds', 1)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (15, N'Diamonds', 2)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (16, N'Diamonds', 3)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (17, N'Diamonds', 4)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (18, N'Diamonds', 5)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (19, N'Diamonds', 6)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (20, N'Diamonds', 7)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (21, N'Diamonds', 8)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (22, N'Diamonds', 9)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (23, N'Diamonds', 10)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (24, N'Diamonds', 11)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (25, N'Diamonds', 12)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (26, N'Diamonds', 13)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (27, N'Spades', 1)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (28, N'Spades', 2)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (29, N'Spades', 3)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (30, N'Spades', 4)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (31, N'Spades', 5)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (32, N'Spades', 6)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (33, N'Spades', 7)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (34, N'Spades', 8)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (35, N'Spades', 9)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (36, N'Spades', 10)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (37, N'Spades', 11)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (38, N'Spades', 12)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (39, N'Spades', 13)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (40, N'Clubs', 1)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (41, N'Clubs', 2)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (42, N'Clubs', 3)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (43, N'Clubs', 4)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (44, N'Clubs', 5)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (45, N'Clubs', 6)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (46, N'Clubs', 7)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (47, N'Clubs', 8)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (48, N'Clubs', 9)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (49, N'Clubs', 10)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (50, N'Clubs', 11)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (51, N'Clubs', 12)
INSERT [dbo].[Card] ([Card Value], [Card Shpe], [Card Real Value]) VALUES (52, N'Clubs', 13)
INSERT [dbo].[GameMode] ([Game mode value], [game mode name]) VALUES (1, N'Limit')
INSERT [dbo].[GameMode] ([Game mode value], [game mode name]) VALUES (2, N'PotLimit')
INSERT [dbo].[GameMode] ([Game mode value], [game mode name]) VALUES (3, N'NoLimit')
INSERT [dbo].[HandStep] ([hand Step value], [hand Step name]) VALUES (1, N'Pre-Flop')
INSERT [dbo].[HandStep] ([hand Step value], [hand Step name]) VALUES (2, N'Flop')
INSERT [dbo].[HandStep] ([hand Step value], [hand Step name]) VALUES (3, N'Turn')
INSERT [dbo].[HandStep] ([hand Step value], [hand Step name]) VALUES (4, N'River')
INSERT [dbo].[LeagueName] ([League Value], [League Name]) VALUES (1, N'A')
INSERT [dbo].[LeagueName] ([League Value], [League Name]) VALUES (2, N'B')
INSERT [dbo].[LeagueName] ([League Value], [League Name]) VALUES (3, N'C')
INSERT [dbo].[LeagueName] ([League Value], [League Name]) VALUES (4, N'D')
INSERT [dbo].[LeagueName] ([League Value], [League Name]) VALUES (5, N'E')
INSERT [dbo].[LeagueName] ([League Value], [League Name]) VALUES (6, N'UnKnow')
INSERT [dbo].[PriorityLogEnum] ([PriorityValue], [ProprityName]) VALUES (1, N'Info')
INSERT [dbo].[PriorityLogEnum] ([PriorityValue], [ProprityName]) VALUES (2, N'Warnning')
INSERT [dbo].[PriorityLogEnum] ([PriorityValue], [ProprityName]) VALUES (3, N'Error')
/****** Object:  Index [IX_Log]    Script Date: 6/1/2017 12:16:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_Log] ON [dbo].[Log]
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [DataBaseSadna] SET  READ_WRITE 
GO
