using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace A01_Biblioteka
{
    internal class Analiza
    {
        public static DataTable Podaci(int g1, int g2, int sif)
        {
            SqlCommand cmd = Konekcija.GetCommand();
            cmd.CommandText = "uspAnaliza";
            cmd.Parameters.AddWithValue("@god1", g1);
            cmd.Parameters.AddWithValue("@god2", g2);
            cmd.Parameters.AddWithValue("@cid", sif);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection.Open();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally { cmd.Connection.Close(); }
        }
    }
}
