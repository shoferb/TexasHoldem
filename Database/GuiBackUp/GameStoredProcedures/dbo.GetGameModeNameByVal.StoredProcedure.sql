USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetGameModeNameByVal]    Script Date: 03/06/2017 15:24:02 ******/
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
