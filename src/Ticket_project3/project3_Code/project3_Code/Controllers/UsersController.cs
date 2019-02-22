using project3_Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project3_Code.Controllers
{
    [Authorize(Roles = "User")]
    public class UsersController : Controller
    {
        private TicketContext db = new TicketContext();

        //GET: MakePurchase
        public ActionResult MakePurchase()
        {
            if (db.Permits.ToList().Count() == 0)
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

        //GET: pDetail
        public ActionResult pDetail()
        {
            var userName = User.Identity.Name;
            var data = db.Users.Where(p => p.UserName == userName).FirstOrDefault();

            return View(data);
        }

        //GET: pEdit
        [HttpGet]
        public ActionResult pEdit()
        {
            string userName = User.Identity.Name;

            return View(db.Users.Find(userName) ?? new User());
        }

        [HttpPost]
        public ActionResult pEdit(User user)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                var data = db.Users.Where(p => p.UserName == userName).FirstOrDefault();
                data.Address = user.Address;
                data.GName = user.GName;
                data.Mobile = user.Mobile;
                data.PostCode = user.PostCode;
                data.SName = user.SName;
                data.State = user.State;
                db.SaveChanges();
                return View("pEditOk");
            }
            string rel = "";
            foreach (var key in ModelState.Keys.ToList())
            {
                var errors = ModelState[key].Errors.ToList();

                foreach (var error in errors) { rel += error.ErrorMessage ; }
            }
            ViewBag.ErrorMessage = rel;
            return View(user);
        }

        //GET: pHistory
        public ActionResult pHistory()
        {
            var userName = User.Identity.Name;
            var data = from x in db.Purchases where x.UserName.UserName == userName orderby x.PId descending select x;

            return View(data.ToList());
        }

        //GET:Confirm
        [HttpGet]
        public ActionResult Confirm(string type, int priceType)
        {
            var userName = User.Identity.Name;
            var userData = db.Users.Find(userName);
            var permitData = db.Permits.Find(type);
            ViewBag.userData = userData;
            ViewBag.permitData = permitData;
            ViewBag.priceType = priceType;
            return View();
        }

        //GET: Confirm
        [HttpPost]
        public ActionResult Confirm(string permitType, string priceType)
        {

            Purchase purchase = new Purchase();
            var type = db.Permits.Find(permitType);
            purchase.Type = type;

            string user = User.Identity.Name;
            var userName = db.Users.Find(user);
            purchase.UserName = userName;

            purchase.StartDate = DateTime.Now;
            var permit = db.Permits.Find(permitType);

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

            db.Purchases.Add(purchase);
            db.SaveChanges();

            return View("ConfirmOk");
        }
    }
}