using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace Repositories
{
    public class DapperFactory
    {
        public static readonly string connectionString =
            ConfigurationUtil.Configuration["ConnectionStrings:DefaultConnection"].ToString();
             

        public static OracleConnection CrateOracleConnection()
        {
            
           var connection = new OracleConnection(connectionString);
            connection.Open();
            return connection;
        }

    }
}
