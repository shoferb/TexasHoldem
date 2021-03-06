﻿using System;
using System.Collections.Generic;
using System.Linq;
using TexasHoldem.communication.Impl;
using TexasHoldem.DatabaseProxy;
using TexasHoldem.Logic.Game;
using TexasHoldem.Logic.Game_Control;
using TexasHoldem.Logic.Notifications_And_Logs;
using TexasHoldem.Logic.Replay;
using TexasHoldem.Logic.Users;
using TexasHoldemShared;
using TexasHoldemShared.CommMessages;

namespace TexasHoldem.Logic.GameControl
{
    public class GameCenter
    {
        

        public int leagueGap { get; set; }
        //    private List<IGame> games;
        private GameDataProxy proxyDB; 
        private static int _roomIdCounter = 1;
        private readonly SystemControl _systemControl ;
        private readonly LogControl logControl;
        private readonly ReplayManager replayManager;
        private MessageEventHandler _messageEventHandler;
        private readonly SessionIdHandler sidHandler;
        private List< GameReplay> _replaysOfGames = new List<GameReplay>();
        private static readonly object padlock = new object();

        public GameCenter(SystemControl sys, LogControl log, ReplayManager replay, SessionIdHandler sidH)
        {
            proxyDB = new GameDataProxy(this);
            replayManager = replay;
            replayManager.InitProxy(proxyDB);
            _systemControl = sys;
            logControl = log;
           
       //     this.games = new List<IGame>();
            sidHandler = sidH;
        }
        public ReplayManager GetRepMan()
        {
            return replayManager;
        }
        public SessionIdHandler GetSessionIdHandler()
        {
            return this.sidHandler;
        }
        public LogControl GetLogControl()
        {
            return this.logControl;
        }

        public SystemControl GetSysControl()
        {
            return this._systemControl;
        }
        public void SetMessageHandler(MessageEventHandler handler)
        {
            _messageEventHandler = handler;
        }

        public IEnumerator<ActionResultInfo> RemoveSpectatorFromRoom(IUser user, int roomId)
        {
            return DoAction(user, CommunicationMessage.ActionType.SpectatorLeave, 0, roomId);
           
        }

        public IEnumerator<ActionResultInfo> AddSpectatorToRoom(IUser user, int roomId)
        {
            IEnumerator<ActionResultInfo> inumerator = new List<ActionResultInfo>().GetEnumerator();
            IGame gameRoom = GetRoomById(roomId);
           
            if (gameRoom != null)
            {
                inumerator = gameRoom.AddSpectetorToRoom(user);
                proxyDB.UpdateGameRoom((GameRoom)gameRoom);
                proxyDB.UpdateGameRoomPotSize(gameRoom.GetPotSize(), gameRoom.Id);
            }
            return inumerator;
        }

        public void UpdateExistingGameDB(GameRoom gm)
        {
            proxyDB.UpdateGameRoom((GameRoom)gm);
            proxyDB.UpdateGameRoomPotSize(gm.GetPotSize(), gm.Id);
        }

        public void InsertNewGameInExistingRoomDB(GameRoom gm)
        {
            proxyDB.InsertGameRoom(gm);
        }

        public IEnumerator<ActionResultInfo> DoAction(IUser user, CommunicationMessage.ActionType action, int amount, int roomId)
        {
            IGame gm = GetRoomById(roomId);
           
            IEnumerator<ActionResultInfo> toRet = gm.DoAction(user, action, amount, true);
            proxyDB.UpdateGameRoom((GameRoom)gm);
            proxyDB.UpdateGameRoomPotSize(gm.GetPotSize(), gm.Id);
            //if(action == CommunicationMessage.ActionType.StartGame && gm.IsGameActive())
            //{
            //    foreach (Player p in gm.GetPlayersInRoom())
            //    {
            //        p.user.AddRoomToActiveGameList(gm);
            //    }
            //}

            return toRet;
        }

        public List<Player> getPlayersInRoom(int roomId)
        {
            IGame gm = GetRoomById(roomId);
            return gm.GetPlayersInRoom();
        }

        public List<Spectetor> getSpectatorsInRoom(int roomId)
        {
            IGame gm = GetRoomById(roomId);
            return gm.GetSpectetorInRoom();
        }

        //minBet is the BB
        public bool CreateNewRoomWithRoomId(int roomId, IUser user, int startingChip, bool canSpectate, GameMode gameModeChosen,
            int minPlayersInRoom, int maxPlayersInRoom, int enterPayingMoney, int minBet)
        {
            lock (padlock)
            {
                if (user == null || startingChip < 0)
                {
                    return false;
                }
                if (minPlayersInRoom <= 0)
                {
                    ErrorLog log = new ErrorLog("Error while trying to create room, mim amount of player is invalid - less or equal to zero");
                    logControl.AddErrorLog(log);
                    return false;
                }
                if (minBet <= 0)
                {
                    ErrorLog log = new ErrorLog("Error while trying to create room, min bet is invalid - less or equal to zero");
                    logControl.AddErrorLog(log);
                    return false;
                }
                if (maxPlayersInRoom <= 0)
                {
                    ErrorLog log = new ErrorLog("Error while trying to create room, Max amount of player is invalid - less or equal to zero");
                    logControl.AddErrorLog(log);
                    return false;
                }
                if (minPlayersInRoom > maxPlayersInRoom)
                {
                    ErrorLog log = new ErrorLog("Error while trying to create room, invalid input - min player in room is bigger than max player in room");
                    logControl.AddErrorLog(log);
                    return false;
                }
                if (enterPayingMoney < 0)
                {
                    ErrorLog log = new ErrorLog("Error while trying to create room, invalid input - the entering money of the player is a negative number");
                    logControl.AddErrorLog(log);
                    return false;
                }
                List<Player> players = new List<Player>();
                if (enterPayingMoney > 0)
                {
                    //int newMoney = user.Money() - enterPayingMoney;
                    if (!user.ReduceMoneyIfPossible(enterPayingMoney))
                    {
                        ErrorLog log = new ErrorLog("not enough money to pay the fee for user id: " + user.Id());
                        logControl.AddErrorLog(log);
                        return false;
                    }
                }
                Player player = new Player(user, startingChip , roomId);
                players.Add(player);
                Decorator decorator = CreateDecorator(minBet, startingChip, canSpectate, minPlayersInRoom, maxPlayersInRoom, enterPayingMoney, gameModeChosen, user.GetLeague());
                GameRoom room = new GameRoom(players, roomId, decorator, this, logControl, replayManager, sidHandler);
                return AddRoom(room);
            }

            
        }

        public Decorator CreateDecorator(int minBet, int startingChip, bool canSpectate, int minPlayersInRoom, int maxPlayersInRoom, int enterPayingMoney, GameMode gameModeChosen, LeagueName league)
        {
            Decorator mid = new MiddleGameDecorator(gameModeChosen, minBet, minBet / 2);
            Decorator before = new BeforeGameDecorator(minBet, startingChip, canSpectate, minPlayersInRoom, maxPlayersInRoom, enterPayingMoney, league);
            before.SetNextDecorator(mid);
            return before;
        }

        //return the next room Id
        public int GetNextIdRoom()
        {
            lock (padlock)
            {
                int toReturn = 1;
                List<IGame> res = GetAllGames();
                if (res.Count == 0)
                {
                    return toReturn;
                }
                toReturn = res.OrderByDescending(game => game.Id).First().Id + 1;
                return toReturn;
            }
        }

        private int CurrRoomId()
        {
            return _roomIdCounter;
        }
        //Add new room the games list
        public bool AddRoom(GameRoom roomToAdd)
        {
            lock (padlock)
            {
                bool toReturn = false;
                if (roomToAdd == null)
                {
                    return toReturn;
                }
                try
                {
                  //  this.games.Add(roomToAdd);
                    toReturn = proxyDB.InsertNewGameRoom(roomToAdd);
                }
                catch (Exception e)
                {
                    ErrorLog log = new ErrorLog("Error while trying to add new room to game center");
                    logControl.AddErrorLog(log);
                    toReturn = false;
                }
                return toReturn;
            }
        }

        //remove room form games list - remove the room remove the game from active game list and spectetor in user
        public bool RemoveRoom(int roomId)
        {
            lock (padlock)
            {
                bool toReturn = false;
                IGame toRemove = GetRoomById(roomId);
                try
                {
                    //games.Remove(toRemove);
                    bool ans = proxyDB.DeleteGameRoomPref(roomId);
                    bool ans2 = proxyDB.DeleteGameRoom(roomId, ((GameRoom)toRemove).GetGameNum());
                    toReturn = ans & ans2;
                }
                catch (Exception e)
                {
                    ErrorLog log = new ErrorLog("Error while trying to remove game room");
                    logControl.AddErrorLog(log);
                    toReturn = false;
                }
                return toReturn;
            }
        }

        public List<IGame> GetAllActiveGamesAUserCanJoin(IUser user)
        {
            List<IGame> toReturn = new List<IGame>();
            lock (padlock)
            {
                int userMoney = user.Money();
                int userPoints = user.Points();
                bool isUnKnow = user.IsUnKnow();
                List<IGame> games = proxyDB.GetAllGames();
                foreach (IGame room in games)
                {
                    if (room.CanJoin(user))
                    {
                        toReturn.Add(room);
                    }
                }
            }
            return toReturn;
        }

        //return room by room if - suncronized due to for
        //return null if room Id smaller than 0 or not found
        public IGame GetRoomById(int roomId)
        {
            return proxyDB.GetGameRoombyId(roomId);
        }

        //return true if there is a room with this Id
        public bool IsRoomExist(int roomId)
        {
            lock (padlock)
            {
                bool toReturn = false;
                if (!IsValidInputNotSmallerZero(roomId))
                {
                    return toReturn;
                }
                IGame room = GetRoomById(roomId);
                List<IGame> all = GetAllGames();
                if (room != null)
                {
                    foreach (IGame g in all)
                    {
                        if (g.Id == room.Id)
                        {
                            return true;
                        }
                    }
                }
               
                return false;
            }
        }

        //return all games in the system 0 active and non active
        public List<IGame> GetGames()
        {
            return proxyDB.GetAllGames();
        }

        //get all active games - syncronized
        public List<IGame> GetAllActiveGame()
        {
            return proxyDB.GetAllActiveGameRooms();
        }
        //get all active games of user 
        //syncronizef due to for
        public List<IGame> GetActiveGamesByUserName(IUser user)
        {
            lock (padlock)
            {
                List<IGame> toReturn = new List<IGame>();
                try { 
                    int userId = user.Id();
                    toReturn = proxyDB.GetAllUserActiveGames(userId);
                    return toReturn;
                }
                catch (Exception e)
                {
                    ErrorLog log = new ErrorLog("Error: while trying get user active games - username: " + user.MemberName());
                    logControl.AddErrorLog(log);
                    return toReturn;
                }

                return toReturn;
            }
        }


        //get all the game user spectete
        //syncronized due to for
        public List<IGame> GetSpectetorGamesByUserName(IUser user)
        {
            lock (padlock)
            {
                List<IGame> toReturn = null;
                try
                {
                    int userId = user.Id();
                    toReturn = proxyDB.GetUserSpectetorsGameResult(userId);
                    return toReturn;
                }
                catch (Exception e)
                {
                    ErrorLog log = new ErrorLog("Error: while trying get user spectetor games - username: " + user.MemberName());
                    logControl.AddErrorLog(log);
                    return toReturn;
                }

                return toReturn;
            }

        }


        public List<IGame> GetAllSpectetorGame()
        {
            return proxyDB.GetAllSpectatebleGameRooms();
        }

        //return list of games with pot size
        public List<IGame> GetAllGamesByPotSize(int potSize)
        {
            return proxyDB.GetGameRoomsByPotSize(potSize);
        }

        //return list of games with game mode:
        //limit / no - limit / pot limit
        public List<IGame> GetGamesByGameMode(GameMode gm)
        {
            return proxyDB.GetGameRoomsByGameMode(gm);
        }

        //return list of games by buy in policy
        public List<IGame> GetGamesByBuyInPolicy(int buyIn)
        {
            return proxyDB.GetGameRoomsByBuyInPolicy(buyIn);
        }

        //return list of games by min player in room
        public List<IGame> GetGamesByMinPlayer(int min)
        {

            return proxyDB.GetGameRoomsByMinPlayers(min);
        }

        //return list of games by max player in room
        public List<IGame> GetGamesByMaxPlayer(int max)
        {
            return proxyDB.GetGameRoomsByMaxPlayers(max);
        }

        //return list of games by min bet in room
        //syncronized - due to for
        public List<IGame> GetGamesByMinBet(int minBet)
        {
            return proxyDB.GetGameRoomsByMinBet(minBet);
        }

        //return list of games by starting chip policy
        //return null if startingChup <=0
        //syncronized - due to for
        public List<IGame> GetGamesByStartingChip(int startingChip)
        {
            return proxyDB.GetGameRoomsByStartingChip(startingChip);
        }

        //chaeck if game is spectetable

        public bool IsGameCanSpectete(int roomId)
        {
            bool toReturn = false;
            IGame room = GetRoomById(roomId);
            if (room.IsSpectatable())
            {
                toReturn = true;
            }
            return toReturn;
        }

        //check if game is active game
        public bool IsGameActive(int roomId)
        {
            bool toReturn = false;
            IGame room = GetRoomById(roomId);
            if (room.IsGameActive())
            {
                toReturn = true;
            }
            return toReturn;
        }

        public List<IGame> GetAllGames()
        {
            return proxyDB.GetAllGames();
        }

        private bool IsValidInputNotSmallerEqualZero(int toCheck)
        {
            return toCheck > 0;
        }

        private bool IsValidInputNotSmallerZero(int toCheck)
        {
            return toCheck >= 0;
        }
        
        public IEnumerator<int> CanSendPlayerBrodcast(IUser user, int roomId)
        {
            lock (padlock)
            {
                IGame game = GetRoomById(roomId);
                if (game == null || !game.IsPlayerInRoom(user))
                {
                    return new List<int>().GetEnumerator();
                }
                var playerIds = game.GetPlayersInRoom().ConvertAll(p => p.user.Id());
                var spectIds = game.GetSpectetorInRoom().ConvertAll(s => s.user.Id());
                var allIds = playerIds.Union(spectIds);
                return allIds.GetEnumerator();
            }
        }

        public IEnumerator<int> CanSendSpectetorBrodcast(IUser user, int roomId)
        {
            lock (padlock)
            {
                IGame game = GetRoomById(roomId);
                if (game == null || game.IsSpectetorInRoom(user))
                {
                    return new List<int>().GetEnumerator();
                }
                return  game.GetSpectetorInRoom().ConvertAll(s => s.user.Id()).GetEnumerator();
                
            }
        }

        public bool CanSendPlayerWhisper(IUser sender, IUser reciver, int roomId)
        {
            lock (padlock)
            {
                IGame game = GetRoomById(roomId);
                if (game == null)
                {
                    return false;
                }
                bool isSenderPlayer = game.IsPlayerInRoom(sender);
                bool isReceiverActive = reciver.IsLogin();
                bool isReciverSectetorOrPlayer = game.IsPlayerInRoom(reciver) || game.IsSpectetorInRoom(reciver);
                return isSenderPlayer && isReciverSectetorOrPlayer && isReceiverActive;
            }
        }

        public bool CanSendSpectetorWhisper(IUser sender, IUser reciver, int roomId)
        {
            lock (padlock)
            {
                IGame game = GetRoomById(roomId);
                if (game == null)
                {
                    return false;
                }
                bool isSenderSpectetor = game.IsSpectetorInRoom(sender);
                bool isReceiverActive = reciver.IsLogin();
                bool isReciverSpector = game.IsSpectetorInRoom(reciver);
                return isSenderSpectetor && isReciverSpector && isReceiverActive;
            }
        }

        public IEnumerator<ActionResultInfo> ReturnToGameAsPlayer(IUser user, int roomId)
        {
            IGame gm = GetRoomById(roomId);

            IEnumerator<ActionResultInfo> toRet = gm.ReturnToGameAsPlayer(user);
            proxyDB.UpdateGameRoom((GameRoom)gm);
            proxyDB.UpdateGameRoomPotSize(gm.GetPotSize(), gm.Id);
            return toRet;
        }

        public IEnumerator<ActionResultInfo> ReturnToGameAsSpec(IUser user, int roomId)
        {
            IGame gm = GetRoomById(roomId);

            IEnumerator<ActionResultInfo> toRet = gm.ReturnToGameAsSpec(user);
            proxyDB.UpdateGameRoom((GameRoom)gm);
            proxyDB.UpdateGameRoomPotSize(gm.GetPotSize(), gm.Id);
            return toRet;
        }
    }


}
