using Common.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class Rezultat : IRezultat
    {
        public string sifraPodrucija { get; set; }
        public string nazivPodrucija { get; set; }
        public DateTime dan { get; set; }
        public DateTime poslednjeMerenje { get; set; }
        public float minimum { get; set; }
        public float maximum { get; set; }
        public float devijacija { get; set; }
    }
}
