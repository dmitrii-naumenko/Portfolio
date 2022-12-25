using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWebClient.SearchService;
using SearchParameter = MvcWebClient.Models.SearchParameter;

namespace MvcWebClient.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ViewResult SearchPage()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ViewResult SearchPage(SearchParameter parameter)
        {
            if (ModelState.IsValid)
            {

                var par = new SearchService.SearchParameter
                {
                    FirstName = parameter.FirstName
                };

                using (var service = new NcSearchServiceClient())
                {
                    if (service.Search(par, "user.testservice@yandex.ru", 100) == true)
                        return View("Processing");
                    else
                        return View("Error");
                }
            }
            else
                return View();
        }

    }
}
