using Eregister.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Eregister.Controllers
{
    public class UserProfileController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult View()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Where(x => x.Id == userId).FirstOrDefault();
            var role = UserManager.GetRoles(userId).FirstOrDefault();

            int? roleAddress = null;

            if (role=="Student")
            {
                var student = db.Students?.Where(x => x.ApplicationUser.Id == userId)?.FirstOrDefault()?.AddressID;
                roleAddress = student;
            }
            else if (role == "Teacher")
            {
                var teacher = db.Teachers?.Where(x => x.ApplicationUser.Id == userId)?.FirstOrDefault()?.AddressID;
                roleAddress = teacher;
            }
            else if (role == "Parent")
            {
                var parent = db.Parents?.Where(x => x.ApplicationUser.Id == userId)?.FirstOrDefault()?.AddressID;
                roleAddress = parent;
            }

            Address address = db.Addresses?.Where(x => x.AddressID == roleAddress)?.FirstOrDefault();

            UserProfileViewModel userVM = new UserProfileViewModel();
            userVM.UserId = user.Id;
            userVM.Name = user.FirstName;
            userVM.Surname = user.LastName;
            userVM.Email = user.Email;

            userVM.Street = address!=null ? address.Street : "brak";
            userVM.City = address != null ? address.City : "brak";
            userVM.PostalCode = address != null ? address.PostalCode : "brak";
            userVM.Country = address != null ? address.Country : "brak";
            userVM.Phone = address != null ? address.Phone : "brak";

            return View(userVM);
        }

        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Where(x => x.Id == userId).FirstOrDefault();

            UserProfileViewModel userVM = new UserProfileViewModel();
            userVM.UserId = user.Id;
            userVM.Login = user.UserName;
            userVM.Email = user.Email;

            return View(userVM);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "ImageByte")]UserProfileViewModel editUser)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["ImageByte"];

                    using (var binary = new BinaryReader(poImgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }
                }
                var usrId = User.Identity.GetUserId();
                ApplicationUser user = db.Users.Where(x => x.Id == usrId).FirstOrDefault();
                user.ImageByte = imageData;
                user.UserName = editUser.Login;
                user.Email = editUser.Email;

                Address address = new Address();
                address.City = editUser.City;
                address.Country = editUser.Country;
                address.Phone = editUser.Phone;
                address.PostalCode = editUser.PostalCode;
                address.Street = editUser.Street;
                db.Addresses.Add(address);

                if(User.IsInRole("Student"))
                {
                    var studentAddress = db.Students.Where(x => x.UserId == usrId).FirstOrDefault();
                    if(studentAddress.AddressID!=null & studentAddress.AddressID!=0)
                    studentAddress.AddressID = address.AddressID;
                }
                if (User.IsInRole("Teacher"))
                {
                    var teacherAddress = db.Teachers.Where(x => x.UserId == usrId).FirstOrDefault();
                    if (teacherAddress.AddressID != null & teacherAddress.AddressID != 0)
                        teacherAddress.AddressID = address.AddressID;
                }
                if (User.IsInRole("Parent"))
                {
                    var parentAddress = db.Students.Where(x => x.UserId == usrId).FirstOrDefault();
                    if (parentAddress.AddressID != null & parentAddress.AddressID != 0)
                        parentAddress.AddressID = address.AddressID;
                }
                db.SaveChanges();
            }
            //return RedirectToAction("Index", "Home");
            return Redirect(Request.UrlReferrer.ToString());
        }

        public FileContentResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");
                }
                var Users = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = Users.Users.Where(x => x.Id == userId).FirstOrDefault();

                return new FileContentResult(userImage.ImageByte, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Content/images/pic.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");
            }
        }

        public FileContentResult UserCommentPhotos(string id)
        {
            var Users = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            var userImage = Users.Users?.Where(x => x.NameSurname == id)?.FirstOrDefault();

            if (userImage != null)
                return new FileContentResult(userImage.ImageByte, "image/jpeg");
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Content/images/pic.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");
            }
        }
    }
}
