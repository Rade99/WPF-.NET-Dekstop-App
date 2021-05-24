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
    public class DAOMerenje : IDAOMerenje
    {

        private List<string> izmerenaPodrucja;
        private List<IMerenje> merenjaPoDanuIVremenu;
        private List<IMerenje> merenjaPoSifriPodrucja;
        private DateTime poslednjeMerenje;
        private bool proveraUpisaNovogMerenja;
        private bool postojanjeMerenjaZaDatuSifruTogDana;
        string lokacijaDB = @"C:\Users\ShinobiCooL\Desktop\RES Projekat Unit Testovi\DAO\DB\DB.db";

        public List<string> IzmerenaPodrucja { get => izmerenaPodrucja; set => izmerenaPodrucja = value; }

        public List<IMerenje> MerenjaPoDanuIVremenu { get => merenjaPoDanuIVremenu; set => merenjaPoDanuIVremenu = value; }

        public List<IMerenje> MerenjaPoSifriPodrucja { get => merenjaPoSifriPodrucja; set => merenjaPoSifriPodrucja = value; }

        public DateTime PoslednjeMerenje { get => poslednjeMerenje; set => poslednjeMerenje = value; }

        public bool ProveraUpisaNovogMerenja { get => proveraUpisaNovogMerenja; set => proveraUpisaNovogMerenja = value; }

        public bool PostojanjeMerenjaZaDatuSifruTogDana { get => postojanjeMerenjaZaDatuSifruTogDana; set => postojanjeMerenjaZaDatuSifruTogDana = value; }

        


        public List<string> GetIzmerenaPodrucja()
        {
            List<string> temp = new List<string>();

            using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "select nzv from Podrucja where sfr in (select sfr from Merenja)";

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            temp.Add(reader.GetString(0));
                        }
                    }
                }
            }

            IzmerenaPodrucja = temp;
            return temp;
        }

        public List<IMerenje> GetMerenjaPoDanuIVremenu(DateTime dt, string sifra)
        {
            List<IMerenje> merenja = new List<IMerenje>();
            
            using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB)) 
            {
             
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {

                    cmd.CommandText = "select * from Merenja,Podrucja where date(dv)=date(@dt) and Merenja.sfr=@sifra and Merenja.sfr = Podrucja.sfr";

                    cmd.Parameters.AddWithValue("@dt", dt.ToString("s"));
                    cmd.Parameters.AddWithValue("@sifra", sifra);



                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Merenje m = new Merenje();

                            // m.SifraPodrucija = reader.GetString(0);
                            m.sifraPodrucija = reader.GetString(0);

                            m.dan = reader.GetDateTime(1);

                            m.vreme = reader.GetDateTime(1);

                            m.potrosnja = reader.GetFloat(2);

                            m.nazivPodrucija = reader.GetString(4);
                            merenja.Add(m);

                        }
                    }
                }
            }

            MerenjaPoDanuIVremenu = merenja;
            return merenja;
        }

        public List<IMerenje> GetMerenjaPoSifriPodrucija(string sifra)
        {

            List<IMerenje> merenja = new List<IMerenje>();

            using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {

                    cmd.CommandText = "select * from Merenja,Podrucja where Merenja.sfr=@sifra and Merenja.sfr = Podrucja.sfr";

                    cmd.Parameters.AddWithValue("@sifra", sifra);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Merenje m = new Merenje();

                            m.sifraPodrucija = reader.GetString(0);

                            m.dan = reader.GetDateTime(1);

                            m.vreme = reader.GetDateTime(1);

                            m.potrosnja = reader.GetFloat(2);

                            m.nazivPodrucija = reader.GetString(4);

                            merenja.Add(m);
                        }
                    }
                }
            }

            MerenjaPoSifriPodrucja = merenja;
            return merenja;
        }

        public DateTime GetPoslednjeMerenje(string sifra,DateTime dv)
        {
          
            try
            {
                using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
                {
                    con.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = "select max(dv) from Merenja where sfr=@sifra and date(dv)=date(@dv)";

                        cmd.Parameters.AddWithValue("@sifra", sifra);
                        cmd.Parameters.AddWithValue("@dv", dv);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            PoslednjeMerenje = reader.GetDateTime(0);
                            return reader.GetDateTime(0);
                        }
                    }
                }
            }
            catch(Exception)
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
                        cmd.CommandText = "select * from Merenja where sfr=@sifra and date(dv) = date(@dv)";

                        cmd.Parameters.AddWithValue("@sifra", sifra);
                        cmd.Parameters.AddWithValue("@dv", dt);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                PostojanjeMerenjaZaDatuSifruTogDana = true;
                                return true;
                            }
                            else
                            {
                                PostojanjeMerenjaZaDatuSifruTogDana = false;
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


        public void UpisNovogMerenja(string sifra, DateTime vrememerenja, float potrosnja)
        {
            using (SQLiteConnection con = new SQLiteConnection("data source=" + lokacijaDB))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "insert into Merenja(sfr,dv,potr)" +
                               "values(@sifra,@vrememerenja,@potrosnja)";

                    cmd.Parameters.AddWithValue("@sifra", sifra);
                    cmd.Parameters.AddWithValue("@vrememerenja", vrememerenja.ToString("s"));
                    cmd.Parameters.AddWithValue("@potrosnja", potrosnja);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
