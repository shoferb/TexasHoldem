USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[InsertPlayerToDb]    Script Date: 04/06/2017 14:13:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertPlayerToDb] @roomId int, @user_Id int, @is_player_active bit,
                        @player_name varchar(50), @Total_chip int, @Round_chip_bet int, @Player_action_the_round bit,
                        @first_card int, @secund_card int, @Game_Id int
AS
BEGIN 
      Insert into Players values(@roomId, @user_Id , @is_player_active,
                        @player_name , @Total_chip , @Round_chip_bet , @Player_action_the_round,
                        @first_card, @secund_card , @Game_Id )
END




GO
