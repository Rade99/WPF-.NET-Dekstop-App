using Common.ResidentContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidentExecutor
{
    public class CsvInput : ICsvInput
    {
        //List<string> csvPodrucja = new List<string>();
        //Dictionary<int, int> csvFunkcije = new Dictionary<int, int>();

        //public List<string> CsvPodrucja { get => csvPodrucja; set => csvPodrucja = value; }
        //public Dictionary<int, int> CsvFunkcije { get => csvFunkcije; set => csvFunkcije = value; }
        public List<string> csvPodrucja { get; set; }
        public Dictionary<int, int> csvFunkcije { get; set; }

        public CsvInput()
        {
            csvFunkcije = new Dictionary<int, int>();
            csvPodrucja = new List<string>();
        }
    }
}
