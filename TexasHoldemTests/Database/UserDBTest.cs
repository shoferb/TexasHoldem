﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.VisualStudio.TestTools.UnitTesting;
using TexasHoldem.Database;
using TexasHoldem.Database.DatabaseObject;

namespace TexasHoldemTests.Database
{
    [TestClass()]
    public class UserDBTests
    {
       
      
        [TestMethod()]
        public void GetAllUserTest_good_id()
        {
            UserDB usd = new UserDB();
            userDatabaseOb toAdd  = new userDatabaseOb(305077901, "orelie26", "orelie", "orelie@post.bgu.ac.il",
                "123456789", "/GuiScreen/Photos/Avatar/devil.png", 0, 1000, 0, 1, 0, 0, 0, true);
            usd.AddNewUser(toAdd);
           Assert.IsTrue(true);
        }

        [TestMethod()]
        public void GetUserByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetUserByUserNameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddNewUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditUserIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditUserNameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditNameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditEmailTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditPasswordTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditUserHighestCashGainInGameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditUserIsActiveTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditUserLeagueNameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditUserMoneyTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditUserNumOfGamesPlayedTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditUserTotalProfitTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditUserWinNumTest()
        {
            Assert.Fail();
        }
    }
}