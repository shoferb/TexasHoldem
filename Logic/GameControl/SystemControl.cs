﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Game;
using TexasHoldem.Logic.Users;

namespace TexasHoldem.Logic.Game_Control
{
    public class SystemControl
    {
        private List<User> users;

        private static SystemControl systemControlInstance = null;
        private static readonly object padlock = new object();


        public static SystemControl SystemControlInstance
        {
            get
            {
                lock (padlock)
                {
                    if (systemControlInstance == null)
                    {
                        systemControlInstance = new SystemControl();
                    }
                    return systemControlInstance;
                }
            }
        }

        private SystemControl()
        {
            this.users = new List<User>();
        }

        //getter seeter user list
        public List<User> Users
        {
            get
            {
                return users;
            }

            set
            {
                users = value;
            }
        }

        //add new user  - syncronized
        public bool AddNewUser(User newUser)
        {
            lock (padlock)
            {
                bool toReturn = false;
                try
                {
                    users.Add(newUser);
                    toReturn = true;
                }
                catch (Exception e)
                {
                    toReturn = false;
                }
                return toReturn;
            }
        }


        //remove user from user list byID - syncronized
        public bool RemoveUserById(int id)
        {
            lock (padlock)
            {
                bool toReturn = false;
                User original = GetUserWithId(id);
                try
                {
                    users.Remove(original);
                    toReturn = true;
                }
                catch (Exception e)
                {
                    toReturn = false;
                }
                return toReturn;
            }
        }


        //remove user by name and password  - syncronized
        public bool RemoveUserByUserNameAndPassword(string username, string password)
        {
            bool toReturn;
            User toRemove = null;
            bool found = false;
            lock (padlock)
            {
                foreach (User u in users)
                {
                    if ((u.Password.Equals(password)) && (u.MemberName.Equals(username)))
                    {
                        toRemove = u;
                        found = true;
                    }
                }
                try
                {
                    users.Remove(toRemove);
                    toReturn = true;
                }
                catch (Exception e)
                {
                    toReturn = false;
                }
                return toReturn;
            }
        }


        //remove user - syncronized
        public bool RemoveUser(User toRemove)
        {
            lock (padlock)
            {
                bool toReturn = false;
                try
                {
                    users.Remove(toRemove);
                    toReturn = true;
                }
                catch (Exception e)
                {
                    toReturn = false;
                }
                return toReturn;
            }
        }


        //?to remove - change 2 user in list nade to swap chcnges of user into list - synctonized
        public bool ReplaceUser(User oldUser, User newUser)
        {
            lock (padlock)
            {
                bool toReturn = false;
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i] == oldUser)
                    {
                        users[i] = newUser;
                        toReturn = true;
                    }
                }
                return toReturn;
            }
        }


        //find user by user name - syncronized (due to foreatch)
        private User FindUser(string username)
        {
            lock (padlock)
            {
                User toRerutn = null;
                foreach (User u in users)
                {
                    if (u.MemberName.Equals(username))
                    {
                        toRerutn = u;
                    }
                }
                return toRerutn;
            }
        }



        //login to system- syncronized
        public bool Login(string UserName, string password)
        {
            bool toReturn = false;
            User original = FindUser(UserName);
            if (original == null)
            {
                return toReturn;

            }
            lock (padlock)
            {
                foreach (User u in users)
                {
                    if ((u.Password.Equals(password)) && (u.MemberName.Equals(UserName)))
                    {
                        User toAdd = original;
                        toAdd.IsActive = true;
                        // ReplaceUser(original, toAdd);
                        toReturn = true;
                    }
                }
                return toReturn;
            }
        }


        //user logout from system - syncronized
        public bool Logout(int id)
        {
            bool toReturn = false;
            User original = GetUserWithId(id);
            User changed = original;
            if (original == null)
            {
                return toReturn;

            }
            lock (padlock)
            {
                changed.IsActive = false;
                toReturn = ReplaceUser(original, changed);
                return toReturn;
            }
        }


        //register to system - return bool that tell is success or fail - syncronized
        public bool RegisterToSystem(int id, string name, string memberName, string password, int money, string email)
        {
            bool toReturn = false;
            lock (padlock)
            {
                foreach (User u in users)
                {
                    if (u.MemberName.Equals(memberName))
                    {
                        //was string toReturn = "Registration failed - this user name is taken!";
                        return toReturn; // fail username taken
                    }
                }
                User newUser = new User(id, name, memberName, password, 0, money, email);
                toReturn = AddNewUser(newUser);
                return toReturn;
            }
        }


        //return true - if user name free, false otherwise 
        //syncrinized - due to foreath
        public bool IsUsernameFree(string username)
        {
            lock (padlock)
            {
                bool toReturn = true;
                foreach (User u in users)
                {
                    if (u.MemberName.Equals(username))
                    {
                        toReturn = false;
                    }
                }
                return toReturn;
            }
        }

        //return true - if user Id free, false otherwise 
        //syncrinized - due to foreath
        public bool IsIdFree(int ID)
        {
            lock (padlock)
            {
                bool toReturn = true;
                foreach (User u in users)
                {
                    if (u.Id ==  ID)
                    {
                        toReturn = false;
                    }
                }
                return toReturn;
            }
        }
        //return true if user with id exist 
        //syncronized - due to foreatch
        public bool IsUserWithId(int id)
        {
            lock (padlock)
            {
                bool toReturn = false;
                foreach (User u in users)
                {
                    if (u.Id == id)
                    {
                        toReturn = true;
                    }

                }
                return toReturn;
            }
        }


        //get user by id
        //syncronized - due to for
        public User GetUserWithId(int id)
        {
            lock (padlock)
            {
                User toReturn = null;
                foreach (User u in users)
                {
                    if (u.Id == id)
                    {
                        toReturn = u;
                    }
                }
                return toReturn;
            }
        }


        //check if email valid according to .NET convention
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        //use case - allow edit email - syncronized
        public bool EditEmail(int id, string newEmail)
        {
            lock (padlock)
            {
                bool toReturn = false;
                try
                {
                    User toEdit = GetUserWithId(id);
                    User changed = toEdit;
                    
                    bool valid = IsValidEmail(newEmail);
                    if (valid)
                    {
                        changed.Email = newEmail;
                        // toReturn = ReplaceUser(toEdit, changed);
                        toReturn = true;
                    }
                }
                catch (Exception e)
                {
                    toReturn = false;
                }
                /*
                else
                {
                    toReturn = false;
                }
                */
                return toReturn;
            }
        }

        public bool IsValidPassword(string password)
        {
            bool toReturn = false;
            int len = password.Length;
            if (len > 7 && len < 13)
            {
                toReturn = true;
            }
                return toReturn;
        }

        
      
        //use case allow to change password - syncronized
        public bool EditPassword(int id, string newPassword)
        {
            lock (padlock)
            {
                bool toReturn= false;
                try
                {
                    User toEdit = GetUserWithId(id);
                   
                    User changed = toEdit;
                    int len = newPassword.Length;
                    if (len > 7 && len < 13)
                    {

                        changed.Password = newPassword;
                        //toReturn = ReplaceUser(toEdit, changed);
                        toReturn = true;
                    }
                }
                catch (Exception e)
                {
                    toReturn = false;
                }
                
                return toReturn;
            }
        }


       
        //not use leave only namy for future use
        public bool EditUserName(int id, string newUserName)
        {
            lock (padlock)
            {
                bool toReturn = false;
                try
                {
                    User toEdit = GetUserWithId(id);
                    User changed = toEdit;

                    bool validname = IsUsernameFree(newUserName);
                    if (validname)
                    {
                        changed.MemberName = newUserName;
                        //toReturn = ReplaceUser(toEdit, changed);
                        toReturn = true;
                    }
                }
                catch (Exception e)
                {
                    toReturn = false;
                }
                return toReturn;
            }
        }


        //edit avatar pic path - syncronized
        public bool EditAvatar(int id, string newAvatarPath)
        {
            lock (padlock)
            {
                User toEdit = GetUserWithId(id);
                bool toReturn;
                try
                {
                    toEdit.Avatar = newAvatarPath;
                    toReturn = true;
                }
                catch (Exception e)
                {
                    toReturn = false;
                }
                return toReturn;
            }
        }


       
        //edit user id - syncronized
        public bool EditUserID(int id, int newId)
        {
            lock (padlock)
            {
                bool toReturn = false;
                try
                {
                    bool isFree = IsIdFree(newId);
                    if (isFree)
                    {
                        User toEdit = GetUserWithId(id);
                        User changed = toEdit;
                        changed.Id = newId;
                        toReturn = ReplaceUser(toEdit, changed);

                    }
                }
                catch (Exception e)
                {
                    toReturn = false;
                }
               
                return toReturn;
            }
        }


        //edit user point - syncronized
        
        public bool EditUserPoints(int id, int newPoints)
        {
            lock (padlock)
            {
                bool toReturn;
                try
                {
                    User toEdit = GetUserWithId(id);
                    User changed = toEdit;

                    changed.Points = newPoints;
                    //toReturn = ReplaceUser(toEdit, changed);
                    toReturn = true;
                }
                catch (Exception e)
                {
                    toReturn = false;
                }
               
                return toReturn;
            }
        }


        //syncronized
        
        public bool EditUserMoney(int id, int newMoney)
        {
            lock (padlock)
            {
                bool toReturn;
                try
                {
                    User toEdit = GetUserWithId(id);
                    User changed = toEdit;

                    changed.Money = newMoney;
                    //toReturn = ReplaceUser(toEdit, changed);
                    toReturn = true;
                }
                catch (Exception e)
                {
                    toReturn = false;
                }
                
                return toReturn;
            }
        }


        //syncronized - cange when user login / logout
        public bool EditActiveGame(int id, bool activemode)
        {
            lock (padlock)
            {
                bool toReturn;
                try
                {
                    User toEdit = GetUserWithId(id);
                    User changed = toEdit;

                    changed.IsActive = activemode;
                    //toReturn = ReplaceUser(toEdit, changed);
                    toReturn = true;
                }
                catch (Exception e)
                {
                    toReturn = false;
                }
                return toReturn;
            }
        }


        //remove room form user active game list - syncronized
        public bool RemoveRoomFromActiveRoom(int roomId, int userId)
        {
            lock (padlock)
            {
                bool toReturn;
                try
                {
                    GameRoom toRemove = GameCenter.Instance.GetRoomById(roomId);
                    User user = GetUserWithId(userId);
                    user.ActiveGameList.Remove(toRemove);
                    toReturn = true;
                }
                catch (Exception e)
                {
                    toReturn = false;
                }
                return toReturn;
            }
        }


        //remove room from user specteted game list - sncromized
        public bool RemoveRoomFromSpectetRoom(int roomId, int userId)
        {
            bool toReturn;
            lock (padlock)
            {
                try
                {
                    GameRoom toRemove = GameCenter.Instance.GetRoomById(roomId);
                    User user = GetUserWithId(userId);
                    user.SpectateGameList.Remove(toRemove);
                    toReturn = true;
                }
                catch (Exception e)
                {
                    toReturn = false;
                }
                return toReturn;
            }
        }


        //return if game is active game on is acrive game ist
        public bool HasThisActiveGame(int roomId, int userId)
        {
            bool toReturn;
            GameRoom toCheck = GameCenter.Instance.GetRoomById(roomId);
            User user = GetUserWithId(userId);
            toReturn = user.ActiveGameList.Contains(toCheck);
            return toReturn;
        }


        //return if game is spectetable game on is spectet game ist
        public bool HasThisSpectetorGame(int roomId, int userId)
        {
            bool toReturn = false;
            GameRoom toCheck = GameCenter.Instance.GetRoomById(roomId);
            User user = GetUserWithId(userId);
            toReturn = user.SpectateGameList.Contains(toCheck);
            return toReturn;
        }


        //get all active games of user 
        //syncronizef due to for
        public List<GameRoom> GetActiveGamesByUserName(string userName)
        {
            lock (padlock)
            {
                List<GameRoom> toReturn = new List<GameRoom>();
                User user = FindUser(userName);
                foreach (GameRoom room in user.ActiveGameList)
                {
                    if (room._isActiveGame)
                    {
                        toReturn.Add(room);
                    }
                }
                return toReturn;
            }
        }


        //get all the game user spectete
        //syncronized due to for
        public List<GameRoom> GetSpectetorGamesByUserName(string userName)
        {
            lock (padlock)
            {
                List<GameRoom> toReturn = new List<GameRoom>();
                User user = FindUser(userName);
                foreach (GameRoom room in user.ActiveGameList)
                {
                    if (room._isSpectetor)
                    {
                        toReturn.Add(room);
                    }
                }
                return toReturn;
            }
        }


        //return true if user is with the highest rank
        public bool IsHigestRankUser(int userId)
        {
            User user = GetUserWithId(userId);
            bool toReturn = user.IsHigherRank;
            return toReturn;
        }



        //change the gap and change league table
        //syncronized
        public bool ChangeGapByHighestUserAndCreateNewLeague(int userId, int newGap)
        {
            lock (padlock)
            {
                bool toReturn = false;
                bool isHighest = IsHigestRankUser(userId);
                if (!isHighest)
                {
                    return toReturn;
                    
                }
                toReturn = GameCenter.Instance.EditLeagueGap(newGap);
                bool change = GameCenter.Instance.LeagueChangeAfterGapChange(newGap);
                toReturn = (toReturn && change);
                return toReturn;
            }
        }

        public List<User> SortByRank()
        {
            List<User> sort = new List<User>();
            sort.OrderByDescending(r => r.rank);
            return sort;
        }

        public int GetUserRank(int userId)
        {
            List<User> sort = SortByRank();
            User user = GetUserWithId(userId);
            int toReturn = sort.IndexOf(user);
            user.rank = toReturn;
            return toReturn;
        }

        public List<User> SortUserByPoint()
        {
            List<User> sort = new List<User>();
            sort.OrderBy(p => p.Points);
            return sort;
        }

        public bool MovePlayerBetweenLeague(int highestId, int userToMove, int newPoint)
        {
            lock (padlock)
            {
                bool toReturn = false;
                //check to see is the higest user

                if (!IsHigestRankUser(highestId))
                {
                    return toReturn;
                }
                try
                {
                    User highest = GetUserWithId(highestId);
                    User toChange = GetUserWithId(userToMove);
                    toChange.Points = newPoint;
                    toReturn = true;
                }
                catch (Exception e)
                {
                    toReturn = false;
                }

                return toReturn;
            }
        }

        public bool SetDefultLeauseToNewUsers(int highestId, int newPoint)
        {
            bool toReturn = false;
            //check to see is the higest user
            lock (padlock)
            {
                if (!IsHigestRankUser(highestId))
                {
                    return toReturn;
                }
                try
                {
                    List<User> newUser = new List<User>();
                    foreach (User u in users)
                    {
                        if (u.Points == 0)
                        {
                            newUser.Add(u);
                        }
                    }
                    for (int i = 0; i < newUser.Count; i++)
                    {
                        newUser[i].Points = newPoint;
                    }

                    toReturn = true;
                }
                catch (Exception e)
                {
                    toReturn = false;
                }

                return toReturn;
            }
        }
    }
}
