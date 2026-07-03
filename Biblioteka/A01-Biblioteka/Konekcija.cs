using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace A01_Biblioteka
{
    internal class Konekcija
    {
        public static SqlCommand GetCommand()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"server=.\sqlexpress; database=31-Biblioteka1; user id = sa; password=ikt");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }
    }
}
