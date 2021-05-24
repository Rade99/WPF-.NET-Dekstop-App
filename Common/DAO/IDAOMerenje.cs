using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DAO
{
    public interface IDAOMerenje
    {      
       

        List<IMerenje> GetMerenjaPoDanuIVremenu(DateTime dt, string sifra);

        List<IMerenje> GetMerenjaPoSifriPodrucija(string sifra, DateTime datu);

        DateTime GetPoslednjeMerenje(string sifra, DateTime dv);

        bool PostojiMerenjeZaDatuSifruTogDana(string sifra, DateTime dt);

        void UpisNovogMerenja(string sifra, DateTime vrememerenja, float potrosnja);

        List<string> GetIzmerenaPodrucja();
    }
}
