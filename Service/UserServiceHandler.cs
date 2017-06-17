using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using TexasHoldem.DatabaseProxy;
using TexasHoldem.Logic.Game;
using TexasHoldem.Logic.Game_Control;
using TexasHoldem.Logic.GameControl;
using TexasHoldem.Logic.Notifications_And_Logs;
using TexasHoldem.Logic.Replay;
using TexasHoldem.Logic.Users;
using TexasHoldemShared;

namespace TexasHoldem.Service
{
    public class UserServiceHandler 
    {
        private SystemControl sc;
        private GameCenter gc;
        private UserDataProxy userDataProxy;

        public UserServiceHandler (GameCenter game, SystemControl system)
        {
            sc = system;
            gc = game;
            userDataProxy = new UserDataProxy();
        }

        //Use-Case: user can login to system

        public IUser LoginUser(string username, string password)
        {
            IUser user = sc.GetIUSerByUsername(username);
            if (user == null || !user.Password().Equals(password))
            {
                return user;
            }
            Console.WriteLine("in login user login?:"+user.IsLogin());

            if (user.Login())
            {
                Console.WriteLine("before login db");
                userDataProxy.Login(user);
                Console.WriteLine("after login db");
            }
            return user;
        }


        //Use-Case: user can logput from system
        public IUser LogoutUser(int userId)
        {
            IUser user = sc.GetUserWithId(userId);
            if (user == null || !user.IsLogin())
            {
                return user;
            }

            var toReturn = user.Logout();
            if (toReturn)
            {
                userDataProxy.Logout(user);
                return user;
            }
            return null;
        }

        
        //register to system - return bool that tell is success or fail - syncronized
        public bool RegisterToSystem(int id, string name, string memberName, string password, int money, string email)
        {
            return sc.RegisterToSystem(id, name, memberName, password, money, email);
        }


        //by name and password
        public bool DeleteUser(string name, string password)
        {
            bool toReturn = false;
            IUser user = sc.GetIUSerByUsername(name);
            if (user == null || !user.Password().Equals(password))
            {
                return toReturn;
            }
            toReturn = sc.RemoveUserByUserNameAndPassword(name, password);
            return toReturn;
        }

        //by Id 
        public bool DeleteUserById(int id)
        {
            
            bool toReturn = sc.RemoveUserById(id);
            return toReturn;
        }


        //for test only
        //use-case: user can edit is points
        public bool EditUserPoints(int userId, int newPoints)
        {
            bool toReturn = false;
            IUser user = sc.GetUserWithId(userId);
            if (user == null)
            {
                return toReturn;
            }
            toReturn = user.EditUserPoints(newPoints);
            if (toReturn)
            {
                userDataProxy.EditUserPoints(userId,newPoints);
            }
            return toReturn;
        }

        //use-case: user can edit is password
        public bool EditUserPassword(int userId, string newPassword)
        {
            bool toReturn = false;
            IUser user = sc.GetUserWithId(userId);
            if (user == null)
            {
                return toReturn;
            }
            toReturn = user.EditPassword(newPassword);
            if (toReturn)
            {
                userDataProxy.EditPassword(userId,newPassword);
            }
            return toReturn;
        }


        //use-case: user can edit is email
        public bool EditUserEmail(int userId, string newEmail)
        {
            bool toReturn = false;
            IUser user = sc.GetUserWithId(userId);
            if (user == null)
            {
                return toReturn;
            }
            toReturn = user.EditEmail(newEmail);
            if (toReturn)
            {
                userDataProxy.EditEmail(userId,newEmail);
            }
            return toReturn;
        }

        //use-case: user can edit is userName
        public bool EditUserName(int userId, string newName)
        {
            bool toReturn = false;
            IUser user = sc.GetUserWithId(userId);
            if (user == null || !sc.IsUsernameFree(newName))
            {
                return toReturn;
            }
            toReturn = user.EditUserName(newName);
            if (toReturn)
            {
                userDataProxy.EditUserName(userId, newName);
            }
            return toReturn;
        }

        //use-case: user can edit is name
        public bool EditName(int userId, string newName)
        {
            bool toReturn = false;
            IUser user = sc.GetUserWithId(userId);
            if (user == null)
            {
                return toReturn;
            }
            toReturn = user.EditName(newName);
            if (toReturn)
            {
                userDataProxy.EditName(userId, newName);
            }
            return toReturn;
        }


        //use-case: user can edit is Id
        public bool EditId(int userId, int newId)
        {
            bool toReturn = false;
            IUser user = sc.GetUserWithId(userId);
            if (user == null || !sc.IsIdFree(newId))
            {
                return toReturn;
            }
            toReturn = user.EditId(newId);
            if (toReturn)
            {
                userDataProxy.EditUserId(userId, newId);
            }
            return toReturn;
        }

        //use-case: user can edit is money
        public bool EditMoney(int userId, int newmoney)
        {
            bool toReturn = false;
            IUser user = sc.GetUserWithId(userId);
            if (user == null)
            {
                return toReturn;
            }
            toReturn = user.EditUserMoney(newmoney);
            if (toReturn)
            {
                userDataProxy.EditUserMoney(userId, newmoney);
            }
            return toReturn;
        }
 


     

        //use-case: user can edit is avatar
        public bool EditUserAvatar(int id, string newAvatarPath)
        {
            bool toReturn = false;
            IUser user = sc.GetUserWithId(id);
            if (user == null)
            {
                return toReturn;
            }
            toReturn = user.EditAvatar(newAvatarPath);
            if (toReturn)
            {
                userDataProxy.EditUserAvatar(id, newAvatarPath);
            }
            return toReturn;
        }


    

        
        public List<IGame> GetSpectetorGamesByUserName(string userName)
        {
            List<IGame> toReturn = sc.GetSpectetorGamesByUserName(userName);
            return toReturn;
        }

        public IUser GetIUserByUserName(string userName)
        {
            IUser toReturn = sc.GetIUSerByUsername(userName);
            return toReturn;
        }

        public List<IUser> GetAllUser()
        {
            return sc.GetAllUser();
        }

        public IUser GetUserById(int id)
        {
            return sc.GetUserWithId(id);
        }

        public LeagueName GetUserLeague(int userId)
        {
            return sc.GetUserWithId(userId).GetLeague();
        }

        public bool DevideLeague()
        {
            return sc.DivideLeague();
        }

        public List<IUser> GetUsersByTotalProfit()
        {
            return sc.GetUsersByTotalProfit();
        }

        public List<IUser> GetUsersByHighestCash()
        {
            return sc.GetUsersByHighestCash();
        }

        public List<IUser> GetUsersByNumOfGames()
        {
            return sc.GetUsersByNumOfGames();
        }

        public UserStatistics GetUserStatistics(int userId)
        {
            IUser user = GetUserById(userId);
            if (user != null)
            {
                return new UserStatistics(user.GetAvgCashGainPerGame(), user.GetAvgProfit());
            }
            return null;
        }
    }
}