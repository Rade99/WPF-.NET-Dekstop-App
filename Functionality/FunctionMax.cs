using Common.DAO;
using Common.Functionality;
using DAO;
using DAO.Exceptions;
using Functionality.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functionality
{
    public class FunctionMax : IFunctions
    {
        DAOMerenje dAOMerenje = new DAOMerenje();
        DAORezultat dAORezultat = new DAORezultat();
        PoslednjeMerenje poslednjeMerenje = new PoslednjeMerenje();
        public bool Execute(List<IMerenje> merenja, IDAOMerenje daomerenje, IDAORezultat daorezultat)
        {
             float maksimum;
            try
            {
                maksimum = merenja[0].potrosnja;
            }
            catch (Exception)
            {
                throw new ListaMerenjaPraznaException();
            }

            foreach (var merenje in merenja)
            {
                if (merenje.potrosnja < 0)
                {
                    throw new NegativnaPotrosnjaException();
                }

                if (merenje.potrosnja > maksimum)
                {
                    maksimum = merenje.potrosnja;
                }
            }
            DateTime poslMerenje = daomerenje.GetPoslednjeMerenje(merenja[0].sifraPodrucija,merenja[0].vreme);

            if (poslednjeMerenje.ProveraPreUpisa(merenja[0].sifraPodrucija, poslMerenje,2,daomerenje,daorezultat) == true)
            {
                dAORezultat.UpisMax(merenja[0].sifraPodrucija, merenja[0].nazivPodrucija, DateTime.Now, poslMerenje, maksimum);
                return true;
            }
            return false;
               
        }
    }
}
