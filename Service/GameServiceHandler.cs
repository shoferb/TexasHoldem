using System.Collections.Generic;
using TexasHoldem.communication.Impl;
using TexasHoldem.DatabaseProxy;
using TexasHoldem.Logic.Game;
using TexasHoldem.Logic.Game_Control;
using TexasHoldem.Logic.GameControl;
using TexasHoldem.Logic.Notifications_And_Logs;
using TexasHoldem.Logic.Replay;
using TexasHoldem.Logic.Users;
using TexasHoldem.Service.interfaces;
using TexasHoldemShared;
using TexasHoldemShared.CommMessages;

namespace TexasHoldem.Service
{
    public class GameServiceHandler : IGameService
    {
        
        private static GameCenter _gameCenter;
        private readonly SystemControl _systemControl;
        private readonly ReplayManager _replayManager;
        private readonly LogControl _logControl;
        private readonly GameDataProxy _proxyDb;

        public GameServiceHandler(GameCenter gc, SystemControl sys, LogControl log, 
            ReplayManager replay, SessionIdHandler sidHandler)
        {
            _gameCenter = gc;
            _systemControl = sys;
            _logControl = log;
            _replayManager = replay;
            //new MessageEventHandler(gc, sys, log, replay, sidHandler);
            _proxyDb = new GameDataProxy(_gameCenter);
        }

        public IEnumerator<ActionResultInfo> DoAction(int userId, CommunicationMessage.ActionType action,
            int amount, int roomId)
        {
            IUser user = _systemControl.GetUserWithId(userId);
            return _gameCenter.DoAction(user, action, amount, roomId);
        }

        public IEnumerator<ActionResultInfo> ReturnToGameAsPlayer(int userId, int roomId)
        {
            IUser user = _systemControl.GetUserWithId(userId);
            return _gameCenter.ReturnToGameAsPlayer(user, roomId);
        }

        public IEnumerator<ActionResultInfo> ReturnToGameAsSpec(int userId, int roomId)
        {
            IUser user = _systemControl.GetUserWithId(userId);
            return _gameCenter.ReturnToGameAsSpec(user, roomId);
        }

        public List<Player> GetPlayersInRoom(int roomId)
        {
            return _gameCenter.getPlayersInRoom(roomId);
        }

        public List<Spectetor> GetSpectatorsInRoom(int roomId)
        {
            return _gameCenter.getSpectatorsInRoom(roomId);
        }
        public IGame GetGameFromId(int gameId)
        {
            return _gameCenter.GetRoomById(gameId);
        }

       public bool CreateNewRoomWithRoomId(int roomId,int userId, int startingChip, bool isSpectetor, GameMode gameModeChosen,
            int minPlayersInRoom, int maxPlayersInRoom, int enterPayingMoney, int minBet)
        {
            IUser user = _systemControl.GetUserWithId(userId);
            return _gameCenter.CreateNewRoomWithRoomId(roomId, user, startingChip, isSpectetor, gameModeChosen,
                minPlayersInRoom, maxPlayersInRoom, enterPayingMoney, minBet);
        }

        //create room and add to games list game center
        public int CreateNewRoom(int userId, int startingChip, bool isSpectetor, GameMode gameModeChosen,
            int minPlayersInRoom, int maxPlayersInRoom, int enterPayingMoney, int minBet)
        {
            IUser user = _systemControl.GetUserWithId(userId);
            int roomID = _gameCenter.GetNextIdRoom();
            if (!_gameCenter.CreateNewRoomWithRoomId(roomID, user, startingChip, isSpectetor, gameModeChosen,
                minPlayersInRoom, maxPlayersInRoom, enterPayingMoney, minBet))
            {
                return -1;
            }
            return roomID;
        }
        
        public List<string> GetGameReplay(int roomId, int gameNum, int userId)
        {
            GameReplay replay = _replayManager.GetGameReplayForUser(roomId, gameNum, userId);
            List<string> replays = new List<string>();
            if (replay == null)
            {
                return replays;
            }
            TexasHoldem.Logic.Actions.Action action = replay.GetNextAction();
            while (action != null)
            {
                replays.Add(action.ToString());
                action = replay.GetNextAction();
            }
            return replays;
        }

        public IEnumerator<ActionResultInfo> RemoveSpectatorFromRoom(int userId, int roomId)
        {
            //return DoAction(userId, CommunicationMessage.ActionType.SpectatorLeave, 0, roomId);
           //todo - new call remove above
             IUser user = _systemControl.GetUserWithId(userId);
            return _gameCenter.RemoveSpectatorFromRoom(user, roomId);
            

        }

        public IEnumerator<ActionResultInfo> AddSpectatorToRoom(int userId, int roomId)
        {
            //IEnumerator<ActionResultInfo> inumerator = new List<ActionResultInfo>().GetEnumerator();
           
            //IUser user = _systemControl.GetUserWithId(userId);
            //if ( user != null)
            //{
            //    inumerator = _gameCenter.AddSpectatorToRoom(user, roomId); 
            //}
            //return inumerator;
            //todo - new call below remove above
            
            IEnumerator<ActionResultInfo> inumerator = new List<ActionResultInfo>().GetEnumerator();
            IGame gameRoom = _gameCenter.GetRoomById(roomId);
            IUser user = _systemControl.GetUserWithId(userId);
            if (gameRoom != null && user != null)
            {
                inumerator = gameRoom.AddSpectetorToRoom(user);
                _proxyDb.UpdateGameRoom((GameRoom)gameRoom);
                _proxyDb.UpdateGameRoomPotSize(gameRoom.GetPotSize(), gameRoom.Id);
            }
            return inumerator;
        }

        public IGame GetGameById(int id)
        {
            return _gameCenter.GetRoomById(id);
        }

        public List<IGame> GetAllActiveGames()
        {
            List<IGame> toReturn = _gameCenter.GetAllActiveGame();
            return toReturn;
        }
   
        public List<IGame> GetAllActiveGamesAUserCanJoin(int userId)
        {
            IUser user = _systemControl.GetUserWithId(userId);
            List<IGame> toReturn = new List<IGame>();
            if (user != null)
            {
                toReturn = _gameCenter.GetAllActiveGamesAUserCanJoin(user);
            }

            return toReturn;
        }

        public List<IGame> GetAllGames()
        {
            List<IGame> toReturn = _gameCenter.GetAllGames();
            return toReturn;
        }

        public List<IGame> GetSpectateableGames()
        {
            List<IGame> toReturn = _gameCenter.GetAllSpectetorGame();
            return toReturn;
        }

        public List<IGame> GetGamesByPotSize(int potSize)
        {
            List<IGame> toReturn = _gameCenter.GetAllGamesByPotSize(potSize);
            return toReturn;
        }

        public List<IGame> GetGamesByGameMode(GameMode gm)
        {
            List<IGame> toReturn = _gameCenter.GetGamesByGameMode(gm);
            return toReturn;
        }

        //return list of games by buy in policy
        public List<IGame> GetGamesByBuyInPolicy(int buyIn)
        {
            List<IGame> toReturn = _gameCenter.GetGamesByBuyInPolicy(buyIn);
            return toReturn;
        }

        //return list of games by min player in room
        public List<IGame> GetGamesByMinPlayer(int min)
        {
            List<IGame> toReturn = _gameCenter.GetGamesByMinPlayer(min);
            return toReturn;
        }

        //return list of games by min bet in room
        public List<IGame> GetGamesByMinBet(int minBet)
        {
            List<IGame> toRetun = _gameCenter.GetGamesByMinBet(minBet);
            return toRetun;
        }

        //return list of games by starting chip policy
        public List<IGame> GetGamesByStartingChip(int startingChip)
        {
            List<IGame> toRetun = _gameCenter.GetGamesByStartingChip(startingChip);
            return toRetun;
        }

        //return list of games by max player in room
        public List<IGame> GetGamesByMaxPlayer(int max)
        {
            List<IGame> toReturn = _gameCenter.GetGamesByMaxPlayer(max);
            return toReturn;
        }

        //check player is in the game room 
        public IEnumerator<int> CanSendPlayerBrodcast(int playerId, int roomId)
        {
            bool isUserExist = _systemControl.IsUserExist(playerId);
            if (!isUserExist)
            {
                return null;
            }
            IUser user = _systemControl.GetUserWithId(playerId);
            return _gameCenter.CanSendPlayerBrodcast(user,roomId);
        }

        //check spectetor is in the game room 
        public IEnumerator<int> CanSendSpectetorBrodcast(int idSpectetor, int roomId)
        {
            bool isUserExist = _systemControl.IsUserExist(idSpectetor);
            if (!isUserExist)
            {
                return null;
            }
            IUser user = _systemControl.GetUserWithId(idSpectetor);
            return _gameCenter.CanSendSpectetorBrodcast(user, roomId);
        }

        //check id sender is player, reciver exist, + rules if can send this to reciver
        public bool CanSendPlayerWhisper(int idSender, string reciverUsername, int roomId)
        {
            bool toReturn = false;
            bool senderExist = _systemControl.IsUserExist(idSender);
            if (!senderExist)
            {
                return toReturn;
            }
            IUser reciver = _systemControl.GetIUSerByUsername(reciverUsername);
            if(reciver == null)
            {
                return toReturn;
            }
            IUser sender = _systemControl.GetUserWithId(idSender);
            toReturn = _gameCenter.CanSendPlayerWhisper(sender, reciver, roomId);
            return toReturn;
        }

        //check id sender is spectetor, reciver exist, + rules if can send this to reciver
        public bool CanSendSpectetorWhisper(int idSender, string reciverUsername, int roomId)
        {
            bool toReturn = false;
            bool senderExist = _systemControl.IsUserExist(idSender);
            if (!senderExist)
            {
                return toReturn;
            }
            IUser reciver = _systemControl.GetIUSerByUsername(reciverUsername);
            if (reciver == null)
            {
                return toReturn;
            }
            IUser sender = _systemControl.GetUserWithId(idSender);
            toReturn = _gameCenter.CanSendSpectetorWhisper(sender, reciver, roomId);
            return toReturn;
        }

        public List<IGame> GetActiveGamesByUserName(string userName)
        {
            List<IGame> toReturn = null;
            
                if (userName.Equals("") || userName.Equals(" "))
                {
                    ErrorLog log = new ErrorLog("Error: while trying get user active games - username: " + userName + " empty");
                    _logControl.AddErrorLog(log);
                    return toReturn;
                }

                if (_systemControl.IsUsernameFree(userName))
                {
                    ErrorLog log = new ErrorLog("Error: while trying get user active games - username: " + userName + " dose not exist!");
                    _logControl.AddErrorLog(log);
                    return toReturn;
                }
                IUser user = _systemControl.GetIUSerByUsername(userName);
                if (user == null)
                {
                    return toReturn;
                }
                toReturn = _gameCenter.GetActiveGamesByUserName(user);
            return toReturn;
        }

        public List<IGame> GetSpectetorGamesByUserName(string userName)
        {
            List<IGame> toReturn = new List<IGame>();
            if (userName.Equals("") || userName.Equals(" "))
            {
                ErrorLog log = new ErrorLog("Error: while trying get user spectetor games - username: " + userName + " empty");
                _logControl.AddErrorLog(log);
                return toReturn;
            }

            if (_systemControl.IsUsernameFree(userName))
            {
                ErrorLog log = new ErrorLog("Error: while trying get user spectetor games - username: " + userName + " dose not exist!");
                _logControl.AddErrorLog(log);
                return toReturn;
            }
            IUser user = _systemControl.GetIUSerByUsername(userName);
            if (user == null)
            {
                return toReturn;
            }
            toReturn = _gameCenter.GetSpectetorGamesByUserName(user);
            return toReturn;
        }
    }
}