using Eregister.Models;
using Eregister.Models.Mail;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Eregister.Controllers
{
    public class MailController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult Send(MailModel _objModelMail)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();                
                mail.To.Add(_objModelMail.To);
                mail.From = new MailAddress(_objModelMail.From);
                mail.Subject = _objModelMail.Subject;
                string Body = _objModelMail.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential("test.na.projekt@gmail.com", "!Testnaprojekt"),
                    EnableSsl = true
                };
                smtp.Send(mail);

                var receiver = db.Users?.Where(x => x.Email == _objModelMail.To)?.FirstOrDefault();
                if(receiver != null)
                {
                    Message msg = new Message()
                    {
                        UserId = receiver.Id,
                        UserFrom = _objModelMail.Name + " " + _objModelMail.Surname,
                        From = _objModelMail.From,
                        Title = _objModelMail.Subject,
                        Body = _objModelMail.Body,
                        AddDate = DateTime.Now,
                        IsOn = true
                    };
                    db.Messages.Add(msg);
                    db.SaveChanges();
                }
            }
            //return RedirectToAction("Index", "Home");
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult ModalMail()
        {
            return PartialView("_ModalMail");
        }

        public ActionResult ModalMessages()
        {          
            var usr = User.Identity.GetUserId();
            var messages = db.Messages?.Where(x => x.ApplicationUser.Id == usr)?.ToList();
            if (messages != null && messages.Count > 0)
            {
                return PartialView("_ModalMessages", messages);
            }
            //else return RedirectToAction("Index", "Home");
            else return Redirect(Request.UrlReferrer.ToString());

        }

        [HttpPost]
        public ActionResult ClearMessages(int go)
        {
            if (User.Identity.GetUserId() != null && !String.IsNullOrEmpty(User.Identity.GetUserId()))
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
                var userId = User.Identity.GetUserId();
                if (user != null)
                {
                    var messages = db.Messages.Where(x => x.ApplicationUser.Id == userId).ToList();
                    if (messages != null & messages.Count > 0)
                    {
                        foreach (var i in messages)
                        {
                            i.IsOn = false;
                        }
                        db.SaveChanges();
                        ViewData.Remove("MessagesContent");
                        ViewData.Remove("MessagesCount");
                    }
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}