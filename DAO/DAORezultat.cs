using Common.DAO;
using DAO.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAORezultat : IDAORezultat
    {
        string lokacijaDB = @"C:\fax\res\ProjacSaTestovima\FinalFinalRES\DAO\DB\DB.db";
        private List<IRezultat> rezultati;

        public List<IRezultat> Rezultati { get => rezultati; set => rezultati = value; }

        public List<IRezultat> GetRezultat()
        {
            List<IRezultat> rezultati = new List<IRezultat>();


            using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {

                    cmd.CommandText = "select * from Rezultati";

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Rezultat rezultat = new Rezultat();

                            rezultat.sifraPodrucija = reader.GetString(0);
                            rezultat.nazivPodrucija = reader.GetString(1);


                            rezultat.dan = reader.GetDateTime(2);// ovo je mnogo bolje

                            rezultat.poslednjeMerenje = reader.GetDateTime(3);

                            if (!reader.IsDBNull(4))
                            {
                                rezultat.minimum = reader.GetFloat(4);
                            }

                            if (!reader.IsDBNull(5))
                            {
                                rezultat.maximum = reader.GetFloat(5);
                            }


                            if (!reader.IsDBNull(6))
                            {
                                rezultat.devijacija = reader.GetFloat(6);
                            }

                            rezultati.Add(rezultat);
                        }
                    }
                }
            }
            Rezultati = rezultati;
            return rezultati;
        }

        public void UpisDevijacija(string sifra, string nazivpod, DateTime vremeprocuna, DateTime vrememerenja, float devijacija)
        {
            ProveraNegativnogBroja(devijacija);

            bool procitaonesto;

            using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {

                    cmd.CommandText = "select * from Rezultati where sfr=@sifra and nzv=@nazivpod and date(vizv)=date(@vremeprocuna)";

                    cmd.Parameters.AddWithValue("@sifra", sifra);
                    cmd.Parameters.AddWithValue("@nazivpod", nazivpod);
                    cmd.Parameters.AddWithValue("@vremeprocuna", vremeprocuna.ToString("s"));
                    cmd.Parameters.AddWithValue("@vrememerenja", vrememerenja.ToString("s"));

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        procitaonesto = reader.HasRows;
                    }

                    if (procitaonesto == false)
                    {
                        cmd.CommandText = "insert into Rezultati(sfr,nzv,vizv,psm,dev)" +
                                    "values(@sifra,@nazivpod,@vremeprocuna,@vrememerenja,@devijacija)";

                        cmd.Parameters.AddWithValue("@sifra", sifra);
                        cmd.Parameters.AddWithValue("@nazivpod", nazivpod);
                        cmd.Parameters.AddWithValue("@vremeprocuna", vremeprocuna.ToString("s"));
                        cmd.Parameters.AddWithValue("@vrememerenja", vrememerenja.ToString("s"));
                        cmd.Parameters.AddWithValue("@devijacija", devijacija);

                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.CommandText = "update Rezultati set vizv=@vremeprocuna, psm=@vrememerenja, dev=@devijacija where sfr=@sifra and date(psm)=date(@vrememerenja)";
                        cmd.Parameters.AddWithValue("@sifra", sifra);
                        cmd.Parameters.AddWithValue("@vremeprocuna", vremeprocuna.ToString("s"));
                        cmd.Parameters.AddWithValue("@vrememerenja", vrememerenja.ToString("s"));
                        cmd.Parameters.AddWithValue("@devijacija", devijacija);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void UpisMax(string sifra, string nazivpod, DateTime vremeprocuna, DateTime vrememerenja, float maxpotrosnja)
        {
            ProveraNegativnogBroja(maxpotrosnja);

            bool procitaonesto;


            using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {

                    cmd.CommandText = "select * from Rezultati where sfr=@sifra and nzv=@nazivpod and date(vizv)=date(@vremeprocuna)";

                    cmd.Parameters.AddWithValue("@sifra", sifra);
                    cmd.Parameters.AddWithValue("@nazivpod", nazivpod);
                    cmd.Parameters.AddWithValue("@vremeprocuna", vremeprocuna.ToString("s"));
                    cmd.Parameters.AddWithValue("@vrememerenja", vrememerenja.ToString("s"));

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        procitaonesto = reader.HasRows;
                    }

                    if (procitaonesto == false)
                    {
                        cmd.CommandText = "insert into Rezultati(sfr,nzv,vizv,psm,maxm)" +
                                    "values(@sifra,@nazivpod,@vremeprocuna,@vrememerenja,@maxpotrosnja)";

                        cmd.Parameters.AddWithValue("@sifra", sifra);
                        cmd.Parameters.AddWithValue("@nazivpod", nazivpod);
                        cmd.Parameters.AddWithValue("@vremeprocuna", vremeprocuna.ToString("s"));
                        cmd.Parameters.AddWithValue("@vrememerenja", vrememerenja.ToString("s"));
                        cmd.Parameters.AddWithValue("@maxpotrosnja", maxpotrosnja);

                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.CommandText = "update Rezultati set vizv=@vremeprocuna, psm=@vrememerenja, maxm=@maxpotrosnja where sfr=@sifra and date(psm)=date(@vrememerenja)";
                        cmd.Parameters.AddWithValue("@sifra", sifra);
                        cmd.Parameters.AddWithValue("@vremeprocuna", vremeprocuna.ToString("s"));
                        cmd.Parameters.AddWithValue("@vrememerenja", vrememerenja.ToString("s"));
                        cmd.Parameters.AddWithValue("@maxpotrosnja", maxpotrosnja);

                        cmd.ExecuteNonQuery();
                    }


                }
            }
        }

        public void UpisMin(string sifra, string nazivpod, DateTime vremeprocuna, DateTime vrememerenja, float minpotrosnja)
        {
            ProveraNegativnogBroja(minpotrosnja);

            try
            {
                bool procitaonesto;

                using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
                {
                    con.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {

                        cmd.CommandText = "select * from Rezultati where sfr=@sifra and nzv=@nazivpod and date(vizv)=date(@vremeprocuna)"; //and psm=@vrememerenja";

                        cmd.Parameters.AddWithValue("@sifra", sifra);
                        cmd.Parameters.AddWithValue("@nazivpod", nazivpod);
                        cmd.Parameters.AddWithValue("@vremeprocuna", vremeprocuna.ToString("s"));
                        cmd.Parameters.AddWithValue("@vrememerenja", vrememerenja.ToString("s"));

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            procitaonesto = reader.HasRows;
                        }

                        if (procitaonesto == false)
                        {
                            cmd.CommandText = "insert into Rezultati(sfr,nzv,vizv,psm,minm)" +
                                        "values(@sifra,@nazivpod,@vremeprocuna,@vrememerenja,@minpotrosnja)";

                            cmd.Parameters.AddWithValue("@sifra", sifra);
                            cmd.Parameters.AddWithValue("@nazivpod", nazivpod);
                            cmd.Parameters.AddWithValue("@vremeprocuna", vremeprocuna.ToString("s"));
                            cmd.Parameters.AddWithValue("@vrememerenja", vrememerenja.ToString("s"));
                            cmd.Parameters.AddWithValue("@minpotrosnja", minpotrosnja);

                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = "update Rezultati set vizv=@vremeprocuna, psm=@vrememerenja, minm=@minpotrosnja where sfr=@sifra and date(psm)=date(@vrememerenja)";
                            cmd.Parameters.AddWithValue("@sifra", sifra);
                            cmd.Parameters.AddWithValue("@vremeprocuna", vremeprocuna.ToString("s"));
                            cmd.Parameters.AddWithValue("@vrememerenja", vrememerenja.ToString("s"));
                            cmd.Parameters.AddWithValue("@minpotrosnja", minpotrosnja);

                            cmd.ExecuteNonQuery();
                        }


                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public DateTime PoslednjeMerenje(string sifra, DateTime psm)
        {

            try
            {
                using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
                {
                    con.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = "select psm from Rezultati where sfr=@sifra and date(psm) = date(@psm)";

                        cmd.Parameters.AddWithValue("@sifra", sifra);
                        cmd.Parameters.AddWithValue("@psm", psm);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            return reader.GetDateTime(0);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new SifraNijePronadjenaException(); // ovako treba
            }


        }

        public bool PostojiMerenjeZaDatuSifruTogDana(string sifra, DateTime dt)
        {

            try
            {
                using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
                {
                    con.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = "select * from Rezultati where sfr=@sifra and date(psm) = date(@psm)";

                        cmd.Parameters.AddWithValue("@sifra", sifra);
                        cmd.Parameters.AddWithValue("@psm", dt);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
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
            catch (Exception)
            {
                throw new SifraNijePronadjenaException(); // ovako treba
            }


        }

        private void ProveraNegativnogBroja(float num)
        {
            if (num < 0)
            {
                throw new NegativnaPotrosnjaException();
            }
        }

        public bool ProveraPraznoPoljeZaPotrosnju(string sifra, DateTime vrememerenja, int mmd) 
        {

            using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    

                    if(mmd==1)
                    {
                       
                        cmd.CommandText = "select minm from Rezultati where sfr=@sifra and date(psm) = date(@psm)";

                        cmd.Parameters.AddWithValue("@sifra", sifra);
                        cmd.Parameters.AddWithValue("@psm", vrememerenja);
                    }
                    else if(mmd==2)
                    {
                      
                        cmd.CommandText = "select maxm from Rezultati where sfr=@sifra and date(psm) = date(@psm)";

                        cmd.Parameters.AddWithValue("@sifra", sifra);
                        cmd.Parameters.AddWithValue("@psm", vrememerenja);
                    }
                    else if(mmd==3)
                    {
                        
                        cmd.CommandText = "select dev from Rezultati where sfr=@sifra and date(psm) = date(@psm)";

                        cmd.Parameters.AddWithValue("@sifra", sifra);
                        cmd.Parameters.AddWithValue("@psm", vrememerenja);
                    }


                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.GetValue(0) == DBNull.Value)
                            return false;
                        else
                            return true;
                    }
                    
                   
                }
            }
        }


    }
}


