using Common.DAO;
using Common.Functionality;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using Functionality.Exceptions;
using System.Text;
using System.Threading.Tasks;
using DAO.Exceptions;

namespace Functionality
{
    public class FunctionMin : IFunctions
    {


        PoslednjeMerenje poslednjeMerenje = new PoslednjeMerenje();
        public bool Execute(List<IMerenje> merenja, IDAOMerenje daomerenje,IDAORezultat daorezultat)
        {
            float minimum;

            try
            {
                 minimum = merenja[0].potrosnja;
            }
            catch(Exception)
            {
                throw new ListaMerenjaPraznaException();
            }
            

            foreach (var merenje in merenja)
            {
                if (merenje.potrosnja < 0)
                {
                    throw new NegativnaPotrosnjaException();
                }

                if (merenje.potrosnja < minimum)
                {
                    minimum = merenje.potrosnja;
                }
            }

            DateTime poslMerenje = daomerenje.GetPoslednjeMerenje(merenja[0].sifraPodrucija,merenja[0].vreme);

            if(poslednjeMerenje.ProveraPreUpisa(merenja[0].sifraPodrucija, poslMerenje,1,daomerenje,daorezultat) ==true)
            {
                daorezultat.UpisMin(merenja[0].sifraPodrucija, merenja[0].nazivPodrucija, DateTime.Now, poslMerenje, minimum);
                return true;
            }

            return false;
        }
    
    }
}
