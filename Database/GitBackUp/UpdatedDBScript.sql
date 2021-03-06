USE [DataBaseSadna]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 27/06/2017 21:56:18 ******/
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
/****** Object:  Table [dbo].[GameMode]    Script Date: 27/06/2017 21:56:18 ******/
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
/****** Object:  Table [dbo].[GameRoom]    Script Date: 27/06/2017 21:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GameRoom](
	[RoomId] [int] NOT NULL,
	[GameId] [int] NOT NULL,
	[Replay] [varchar](max) NOT NULL,
	[GameXML] [xml] NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_GameRoom] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC,
	[GameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GameRoomPreferance]    Script Date: 27/06/2017 21:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameRoomPreferance](
	[Roomid] [int] NOT NULL,
	[CanSpectate] [bit] NULL,
	[MinPlayers] [int] NULL,
	[MaxPlayers] [int] NULL,
	[BuyInPolicy] [int] NULL,
	[EnterGamePolicy] [int] NULL,
	[MinBet] [int] NULL,
	[League] [int] NULL,
	[GameMode] [int] NULL,
	[GameId] [int] NOT NULL,
	[PotSize] [int] NULL,
 CONSTRAINT [PK_GameRoomPreferance] PRIMARY KEY CLUSTERED 
(
	[Roomid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LeagueName]    Script Date: 27/06/2017 21:56:18 ******/
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
/****** Object:  Table [dbo].[Log]    Script Date: 27/06/2017 21:56:18 ******/
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
/****** Object:  Table [dbo].[PriorityLogEnum]    Script Date: 27/06/2017 21:56:18 ******/
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
/****** Object:  Table [dbo].[SpectetorGamesOfUser]    Script Date: 27/06/2017 21:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpectetorGamesOfUser](
	[userId] [int] NOT NULL,
	[roomId] [int] NOT NULL,
	[Game Id] [int] NOT NULL,
 CONSTRAINT [PK_SpectetorGamesOfUser] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemLog]    Script Date: 27/06/2017 21:56:18 ******/
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
	[game Id] [int] NOT NULL,
 CONSTRAINT [PK_SystemLog] PRIMARY KEY CLUSTERED 
(
	[logId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserActiveGames]    Script Date: 27/06/2017 21:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserActiveGames](
	[userId] [int] NOT NULL,
	[roomId] [int] NOT NULL,
	[Game Id] [int] NOT NULL,
 CONSTRAINT [PK_UserActiveGames] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserTable]    Script Date: 27/06/2017 21:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserTable](
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
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ErrorLog]  WITH CHECK ADD  CONSTRAINT [FK_ErrorLog_Log] FOREIGN KEY([logId])
REFERENCES [dbo].[Log] ([LogId])
GO
ALTER TABLE [dbo].[ErrorLog] CHECK CONSTRAINT [FK_ErrorLog_Log]
GO
ALTER TABLE [dbo].[GameRoomPreferance]  WITH CHECK ADD  CONSTRAINT [FK_GameRoomPreferance_GameMode1] FOREIGN KEY([GameMode])
REFERENCES [dbo].[GameMode] ([Game mode value])
GO
ALTER TABLE [dbo].[GameRoomPreferance] CHECK CONSTRAINT [FK_GameRoomPreferance_GameMode1]
GO
ALTER TABLE [dbo].[GameRoomPreferance]  WITH CHECK ADD  CONSTRAINT [FK_GameRoomPreferance_GameRoom1] FOREIGN KEY([Roomid], [GameId])
REFERENCES [dbo].[GameRoom] ([RoomId], [GameId])
GO
ALTER TABLE [dbo].[GameRoomPreferance] CHECK CONSTRAINT [FK_GameRoomPreferance_GameRoom1]
GO
ALTER TABLE [dbo].[GameRoomPreferance]  WITH CHECK ADD  CONSTRAINT [FK_GameRoomPreferance_LeagueName1] FOREIGN KEY([League])
REFERENCES [dbo].[LeagueName] ([League Value])
GO
ALTER TABLE [dbo].[GameRoomPreferance] CHECK CONSTRAINT [FK_GameRoomPreferance_LeagueName1]
GO
ALTER TABLE [dbo].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Log_PriorityLogEnum] FOREIGN KEY([LogPriority])
REFERENCES [dbo].[PriorityLogEnum] ([PriorityValue])
GO
ALTER TABLE [dbo].[Log] CHECK CONSTRAINT [FK_Log_PriorityLogEnum]
GO
ALTER TABLE [dbo].[SpectetorGamesOfUser]  WITH CHECK ADD  CONSTRAINT [FK_SpectetorGamesOfUser_GameRoom] FOREIGN KEY([roomId], [Game Id])
REFERENCES [dbo].[GameRoom] ([RoomId], [GameId])
GO
ALTER TABLE [dbo].[SpectetorGamesOfUser] CHECK CONSTRAINT [FK_SpectetorGamesOfUser_GameRoom]
GO
ALTER TABLE [dbo].[SpectetorGamesOfUser]  WITH CHECK ADD  CONSTRAINT [FK_SpectetorGamesOfUser_User] FOREIGN KEY([userId])
REFERENCES [dbo].[UserTable] ([userId])
GO
ALTER TABLE [dbo].[SpectetorGamesOfUser] CHECK CONSTRAINT [FK_SpectetorGamesOfUser_User]
GO
ALTER TABLE [dbo].[SystemLog]  WITH CHECK ADD  CONSTRAINT [FK_SystemLog_GameRoom] FOREIGN KEY([roomId], [game Id])
REFERENCES [dbo].[GameRoom] ([RoomId], [GameId])
GO
ALTER TABLE [dbo].[SystemLog] CHECK CONSTRAINT [FK_SystemLog_GameRoom]
GO
ALTER TABLE [dbo].[SystemLog]  WITH CHECK ADD  CONSTRAINT [FK_SystemLog_Log] FOREIGN KEY([logId])
REFERENCES [dbo].[Log] ([LogId])
GO
ALTER TABLE [dbo].[SystemLog] CHECK CONSTRAINT [FK_SystemLog_Log]
GO
ALTER TABLE [dbo].[UserActiveGames]  WITH CHECK ADD  CONSTRAINT [FK_UserActiveGames_GameRoom] FOREIGN KEY([roomId], [Game Id])
REFERENCES [dbo].[GameRoom] ([RoomId], [GameId])
GO
ALTER TABLE [dbo].[UserActiveGames] CHECK CONSTRAINT [FK_UserActiveGames_GameRoom]
GO
ALTER TABLE [dbo].[UserActiveGames]  WITH CHECK ADD  CONSTRAINT [FK_UserActiveGames_User] FOREIGN KEY([userId])
REFERENCES [dbo].[UserTable] ([userId])
GO
ALTER TABLE [dbo].[UserActiveGames] CHECK CONSTRAINT [FK_UserActiveGames_User]
GO
