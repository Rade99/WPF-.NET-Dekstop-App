using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DAO
{
    public interface IDAORezultat
    {
       // public List<IRezultat> Rezultati;
        void UpisMin(string sifra, string nazivpod, DateTime vremeprocuna, DateTime vrememerenja, float minpotrosnja);
        void UpisMax(string sifra, string nazivpod, DateTime vremeprocuna, DateTime vrememerenja, float maxpotrosnja);
        void UpisDevijacija(string sifra, string nazivpod, DateTime vremeprocuna, DateTime vrememerenja, float devijacija);
        bool ProveraPraznoPoljeZaPotrosnju(string sifra, DateTime vrememerenja, int mmd);
        bool PostojiMerenjeZaDatuSifruTogDana(string sifra, DateTime poslednjemerenje);
        List<IRezultat> GetRezultat();
    }
}
