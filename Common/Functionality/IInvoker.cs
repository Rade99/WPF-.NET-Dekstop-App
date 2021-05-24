using Common.DAO;
using Common.ResidentContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Functionality
{
    public interface IInvoker
    {
        List<bool> UspenoIzvrseneFunkcije { get; set; }

        void InvokeResident(IDAOMerenje merenje, ICsvInput csvParams,IDAORezultat daorezultat);

        void InvokeSingle(DateTime datum, string sfr, int brFunkcije, IDAOMerenje daomerenje, IDAORezultat daorezultat);
    }
}
