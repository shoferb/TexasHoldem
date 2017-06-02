﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TexasHoldem.Database.EntityFramework.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DataBaseSadnaEntitiesNewest : DbContext
    {
        public DataBaseSadnaEntitiesNewest()
            : base("name=DataBaseSadnaEntitiesNewest")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Deck> Decks { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<GameMode> GameModes { get; set; }
        public virtual DbSet<GameReplay> GameReplays { get; set; }
        public virtual DbSet<GameRoom> GameRooms { get; set; }
        public virtual DbSet<GameRoomPreferance> GameRoomPreferances { get; set; }
        public virtual DbSet<HandStep> HandSteps { get; set; }
        public virtual DbSet<LeagueName> LeagueNames { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PriorityLogEnum> PriorityLogEnums { get; set; }
        public virtual DbSet<SystemLog> SystemLogs { get; set; }
        public virtual DbSet<UserTable> UserTables { get; set; }
    
        public virtual int AddNewUser(Nullable<int> userId, string username, string name, string email, string password, string avatar, Nullable<int> points, Nullable<int> money, Nullable<int> gamesPlayed, Nullable<int> leagueName, Nullable<int> winNum, Nullable<int> highestCashGainInGame, Nullable<int> totalProfit, Nullable<bool> isActive)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var avatarParameter = avatar != null ?
                new ObjectParameter("avatar", avatar) :
                new ObjectParameter("avatar", typeof(string));
    
            var pointsParameter = points.HasValue ?
                new ObjectParameter("points", points) :
                new ObjectParameter("points", typeof(int));
    
            var moneyParameter = money.HasValue ?
                new ObjectParameter("money", money) :
                new ObjectParameter("money", typeof(int));
    
            var gamesPlayedParameter = gamesPlayed.HasValue ?
                new ObjectParameter("gamesPlayed", gamesPlayed) :
                new ObjectParameter("gamesPlayed", typeof(int));
    
            var leagueNameParameter = leagueName.HasValue ?
                new ObjectParameter("leagueName", leagueName) :
                new ObjectParameter("leagueName", typeof(int));
    
            var winNumParameter = winNum.HasValue ?
                new ObjectParameter("winNum", winNum) :
                new ObjectParameter("winNum", typeof(int));
    
            var highestCashGainInGameParameter = highestCashGainInGame.HasValue ?
                new ObjectParameter("highestCashGainInGame", highestCashGainInGame) :
                new ObjectParameter("highestCashGainInGame", typeof(int));
    
            var totalProfitParameter = totalProfit.HasValue ?
                new ObjectParameter("totalProfit", totalProfit) :
                new ObjectParameter("totalProfit", typeof(int));
    
            var isActiveParameter = isActive.HasValue ?
                new ObjectParameter("isActive", isActive) :
                new ObjectParameter("isActive", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddNewUser", userIdParameter, usernameParameter, nameParameter, emailParameter, passwordParameter, avatarParameter, pointsParameter, moneyParameter, gamesPlayedParameter, leagueNameParameter, winNumParameter, highestCashGainInGameParameter, totalProfitParameter, isActiveParameter);
        }
    
        public virtual int EditAvatar(Nullable<int> userId, string newAvatar)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var newAvatarParameter = newAvatar != null ?
                new ObjectParameter("newAvatar", newAvatar) :
                new ObjectParameter("newAvatar", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditAvatar", userIdParameter, newAvatarParameter);
        }
    
        public virtual int EditEmail(Nullable<int> userId, string newEmail)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var newEmailParameter = newEmail != null ?
                new ObjectParameter("newEmail", newEmail) :
                new ObjectParameter("newEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditEmail", userIdParameter, newEmailParameter);
        }
    
        public virtual int EditName(Nullable<int> userId, string newName)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var newNameParameter = newName != null ?
                new ObjectParameter("newName", newName) :
                new ObjectParameter("newName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditName", userIdParameter, newNameParameter);
        }
    
        public virtual int EditPassword(Nullable<int> userId, string newPassword)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var newPasswordParameter = newPassword != null ?
                new ObjectParameter("newPassword", newPassword) :
                new ObjectParameter("newPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditPassword", userIdParameter, newPasswordParameter);
        }
    
        public virtual int EditUserHighestCashGainInGame(Nullable<int> userId, Nullable<int> newHighestCashGainInGame)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var newHighestCashGainInGameParameter = newHighestCashGainInGame.HasValue ?
                new ObjectParameter("newHighestCashGainInGame", newHighestCashGainInGame) :
                new ObjectParameter("newHighestCashGainInGame", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditUserHighestCashGainInGame", userIdParameter, newHighestCashGainInGameParameter);
        }
    
        public virtual int EditUserId(Nullable<int> newUserId, Nullable<int> oldUserId)
        {
            var newUserIdParameter = newUserId.HasValue ?
                new ObjectParameter("NewUserId", newUserId) :
                new ObjectParameter("NewUserId", typeof(int));
    
            var oldUserIdParameter = oldUserId.HasValue ?
                new ObjectParameter("oldUserId", oldUserId) :
                new ObjectParameter("oldUserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditUserId", newUserIdParameter, oldUserIdParameter);
        }
    
        public virtual int EditUserIsActive(Nullable<int> userId, Nullable<bool> newIsActive)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var newIsActiveParameter = newIsActive.HasValue ?
                new ObjectParameter("newIsActive", newIsActive) :
                new ObjectParameter("newIsActive", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditUserIsActive", userIdParameter, newIsActiveParameter);
        }
    
        public virtual int EditUserLeagueName(Nullable<int> userId, Nullable<int> newLeague)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var newLeagueParameter = newLeague.HasValue ?
                new ObjectParameter("newLeague", newLeague) :
                new ObjectParameter("newLeague", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditUserLeagueName", userIdParameter, newLeagueParameter);
        }
    
        public virtual int EditUserMoney(Nullable<int> userId, Nullable<int> newMoney)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var newMoneyParameter = newMoney.HasValue ?
                new ObjectParameter("newMoney", newMoney) :
                new ObjectParameter("newMoney", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditUserMoney", userIdParameter, newMoneyParameter);
        }
    
        public virtual int EditUsername(Nullable<int> userId, string newUserName)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var newUserNameParameter = newUserName != null ?
                new ObjectParameter("newUserName", newUserName) :
                new ObjectParameter("newUserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditUsername", userIdParameter, newUserNameParameter);
        }
    
        public virtual int EditUserNumOfGamesPlayed(Nullable<int> userId, Nullable<int> newNumOfGame)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var newNumOfGameParameter = newNumOfGame.HasValue ?
                new ObjectParameter("newNumOfGame", newNumOfGame) :
                new ObjectParameter("newNumOfGame", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditUserNumOfGamesPlayed", userIdParameter, newNumOfGameParameter);
        }
    
        public virtual int EditUserPoints(Nullable<int> userId, Nullable<int> newPoints)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var newPointsParameter = newPoints.HasValue ?
                new ObjectParameter("newPoints", newPoints) :
                new ObjectParameter("newPoints", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditUserPoints", userIdParameter, newPointsParameter);
        }
    
        public virtual int EditUserTotalProfit(Nullable<int> userId, Nullable<int> newTotalProfit)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var newTotalProfitParameter = newTotalProfit.HasValue ?
                new ObjectParameter("newTotalProfit", newTotalProfit) :
                new ObjectParameter("newTotalProfit", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditUserTotalProfit", userIdParameter, newTotalProfitParameter);
        }
    
        public virtual int EditUserWinNum(Nullable<int> userId, Nullable<int> newWinNum)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var newWinNumParameter = newWinNum.HasValue ?
                new ObjectParameter("newWinNum", newWinNum) :
                new ObjectParameter("newWinNum", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditUserWinNum", userIdParameter, newWinNumParameter);
        }
    
        public virtual ObjectResult<GetAllUser_Result> GetAllUser()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllUser_Result>("GetAllUser");
        }
    
        public virtual ObjectResult<GetUserByUserId_Result> GetUserByUserId(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUserByUserId_Result>("GetUserByUserId", userIdParameter);
        }
    
        public virtual ObjectResult<GetUserByUserName_Result> GetUserByUserName(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUserByUserName_Result>("GetUserByUserName", usernameParameter);
        }
    }
}
