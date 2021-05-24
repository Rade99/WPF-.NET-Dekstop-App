using Common.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class Merenje : IMerenje
    {
        public string sifraPodrucija { get; set; }
        public string nazivPodrucija { get; set; }
        public DateTime dan { get; set; }
        public DateTime vreme { get; set; }
        public float potrosnja { get; set; }
    }
}
