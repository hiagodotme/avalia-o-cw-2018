using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto.DBUtils
{
    public class DB : IDisposable
    {
        SqlConnection connection;
        public SqlConnection getConn()
        {
            string connectionString =
                "Data Source=(local);Initial Catalog=BDVendaDireta;"
                + "Integrated Security=true";
            connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}