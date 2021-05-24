using Common.DAO;
using Common.Functionality;
using Common.ResidentContract;
using DAO;
using Functionality;
using ResidentExecutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interlayer
{
    public class Medjusloj
    {
        IInvoker invoker = new Invoker();
        IDAOMerenje dAOMerenje = new DAOMerenje();
        IDAORezultat dAORezultat = new DAORezultat();
        IDAOPodrucja dAOPodrucja = new DAOPodrucja();

        public void InvokeResidentMedju()
        {
            ICitanje csvReader = new Citanje();
            ICsvInput csvInput = csvReader.GetCsvParams(); 
            invoker.InvokeResident(dAOMerenje, csvInput, dAORezultat);
        }

        public void InvokeSingleMedju(DateTime datum, string sfr, int brFunkcije)
        {
            invoker.InvokeSingle(datum, sfr, brFunkcije, dAOMerenje, dAORezultat);
        }

        public List<string> GetSvaPodrucjaMedju()
        {
            return dAOPodrucja.GetSvaPodrucja();
        }

        public List<string>  GetIzmerenaPodrucjaMedju()
        {
            return dAOMerenje.GetIzmerenaPodrucja();
        }

        public List<IMerenje> GetMerenjaPoDanuIVremenuMedju(DateTime dt, string sifra)
        {
            return dAOMerenje.GetMerenjaPoDanuIVremenu(dt, sifra);
        }

        public string GetSifreByNazivPodrucjaMedju(string nzv)
        {
            return dAOPodrucja.GetSifreByNazivPodrucja(nzv);
        }

        public List<IMerenje> GetMerenjaPoSifriPodrucijaMedju(string sifra,DateTime selectedDate)
        {
            return dAOMerenje.GetMerenjaPoSifriPodrucija(sifra, selectedDate);
        }

        public DateTime GetPoslednjeMerenjeMedju(string sifra, DateTime dv)
        {
            return dAOMerenje.GetPoslednjeMerenje(sifra, dv);
        }

        public List<string> GetSveSifreMedju()
        {
            return dAOPodrucja.GetSveSifre();
        }

        public void UpisNovogPodrucjaMedju(string sifra, string nazivPodrucja)
        {
             dAOPodrucja.UpisNovogPodrucja(sifra, nazivPodrucja);
        }

        public void UpisNovogMerenjaMedju(string sifra, DateTime vrememerenja, float potrosnja)
        {
            dAOMerenje.UpisNovogMerenja(sifra, vrememerenja, potrosnja);
        }

        public List<IRezultat> GetRezultatMedju()
        {
            return dAORezultat.GetRezultat(); 
        }


    }
}
