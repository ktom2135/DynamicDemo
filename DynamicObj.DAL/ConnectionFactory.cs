using DynamicObj.Share;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DynamicObj.DAL
{
    public class ConnectionFactory
    {
        
        public IDbConnection getDBConnection()
        {
            return new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Database12.mdf;Integrated Security=True");
        }
    }
}
