using Common.ResidentContract;
using DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidentExecutor
{
    public class Citanje : ICitanje
    {
        public ICsvInput GetCsvParams()
        {

            ICsvInput temp = new CsvInput();
            DAOPodrucja daoPodrucja = new DAOPodrucja();


            //string path = @"..\..\..\ResidentExecutor\CSV_file\rezidentne_funkcije.csv";
            string path = @"C:\fax\res\ProjacSaTestovima\FinalFinalRES\ResidentExecutor\CSV_file\rezidentne_funkcije.csv";
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    if (values[1] == "UKLJUCENO")
                        continue;
                    if (values[0] != "" && values[1] != "")
                    {
                        if (Int32.Parse(values[0]) < 1 || Int32.Parse(values[0]) > 3)
                        {
                            Exception e = new Exception("Id funkcije nije u granicama od 1 do 3");
                            throw e;

                        }

                        if (Int32.Parse(values[1]) < 0 || Int32.Parse(values[1]) > 1)
                        {
                            Exception e = new Exception("Uklj/isklj funkcije nije u granicama od 1 do 3");
                            throw e;

                        }

                        temp.csvFunkcije.Add(Int32.Parse(values[0]), Int32.Parse(values[1]));
                    }
                    if (values[2] != null && values[2] != "" && daoPodrucja.GetProslednjenaSifra(values[2]))
                        temp.csvPodrucja.Add(values[2]);
                }
            }

            return temp;
        }
    }
}
