using Common.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Functionality
{
    public interface IFunctions
    {
         bool Execute(List<IMerenje> merenja, IDAOMerenje daomerenje, IDAORezultat daorezultat);
    }
}
