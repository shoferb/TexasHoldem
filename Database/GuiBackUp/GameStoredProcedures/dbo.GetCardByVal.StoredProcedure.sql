USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetCardByVal]    Script Date: 03/06/2017 15:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCardByVal] @Val int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM Card
WHERE [Card Value] = @Val
END


GO
