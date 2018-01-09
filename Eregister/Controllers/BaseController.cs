using Eregister.App_Start;
using Eregister.Models;
using Microsoft.AspNet.Identity;
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
                // var userId = UserManager.FindByNameAsync(username); //UserManager.FindByNameAsync(username).ToString();
                var userId = User.Identity.GetUserId();

                if (!string.IsNullOrEmpty(username))
                {
                    var user = context.Users.SingleOrDefault(u => u.UserName == username); //walic po userId nie userLogin
                    // Student student = new Student();
                    int notsId = 0;

                    if (user.Teacher != null)
                    {
                        var userNots = context.Alerts.Where(x => x.TeacherUserId == userId).ToList();

                      //  var alerts = context.AlertContents.Where(a => a.AlertContentID == notsId).ToList();

                        List<string> notificationsContent = new List<string>();

                        foreach (var i in userNots)
                        {
                            if (i.IsOn==true)
                            {
                                notsId = i.AlertContentID;

                                var alert = context.AlertContents.Where(a => a.AlertContentID == notsId).FirstOrDefault().Content;
                                var alertDate = i.AddDate;
                                string dateFormatted = "Brak daty";
                              //  if (alertDate!=null) dateFormatted = alertDate.ToString();
                                if (alertDate!=null) dateFormatted = alertDate.Value.ToString("ddMMyyyyHHmm");
                                string content = dateFormatted + " " + alert;
                                notificationsContent.Add(content);
                            }
                        }
                        if (notificationsContent.Count > 0)
                        {
                            ViewData.Add("NotificationsCount", notificationsContent.Count);

                            ViewData.Add("NotificationsContent", notificationsContent);
                        }
                        else ViewData["NotificationsCount"]=null;


                    }
                    //else if (user.Student != null)
                    //{
                    //    if (user.Student.AlertID != 0)
                    //    {
                    //        notsId = user.Student.AlertID.Value;
                    //        var isOn = user.Student.Alert.IsOn;
                    //        var notsCount = user.Student.Alert.Count;

                    //        if (notsId != 0 && isOn == true && notsCount > 0)//|| notsId != null)
                    //        {
                    //            var alerts = context.AlertContents.Where(a => a.AlertContentID == notsId).ToList();
                    //            var notificationsCount = alerts.Count;
                    //            List<string> notificationsContent = new List<string>();
                    //            foreach (var item in alerts)
                    //            {
                    //                notificationsContent.Add(item.Content);
                    //            }
                    //            if (isOn == true && notsCount > 0) ViewData.Add("NotificationsCount", 9);// notificationsCount);

                    //            ViewData.Add("NotificationsContent", notificationsContent);
                    //        }
                    //    }
                    //}


                    string fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
                    string joinDate = user.JoinDate.ToString();

                    string mySkin = user.CustomSkin;
                    mySkin = "~/Views/Shared/_AdminTemplate.cshtml";

                    ViewData.Add("FullName", fullName);
                    ViewData.Add("FirstName", user.FirstName);
                    ViewData.Add("LastName", user.LastName);
                    ViewData.Add("JoinDate", joinDate);
                    ViewData.Add("UserRank", user.Roles.FirstOrDefault());
                    ViewData.Add("Skin", mySkin);

                }
            }
            base.OnActionExecuted(filterContext);
        }
    }
}
