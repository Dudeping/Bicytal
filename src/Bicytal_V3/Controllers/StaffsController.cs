using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Bicytal.Infrastructure;
using Bicytal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Bicytal.Controllers
{
    [Authorize(Roles = "Staff")]
    public class StaffsController : Controller
    {
        TicketContext db = new TicketContext();

        //GET: ManagePermits
        public ActionResult ManagePermits()
        {
            return View(db.Permits.ToList());
        }

        //POST: AddPermits
        [HttpPost]
        public ActionResult AddPermits(Permit permit)
        {
            if (!ModelState.IsValid)
            {
                string rel = "";
                foreach (var key in ModelState.Keys.ToList())
                {
                    var errors = ModelState[key].Errors.ToList();

                    foreach (var error in errors) { rel += error.ErrorMessage; }
                }
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = rel }.ToJson());
            }
            try
            {
                db.Permits.Add(permit);
                db.SaveChanges();
                return Content(new AjaxResult { state = ResultType.success.ToString() }.ToJson());
            }
            catch(Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }

        //POST: EditPermits
        [HttpPost]
        public ActionResult EditPermits(Permit model)
        {
            if(!ModelState.IsValid)
            {
                string rel = "";
                foreach (var key in ModelState.Keys.ToList())
                {
                    var errors = ModelState[key].Errors.ToList();

                    foreach (var error in errors) { rel += error.ErrorMessage; }
                }
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = rel }.ToJson());
            }
            Permit permit = db.Permits.Find(model.Type);
            if(permit == null)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Not find this ticket!" }.ToJson());
            }
            try
            {
                permit.Description = model.Description;
                permit.HalfYear_price = model.HalfYear_price;
                permit.Quarter_price = model.Quarter_price;
                permit.Year_price = model.Year_price;
                db.SaveChanges();
                return Content(new AjaxResult { state = ResultType.success.ToString() }.ToJson());
            }
            catch(Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }

        //POST: DeletePermits
        [HttpPost]
        public ActionResult DeletePermits(string pid)
        {
            Permit permit = db.Permits.Find(pid);
            if(permit == null)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Not find this ticket!" }.ToJson());
            }
            try
            {
                db.Permits.Remove(permit);
                db.SaveChanges();
                return Content(new AjaxResult { state = ResultType.success.ToString() }.ToJson());
            }
            catch(Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }

        public ActionResult PlotChart(string type = "Column", string dim = "2D")
        {
            ViewBag.plotType = type;
            ViewBag.dim = dim;
            var permitType = db.Permits.ToList();
            ViewBag.permitType = permitType;

            Dictionary<string, int> PurNum = new Dictionary<string, int>();
            foreach (var item in permitType)
            {
                int num = db.Purchases.Where(p => p.Type.Type == item.Type).Count();
                PurNum.Add(item.Type, num);
            }
            ViewBag.PurNum = PurNum;

            return View();
        }

    }
}