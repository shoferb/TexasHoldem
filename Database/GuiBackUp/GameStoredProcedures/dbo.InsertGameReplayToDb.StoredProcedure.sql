USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[InsertGameReplayToDb]    Script Date: 04/06/2017 13:20:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertGameReplayToDb] @repStr varchar(5000), @gameId int, @roomId int
AS
BEGIN 
      Insert into GameReplay values(@roomId, @gameId, @gameId, @repStr)
END



GO
