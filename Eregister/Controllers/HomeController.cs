using Eregister.App_Start;
using Eregister.DAL;
using Eregister.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        #region TeachersList       
        //public ActionResult TeachersList()
        //{
        //    return View();
        //}

        public ActionResult TeachersList()
        {
            SubjectsViewModel subjectViewModel = new SubjectsViewModel();
            List<SubjectsViewModel> teachersList = new List<SubjectsViewModel>();

            var teacherSubjects = context.TeacherSubjects?.Where(x => x.SubjectID != 0).ToList();

            Group ownGroup = new Group();

            if (teacherSubjects != null)
            {
                SubjectsViewModel item = new SubjectsViewModel();
                foreach (var t in teacherSubjects)
                {
                    var user = context.Users.Find(t.UserId);
                    var subject = context.Subjects.Where(x => x.SubjectID == t.SubjectID).FirstOrDefault().Name;

                    item.UserName = user.NameSurname;
                    item.Phone = user.PhoneNumber;
                    item.Mail = user.Email;
                    if (subject != null) item.Subject = subject;
                    else item.Subject = "Brak danych";
                }
                teachersList.Add(item);
            }

            return View(teachersList);
        }

        public ActionResult Gallery()
        {
            return View();
        }

        #endregion TeachersList

        //UWIERZYTELNIANIE
        #region Authorize
        public ActionResult ModalAuthorize()
        {
            return PartialView("_ModalAuthorize");
        }

        [HttpPost]
        public ActionResult Authorize()
        {
            return RedirectToAction("Index");
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AuthorizeFromCandidate(string code, string pesel) //(string code)
        {
            if (!string.IsNullOrWhiteSpace(code) && !string.IsNullOrWhiteSpace(pesel) && pesel?.Length==11)
            {
                string userName = User.Identity.Name;
                string loggedUserId = User.Identity.GetUserId();

                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                long pes = 0;
                bool result = Int64.TryParse(pesel, out pes);
                var token = GetAuthorizationToken(user.Id);
                token.TokenIsValid = true; //zakomentowac te sztywne dane
                token.TokenAdvIsValid = true;
               // token.TokenValue = "Student"; // temp code dla candidatee->student

                if (User.IsInRole("Candidate") && code == token.TokenValue && token.TokenIsValid)
                {
                    var ifStudentExist = context.Students.Find(loggedUserId);
                    if (ifStudentExist == null)
                    {
                        var userRoles = await UserManager.GetRolesAsync(user.Id);
                        string[] roles = new string[userRoles.Count];
                        userRoles.CopyTo(roles, 0);
                        await UserManager.RemoveFromRolesAsync(user.Id, roles);
                        await UserManager.AddToRoleAsync(user.Id, "Student");

                        var usr = User as ClaimsPrincipal;
                        var identity = usr.Identity as ClaimsIdentity;
                        var claim = (from c in usr.Claims
                                     where c.Value == "Candidate"
                                     select c).Single();
                        identity.RemoveClaim(claim);
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Student"));
                        user.TokenIsValid = false;
                        var myStudent = new Student()
                        {
                            UserId = loggedUserId,
                            Pesel = result ? pesel : "",

                        };
                        context.Students.Add(myStudent);
                        context.SaveChanges();
                    }
                  //  ViewBag.ResultMessage = "Uwierzytelniono, dodano do listy";
                    TempData["msg"] = "<script>alert('Uwierzytelniono! Dodano Cię do listy studentów');</script>";
                }


                else if (User.IsInRole("Candidate") && code == token.TokenValueAdv && token.TokenAdvIsValid)
                {
                    var ifTeacherExist = context.Teachers.Find(loggedUserId);
                    if (ifTeacherExist == null)
                    {
                        var userRoles = await UserManager.GetRolesAsync(user.Id);
                        string[] roles = new string[userRoles.Count];
                        userRoles.CopyTo(roles, 0);
                        await UserManager.RemoveFromRolesAsync(user.Id, roles);
                        await UserManager.AddToRoleAsync(user.Id, "Teacher");

                        var usr = User as ClaimsPrincipal;
                        var identity = usr.Identity as ClaimsIdentity;
                        var claim = (from c in usr.Claims
                                     where c.Value == "Candidate"
                                     select c).Single();
                        identity.RemoveClaim(claim);
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Teacher"));
                        user.TokenAdvIsValid = false;
                        var myTeacher = new Teacher()
                        {
                            UserId = loggedUserId,
                            Pesel = result ? pesel : "",

                        };
                        context.Teachers.Add(myTeacher);
                        context.SaveChanges();
                    }
                    //  ViewBag.ResultMessage = "Uwierzytelniono, dodano do listy";
                    TempData["msg"] = "<script>alert('Uwierzytelniono! Dodano Cię do listy Nauczycieli');</script>";
                }

                else
                {
              //      ViewBag.ResultMessage = "Nie uwierzytelniono";
                    TempData["msg"] = "<script>alert('Nie wierzytelniono! Sprawdź poprawność kodu i pesel');</script>";
                }
            }
            return RedirectToAction("Index");
        }

        #endregion

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
                        foreach (var i in teacherNots)
                        {
                            context.Alerts.Remove(i);
                        }
                        context.SaveChanges();
                        ViewData.Remove("NotificationsContent");
                        ViewData.Remove("NotificationsCount");
                    }
                }
                else if (user.Student != null)
                {
                    var studentNots = context.Alerts.Where(x => x.StudentUserId == userId).ToList();

                    if (studentNots != null & studentNots.Count > 0)
                    {
                        foreach (var i in studentNots)
                        {
                            context.Alerts.Remove(i);
                        }
                        context.SaveChanges();
                        ViewData.Remove("NotificationsContent");
                        ViewData.Remove("NotificationsCount");
                    }
                }
            }
            return Redirect(Request.UrlReferrer.ToString());          
        }

        #region Helpers
        public ApplicationUser GetAuthorizationToken(string userId)
        {
            var token = context.Users.Where(x => x.Id == userId).FirstOrDefault();
            return token;
        }
        #endregion

        #region Skins
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult ChangeSkin(string skin)
        {
            var userId = User.Identity.GetUserId();
            var user = context.Users.Where(x => x.Id == userId).FirstOrDefault();
            switch(skin)
            {
                case "purple":
                    user.CustomSkin = "purple";
                    break;
                case "red":
                    user.CustomSkin = "red";
                    break;
                default:
                    user.CustomSkin = "green";
                    break;
            }
            context.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        #endregion

    }
}