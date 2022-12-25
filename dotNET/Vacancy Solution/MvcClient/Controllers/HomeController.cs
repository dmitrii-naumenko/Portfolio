using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcClient.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Online()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Online(Models.SearchResponce searchResponce)
		{
			using (var service = new VacancyServiceReference.VacancyServiceClient())
			{
				var vacancies = service.GetOnlineVacancies(searchResponce.SearchText);
				return View("List", vacancies);
			}
		}

		[HttpGet]
		public ActionResult Local()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Local(Models.SearchResponce searchResponce)
		{
			using (var service = new VacancyServiceReference.VacancyServiceClient())
			{
				var vacancies = service.GetLocalVacancies(searchResponce.SearchText);
				return View("List", vacancies);
			}
		}

	}
}