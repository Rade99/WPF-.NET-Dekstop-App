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
        IDAOMerenje daomerenje = new DAOMerenje();
        IDAORezultat daorezultat = new DAORezultat();

        public void InvokeResidentMedju()
        {
            ICitanje csvReader = new Citanje();
            ICsvInput csvInput = csvReader.GetCsvParams(); 
            invoker.InvokeResident(daomerenje, csvInput,daorezultat);
        }

        public void InvokeSingleMedju(DateTime datum, string sfr, int brFunkcije)
        {
            invoker.InvokeSingle(datum, sfr, brFunkcije, daomerenje, daorezultat);
        }


    }
}
