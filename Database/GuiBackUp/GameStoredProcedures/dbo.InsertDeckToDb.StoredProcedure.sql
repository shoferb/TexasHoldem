USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[InsertDeckToDb]    Script Date: 04/06/2017 13:05:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertDeckToDb] @cardVal int, @gameId int, @roomId int
AS
BEGIN 
      Insert into Deck values(@cardVal,	@roomId, @gameId,@cardVal)
END



GO
