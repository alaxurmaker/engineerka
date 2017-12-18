using Eregister.App_Start;
using Eregister.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eregister.Controllers
{
    public class BaseController : Controller
    {
        public BaseController() { }

        public BaseController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var context = new ApplicationDbContext();
                var username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                {
                    var user = context.Users.SingleOrDefault(u => u.UserName == username);
                    string fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
                    string joinDate = user.JoinDate.ToString();

                    ViewData.Add("FullName", fullName);
                    ViewData.Add("FirstName", user.FirstName);
                    ViewData.Add("LastName", user.LastName);
                    ViewData.Add("JoinDate", joinDate);
                    ViewData.Add("UserRank", user.Roles.FirstOrDefault());
                }
            }
            base.OnActionExecuted(filterContext);
        }
    }
}
