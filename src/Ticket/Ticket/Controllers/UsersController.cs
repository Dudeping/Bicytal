using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ticket.Models;

namespace Ticket.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        TicketContext servicedb = new TicketContext();
        // GET: User
        //Personal Details
        [HttpGet]
        public ActionResult Index()
        {
            var userName = User.Identity.Name;
            var data = servicedb.Users.Where(p => p.UserName == userName).FirstOrDefault();

            return View(data);
        }

        //GET
        [HttpGet]
        public ActionResult EidtMyDetail()
        {
            var userName = User.Identity.Name;
            var data = servicedb.Users.Where(p => p.UserName == userName).FirstOrDefault();

            return View(data);
        }

        //POST
        [HttpPost]
        public ActionResult EidtMyDetail(User user)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                var data = servicedb.Users.Where(p => p.UserName == userName).FirstOrDefault();
                data.Address = user.Address;
                data.GName = user.GName;
                data.Mobile = user.Mobile;
                data.PostCode = user.PostCode;
                data.SName = user.SName;
                data.State = user.State;
                servicedb.SaveChanges();
                return Content("<script>alert('OK！');location.href='/Users/Index'</script>");
            }

            return View(user);
        }

        //GET
        public ActionResult MyPurchases()
        {
            var userName = User.Identity.Name;
            var data = from x in servicedb.Purchases where x.UserName.UserName == userName orderby x.PId descending select x;

            return View(data.ToList());
        }
        
        public ActionResult MyPurchasesDetail(int pid)
        {
            var data = servicedb.Purchases.Where(p => p.PId == pid);
            return View(data.FirstOrDefault());
        }
        
        public ActionResult MakePurchase()
        {
            return View(servicedb.Permits.ToList());
        }
        
        [HttpGet]
        public ActionResult ConfirmMake(string type, int priceType)
        {
            var userName = User.Identity.Name;
            var userData = servicedb.Users.Find(userName);
            var permitData = servicedb.Permits.Find(type);
            ViewBag.userData = userData;
            ViewBag.permitData = permitData;
            ViewBag.priceType = priceType;
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmMake()
        {
            var permitType = Request["permitType"];
            var priceType = Request["priceType"];

            Purchase purchase = new Purchase();
            var type = servicedb.Permits.Find(permitType);
            purchase.Type = type;

            string user = User.Identity.Name;
            var userName = servicedb.Users.Find(user);
            purchase.UserName = userName;

            purchase.StartDate = DateTime.Now;
            var permit = servicedb.Permits.Find(permitType);

            switch (Int32.Parse(priceType))
            {
                case 0:
                    purchase.Duration = 3;
                    purchase.Cost = permit.Quarter_price;
                    break;

                case 1:
                    purchase.Duration = 6;
                    purchase.Cost = permit.HalfYear_price;
                    break;

                case 2:
                    purchase.Duration = 12;
                    purchase.Cost = permit.Year_price;
                    break;
            }

            purchase.PTime = DateTime.Now;

            servicedb.Purchases.Add(purchase);
            servicedb.SaveChanges();

            return Content("<script>alert('OK！');location.href='/Users/MyPurchases'</script>");
        }
    }
}
