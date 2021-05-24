using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DAO
{
    public interface IDAOPodrucja
    {
        List<string> GetSveSifre();
        string GetSifreByNazivPodrucja(string nzv);
        List<string> GetSvaPodrucja();
        void UpisNovogPodrucja(string sifra, string nazivPodrucja);

    }
}
