using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bicytal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Request.IsAuthenticated ? View() : View("ParkingPermit");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}