using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DAO
{
    public interface IRezultat
    {
        string sifraPodrucija { get; set; }
        string nazivPodrucija { get; set; }
        DateTime dan { get; set; }
        DateTime poslednjeMerenje { get; set; }
        float minimum { get; set; }
        float maximum { get; set; }
        float devijacija { get; set; }
    }
}
