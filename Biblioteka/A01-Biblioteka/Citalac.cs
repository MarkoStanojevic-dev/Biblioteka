using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace A01_Biblioteka
{
    internal class Citalac
    {
        public int Sifra { get; set; }
        public string JMBG { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }

        public static List<Citalac> UcitajSve()
        {
            SqlCommand cmd = Konekcija.GetCommand();
            cmd.CommandText = "uspCitalac";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection.Open();
                da.Fill(dt);
                List<Citalac> lista = new List<Citalac>();
                foreach(DataRow dr in dt.Rows)
                {
                    Citalac c = new Citalac();
                    c.Sifra = (int)dr[0];
                    c.JMBG = (string)dr[1];
                    c.Ime = (string)dr[2];
                    c.Prezime = (string)dr[3];
                    if (dr[4]!=DBNull.Value) c.Adresa = (string)dr[4];
                    lista.Add(c);
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool Upisi()
        {
            SqlCommand cmd = Konekcija.GetCommand();
            cmd.CommandText = "uspUpisiCitaoca";
            cmd.Parameters.AddWithValue("@jmbg", this.JMBG);
            cmd.Parameters.AddWithValue("@ime", this.Ime);
            cmd.Parameters.AddWithValue("@prezime", this.Prezime);
            cmd.Parameters.AddWithValue("@adresa", this.Adresa);
            try
            {
                cmd.Connection.Open();
                int br= cmd.ExecuteNonQuery();
                if (br == 1) return true;
                else throw new Exception("nije upisano");
            }
            catch (Exception ex)
            {
                return false;
            }
            finally { cmd.Connection.Close(); }
        }

        public override string ToString()
        {
            return string.Format("{0}-{1} {2}", this.Sifra, this.Ime, this.Prezime);
        }
    }
}
