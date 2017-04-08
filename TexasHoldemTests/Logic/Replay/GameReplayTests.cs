﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using TexasHoldem.Logic.Replay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Actions;
using TexasHoldem.Logic.Game;
using TexasHoldem.Logic.Users;

namespace TexasHoldem.Logic.Replay.Tests
{
    [TestClass()]
    public class GameReplayTests
    {
        private GameReplay testGR;
        private Actions.Action testAction1;
        private Actions.Action testAction2;


        [TestInitialize()]
        public void Initialize()
        {
            testGR = new GameReplay(1, 1);
            testAction1 = new CallAction(new Card(Suits.Clubs, "1"), new Card(Suits.Diamonds, "2"), 1, Role.None, 10,
            new Player(1, "test", "mem", 123, 10, 100, "email@gmail.com", 1, true), 1, 1);
            testAction2 = new CallAction(new Card(Suits.Hearts, "1"), new Card(Suits.Spades, "2"), 1, Role.None, 10,
            new Player(1, "test", "mem", 123, 10, 100, "email@gmail.com", 1, true), 2, 2);
            testGR.AddAction(testAction1);
            testGR.AddAction(testAction2);
        }

        [TestMethod()]
        public void GetNextActionTest()
        {
            Assert.IsTrue(testGR.GetNextAction() == testAction1);
            Assert.IsTrue(testGR.GetNextAction() == testAction2);
            Assert.IsNull(testGR.GetNextAction());
            testGR.StartOver();
        }

        [TestMethod()]
        public void StartOverTest()
        {
            while (testGR.GetNextAction() != null)
            {
                testGR.GetNextAction();
            }
            Assert.IsNull(testGR.GetNextAction());
            testGR.StartOver();
            Assert.IsTrue(testGR.GetNextAction() == testAction1);
            Assert.IsTrue(testGR.GetNextAction() == testAction2);
            Assert.IsNull(testGR.GetNextAction());
        }

        [TestMethod()]
        public void RightGameTest()
        {
            Assert.IsTrue(testGR.RightGame(1, 1));
            Assert.IsFalse(testGR.RightGame(2, 1));
            Assert.IsFalse(testGR.RightGame(1, 2));
            Assert.IsFalse(testGR.RightGame(3, 3));
        }

    }
}