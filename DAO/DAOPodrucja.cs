using Common.DAO;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAOPodrucja : IDAOPodrucja
    {
        string lokacijaDB = @"C:\fax\res\ProjacSaTestovima\FinalFinalRES\DAO\DB\DB.db";
        public string GetSifreByNazivPodrucja(string nzv)
        {
            using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "select sfr from Podrucja where nzv=@nzv";

                    cmd.Parameters.AddWithValue("@nzv", nzv);



                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {

                        reader.Read();
                        return reader.GetString(0);
                    }
                }
            }
        }

        public bool GetProslednjenaSifra(string sfr)
        {
            using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "select * from Podrucja where sfr=@sfr";

                    cmd.Parameters.AddWithValue("@sfr", sfr);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {

                        if(reader.HasRows == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }
        }

        public List<string> GetSvaPodrucja()
        {
            List<string> temp = new List<string>();

            using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "select nzv from Podrucja";



                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            temp.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return temp;
        }

        public List<string> GetSveSifre()
        {
            List<string> temp = new List<string>();

            using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "select sfr from Podrucja";



                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            temp.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return temp;
        }

        public void UpisNovogPodrucja(string sifra, string nazivPodrucja)
        {
            using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "insert into Podrucja(sfr,nzv)" +
                               "values(@sifra,@nazivPodrucja)";

                    cmd.Parameters.AddWithValue("@sifra", sifra);
                    cmd.Parameters.AddWithValue("@nazivPodrucja", nazivPodrucja);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
