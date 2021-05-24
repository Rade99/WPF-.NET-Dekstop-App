using Common.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Functionality
{
    public interface IPoslednjeMerenje
    {
        bool ProveraPreUpisa(string sifra, DateTime poslednjemerenje, int mmd, IDAOMerenje daomerenje, IDAORezultat daorezultat);
    }
}
