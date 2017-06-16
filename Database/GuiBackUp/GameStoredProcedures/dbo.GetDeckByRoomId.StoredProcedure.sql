USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetDeckByRoomId]    Script Date: 03/06/2017 15:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetDeckByRoomId] @RoomId int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM Deck
WHERE [room Id] = @RoomId
END


GO
