using Eregister.App_Start;
using Eregister.DAL;
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
    public class HomeController : BaseController
    {
        public ApplicationDbContext context { get; set; }

        public HomeController()
        {
            context = new ApplicationDbContext();
        }


        public ActionResult Index()
        {
            ViewBag.Alert = TempData["Alert"];
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //do testow
        public ActionResult Success()
        {
            ViewBag.Message = "Powiodło się";

            return View();
        }
        //do testow
        public ActionResult Fail()
        {
            ViewBag.Message = "Nie udało się";

            return View();
        }

        //UWIERZYTELNIANIE

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AuthorizeFromCandidate(string code, string pesel) //(string code)
        {
            if (!string.IsNullOrWhiteSpace(code) && !string.IsNullOrWhiteSpace(pesel))
            {
                string userName = User.Identity.Name;
                string loggedUserId = User.Identity.GetUserId();

                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                long pes = 0;
                bool result = Int64.TryParse(pesel, out pes);
                var token = GetAuthorizationToken(user.Id);

                token.TokenIsValid = true; //zakomentowac te sztywne dane
               // token.TokenValue = "Student"; // temp code dla candidatee->student

                if (User.IsInRole("Candidate") && code == token.TokenValue && token.TokenIsValid)
                {
                    // var doesStudentExist = context.Students.Where(s => s.UserId.Equals(loggedUserId)).ToList();//.ToList().FirstOrDefault().UserId;
                    // PostCategory postCategory = _context.PostCategories.Where(x => x.PostId == postid && x.CategoryId == categoryid).FirstOrDefault();
                    var ifStudentExist = context.Students.Find(loggedUserId);
                    if (ifStudentExist == null)
                    {
                        UserManager.RemoveFromRole(user.Id, "Candidate");
                        UserManager.AddToRole(user.Id, "Student");
                        user.TokenIsValid = false;
                        //var myStudent = new Student() { UserId = loggedUserId, JoinDate = DateTime.Now, Pesel = result ? pesel : "",
                        var myStudent = new Student()
                        {
                            UserId = loggedUserId,
                            Pesel = result ? pesel : "",

                        };
                        context.Students.Add(myStudent);
                        context.SaveChanges();
                    }
                    ViewBag.ResultMessage = "Uwierzytelniono, dodano do listy";
                }
                else
                {
                    ViewBag.ResultMessage = "Nie uwierzytelniono";
                }
            }
            return View("Index");
        }


        //[ValidateInput(false)]
        [HttpPost]
        public ActionResult ClearNotifications(int go)
        {
            // if (User != null)
            if (User.Identity.GetUserId() != null && !String.IsNullOrEmpty(User.Identity.GetUserId()))
            {
                var context = new ApplicationDbContext();
                var user = context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
                var userId = User.Identity.GetUserId();
                if (user.Teacher != null)
                {
                    var teacherNots = context.Alerts.Where(x => x.TeacherUserId == userId).ToList();

                    if (teacherNots != null & teacherNots.Count > 0)
                    {
                        //var notsId = user.Teacher.AlertID.Value;
                        //var isOn = user.Teacher.Alert.IsOn;

                        //var notsCount = user.Teacher.Alert.Count;

                        //if (notsId != 0)
                        //{
                        //    //  isOn = false;
                        //    //  notsCount = 0;

                        //    user.Teacher.Alert.IsOn = false;
                        //    user.Teacher.Alert.Count = 0;

                        //    context.SaveChanges();

                        foreach (var i in teacherNots)
                        {
                            context.Alerts.Remove(i);
                        }
                        context.SaveChanges();
                        ViewData.Remove("NotificationsContent");
                        ViewData.Remove("NotificationsCount");
                        //  }
                    }
                }
                //else if (user.Student != null)
                //{

                //}
            }
            return Redirect(Request.UrlReferrer.ToString());
          //  return new EmptyResult();
           
        }

        #region Helpers
        public ApplicationUser GetAuthorizationToken(string userId)
        {
            var token = context.Users.Where(x => x.Id == userId).FirstOrDefault();
            return token;
        }

        //public Student AddCandidateToStudents(string userID)
        //{
        //    Group group = new Group();
        //    group.Teacher.UserId
        //    group.StudentID = userID;
        //    var addGroup = context.Groups.Add()
        //    var student = context.MyUserss.Where(x => x.UserId == userId).FirstOrDefault();
        //    return token;
        //}
        #endregion
    }
}