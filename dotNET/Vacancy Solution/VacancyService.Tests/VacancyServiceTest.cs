using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VacancyService.Tests
{
	[TestClass]
	public class VacancyServiceTest
	{
		[TestMethod]
		public void GetOnlineVacanciesTest()
		{
			var srv = new VacancyService();
			var expected = 50;

			var vacancies = srv.GetOnlineVacancies("голова");

			Assert.AreEqual(expected, vacancies.Length, string.Format("Vacantion count for test request must be {0}", expected));
		}
	}
}
