using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib.TestClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result = new Bib.DbService.SqlBaseService("Data Source=192.168.2.10;Initial Catalog=BIBLIO;User=sysadm;Password=geheim;ServerName=server1").GetTables();
            Console.WriteLine(result);
        }
    }
}
