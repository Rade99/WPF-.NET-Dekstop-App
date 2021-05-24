using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ResidentContract
{
    public interface ICsvInput
    {
        List<string> csvPodrucja { get; set; }
        Dictionary<int, int> csvFunkcije { get; set; }

        
    }
}
