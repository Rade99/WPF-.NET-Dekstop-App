using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DAO
{
    public interface IDAOMerenje
    {      
        List<string> IzmerenaPodrucja { get; set; }

        List<IMerenje> MerenjaPoDanuIVremenu { get; set; }

        List<IMerenje> MerenjaPoSifriPodrucja { get; set; }

        DateTime PoslednjeMerenje { get; set; }

        bool ProveraUpisaNovogMerenja { get; set; }

        bool PostojanjeMerenjaZaDatuSifruTogDana { get; set; }

        List<IMerenje> GetMerenjaPoDanuIVremenu(DateTime dt, string sifra);

        List<IMerenje> GetMerenjaPoSifriPodrucija(string sifra);

        DateTime GetPoslednjeMerenje(string sifra, DateTime dv);

        bool PostojiMerenjeZaDatuSifruTogDana(string sifra, DateTime dt);

        void UpisNovogMerenja(string sifra, DateTime vrememerenja, float potrosnja);

        List<string> GetIzmerenaPodrucja();
    }
}
