﻿using clientCommunication.handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemShared.CommMessages;
using TexasHoldemShared.CommMessages.ClientToServer;

namespace clientCommunication.Logic
{
    class ClienLogic
    {
        private readonly int _userId;
        private ClientEventHandler _eventHandler;

        public ClienLogic(int userId, string server)
        {
            _userId = userId;
        }

        //needed to be call after create new ClientEventHandler and a new client logic
        public void init(ClientEventHandler handler)
        {
            _eventHandler = handler;
        }

        public void editDetails(TexasHoldemShared.CommMessages.ClientToServer.EditCommMessage.EditField field, string value)
        {
            //checkifAnyConstraints?
            EditCommMessage toSend = new EditCommMessage(_userId, field, value);
            _eventHandler.SendNewEvent(toSend);
        }
         public void joinTheGame(int roomId)
         {
             ActionCommMessage toSend = new ActionCommMessage(_userId, TexasHoldemShared.CommMessages.CommunicationMessage.ActionType.Join, -1, roomId);
             _eventHandler.SendNewEvent(toSend);
             
         }

         public void leaveTheGame(int roomId)
         {
             ActionCommMessage toSend = new ActionCommMessage(_userId, TexasHoldemShared.CommMessages.CommunicationMessage.ActionType.Leave, -1, roomId);
             _eventHandler.SendNewEvent(toSend);
         }
         public void startTheGame(int roomId)
         {
             ActionCommMessage toSend = new ActionCommMessage(_userId, TexasHoldemShared.CommMessages.CommunicationMessage.ActionType.StartGame, -1, roomId);
             _eventHandler.SendNewEvent(toSend);
         }

         public void login(string userName, string password)
         {
             LoginCommMessage toSend = new LoginCommMessage(_userId, true, userName, password);
             _eventHandler.SendNewEvent(toSend);
         }
         public void logout(string userName, string password)
         {
             LoginCommMessage toSend = new LoginCommMessage(_userId, false, userName, password);
             _eventHandler.SendNewEvent(toSend);
         }

         public void register(string name, string memberName, string password, int money, string email)
         {
             RegisterCommMessage toSend = new RegisterCommMessage(_userId, name, memberName, password, money,email);
             _eventHandler.SendNewEvent(toSend);
         }




      //  public void showOptionsMove(TexasHoldemShared.CommMessages.CommunicationMessage.ActionType[] options, int roomId)

        public Tuple<TexasHoldemShared.CommMessages.CommunicationMessage.ActionType, int> showOptionsMove(TexasHoldemShared.CommMessages.CommunicationMessage.ActionType[] options, int roomId)
        {
            //GUI
            //after Chosen: Call /notifyChosenMove(options, chosenMove);
            Tuple<TexasHoldemShared.CommMessages.CommunicationMessage.ActionType, int> toRet = new Tuple<TexasHoldemShared.CommMessages.CommunicationMessage.ActionType, int>(TexasHoldemShared.CommMessages.CommunicationMessage.ActionType.Fold,0);
           return toRet;
            
        }
        public Tuple<TexasHoldemShared.CommMessages.CommunicationMessage.ActionType, int> notifyChosenMove(TexasHoldemShared.CommMessages.CommunicationMessage.ActionType[] options, TexasHoldemShared.CommMessages.CommunicationMessage.ActionType move, int amount)
        {
            bool legalMove = false;
            foreach(TexasHoldemShared.CommMessages.CommunicationMessage.ActionType action in options)
            {
                if (action.Equals(move))
                {
                    legalMove = true;
                }
            }
            if (legalMove)
            {
                if (move.Equals(TexasHoldemShared.CommMessages.CommunicationMessage.ActionType.Fold))
                {
                    amount = -1;//amount isnt relevant
                    Tuple<TexasHoldemShared.CommMessages.CommunicationMessage.ActionType, int> toRet = new Tuple<TexasHoldemShared.CommMessages.CommunicationMessage.ActionType, int>(move, amount);
                    return toRet;
                }
                else if ((move.Equals(TexasHoldemShared.CommMessages.CommunicationMessage.ActionType.Bet)) && (amount >= 0))
                {
                    Tuple<TexasHoldemShared.CommMessages.CommunicationMessage.ActionType, int> toRet = new Tuple<TexasHoldemShared.CommMessages.CommunicationMessage.ActionType, int>(move, amount);
                    return toRet;
                }
            }
           
                //appropriate Log
                //notify the client for illegal input
                amount = -2;//illegal answer for client
                Tuple<TexasHoldemShared.CommMessages.CommunicationMessage.ActionType, int> ret = new Tuple<TexasHoldemShared.CommMessages.CommunicationMessage.ActionType, int>(move, amount);
                return ret;
        }
    }
}
