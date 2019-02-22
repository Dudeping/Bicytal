using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using project3_Code.Infrastructure;
using project3_Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace project3_Code.Controllers
{
    public class AccountController : Controller
    {
        private TicketContext db = new TicketContext();
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //POST: Login
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string username, string password, string code)
        {
            if (username == "")
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Please enter your email address." }.ToJson());

            if (password == "")
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Please enter the login password." }.ToJson());

            if (code == "")
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Please enter the VerCode." }.ToJson());

            if (Session["nfine_session_verifycode"].IsEmpty() || Md5.md5(code.ToLower(), 16) != Session["nfine_session_verifycode"].ToString())
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "VerCode error." }.ToJson());

            AppUser user = await UserManager.FindAsync(username, password);
            if(user == null)
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "user does not exist." }.ToJson());


            var claimsIdentity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthManager.SignOut();
            AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, claimsIdentity);

            return Content(new AjaxResult { state = ResultType.success.ToString() }.ToJson());
        }

        // GET: Register
        [HttpGet]
        public async Task<ActionResult> Register()
        {
            if(RoleManager.Roles.Count() == 0)
            {
                await RoleManager.CreateAsync(new AppRole("Staff"));
                await RoleManager.CreateAsync(new AppRole("User"));
                await UserManager.CreateAsync(new AppUser { Email = "permits@westernsydney.edu.au", UserName = "permits@westernsydney.edu.au" }, "Pa$$word1");
                AppUser user = await UserManager.FindByEmailAsync("permits@westernsydney.edu.au");
                AppRole role = await RoleManager.FindByNameAsync("Staff");
                await UserManager.AddToRoleAsync(user.Id, role.Name);
            }
            return View();
        }

        //POST: Register
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(string username, string password, string code)
        {
            if(username == "")
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Please enter your email address." }.ToJson());

            if (password == "")
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Please enter the login password." }.ToJson());

            if (code == "")
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Please enter the VerCode." }.ToJson());

            if (Session["nfine_session_verifycode"].IsEmpty() || Md5.md5(code.ToLower(), 16) != Session["nfine_session_verifycode"].ToString())
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "VerCode error." }.ToJson());

            IdentityResult result = await UserManager.CreateAsync(new AppUser { Email = username, UserName = username }, password);
            
            if(result.Succeeded)
            {
                AppRole role = await RoleManager.FindByNameAsync("User");
                AppUser user = await UserManager.FindByEmailAsync(username);
                result = await UserManager.AddToRoleAsync(user.Id, role.Name);
                if(result.Succeeded)
                {
                    User _user = new Models.User();
                    _user.UserName = username;
                    db.Users.Add(_user);
                    db.SaveChanges();

                    return Content(new AjaxResult { state = ResultType.success.ToString() }.ToJson());
                }
                else
                    return Content(new AjaxResult { state = ResultType.error.ToString(), message = AddErrorsFromResult(result) }.ToJson());
            }
            else
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = AddErrorsFromResult(result) }.ToJson());
        }

        //POST: Logout
        [HttpPost]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            AuthManager.SignOut();

            return Redirect("/");
        }

        //GET: GetAuthCode
        [HttpGet]
        public ActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }

        private string AddErrorsFromResult(IdentityResult result)
        {
            string ret = "";
            foreach (string error in result.Errors)
            {
                ret += error;
            }
            return ret;
        }

        private IAuthenticationManager AuthManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }

        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }
    }
}