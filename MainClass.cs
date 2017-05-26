﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.communication.Impl;
using TexasHoldem.communication.Interfaces;
using TexasHoldem.Logic.Game_Control;
using TexasHoldem.Logic.GameControl;
using TexasHoldem.Logic.Replay;

namespace TexasHoldem
{
    public class MainClass
    {
        public static void Main()
        {
            //init instances:
            LogControl logControl = new LogControl();
            SystemControl sysControl = new SystemControl(logControl);
            ReplayManager replayManager = new ReplayManager();
            GameCenter gameCenter = new GameCenter(sysControl, logControl, replayManager);
            var commHandler = CommunicationHandler.GetInstance();
            Task commTask = Task.Factory.StartNew(commHandler.Start);
            Console.WriteLine("starting comm");
            MessageEventHandler eventHandler = new MessageEventHandler(gameCenter, sysControl, logControl, replayManager);
            Task eventTask = Task.Factory.StartNew(eventHandler.HandleIncomingMsgs);
            commTask.Wait();
        }
    }
}
