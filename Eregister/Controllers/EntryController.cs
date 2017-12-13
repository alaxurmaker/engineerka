//using DBModels;
//using Eregister.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;

//namespace Eregister.Controllers
//{
//    public class EntryController : Controller
//    {
//        private EFDatabaseContext db = new EFDatabaseContext();

//        // GET: Entry
//        public ActionResult Index()
//        {
//            return View();
//        }

//        //
//        // GET: /Account/Login
//        [AllowAnonymous]
//        public ActionResult Login()
//        {
//          //  ViewBag.ReturnUrl = returnUrl;
//            return View();
//        }


//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Login(LoginViewModel model)//, string returnUrl)
//        {
//            if(ModelState.IsValid)
//            {
//                string login = model.Login;
//                string password = model.Password;

//                bool userValid = db.Users.Any(user => user.Login == login && user.Password == password);

//                if(userValid)
//                {
//                    return RedirectToAction("Index", "Home");
//                }
//                else
//                {
//                    return RedirectToAction("Login", "Entry");
//                }
//            }
//            else
//            {
//                ModelState.AddModelError("", "The user name or password provided is incorrect.");
//                return View();
//            }
//        }

//    }
//}