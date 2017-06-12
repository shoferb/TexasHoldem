USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetHandStepValByName]    Script Date: 04/06/2017 12:32:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetHandStepValByName] @name varchar(10)
AS
BEGIN 
     SET NOCOUNT ON 
SELECT [hand Step value]
FROM HandStep
WHERE [hand Step name] = @name
END



GO
