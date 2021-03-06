﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using TexasHoldem.Logic.Game;
using TexasHoldem.Logic.GameControl;
using TexasHoldem.Logic.Users;
using TexasHoldemShared.CommMessages;
using TexasHoldemTests.AcptTests.Bridges;
using Moq;

namespace TexasHoldemTests.AcptTests.tests
{
    [TestFixture]
    public class UserAcptTests : AcptTest
    {
        private int _userId2 = -1;
        private string _user2Name;
        private string _user2Pw;
        private string _user2EmailGood;
        private int _userId3 = -1;
        private string _user3Name;
        private string _user3Pw;
        private string _user3EmailGood;
        private int _userId4 = -1;
        private string _user4Name;
        private string _user4Pw;
        private string _user4EmailGood;
        private int _userId5 = -1;
        private string _user5Name;
        private string _user5Pw;
        private string _user5EmailGood;
        private int _userId6 = -1;
        private string _user6Name;
        private string _user6Pw;
        private string _user6EmailGood;
        private string _userPwBad;
        private string _userEmailBad;

        //setup: (called from case)
        protected override void SubClassInit()
        {
            _userEmailBad = "אבי חיון איז היר";
            _userPwBad = "-~~~~~~~~~~~~~~~~~~~~~~~~~~~~`";
            _userId2 = new Random().Next() + 9292;
            _user2Name = "yarden";
            _user2EmailGood = "yarden@gmail.com";
            _user2Pw = "123456789";

            _userId3 = new Random().Next() + 91;
            _user3Name = "Aviv";
            _user3EmailGood = "Aviv@gmail.com";
            _user3Pw = "123456789";

            _userId4 = new Random().Next() + 234;
            _user4Name = "Yarden2";
            _user4EmailGood = "YRD@gmail.com";
            _user4Pw = "123456789";

            _userId5 = 305509069;
            _user5Name = "Orellie";
            _user5EmailGood = "o@gmail.com";
            _user5Pw = "12345555";

            _userId6 = 305509000;
            _user6Name = "bar";
            _user6EmailGood = "b@gmail.com";
            _user6Pw = "9191919191";


            //RegisterUser(_userId2, _user2Name, _user2Pw, _user2EmailGood);
            //RegisterUser(_userId3, _user3Name, _user3Pw, _user3EmailGood);
            //RegisterUser(_userId4, _user4Name, _user4Pw, _user4EmailGood);


        }


        //tear down: (called from case)
        protected override void SubClassDispose()
        {

            //if (DeleteUser(_userId2))
            //    _userId2 = -1;
            //if (DeleteUser(_userId3))
            //    _userId3 = -1;
            //if (DeleteUser(_userId4))
            //    _userId4 = -1;

            //Assert.True(_userId2 == -1);
            //Assert.True(_userId3 == -1);
            //Assert.True(_userId4 == -1);
        }

        protected void RegisterUser(int userId, string name, string pass, string mail)
        {
            if (!UserBridge.IsThereUser(userId))
            {
                UserBridge.RegisterUser(userId, name, pass, mail);

                Users.Add(userId);
            }
            else if (!UserBridge.IsUserLoggedIn(userId))
            {
                UserBridge.LoginUser(name, pass);
            }
        }

        [TestCase]
        public void UserLoginTestGood()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            RegisterUser1();
            Assert.True(UserBridge.LoginUser(User1Name,User1Pw));
            UserBridge.DeleteUser(UserId);
        }

        [TestCase]
        public void UserLoginTestSad_bad_password()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "11";
            UserEmailGood1 = "gooduser1@gmail.com";
         
            Assert.True(UserBridge.RegisterUser(User1Name, User1Pw, "w2@.com") == -1);
        }

        [TestCase]
        public void UserLoginTestBad()
        {
            Assert.False(UserBridge.LoginUser("", ""));
            Assert.False(UserBridge.LoginUser("אני שם בעברית", User1Pw));
        }

        //logout
        [TestCase]
        public void UserLogoutTestGood()
        {
            int id = SetupUser1();
            Assert.True(UserBridge.LogoutUser(id));
            UserBridge.DeleteUser(id);
        }

        [TestCase]
        public void UserLogoutTestSad()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
           
            Assert.False(UserBridge.LogoutUser(UserId));
        }

        [TestCase]
        public void UserLogoutTestBad()
        {
            Assert.False(UserBridge.LogoutUser(-1));
        }

        //register
        [TestCase]
        public void UserRegisterTestGood()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            Assert.True(UserBridge.RegisterUser(User1Name, User1Pw, UserEmailGood1) != -1);
            UserBridge.DeleteUser(User1Name, User1Pw);
        }

        [TestCase]
        public void UserRegisterTest_Sad_username_taken()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";

            //user name already exists in system 
            UserBridge.RegisterUser(User1Name, User1Pw, UserEmailGood1);
            Assert.True(UserBridge.RegisterUser(User1Name, User1Pw, UserEmailGood1) == -1);
            UserBridge.DeleteUser(User1Name, User1Pw);

        }

        [TestCase]
        public void UserRegisterTest_Sad_invalid_email_hebrew()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            Assert.True(UserBridge.RegisterUser(User1Name, User1Pw, "היי") == -1);
  
        }

        [TestCase]
        public void UserRegisterTest_Sad_invalid_email_number()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            Assert.True(UserBridge.RegisterUser(User1Name, User1Pw, "-1") == -1);
        }


        [TestCase]
        public void UserRegisterTest_Bad_PwBad()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            _userPwBad = "5";
           // RegisterUser1();
            Assert.True(UserBridge.RegisterUser(User1Name, _userPwBad, UserEmailGood1) == -1);
            UserBridge.DeleteUser(User1Name, _userPwBad);

           
        }

        [TestCase]
        public void UserRegisterTest_bad_Empty_userName()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            _userPwBad = "5";

            Assert.True(UserBridge.RegisterUser("", User1Pw, UserEmailGood1) == -1);
           
        }
        [TestCase]
        public void UserRegisterTest_bad_Empty_password()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            _userPwBad = "5";

         
            Assert.True(UserBridge.RegisterUser(User1Name, "", UserEmailGood1) == -1);
            
        }

        [TestCase]
        public void UserRegisterTest_bad_Empty_Email()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            _userPwBad = "5";
            Assert.True(UserBridge.RegisterUser(User1Name, User1Pw, "") == -1);
        }

        [TestCase]
        public void UserRegisterTest_bad_Nulls_username()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            Assert.True(UserBridge.RegisterUser(null, User1Pw, UserEmailGood1) == -1);
         
        }

        [TestCase]
        public void UserRegisterTest_bad_Nulls_password()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            Assert.True(UserBridge.RegisterUser(User1Name, null, UserEmailGood1) == -1);
            
        }

        [TestCase]
        public void UserRegisterTest_bad_Nulls_email()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            
            Assert.True(UserBridge.RegisterUser(User1Name, User1Pw, null) == -1);
        }
        //edit
        [TestCase]
        public void UserEditNameTest_Good()
        {
            UserId = SetupUser1();

            UserBridge.EditName(UserId, "newName"+UserId);
            Assert.AreEqual(UserBridge.GetUserName(UserId), "newName"+UserId);
            Assert.IsTrue(UserBridge.DeleteUser(UserId));
        }

        [TestCase]
        public void UserEditNameTest_Sad_samename()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            RegisterUser1();
            Assert.False(UserBridge.EditName(UserId, User1Name));
            Assert.AreEqual(UserBridge.GetUserName(UserId), User1Name);
            UserBridge.DeleteUser(UserId);
        }
        [TestCase]
        public void UserEditNameTest_Bad_empty()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            RegisterUser1();

            Assert.False(UserBridge.EditName(UserId, ""));
            Assert.AreEqual(UserBridge.GetUserName(UserId), User1Name);
            UserBridge.DeleteUser(UserId);
        }
        [TestCase]
        public void UserEditNameTest_Bad_invalidId()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            RegisterUser1();

            Assert.False(UserBridge.EditName(-1, User1Name));
            Assert.AreEqual(UserBridge.GetUserName(UserId), User1Name);
            UserBridge.DeleteUser(UserId);
        }

        [TestCase]
        public void UserEditPwTest_Good()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            RegisterUser1();

            Assert.True(UserBridge.EditPw(UserId, _user2Pw));
            Assert.AreEqual(UserBridge.GetUserPw(UserId), _user2Pw);
            UserBridge.DeleteUser(UserId);
        }

        [TestCase]
        public void UserEditPwTest_Sad()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            RegisterUser1();
            _userPwBad = "bad";
            Assert.False(UserBridge.EditPw(UserId, _userPwBad));
            Assert.AreEqual(UserBridge.GetUserPw(UserId), User1Pw);
            UserBridge.DeleteUser(UserId);
        }

        [TestCase]
        public void UserEditPwTest_Bad_invalidId()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            _user2Pw = "newPassword";
            RegisterUser1();
            Assert.False(UserBridge.EditPw(-1, _user2Pw));
            Assert.AreEqual(UserBridge.GetUserPw(UserId), User1Pw);
            UserBridge.DeleteUser(UserId);
        }

        [TestCase]
        public void UserEditPwTest_Bad_invalid_passwords()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            _userPwBad = "bad";
            RegisterUser1();
            Assert.False(UserBridge.EditPw(UserId, _userPwBad));
            Assert.AreEqual(UserBridge.GetUserPw(UserId), User1Pw);
            UserBridge.DeleteUser(UserId);
        }
        [TestCase]
        public void UserEditEmailTestGood()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            RegisterUser1();
            Assert.True(UserBridge.EditEmail(UserId, _user2EmailGood));
            Assert.AreEqual(UserBridge.GetUserEmail(UserId), _user2EmailGood);
            UserBridge.DeleteUser(UserId);

        }

        [TestCase]
        public void UserEditEmailTestBad_logout()
        {
            Assert.False(UserBridge.EditEmail(5658558, UserEmailGood1));
        }

        [TestCase]
        public void UserEditEmailTestBad()
        {

            UserId = new Random().Next();
            User1Name = "Oded" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            RegisterUser1();
            Assert.False(UserBridge.EditEmail(UserId, _userEmailBad));
            Assert.AreEqual(UserBridge.GetUserEmail(UserId), UserEmailGood1);

            Assert.False(UserBridge.EditEmail(UserId, _userEmailBad));
            Assert.AreEqual(UserBridge.GetUserEmail(UserId), UserEmailGood1);
            UserBridge.DeleteUser(UserId);
        }

        [TestCase]
        public void UserEditAvatarTestGood()
        {
            int id = SetupUser1();

            Assert.True(UserBridge.EditAvatar(id, "yarden"));
            Assert.AreEqual(UserBridge.GetUserAvatar(id), "yarden");
            UserBridge.DeleteUser(id);
        }


        [TestCase]
        public void UserAddUserMoneyTestGood()
        {
            UserId = SetupUser1();

            const int amountToChange = 100;
            int prevAmount = UserBridge.GetUserMoney(UserId);
            Assert.True(UserBridge.AddUserMoney(UserId, amountToChange));
            Assert.True(prevAmount == UserBridge.GetUserMoney(UserId) - amountToChange);
            UserBridge.DeleteUser(UserId);
        }

        [TestCase]
        public void UserAddUserMoneyTestBad()
        {
            ////RestartSystem();
            UserId = SetupUser1();
            const int amountToChange = -10000;
            int prevAmount = UserBridge.GetUserMoney(UserId);
            Assert.False(UserBridge.AddUserMoney(UserId, amountToChange));
            Assert.True(prevAmount == UserBridge.GetUserMoney(UserId));
            UserBridge.DeleteUser(UserId);
        }

        //add player to room
        [TestCase]
        public void UserAddToRoomAsPlayerAllMoneyTestGood()
        {
            int roomId = CreateGameWith3Users();
            Assert.True(GameBridge.IsUserInRoom(UserId, roomId));
            Assert.Contains(roomId, UserBridge.GetUsersGameRooms(UserId));
            CleanUp(roomId);
        }

        [TestCase]
        public void UserAddToRoomAsPlayerNoMoneyTestGood()
        {
            UserId = SetupUser1();
            IUser user1 = UserBridge.getUserById(UserId);
            user1.AddMoney(100000000);
            int roomId = new Random().Next();
            CreateGame(roomId, UserId, 100, true, GameMode.NoLimit, 2, 8, 0, 10);
            _userId2 = new Random().Next();
            RegisterUser(_userId2, _user2Name + _userId2, _user2Pw, _user2EmailGood);
            IUser user2 = UserBridge.getUserById(_userId2);
            int money = user2.Money();
            user2.ReduceMoneyIfPossible(money); //so now he is broke
            Assert.IsFalse(UserBridge.AddUserToGameRoomAsPlayer(_userId2, roomId, 10));
            CleanUp(roomId);
            UserBridge.DeleteUser(_userId2);    
        }

        [TestCase]
        public void UserAddToRoomAsPlayerNegUserTestBad()
        {
            UserId = SetupUser1();
            int roomId = new Random().Next();
            CreateGame(roomId, UserId, 100, true, GameMode.NoLimit, 2, 8, 0, 10);
            Assert.IsFalse(UserBridge.AddUserToGameRoomAsPlayer(-1, roomId, 10));
            CleanUp(roomId);
        }

        [TestCase]
        public void UserAddToRoomAsPlayerAllreadySpectatorInRoomTestBad()
        {
            UserId = SetupUser1();
            IUser user1 = UserBridge.getUserById(UserId);
            user1.AddMoney(100000000);
            int roomId = new Random().Next();
            CreateGame(roomId, UserId, 100, true, GameMode.NoLimit, 2, 8, 0, 10);
            Assert.IsFalse(UserBridge.AddUserToGameRoomAsSpectator(UserId, roomId));
            CleanUp(roomId);
        }

        //add spectator to room
        [TestCase]
        public void UserAddToRoomAsSpectatorGood()
        {
            int roomId = CreateGameWith3Users();
            int id = new Random().Next();
            RegisterUser(id, _user2Name + id, _user2Pw, _user2EmailGood);
            Assert.IsFalse(UserBridge.AddUserToGameRoomAsSpectator(id, roomId));
            CleanUp(roomId);
            UserBridge.DeleteUser(id);    
        }

        [TestCase]
        public void UserAddToRoomAsSpectatorNonExsistantRoomTestSad()
        {
            int id = new Random().Next();
            RegisterUser(id, _user2Name + id, _user2Pw, _user2EmailGood);
            int nonExsistanRoom = new Random().Next();
            Assert.IsFalse(UserBridge.AddUserToGameRoomAsSpectator(id, nonExsistanRoom));
            CleanUp(nonExsistanRoom);
            UserBridge.DeleteUser(id);
        }

        [TestCase]
        public void UserAddToRoomAsSpectatorAllreadyPlayerInRoomTestBad()
        {
            UserId = SetupUser1();
            int roomId = new Random().Next();
            CreateGame(roomId, UserId, 100, true, GameMode.NoLimit, 2, 8, 0, 10);
            Assert.False(UserBridge.AddUserToGameRoomAsSpectator(UserId, roomId));
            CleanUp(roomId);
        }

        [TestCase]
        public void UserRemoveFromRoomSpectatorTestGood()
        {
            UserId = SetupUser1();
            int roomId = new Random().Next();
            CreateGame(roomId, UserId, 100, true, GameMode.NoLimit, 2, 8, 0, 10);
            Assert.IsTrue(UserBridge.RemoveUserFromRoom(UserId, roomId));
            CleanUp(roomId);
            UserBridge.DeleteUser(UserId);
        }

        [TestCase]
        public void FoldTestBad()
        {
            //RestartSystem();
            int roomId = CreateGameWith3Users();
            GameBridge.DoAction(_userId2, CommunicationMessage.ActionType.Fold, -1, roomId);
            Assert.True(GameBridge.GetPlayersInRoom(roomId).Exists(p => p.user.Id() == _userId2));
            CleanUp(roomId);
        }


        [TestCase]
        public void FoldTestGood()
        {
            int roomId = CreateGameWith3Users();
            GameBridge.DoAction(UserId, CommunicationMessage.ActionType.Fold, 0, roomId);
            var players = GameBridge.GetPlayersInRoom(roomId);
            Assert.AreEqual(3, players.Count);
            Assert.True(players.Exists(p =>
            {
                var user = p.user;
                return user.Id() == UserId && !p.isPlayerActive;
            }));
            Assert.True(players.Exists(p =>
            {
                var user = p.user;
                return user.Id() == _userId2 && p.isPlayerActive;
            }));
            Assert.True(players.Exists(p =>
            {
                var user = p.user;
                return user.Id() == _userId3 && p.isPlayerActive;
            }));
            CleanUp(roomId);
        }

        [TestCase]
        public void CheckTestSad()
        {
            var roomId = CreateGameWith3Users();
            UserBridge.ReduceUserMoney(UserId, 100000000 - 1); //so user does not have enough money
            //cant raise not eungh money       
            Assert.False(GameBridge.DoAction(UserId, CommunicationMessage.ActionType.Bet, 0, roomId));
            CleanUp(roomId);
        }

        [TestCase]
        public void CheckTestBad()
        {
            //RestartSystem();
            var roomId = CreateGameWith3Users();
            //cant raise not his turn       
            Assert.False(GameBridge.DoAction(_userId2, CommunicationMessage.ActionType.Bet, 0, roomId));
            CleanUp(roomId);
        }

        [TestCase]
        public void CheckTestGood()
        {
            //RestartSystem();
            var roomId = CreateGameWith3Users();
            Assert.True(GameBridge.DoAction(UserId, CommunicationMessage.ActionType.Bet, 100, roomId));
            CleanUp(roomId);
        }

        [TestCase]
        public void RaiseTestGood()
        {
            int roomId = CreateGameWith3Users();
            Assert.True(GameBridge.DoAction(UserId, CommunicationMessage.ActionType.Bet, 100, roomId));
            Assert.True(GameBridge.DoAction(_userId2, CommunicationMessage.ActionType.Bet, 1000, roomId));
            CleanUp(roomId);

        }

        [TestCase]
        public void RaiseTestSad()
        {
            int roomId = CreateGameWith3Users();
            //bet is too small
            Assert.False(GameBridge.DoAction(UserId, CommunicationMessage.ActionType.Bet, 9, roomId));
            CleanUp(roomId);
        }

        [TestCase]
        public void RaiseTestBad()
        {
            int roomId = CreateGameWith3Users();
            Assert.False(GameBridge.DoAction(_userId2, CommunicationMessage.ActionType.Bet, -100, roomId));
            CleanUp(roomId);
        }

        [TestCase]
        public void CallTestGood()
        {
            int roomId = CreateGameWith3Users();
            Assert.True(GameBridge.DoAction(UserId, CommunicationMessage.ActionType.Bet, 10, roomId));
            CleanUp(roomId);
        }

        [TestCase]
        public void CallTestSad()
        {
            //RestartSystem();
            var roomId = CreateGameWith3Users();
            var money = UserBridge.GetUserMoney(UserId);
            Assert.False(GameBridge.DoAction(UserId, CommunicationMessage.ActionType.Bet, money + 1, roomId));
            CleanUp(roomId);
        }

        [TestCase]
        public void CallTestBad()
        {
            //RestartSystem();
            var roomId = CreateGameWith3Users();
            Assert.False(GameBridge.DoAction(_userId2, CommunicationMessage.ActionType.Bet, -10, roomId)); //neg amount
            CleanUp(roomId);
        }

        private int CreateGameWith3Users()
        {
            UserId = SetupUser1();
            IUser user1 = UserBridge.getUserById(UserId);
            user1.AddMoney(100000000);
            int roomId = new Random().Next();
            CreateGame(roomId, UserId, 100, true, GameMode.NoLimit, 2, 8, 0, 10);
            _userId2 = new Random().Next();
            RegisterUser(_userId2, _user2Name + _userId2, _user2Pw, _user2EmailGood);
            IUser user2 = UserBridge.getUserById(_userId2);
            user2.AddMoney(1000);
            Assert.True(UserBridge.AddUserToGameRoomAsPlayer(_userId2, roomId, user2.Money()));
            _userId3 = new Random().Next();
            RegisterUser(_userId3, _user3Name + _userId3, _user3Pw, _user3EmailGood);
            IUser user3 = UserBridge.getUserById(_userId3);
            user3.AddMoney(1000);
            Assert.True(UserBridge.AddUserToGameRoomAsPlayer(_userId3, roomId, user3.Money()));
            GameBridge.StartGame(UserId, roomId);
            return roomId;
        }

        [TestCase]
        public void UknownUserTestGood()
        {
            UserId = SetupUser1();
            IUser user1 = UserBridge.getUserById(UserId);
            Assert.True(user1.IsUnKnow());
            UserBridge.DeleteUser(UserId);
        }

        [TestCase]
        public void RedistributesThePlayersAmongTheLeaguesGood()
        {
            
            int[] ids = new int[6];
            Random rand = new Random();
            for (int i = 0; i < 6; i++)
            {
                ids[i] = rand.Next();
                RegisterUser(ids[i], _user2Name + ids[i], _user2Pw, _user2EmailGood);
            }

            var users = new List<IUser>();

            for (int i = 0; i < 6; i++)
            {
                IUser user = UserBridge.getUserById(ids[i]);
                user.SetNumGamesPlayed(11);
                users.Add(user);
            }

            users[1].EditUserPoints(100);
            users[0].EditUserPoints(10000);
            UserBridge.DividLeage();

            Assert.True(users[0].GetLeague() == LeagueName.E);
            Assert.True(users[1].GetLeague() == LeagueName.E);
            for (int i = 0; i < 6; i++)
            {
                UserBridge.DeleteUser(ids[i]);
            }
        }

        [TestCase]
        public void LeaderBoardByNumOfGamesTestGood()
        {
            int[] userIds = CreateUsersWithNumOfGames(25);

            List<IUser> users = UserBridge.GetUsersByNumOfGames();
            Assert.AreEqual(20, users.Count);
            for (int i = 0; i < 20; i++)
            {
                int currId = users[i].Id();
                Assert.AreEqual(currId, userIds[i]);
            }
            for (int i = 0; i < userIds.Length; i++)
            {
                UserBridge.DeleteUser(userIds[i]);
            }
        }

        [TestCase]
        public void LeaderBoardByNumOfGamesTestSad()
        {
            int[] userIds = CreateUsersWithNumOfGames(1); //only 1 user

            List<IUser> users = UserBridge.GetUsersByNumOfGames();
            int minLen = Math.Min(users.Count, userIds.Length);
            for (int i = 0; i < minLen; i++)
            {
                int currId = users[i].Id();
                Assert.AreEqual(currId, userIds[i]);
            }
            for (int i = 0; i < userIds.Length; i++)
            {
                UserBridge.DeleteUser(userIds[i]);
            }
        }

        [TestCase]
        public void LeaderBoardByHighestCashTestGood()
        {
            var userIds = CreateUsersWithCashGain(25);

            List<IUser> users = UserBridge.GetUsersByHighestCash();
            Assert.AreEqual(20, users.Count);
            for (int i = 0; i < 20; i++)
            {
                int currId = users[i].Id();
                Assert.IsTrue(currId == userIds[i]);
            }
            for (int i = 0; i < userIds.Length; i++)
            {
                UserBridge.DeleteUser(userIds[i]);
            }
        }

        private int[] CreateUsersWithCashGain(int numOfUsers)
        {
            int[] userIds = new int[numOfUsers];
            int maxInt = Int32.MaxValue;
            Random rand = new Random();

            //setup users
            for (int i = 0; i < userIds.Length; i++)
            {
                int id = rand.Next();
                userIds[i] = id;
                RegisterUserToDB(id);
                int currMoney = UserBridge.GetUserMoney(id);
                UserBridge.ChangeUserHighestCashGain(id, maxInt - currMoney);
                maxInt--;
            }
            return userIds;
        }

        private int[] CreateUsersWithNumOfGames(int numOfUsers)
        {
            int[] userIds = new int[numOfUsers];
            int maxInt = Int32.MaxValue;
            Random rand = new Random();

            //setup users
            for (int i = 0; i < userIds.Length; i++)
            {
                int id = rand.Next();
                userIds[i] = id;
                RegisterUserToDB(id);
                UserBridge.ChangeUserNumOfGames(id, maxInt);
                maxInt--;
            }
            return userIds;
        }

        private int[] CreateUsersWithTotalProfit(int numOfUsers)
        {
            int[] userIds = new int[numOfUsers];
            int maxInt = Int32.MaxValue;
            Random rand = new Random();

            //setup users
            for (int i = 0; i < userIds.Length; i++)
            {
                int id = rand.Next();
                userIds[i] = id;
                RegisterUserToDB(id);
                UserBridge.ChangeUserTotalProfit(id, maxInt);
                maxInt--;
            }
            return userIds;
        }

        [TestCase]
        public void LeaderBoardByHighestCashTestSad()
        {
            int[] userIds = CreateUsersWithCashGain(1); //only 1 user

            List<IUser> users = UserBridge.GetUsersByHighestCash();
            Assert.GreaterOrEqual(users.Count, 1);
            Assert.IsTrue(users[0].Id() == userIds[0]);
            foreach (int t in userIds)
            {
                UserBridge.DeleteUser(t);
            }
        }

        [TestCase]
        public void LeaderBoardByTotalProfitTestSad()
        {
            int[] userIds = CreateUsersWithTotalProfit(1); //only 1 user

            List<IUser> users = UserBridge.GetUsersByTotalProfit();
            int minLen = Math.Min(users.Count, userIds.Length);
            for (int i = 0; i < minLen; i++)
            {
                int currId = users[i].Id();
                Assert.AreEqual(currId, userIds[i]);
            }
            for (int i = 0; i < userIds.Length; i++)
            {
                UserBridge.DeleteUser(userIds[i]);
            }
        }

        [TestCase]
        public void LeaderBoardByTotalProfitTestGood()
        {
            int[] userIds = CreateUsersWithTotalProfit(25);

            List<IUser> users = UserBridge.GetUsersByTotalProfit();
            Assert.AreEqual(20, users.Count);
            for (int i = 0; i < users.Count; i++)
            {
                int currId = users[i].Id();
                Assert.AreEqual(currId, userIds[i]);
            }
            for (int i = 0; i < userIds.Length; i++)
            {
                UserBridge.DeleteUser(userIds[i]);
            }
        }

        [TestCase]
        public void AverageCashTestGood()
        {
            UserId = SetupUser1();
            UserBridge.ChangeUserTotalProfit(UserId, 1000);
            UserBridge.ChangeUserNumOfGames(UserId, 10);
            Assert.AreEqual(100, UserBridge.GetUserAvgCashGain(UserId));
            UserBridge.DeleteUser(UserId);
        }

        [TestCase]
        public void AverageCashTest_Bad_noGmesPlayed()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
           
            RegisterUser1();
            IUser user1 = UserBridge.getUserById(UserId);
         //   Assert.AreNotEqual(user1,null);
            Assert.IsTrue(user1.GetAvgCashGainPerGame() == 0.0);
            UserBridge.DeleteUser(UserId);
        }

        [TestCase]
        public void AverageGrossTest_Good()
        {
            UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
            RegisterUser1();
            IUser user1 = UserBridge.getUserById(UserId);
            IncWinAndPoints(user1, 100, 1100, 1);
            Assert.IsTrue(user1.GetAvgProfit() == 100.0);
            UserBridge.DeleteUser(UserId);
        }

        [TestCase]
        public void AverageGrossTestBad()
        {
             UserId = new Random().Next();
            User1Name = "orelie" + UserId;
            User1Pw = "goodPw1234";
            UserEmailGood1 = "gooduser1@gmail.com";
           
            RegisterUser1();
            IUser user1 = UserBridge.getUserById(UserId);
            IUser user2 = UserBridge.getUserById(_userId2);
            Assert.IsTrue(user1.GetAvgProfit() == 0);
            UserBridge.DeleteUser(UserId);
        }

        private void IncWinAndPoints(IUser user, int amount, int points, int numOfWins)
        {
            for (int i = 0; i < numOfWins; i++)
            {
                user.IncWinNum();
            }
            user.UpdateHighestCashInGame(amount);
            user.UpdateTotalProfit(amount);
            user.EditUserPoints(points);
        }

        private Player GetInGamePlayerFromUser(IUser user, int roomId)
        {

            foreach (Player player in GameBridge.GetPlayersInRoom(roomId))
            {
                if (player.user.Id() == user.Id())
                {
                    return player;
                }
            }
            return null;
        }
    }
}
