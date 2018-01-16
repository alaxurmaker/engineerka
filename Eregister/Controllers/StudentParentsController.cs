using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eregister;
using Eregister.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;

namespace Eregister.Controllers
{
    public class StudentParentsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentParents
        public ActionResult Index()
        {
            var studentParents = db.StudentParents.Include(s => s.Parent).Include(s => s.Student);
            return View(studentParents.ToList());
        }

        public ActionResult IndexForParent()
        {
            List<Subject> result = new List<Subject>();

            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var childId = db.StudentParents?.Where(x => x.ParentID == userId)?.FirstOrDefault()?.StudentID;
            if(childId!=null)
            {
                var childSubjects = db.StudentSubjects.Where(x => x.UserId == childId).ToList();
                foreach (var c in childSubjects)
                {
                    Subject sub = db.Subjects.Find(c.SubjectID);
                    result.Add(sub);
                }
            }
            
           
            return View(result);
        }

        // GET: StudentParents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentParent studentParent = db.StudentParents.Find(id);
            if (studentParent == null)
            {
                return HttpNotFound();
            }
            return View(studentParent);
        }

        // GET: StudentParents/Create
        public ActionResult Create()
        {
            var studentsNoParentsAssigned = db.Students.Where(x => x.StudentParents.Select(y=>y.StudentID).Contains(x.ApplicationUser.Id)==false).ToList();
            var studentsSelectList = new List<SelectListItem>();

            foreach (var s in studentsNoParentsAssigned)
            {
                studentsSelectList.Add(new SelectListItem
                {
                    Value = s.ApplicationUser.Id,
                    Text = s.ApplicationUser.NameSurname + " | " + s.ApplicationUser.BirthDate.ToShortDateString()
                });
            }
            var parentsNoParentsAssigned = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("6")).ToList(); 
            var parentsSelectList = new List<SelectListItem>();
            foreach (var p in parentsNoParentsAssigned)
            {
                int adultAge = DateTime.Now.Year - p.BirthDate.Year;
                if(adultAge > 24)
                {
                    parentsSelectList.Add(new SelectListItem
                    {
                        Value = p.Id,
                        Text = p.NameSurname + " | " + p.BirthDate.ToShortDateString()
                    });
                }
            }
            ViewBag.ParentID = parentsSelectList;
            ViewBag.StudentID = studentsSelectList;
            return View();
        }

        // POST: StudentParents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentParentID,StudentID,ParentID")] StudentParent studentParent)
        {
            if (ModelState.IsValid)
            {
                //  var student = db.Students.Find(studentParent.StudentID);
                //   var parent = db.Parents.Find(studentParent.ParentID);
                //studentParent.Student = student;
                //studentParent.Parent = parent;
                //StudentParent studentPar = new StudentParent();
                //studentPar.ParentID = studentParent.ParentID;
                //studentPar.StudentID = studentParent.StudentID;

                ApplicationUser usr = db.Users.Find(studentParent.ParentID);
                    Parent parent = new Parent();
                parent.UserId = usr.Id;
                    db.Parents.Add(parent);
            //    db.SaveChanges();

              //  studentParent.Parent.ApplicationUser.Id = parent.ApplicationUser.Id;
                    db.StudentParents.Add(studentParent);
                    db.SaveChanges();
                    

                return RedirectToAction("Index");
            }

            ViewBag.ParentID = new SelectList(db.Parents, "UserId", "Phone", studentParent.ParentID);
            ViewBag.StudentID = new SelectList(db.Students, "UserId", "Pesel", studentParent.StudentID);
            return View(studentParent);
        }

        // GET: StudentParents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentParent studentParent = db.StudentParents.Find(id);
            if (studentParent == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentID = new SelectList(db.Parents, "UserId", "Phone", studentParent.ParentID);
            ViewBag.StudentID = new SelectList(db.Students, "UserId", "Pesel", studentParent.StudentID);
            return View(studentParent);
        }

        // POST: StudentParents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentParentID,StudentID,ParentID")] StudentParent studentParent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentParent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentID = new SelectList(db.Parents, "UserId", "Phone", studentParent.ParentID);
            ViewBag.StudentID = new SelectList(db.Students, "UserId", "Pesel", studentParent.StudentID);
            return View(studentParent);
        }

        // GET: StudentParents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentParent studentParent = db.StudentParents.Find(id);
            if (studentParent == null)
            {
                return HttpNotFound();
            }
            return View(studentParent);
        }

        // POST: StudentParents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentParent studentParent = db.StudentParents.Find(id);
            db.StudentParents.Remove(studentParent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
