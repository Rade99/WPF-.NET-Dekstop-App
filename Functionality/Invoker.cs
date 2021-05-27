using Common.DAO;
using Common.Functionality;
using Common.ResidentContract;
using DAO;
using DAO.Exceptions;
using Functionality.Exceptions;
using ResidentExecutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Functionality
{
    public class Invoker : IInvoker
    {
        List<bool> uspenosIzvrsenFunkcije = new List<bool>();

        public List<bool> UspenoIzvrseneFunkcije { get => uspenosIzvrsenFunkcije; set => uspenosIzvrsenFunkcije = value; }
        //moramo nesto proveravati pa neka to bude uspesnost izvrsenja funkcioje execute


        public void InvokeResident(IDAOMerenje daomerenje, ICsvInput csvParams, IDAORezultat daorezultat)
        {
           
            List<IFunctions> funkcije = new List<IFunctions>();

            foreach (var item in csvParams.csvFunkcije)
            {
                if (item.Key == 1 && item.Value == 1)
                {
                    funkcije.Add(new FunctionMin());
                }
                else if (item.Key == 2 && item.Value == 1)
                {
                    funkcije.Add(new FunctionMax());

                }
                else if (item.Key == 3 && item.Value == 1)
                {
                    funkcije.Add(new FunctionDevijacija());
                }
            }

                foreach (var nazivPodrucja in csvParams.csvPodrucja)
                {
                    List<IMerenje> merenja = daomerenje.GetMerenjaPoDanuIVremenu(DateTime.Now.Date, nazivPodrucja); 
                if (merenja.Count == 0)
                        continue;
                    foreach (var fun in funkcije)
                    {
                        UspenoIzvrseneFunkcije.Add(fun.Execute(merenja, daomerenje,daorezultat));
                        Thread.Sleep(5000);
                    }
                }

            
        }

        public void InvokeSingle(DateTime datum, string sfr, int brFunkcije, IDAOMerenje daomerenje,IDAORezultat daorezultat)
        {
            if(brFunkcije <1 || brFunkcije >3)
            {
                throw new NeposotojeciBrojFunkcijeException();
            }

            List<IMerenje> merenja = daomerenje.GetMerenjaPoDanuIVremenu(datum, sfr);
            if (brFunkcije == 1)
            {
                FunctionMin min = new FunctionMin();
                min.Execute(merenja, daomerenje, daorezultat);
            }
            else if (brFunkcije == 2)
            {
                FunctionMax max = new FunctionMax();
                max.Execute(merenja, daomerenje, daorezultat);
            }
            else if (brFunkcije == 3)
            {
                FunctionDevijacija dev = new FunctionDevijacija();
                dev.Execute(merenja, daomerenje, daorezultat);
            }
        }
    }
}
