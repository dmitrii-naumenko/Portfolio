using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHost
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("ServiceHost");
			try
			{
				var host = new ServiceHost(typeof(VacancyService.VacancyService));
				host.Open();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			Console.WriteLine("Started");
			Console.ReadKey();
		}
	}
}
