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

namespace Eregister.Controllers
{
    public class StudentSubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentSubjects
        public ActionResult Index()
        {
            var studentSubjects = db.StudentSubjects.Include(s => s.Student).Include(s => s.Subject);
            return View(studentSubjects.ToList());
        }

        // GET: StudentSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentSubject studentSubject = db.StudentSubjects.Find(id);
            if (studentSubject == null)
            {
                return HttpNotFound();
            }
            return View(studentSubject);
        }

        // GET: StudentSubjects/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Students, "UserId", "Pesel");
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name");
            return View();
        }

        // POST: StudentSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentSubjectID,UserId,SubjectID")] StudentSubject studentSubject)
        {
            if (ModelState.IsValid)
            {
                db.StudentSubjects.Add(studentSubject);
               // var TeacherSubject = db.TeacherSubjects.Where(x=>x.SubjectID==studentSubject.SubjectID)
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Students, "UserId", "Pesel", studentSubject.UserId);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", studentSubject.SubjectID);
            return View(studentSubject);
        }

        // GET: StudentSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentSubject studentSubject = db.StudentSubjects.Find(id);
            if (studentSubject == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Students, "UserId", "Pesel", studentSubject.UserId);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", studentSubject.SubjectID);
            return View(studentSubject);
        }

        // POST: StudentSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentSubjectID,UserId,SubjectID")] StudentSubject studentSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Students, "UserId", "Pesel", studentSubject.UserId);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", studentSubject.SubjectID);
            return View(studentSubject);
        }

        // GET: StudentSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentSubject studentSubject = db.StudentSubjects.Find(id);
            if (studentSubject == null)
            {
                return HttpNotFound();
            }
            return View(studentSubject);
        }

        // POST: StudentSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentSubject studentSubject = db.StudentSubjects.Find(id);
            db.StudentSubjects.Remove(studentSubject);
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
