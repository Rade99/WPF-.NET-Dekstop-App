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
        
        public List<string> csvPodrucja { get; set; }
        public Dictionary<int, int> csvFunkcije { get; set; }

        public CsvInput()
        {
            csvFunkcije = new Dictionary<int, int>();
            csvPodrucja = new List<string>();
        }
    }
}
