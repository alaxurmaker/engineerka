﻿using DBModels;
using Eregister.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eregister.Controllers
{
    public class UserProfileController : Controller
    {
        private EFDatabaseContext db = new EFDatabaseContext();

        // GET: UserProfile
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ImageUpload(ImageViewModel model)
        {
           // User user = new DBModels.User();
            int imgId = 0;
            var file = model.ImageFile;
            byte[] imagebyte = null;
            if (file != null)
            {
                file.SaveAs(Server.MapPath("/UploadImage/" + file.FileName));
                BinaryReader reader = new BinaryReader(file.InputStream);
                imagebyte = reader.ReadBytes(file.ContentLength);
                User img = new User();
                img.ImageTitle = file.FileName;
                img.ImageByte = imagebyte;
                img.ImagePath = "/UploadImage/" + file.FileName;
                //do zmiany
                img.Created = new DateTime();
                img.Created = DateTime.Now;
                img.Email = "testowy@vp.pl";
                img.LastLogin = new DateTime();
                img.LastLogin = DateTime.Now;
                img.Login = "test_obrazek";
                img.Password = "pw123";
                img.PasswordHash = "pw123";
                img.PasswordSalt = "pw123";
                img.Role = "test";
                
                db.Users.Add(img);
                db.SaveChanges();
                imgId = img.ImageId;
            }
            return Json(imgId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayingImage(int imgid)
        {
            var img = db.Users.SingleOrDefault(x => x.ImageId == imgid);
            return File(img.ImageByte, "image/jpg");
        }
    }
}