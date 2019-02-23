using Bicytal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bicytal.Controllers
{
    public class HomeController : Controller
    {
        TicketContext db = new TicketContext();
        // GET: Home
        public ActionResult Index()
        {
            if(db.Permits.ToList().Count() == 0)
            {
                Permit permit = new Permit();
                Permit permit1 = new Permit();
                Permit permit2 = new Permit();
                permit.Type = "Yellow";
                permit.Quarter_price = 70;
                permit.HalfYear_price = 120;
                permit.Year_price = 200;
                permit.Description = "description goes here";
                db.Permits.Add(permit);
                db.SaveChanges();
                permit1.Type = "Blue";
                permit1.Quarter_price = 90;
                permit1.HalfYear_price = 160;
                permit1.Year_price = 300;
                permit1.Description = "description goes here";
                db.Permits.Add(permit1);
                db.SaveChanges();
                permit2.Type = "Red";
                permit2.Quarter_price = 280;
                permit2.HalfYear_price = 450;
                permit2.Year_price = 800;
                permit2.Description = "description goes here";
                db.Permits.Add(permit2);
                db.SaveChanges();
                }
                return View(db.Permits.ToList());
        }

        //GET: Contact
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        //POST: Contact
        [HttpPost]
        public ActionResult Contact(int i=0)
        {
            return View("ContactOk");
        }

        //GET About
        public ActionResult About()
        {
            return View();
        }
    }
}