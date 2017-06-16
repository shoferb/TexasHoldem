USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[DeleteGameRoom]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteGameRoom] @roomId int, @gameid int
AS
BEGIN 
     SET NOCOUNT ON 
DELETE 
FROM GameRoom
WHERE Roomid = @roomId AND GameId = @gameid
END




GO
/****** Object:  StoredProcedure [dbo].[DeleteGameRoomPref]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteGameRoomPref] @roomId int
AS
BEGIN 
     SET NOCOUNT ON 
DELETE 
FROM GameRoomPreferance
WHERE Roomid = @roomId 
END




GO
/****** Object:  StoredProcedure [dbo].[GetAllActiveGameRooms]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllActiveGameRooms]
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM GameRoom
WHERE isActive = 1
END



GO
/****** Object:  StoredProcedure [dbo].[GetAllGameRooms]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllGameRooms]
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM GameRoom

END





GO
/****** Object:  StoredProcedure [dbo].[GetAllSpectableGameRooms]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllSpectableGameRooms] @canSpec bit
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.CanSpectate = @canSpec
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameModeNameByVal]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameModeNameByVal] @Val int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT [game mode name]
FROM GameMode
WHERE [Game mode value] = @Val
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameModeValByName]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameModeValByName] @name varchar(10)
AS
BEGIN 
     SET NOCOUNT ON 
SELECT [Game mode value]
FROM GameMode
WHERE [game mode name] = @name
END





GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomById]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomById] @roomid int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM GameRoom
WHERE RoomId = @roomid
ORDER BY GameId DESC
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomPrefById]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomPrefById] @roomid int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM GameRoomPreferance
WHERE RoomId = @roomId 
END




GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomReplyById]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomReplyById] @id int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT RoomId, Replay
FROM GameRoom
WHERE RoomId = @id
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByBuyInPolicy]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomsByBuyInPolicy] @biPol int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.BuyInPolicy = @biPol
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByGameMode]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomsByGameMode] @mode int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.GameMode = @mode
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByMaxPlayers]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomsByMaxPlayers] @max int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.MaxPlayers = @max
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByMinBet]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomsByMinBet] @min int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.MinBet = @min
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByMinPlayers]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomsByMinPlayers] @min int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.MinPlayers = @min
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByPotSize]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomsByPotSize] @potSize int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.PotSize = @potSize
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByStaringChip]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomsByStaringChip] @scpol int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.EnterGamePolicy = @scpol
END



GO
/****** Object:  StoredProcedure [dbo].[GetLeageValByName]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetLeageValByName] @name varchar(10)
AS
BEGIN 
     SET NOCOUNT ON 
SELECT [League Value]
FROM LeagueName
WHERE [League Name] = @name
END




GO
/****** Object:  StoredProcedure [dbo].[InsertGameRoomToDb]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertGameRoomToDb] @roomid int, @gameid int, @replay varchar(MAX),
                        @gameXML XML, @isActive bit
AS
BEGIN 
      Insert into GameRoom values(@roomid, @gameid, @replay, @gameXML, @isActive)
END





GO
/****** Object:  StoredProcedure [dbo].[InsertPrefToDb]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertPrefToDb] @room_id int, @is_Spectetor bit, @Min_player_in_room int,
                        @max_player_in_room int, @BuyInPolicy int, @starting_chip int,
                       @minBet int, @League_name int, @Game_Mode int, @Game_Id int, @potSize int
AS
BEGIN 
      Insert into GameRoomPreferance values( @room_id , @is_Spectetor, @Min_player_in_room,
                        @max_player_in_room , @BuyInPolicy , @starting_chip ,
                        @minBet, @League_name , @Game_Mode , @Game_Id, @potSize )
END





GO
/****** Object:  StoredProcedure [dbo].[UpdateGameRoom]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateGameRoom] @roomId int, @gameid int, @newXML XML, @newIsActive bit, @newRep varchar(MAX)
AS
BEGIN 
     SET NOCOUNT ON 
UPDATE GameRoom
SET GameXML = @newXML, isActive = @newIsActive, Replay = @newRep
WHERE Roomid = @roomId AND GameId = @gameid
END




GO
/****** Object:  StoredProcedure [dbo].[UpdateGameRoomPotSize]    Script Date: 15/06/2017 20:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateGameRoomPotSize] @newPotSize int, @id int
AS
BEGIN 
     SET NOCOUNT ON 
UPDATE GameRoomPreferance
SET PotSize = @newPotSize
WHERE Roomid = @id
END




GO
