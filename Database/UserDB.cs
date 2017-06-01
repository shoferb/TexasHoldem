﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TexasHoldem.Database.DatabaseObject;

namespace TexasHoldem.Database
{
    public class UserDB
    {
        public List<userDatabaseOb> GetAllUser()

        {

            using (IDbConnection db = new SqlConnection

            (ConfigurationManager.ConnectionStrings

                ["DataBaseSadna"].ConnectionString))

            {

                string readSp = "GetAllUser";

                return db.Query<userDatabaseOb>(readSp,

                    commandType: CommandType.StoredProcedure).ToList();

            }
        }
    }
}
