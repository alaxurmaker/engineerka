using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eregister.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //do testow
        public ActionResult Success()
        {
            ViewBag.Message = "Powiodło się";

            return View();
        }
        //do testow
        public ActionResult Fail()
        {
            ViewBag.Message = "Nie udało się";

            return View();
        }
    }
}