using Eregister.App_Start;
using Eregister.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                var userId = User.Identity.GetUserId();

                if (!string.IsNullOrEmpty(userId))
                {
                    var user = context.Users.SingleOrDefault(u => u.Id == userId);
                    int notsId = 0;

                    if (user.Teacher != null)
                    {
                        var userNots = context.Alerts.Where(x => x.TeacherUserId == userId).ToList();
                        List<string> notificationsContent = new List<string>();

                        foreach (var i in userNots)
                        {
                            if (i.IsOn == true)
                            {
                                notsId = i.AlertContentID;

                                var alert = context.AlertContents.Where(a => a.AlertContentID == notsId).FirstOrDefault().Content;
                                var alertDate = i.AddDate;
                                string dateFormatted = "Brak daty";
                                if (alertDate != null)
                                {
                                    var blgCtrl = new BlogController();
                                    var dateNow = blgCtrl.TimeAgo(i.AddDate.Value);
                                    dateFormatted = dateNow;
                                }
                                string content = dateFormatted + " |" +
                                    " " + alert;
                                notificationsContent.Add(content);
                            }
                        }
                        if (notificationsContent.Count > 0)
                        {
                            ViewData.Add("NotificationsCount", notificationsContent.Count);
                            ViewData.Add("NotificationsContent", notificationsContent);
                        }
                        else ViewData["NotificationsCount"] = null;


                    }
                    else if (user.Student != null)
                    {
                        var userNots = context.Alerts.Where(x => x.StudentUserId == userId).ToList();

                        List<string> notificationsContent = new List<string>();

                        foreach (var i in userNots)
                        {
                            if (i.IsOn == true)
                            {
                                notsId = i.AlertContentID;
                                var alert = context.AlertContents.Where(a => a.AlertContentID == notsId).FirstOrDefault().Content;
                                var alertDate = i.AddDate;
                                string dateFormatted = "Brak daty";
                                if (alertDate != null)
                                {
                                    var blgCtrl = new BlogController();
                                    var dateNow = blgCtrl.TimeAgo(i.AddDate.Value);
                                    dateFormatted = dateNow;
                                }
                                string content = dateFormatted + " | " + alert;
                                notificationsContent.Add(content);
                            }
                        }
                        if (notificationsContent.Count > 0)
                        {
                            ViewData.Add("NotificationsCount", notificationsContent.Count);
                            ViewData.Add("NotificationsContent", notificationsContent);
                        }
                        else ViewData["NotificationsCount"] = null;
                    }

                    var userMsgs = context.Messages?.Where(x => x.ApplicationUser.Id == userId)?.ToList();
                    if (userMsgs != null && userMsgs.Count > 0)
                    {
                        List<string> msgsContent = new List<string>();

                        foreach (var i in userMsgs)
                        {
                            if (i.IsOn == true)
                            {
                                var msgDate = i.AddDate;
                                string dateFormatted = "Brak daty";
                                if (msgDate != null)
                                {
                                    var blgCtrl = new BlogController();
                                    var dateNow = blgCtrl.TimeAgo(i.AddDate.Value);
                                    dateFormatted = dateNow;
                                }
                                string msg = dateFormatted + ",";
                                msg += i.UserFrom + ",";
                                msg += i.Title + ",";
                                msg += i.Body;

                                msgsContent.Add(msg);
                            }
                        }
                        if (msgsContent.Count > 0)
                        {
                            ViewData.Add("MessagesCount", msgsContent.Count);
                            ViewData.Add("MessagesContent", msgsContent);
                        }
                        else ViewData["MessagesCount"] = null;
                    }



                    string fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
                    string joinDate = user.JoinDate.ToString();

                    string mySkin = "green";
                    if (!String.IsNullOrEmpty(user.CustomSkin))
                    {
                        mySkin = user.CustomSkin;
                    }
                   // mySkin = "~/Views/Shared/_AdminTemplate.cshtml";

                    ViewData.Add("FullName", fullName);
                    ViewData.Add("FirstName", user.FirstName);
                    ViewData.Add("LastName", user.LastName);
                    ViewData.Add("JoinDate", joinDate);
                    ViewData.Add("UserRank", user.Roles.FirstOrDefault());
                    ViewData.Add("Skin", mySkin);

                    var userRole = context.Users.SingleOrDefault(u => u.Id == userId).Roles.FirstOrDefault().RoleId;
                    string currentRole = "";
                    switch (userRole)
                    {
                        case "1":
                            currentRole = "Admin";
                            break;
                        case "2":
                            currentRole = "Teacher";
                            break;
                        case "3":
                            currentRole = "Parent";
                            break;
                        case "4":
                            currentRole = "Student";
                            break;
                        case "5":
                            currentRole = "Guest";
                            break;
                        case "6":
                            currentRole = "Candidate";
                            break;
                    }

                    var usr = User as ClaimsPrincipal;
                    var identity = usr.Identity as ClaimsIdentity;
                    var claim = (from c in usr.Claims
                                 where c.Value == "Candidate"
                                 select c).SingleOrDefault();
                    if (claim != null)
                    {
                        identity.RemoveClaim(claim);
                        identity.AddClaim(new Claim(ClaimTypes.Role, currentRole));
                    }
                }
            }
            base.OnActionExecuted(filterContext);
        }
    }
}

