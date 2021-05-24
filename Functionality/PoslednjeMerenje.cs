using Common.DAO;
using Common.Functionality;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functionality
{
    public class PoslednjeMerenje : IPoslednjeMerenje
    {
        DAORezultat dAORezultat = new DAORezultat(); // dali ovo da smestim u konstruktor mozda
        DAOMerenje dAOMerenje = new DAOMerenje();

        public bool ProveraPreUpisa(string sifra, DateTime poslednjemerenje, int mmd, IDAOMerenje daomerenje, IDAORezultat daorezultat)
        {

            if(!daorezultat.PostojiMerenjeZaDatuSifruTogDana(sifra, poslednjemerenje)) // u slucaju da ne postoji ni jedno merenje u Tabeli Rezulati slobono upisuj, sprecava se excption ovog donjeg
            {
                return true;
            }
            if(!daomerenje.PostojiMerenjeZaDatuSifruTogDana(sifra, poslednjemerenje)) // ako ne postoji Merenje tog odredjnog dana u Tabeli merenja nema ni sta da proveraa ne treba ni ovo dole da proverava jer ce upasti u raspali excepiton
            {
                return false;
            }
            if(!dAORezultat.ProveraPraznoPoljeZaPotrosnju(sifra, poslednjemerenje, mmd)) // treba da upise ako je kolona null svejedno
            {
                return true;
            }
            /*if(poslednjemerenje == dAORezultat.PoslednjeMerenje(sifra, poslednjemerenje))
            {
                return false;
            }*/
            return true;
        }

    
    }
}
