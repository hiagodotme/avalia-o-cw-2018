using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto.Utils
{
    public class DataBase : IDisposable
    {
        SqlConnection connection;
        public DataBase()
        {
            var key = "connectionStringAzure";
            string connectionString = ConfigurationManager.ConnectionStrings[key]
                .ConnectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public SqlCommand GetSqlCommand()
        {
            SqlCommand sc = new SqlCommand();
            sc.Connection = connection;
            return sc;
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}