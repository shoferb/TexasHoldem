﻿ using System;
using System.Collections.Generic;
using TexasHoldem.Logic.Users;

namespace TexasHoldem.Logic.Game
{
    public class ConcreteGameRoom : GameRoom
    {
        private int _id { get; set; }
        private bool _isActive { get; set; }
        private int _blind { get; set; }
        private int _potSize { get; set; }
        private PlayersList _roomPlayers = new PlayersList();
        private List<Spectetor> _roomSpectetors = new List<Spectetor>();
        private GameHand _hand;
        private Player _currentPlayer { get; set; }
        private Player _currentDealer { get; set; }
        private int _highBetInTurn { get; set; }
        private List<Card> _cardsOnTable { get; set; }
        private Deck _deck;
        private Player _currentSB { get; set; }
        private Player _currentBB { get; set; }

public ConcreteGameRoom(int id, int blind, Player curr, Player dealer, int turn, List<Card> cards, string name, int sb, int bb, int minMoney, int maxMoney, int gameNumber) : base(name, sb, bb, minMoney, maxMoney, gameNumber)
        {
            this._id = id;
            this._isActive = true;
            this._currentPlayer = curr;
            this._currentDealer = dealer;
            this._highBetInTurn = turn;
            this._cardsOnTable = cards;
            this._deck = new Deck();
            this._hand = new GameHand(_deck);
          //TODO  this._potSize;


        }

        private bool addPlayerToGame(Player p)
        {
            if (this._roomPlayers.Count > 8) return false;
            else
            {
               this. _roomPlayers.Add(p);
                return true;
            }
        }

        private bool addSpectetorToGame(Spectetor s)
        { 
           this._roomSpectetors.Add(s);
            return true;
            
        }

        private bool setRoles()
        {
            if (this._roomPlayers.Count < 2) return false;
           else if (this._roomPlayers.Count == 2)
            {
                this._currentDealer = _roomPlayers[0];
                this._currentBB = _roomPlayers[0];
                this._currentSB = _roomPlayers[1];
                this._currentPlayer = _roomPlayers[1];
                return true;
            }
           else if (this._roomPlayers.Count > 2)
            {
                this._currentDealer = _roomPlayers[0];                
                this._currentSB = _roomPlayers[1];
                this._currentBB = _roomPlayers[2];
                this._currentPlayer = _roomPlayers[3];
                return true;
            }
            return false;
        }
        private void Fold()
        {
            throw new NotImplementedException();
        }

        private void Raise(int sum)
        {
            throw new NotImplementedException();
        }

        private void Check()
        {
            throw new NotImplementedException();
        }

        private void Call()
        {
            throw new NotImplementedException();
        }

        private Player findWinner(int sum)
        {   //TODO : byAvivG
            throw new NotImplementedException();
        }

        private void Play()
        {
            bool flag = false;
            while (!flag)
            {
                flag = setRoles();
            }

            foreach (Player p in _roomPlayers)
            {
                
            }
        }

    }
}
