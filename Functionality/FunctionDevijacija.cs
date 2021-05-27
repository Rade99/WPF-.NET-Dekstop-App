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
    public class FunctionDevijacija : IFunctions
    {
       
        PoslednjeMerenje poslednjeMerenje = new PoslednjeMerenje();
        public bool Execute(List<IMerenje> merenja,IDAOMerenje daomerenje,IDAORezultat daorezultat)
        {
            float devijacija = GetMax(merenja) - GetMin(merenja);

            DateTime poslMerenje = daomerenje.GetPoslednjeMerenje(merenja[0].sifraPodrucija,merenja[0].vreme);
            if (poslednjeMerenje.ProveraPreUpisa(merenja[0].sifraPodrucija, poslMerenje,3, daomerenje,daorezultat) == true)
            {
                daorezultat.UpisDevijacija(merenja[0].sifraPodrucija, merenja[0].nazivPodrucija, DateTime.Now, poslMerenje, devijacija);
                return true;
            }

            return false;
                
        }

        private float GetMin(List<IMerenje> merenja)
        {
            float minimum = merenja[0].potrosnja;

            foreach (var merenje in merenja)
            {

                if (merenje.potrosnja < minimum)
                {                  
                    minimum = merenje.potrosnja;
                    
                }
            }

            return minimum;
        }

        private float GetMax(List<IMerenje> merenja)
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
                /*if (merenje.potrosnja < 0)
                {
                    throw new NegativnaPotrosnjaException();
                }*/

                if (merenje.potrosnja > maksimum)
                {
                    maksimum = merenje.potrosnja;
                }
            }

            return maksimum;
        }

    }
}
