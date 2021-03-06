﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TexasHoldem.DatabaseProxy;
using TexasHoldem.Logic.Game;
using TexasHoldem.Logic.GameControl;
using TexasHoldem.Logic.Notifications_And_Logs;
using TexasHoldem.Logic.Users;

namespace TexasHoldem.Logic.Game_Control
{
    public class SystemControl
    {
       

        private static readonly object padlock = new object();
        private LogControl logControl;
        private UserDataProxy userProxy;
        
        public SystemControl(LogControl log)
        {
           
            userProxy = new UserDataProxy();
            this.logControl = log;
            var ServiceTimer = new System.Timers.Timer();
            ServiceTimer.Enabled = true;
            ServiceTimer.Interval = (1000 * 60 * 60 * 24 * 7);//once a week
            ServiceTimer.Elapsed += new System.Timers.ElapsedEventHandler(DivideStart);
        }

   

        
        //remove user from user list byID - syncronized
        public bool RemoveUserById(int id)
        {
            lock (padlock)
            {
                bool toReturn = false;
                if (!IsValidInputNotSmallerZero(id))
                {
                    return toReturn;
                }
                IUser original = GetUserWithId(id);
                if (original==null)
                {
                    return toReturn;
                }
                try
                {
                   userProxy.DeleteUserById(id);
                    toReturn = true;
                }
                catch (Exception e)
                {
                    ErrorLog log = new ErrorLog("Error: while truing to remove user with id: " + id);
                    logControl.AddErrorLog(log);
                    toReturn = false;
                    return toReturn;
                }
                return toReturn;
            }
        }


        //remove user by name and password  - syncronized
        public bool RemoveUserByUserNameAndPassword(string username, string password)
        {
            bool toReturn = false;
            IUser toRemove = userProxy.GetUserByUserName(username);
            lock (padlock)
            {
                try
                {
                    if (toRemove.Password().Equals(password))
                    {
                       
                        userProxy.DeleteUserById(toRemove.Id());
                        toReturn = true;
                    }
                    return toReturn;
                }
                catch (Exception e)
                {
                    ErrorLog log = new ErrorLog("Error: while trying to remove user with user name: "+username+ "and password fail" );
                    logControl.AddErrorLog(log);
                    toReturn = false;
                    return toReturn;
                }
            }
        }


        //remove user - syncronized
        public bool RemoveUser(IUser toRemove)
        {
            lock (padlock)
            {
                bool toReturn = false;
                if (toRemove == null)
                {
                    return toReturn;
                }
                try
                {
                   userProxy.DeleteUserById(toRemove.Id());
                    toReturn = true;
                }
                catch (Exception e)
                {
                    ErrorLog log = new ErrorLog("Error: while trying to remove user by User with user name :" + toRemove.MemberName() + "with id" + toRemove.Id());
                    logControl.AddErrorLog(log);
                    toReturn = false;
                    return toReturn;
                }
                return toReturn;
            }
        }


        //find user by user name - syncronized (due to foreatch)
        //return null if not found or user name is "" || " "
        public IUser GetIUSerByUsername(string username)
        {
            lock (padlock)
            {
                IUser toRerutn = null;
                
                if (username.Equals("")|| username.Equals(" "))
                {
                    return toRerutn;
                }
                try
                {
                   /* foreach (User u in users)
                    {
                        if (u.MemberName().Equals(username))
                        {
                            toRerutn = u;
                            return toRerutn;
                        }
                        
                    }*/
                    toRerutn = userProxy.GetUserByUserName(username);
                    return toRerutn;
                }
                catch
                {
                    ErrorLog log = new ErrorLog("Error: while trying to get user by user name: "+ username );
                    logControl.AddErrorLog(log);

                    return toRerutn;
                }
                
                return toRerutn;
            }
        }

        //register to system - return bool that tell is success or fail - syncronized
        public bool RegisterToSystem(int id, string name, string memberName, string password, int money, string email)
        {
            bool toReturn = false;
            lock (padlock)
            {
                try
                {
                    if (!CanCreateNewUser(id, memberName, password, email))
                    {
                        ErrorLog log = new ErrorLog("Error: while trying register user: onr or more Invalid fields" );
                        logControl.AddErrorLog(log);
                        return toReturn;
                    }
                    if (name.Equals(" ") || name.Equals(""))
                    {
                        ErrorLog log = new ErrorLog("Error: while trying register user: name is empty");
                        logControl.AddErrorLog(log);

                        return toReturn;
                    }
                    if (!IsValidInputNotSmallerZero(money))
                    {
                        ErrorLog log = new ErrorLog("Error: while trying register user:money smaller than zero");
                        logControl.AddErrorLog(log);

                        return toReturn;
                    }

                    IUser newUser = new User(id, name, memberName, password, 0,  money, email);

                    userProxy.AddNewUser(newUser);
                    // users.Add(newUser);
                    toReturn = true;
                   
                }catch(Exception e)
                {
                    ErrorLog log = new ErrorLog("Error: while trying register user");
                    logControl.AddErrorLog(log);

                    toReturn = false;
                    return toReturn;
                }

                 return toReturn;
            }
        }

       

        public bool CanCreateNewUser(int id, string memberName,
            string password, string email)
        {
            
            bool toReturn = IsUsernameFree(memberName) && IsIdFree(id) &&
                IsValidPassword(password) && IsValidEmail(email) && !memberName.Equals("") && !memberName.Equals(" ");
            if (!IsUsernameFree(memberName))
            {
                ErrorLog log = new ErrorLog("Error: while trying ceate user - username: "+memberName+" is NOT free");
                logControl.AddErrorLog(log);
            }
            if (!IsIdFree(id))
            {
                ErrorLog log = new ErrorLog("Error: while trying ceate user - id: " + id + " is NOT free");
                logControl.AddErrorLog(log);
            }
            if (!IsValidPassword(password))
            {
                ErrorLog log = new ErrorLog("Error: while trying ceate user with id" + id+" and username: "+memberName + "password is not valid");
                logControl.AddErrorLog(log);
            }
            if (!IsValidEmail(email))
            {
                ErrorLog log = new ErrorLog("Error: while trying ceate user with id" + id + " and username: " + memberName + "email: "+email +" is not valid");

                logControl.AddErrorLog(log);
            }
            return toReturn;
        }

        //return true - if user name free, false otherwise 
        //syncrinized - due to foreath
        public bool IsUsernameFree(string username)
        {
            lock (padlock)
            {
                bool toReturn = true;
                if (username.Equals("") || username.Equals(" "))
                {
                    toReturn = false;
                    return toReturn;
                }
               /* foreach (IUser u in users)
                {
                    if (u.MemberName().Equals(username))
                    {
                        toReturn = false;
                        return toReturn;
                    }
                }*/
                IUser toCheck = userProxy.GetUserByUserName(username);
                return toCheck==null;
            }
        }

        //return true - if user Id free, false otherwise 
        //syncrinized - due to foreath
        public bool IsIdFree(int ID)
        {
            lock (padlock)
            {
                bool toReturn = true;
                
                if (!IsValidInputNotSmallerZero(ID))
                {
                    toReturn = false;
                    return toReturn;
                }
               /* foreach (IUser u in users)
                {
                    if (u.Id() ==  ID)
                    {
                        toReturn = false;
                        return toReturn;
                    }
                }*/
                IUser tocheack = userProxy.GetUserById(ID);
                return tocheack==null;
            }
        }

        //return true if user with Id exist 
        //syncronized - due to foreatch
        public bool IsUserExist(int id)
        {
            lock (padlock)
            {
                bool toReturn = false;
                try
                {
                    IUser toCheck = userProxy.GetUserById(id);
                    return toCheck != null;
                }
                catch(Exception e)
                {
                    ErrorLog log = new ErrorLog("Error: while checking if user exist:  user whith id" + id);

                    logControl.AddErrorLog(log);
                    return toReturn;
                }
               
                return toReturn;
            }
        }


        //get user by Id - null if not exist / invalid Id
        //syncronized - due to for
        public IUser GetUserWithId(int id)
        {
            lock (padlock)
            {
                IUser toReturn = null;
                try
                {
                  
                    if (!IsValidInputNotSmallerZero(id))
                    {
                        return toReturn;
                    }

                   /* foreach (IUser u in users)
                    {
                        if (u.Id() == id)
                        {
                            toReturn = u;
                            return toReturn;
                        }
                    }*/
                    toReturn = userProxy.GetUserById(id);
                    return toReturn;
                }catch(Exception e)
                {
                    ErrorLog log = new ErrorLog("Error: while Get uset whith id" + id );

                    logControl.AddErrorLog(log);
                    return toReturn;
                }
              
                return toReturn;
            }
        }


        //check if email valid according to .NET convention
        private bool IsValidEmail(string email)
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

        private bool IsValidPassword(string password)
        {
            bool toReturn = false;
            try
            {
                int len = password.Length;
                if (len > 7 && len < 13)
                {
                    toReturn = true;
                }
            }
            catch (Exception e)
            {
                toReturn = false;
                return toReturn;
            }

            return toReturn;
        }

    
     

        public List<IUser> GetAllUser()
        {
            lock (padlock)
            {
                return userProxy.GetAllUser();
            }
        }
        
 


        private bool IsValidInputNotSmallerZero(int toCheck)
        {
            return toCheck >= 0;
        }

        

        public List<IUser> SortByPoint()
        {
            lock (padlock)
            {
                List<IUser> sort = GetAllUser();
                sort.Sort(delegate (IUser x, IUser y)
                {
                    return y.Points().CompareTo(x.Points());
                });
                return sort;
            }
        }

        public void DivideStart(object sender, ElapsedEventArgs e)
        {
            DivideLeague();
        }


        public bool DivideLeague()
        {
            
            lock (padlock)
            {
                try
                {

                    List<IUser> sorted = SortByPoint();
                    int userCount = sorted.Count;
                    int divideTo;
                    if (userCount == 0)
                    {
                        return false;
                    }
                    double temp = (userCount / 5);
                    divideTo = (int)Math.Round(temp);
                    if (divideTo < 2)
                    {
                        divideTo = 2;
                    }
                    int i = 0;
                    int k = 0;
                    LeagueName curr = LeagueName.A;

                    while (i < userCount)
                    {
                        while (k < divideTo && i < userCount)
                        {
                            sorted.ElementAt(i).SetLeague(curr);
                            
                            k++;
                            i++;
                        }
                        k = 0;
                        curr = GetNextLeague(curr);
                    }
                    return true;
                }
                catch
                {
                    ErrorLog log = new ErrorLog("Error: while trying to divide the leauge");
                    logControl.AddErrorLog(log);
                    return false;
                }
                }
               
        }

        private LeagueName GetNextLeague(LeagueName curr)
        {
            LeagueName toReturn = LeagueName.E;
            try
            {
                switch (curr)
                {
                    case LeagueName.A:
                        toReturn = LeagueName.B;
                        return toReturn;
                        break;
                    case LeagueName.B:
                        toReturn = LeagueName.C;
                        return toReturn;
                        break;
                    case LeagueName.C:
                        toReturn = LeagueName.D;
                        return toReturn;
                        break;
                    case LeagueName.D:
                        toReturn = LeagueName.E;
                        return toReturn;
                        break;
                    case LeagueName.E:
                        toReturn = LeagueName.E;
                        return toReturn;
                        break;
                    default:
                        toReturn = LeagueName.E;
                        return toReturn;
                        break;
                  
                }
            }
            catch
            {
                ErrorLog log = new ErrorLog("Error: while trying to ey next League name");
                logControl.AddErrorLog(log);
                return toReturn;
            }
           
            return toReturn;
        }

        public List<IUser> GetUsersByTotalProfit()
        {
            List<IUser> temp = userProxy.GetAllUser();
            return new List<IUser>(temp.OrderByDescending(user => user.TotalProfit)
                .Take(Math.Min(20, temp.Count)));
        }

        public List<IUser> GetUsersByHighestCash()
        {
            List<IUser> temp = userProxy.GetAllUser();
            return new List<IUser>(temp.OrderByDescending(user => user.HighestCashGainInGame)
                .Take(Math.Min(20, temp.Count)));
        }

        public List<IUser> GetUsersByNumOfGames()
        {
            List<IUser> temp = userProxy.GetAllUser();
            return new List<IUser>(temp.OrderByDescending(user => user.GetNumberOfGamesUserPlay())
                .Take(Math.Min(20, temp.Count)));
        }
    }
}
