USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetLeageValByName]    Script Date: 04/06/2017 12:15:38 ******/
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
