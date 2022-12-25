using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace NationalCriminalDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HcSearchService Host");
            ServiceHost host = null;
            try
            {
                host = new ServiceHost(typeof (NcSearchService));
                host.Open();
                Console.WriteLine("Started...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
            if (host != null)
                host.Close();
        }
    }
}
