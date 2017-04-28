using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace oantarija.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Conosca quienes somos...";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contactenos...";

            return View();
        }
        public ActionResult Team()
        {
            ViewBag.Message = "Conosca nuestro equipo...";

            return View();
        }
    }
}