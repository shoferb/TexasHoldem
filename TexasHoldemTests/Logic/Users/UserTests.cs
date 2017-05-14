﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using TexasHoldem.Logic.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Notifications_And_Logs;
using TexasHoldem.Logic.Game;
using TexasHoldem.Logic.GameControl;

namespace TexasHoldem.Logic.Users.Tests
{
    [TestClass()]
    public class UserTests
    {

        private IUser orelie = new User(305077901, "orelie", "orelie26", "123456", 0, 500, "orelie@post.bgu.ac.il");
        private Notification toSend1 = new Notification(11, "joind");
        private Notification toSend2 = new Notification(11, "Exit");

     


        [TestMethod()]
        public void SendNotificationTest()
        {
            Assert.IsTrue(orelie.SendNotification(toSend1));


        }

        [TestMethod()]
        public void AddNotificationToListTest()
        {
            Assert.IsTrue(orelie.AddNotificationToList(toSend1));

        }


   

        [TestMethod()]
        public void IsUnKnowTestGood_on_Create()
        {
            IUser user =  new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsTrue(user.IsUnKnow());
        }


        [TestMethod()]
        public void IsUnKnowTestGood_on_at_10_Games()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            for (int i = 0; i < 10; i++)
            {
                user.IncGamesPlay();
            }
            Assert.IsTrue(user.IsUnKnow());
        }


        [TestMethod()]
        public void IsUnKnowTestGood_More_Than_10()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            for (int i = 0; i < 11; i++)
            {
                user.IncGamesPlay();
            }
            Assert.IsFalse(user.IsUnKnow());
        }

       /* [TestMethod()]
        public void IncGamesPlayTest()
        {
            Assert.Fail();
        }*/

      

        [TestMethod()]
        public void IdTest_good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreEqual(user.Id(), 305077901);
        }

        [TestMethod()]
        public void IdTest_bad()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreNotEqual(user.Id(), 305077902);
        }

        [TestMethod()]
        public void NameTest_good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreEqual(user.Name(), "orelie");
        }


        [TestMethod()]
        public void NameTest_Bad()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreNotEqual(user.Name(), " ");
        }

        [TestMethod()]
        public void MemberNameTest_good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreEqual(user.MemberName(), "orelie26");
        }


        [TestMethod()]
        public void MemberNameTest_Bad()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreNotEqual(user.MemberName(), "orelie18");
        }


        [TestMethod()]
        public void PasswordTest_Good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreEqual(user.Password(), "123456789");
        }


        [TestMethod()]
        public void PasswordTest_Bad()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreNotEqual(user.Password(), "12");
        }


        [TestMethod()]
        public void AvatarTest_Good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreEqual(user.Avatar(), "/GuiScreen/Photos/Avatar/devil.png");
            
        }


        [TestMethod()]
        public void AvatarTest_Bad()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreNotEqual(user.Avatar(), " ");

        }

        [TestMethod()]
        public void PointsTest_Good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreEqual(user.Points(), 0);
        }

        [TestMethod()]
        public void MoneyTest_Good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreEqual(user.Money(), 500); 
        }

        [TestMethod()]
        public void WaitListNotificationTest_Good_inc_size_onCreate()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Notification toSend1 = new Notification(11, "joind");
            Assert.AreEqual(user.WaitListNotification().Count, 0);
         
        }

        [TestMethod()]
        public void WaitListNotificationTest_Good_Added_notifiction()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Notification toSend1 = new Notification(11, "joind");
            user.SendNotification(toSend1);
            Assert.IsTrue(user.WaitListNotification().Contains(toSend1));

        }

        [TestMethod()]
        public void EmailTest_Good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreEqual(user.Email(), "orelie@post.bgu.ac.il");
        }

        [TestMethod()]
        public void EmailTest_Bad()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreNotEqual(user.Email(), " ");
        }
      /*  [TestMethod()]
        public void GamesAvailableToReplayTest()
        {
            Assert.Fail();
        }*/

        [TestMethod()]
        public void ActiveGameListTest_good_on_create()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreEqual(user.ActiveGameList().Count,0);
        }

      

        [TestMethod()]
        public void SpectateGameListTest_good_on_create()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreEqual(user.SpectateGameList().Count, 0);
        }

        [TestMethod()]
        public void WinNumTest_good_on_create()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreEqual(user.WinNum(), 0);
        }

        [TestMethod()]
        public void WinNumTest_Bad_on_create()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreNotEqual(user.WinNum(), 10);
        }

        [TestMethod()]
        public void IncWinNumTest_good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            user.IncWinNum();
            Assert.AreEqual(user.WinNum(), 1);
        }

        [TestMethod()]
        public void LoginTest_Not_Active_on_Create()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.IsLogin());
        }

        [TestMethod()]
        public void LoginTest_good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsTrue(user.Login());
        }

        [TestMethod()]
        public void LogoutTest_good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            user.Login();
            
            Assert.IsTrue(user.Logout());
        }

        [TestMethod()]
        public void EditIdTest_Good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsTrue(user.EditId(305077902));
        }

        [TestMethod()]
        public void EditIdTest_Bad_Smaller_than_Zero()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.EditId(-2));
        }



        [TestMethod()]
        public void EditEmailTest_Good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsTrue(user.EditEmail("orelie@walla.co.il"));
        }

        [TestMethod()]
        public void EditEmailTest_Bad_IValid_address()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.EditEmail("orelie.walla.co.il"));
        }

        [TestMethod()]
        public void EditEmailTest_Bad_Empty()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.EditEmail(""));
        }

        [TestMethod()]
        public void EditPasswordTest_Good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsTrue(user.EditPassword("12345698"));
        }


        [TestMethod()]
        public void EditPasswordTest_Bad_Small()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.EditPassword("12"));
        }

        [TestMethod()]
        public void EditPasswordTest_Bad_big()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.EditPassword("121212121212121212112"));
        }


        [TestMethod()]
        public void EditPasswordTest_Bad_empty()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.EditPassword(""));
        }

        [TestMethod()]
        public void EditUserNameTest_Good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsTrue(user.EditUserName("orelie2222"));
        }

        [TestMethod()]
        public void EditUserNameTest_Bad_empty()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.EditUserName(""));
        }



        [TestMethod()]
        public void EditUserNameTest_Bad_empty2()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.EditUserName(" "));
        }

        [TestMethod()]
        public void EditNameTest_good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsTrue(user.EditName("or"));
        }


        [TestMethod()]
        public void EditNameTest_Bad_empty()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.EditName(""));
        }

        [TestMethod()]
        public void EditNameTest_Bad_empty2()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.EditName(" "));
        }

        [TestMethod()]
        public void EditAvatarTest_good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsTrue(user.EditAvatar("/GuiScreen/Photos/Avatar/Test.png")); 
        }


        [TestMethod()]
        public void EditAvatarTest_bad_empty1()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.EditAvatar("")); 
        }

        [TestMethod()]
        public void EditAvatarTest_bad_empty2()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.EditAvatar(" ")) ;
        }

        [TestMethod()]
        public void EditUserPointsTest_good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsTrue(user.EditUserPoints(100));
        }


        [TestMethod()]
        public void EditUserPointsTest_Bad()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.EditUserPoints(-100));
        }


        [TestMethod()]
        public void ReduceMoneyIfPossibleTest_good_bigger_than_zero_bool()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsTrue(user.ReduceMoneyIfPossible(100));
        }

        [TestMethod()]
        public void ReduceMoneyIfPossibleTest_good_bigger_than_zero()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            user.ReduceMoneyIfPossible(100);
            Assert.AreEqual(user.Money(),400);
        }


        [TestMethod()]
        public void ReduceMoneyIfPossibleTest_good_Equal_to_zero()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            user.ReduceMoneyIfPossible(500);
            Assert.AreEqual(user.Money(), 0);
        }

        [TestMethod()]
        public void ReduceMoneyIfPossibleTest_good_Equal_to_zero_bool()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsTrue(user.ReduceMoneyIfPossible(500));
        }

        [TestMethod()]
        public void ReduceMoneyIfPossibleTest_Bad_Smaller_than_zero()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            user.ReduceMoneyIfPossible(600);
            Assert.AreEqual(user.Money(), 500);
        }

        [TestMethod()]
        public void ReduceMoneyIfPossibleTest_Bad_Smaller_than_zero_bool()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.ReduceMoneyIfPossible(600));
        }

        [TestMethod()]
        public void AddMoneyTest_good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            user.AddMoney(50);
            Assert.AreEqual(user.Money(), 550);
        }

        [TestMethod()]
        public void EditUserMoneyTest_good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsTrue(user.EditUserMoney(100));
        }


        [TestMethod()]
        public void EditUserMoneyTest_Bad()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.EditUserMoney(-100));
        }


        [TestMethod()]
        public void RemoveRoomFromActiveGameListTest()
        {
           
        }

        [TestMethod()]
        public void RemoveRoomFromSpectetorGameListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void HasThisActiveGameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void HasThisSpectetorGameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddRoomToActiveGameListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddRoomToSpectetorGameListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsLoginTest_good_on_Create()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.IsLogin());
        }

        [TestMethod()]
        public void IsLoginTest_good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            user.Login();
            Assert.IsTrue(user.IsLogin());
        }

       

        [TestMethod()]
        public void GetLeagueTest_Good_on_create()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreEqual(user.GetLeague(), LeagueName.Unknow);
        }

        [TestMethod()]
        public void GetLeagueTest_Good_after_10_games()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            for(int i = 0; i < 20; i++)
            {
                user.IncGamesPlay();
            }
            Assert.AreEqual(user.GetLeague(), LeagueName.E);
        }

        [TestMethod()]
        public void GetLeagueTest_Bad_on_create()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.AreNotEqual(user.GetLeague(), LeagueName.A);
        }

        [TestMethod()]
        public void GetLeagueTest_Bad_after_10_games()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            for (int i = 0; i < 20; i++)
            {
                user.IncGamesPlay();
            }
            Assert.AreNotEqual(user.GetLeague(), LeagueName.Unknow);
        }

       

        [TestMethod()]
        public void HasEnoughMoneyTest_good_bool()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsTrue(user.HasEnoughMoney(100, 50));
        }

        [TestMethod()]
        public void HasEnoughMoneyTest_good()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            user.HasEnoughMoney(100, 50);
            Assert.AreEqual(user.Money(),350);
        }

        [TestMethod()]
        public void HasEnoughMoneyTest_Bad_bool()
        {
            IUser user = new User(305077901, "orelie", "orelie26", "123456789", 0, 500, "orelie@post.bgu.ac.il");
            Assert.IsFalse(user.HasEnoughMoney(490, 50));
        }
    }
}