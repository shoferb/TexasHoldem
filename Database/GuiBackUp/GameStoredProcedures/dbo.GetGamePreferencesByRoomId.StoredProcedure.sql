USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetGamePreferencesByRoomId]    Script Date: 03/06/2017 15:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGamePreferencesByRoomId] @RoomId int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM GameRoomPreferance
WHERE [room id] = @RoomId
END


GO
