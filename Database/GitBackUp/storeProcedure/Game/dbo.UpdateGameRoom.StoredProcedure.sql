USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[UpdateGameRoom]    Script Date: 14/06/2017 15:33:01 ******/
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
