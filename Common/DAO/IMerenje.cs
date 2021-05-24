using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DAO
{
    public interface IMerenje
    {
        string sifraPodrucija { get; set; }
        string nazivPodrucija { get; set; }
        DateTime dan { get; set; }
        DateTime vreme { get; set; }
        float potrosnja { get; set; }
    }
}
