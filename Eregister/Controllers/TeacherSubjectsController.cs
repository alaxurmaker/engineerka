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

namespace Eregister.Controllers
{
    public class TeacherSubjectsController : BaseController//Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TeacherSubjects
        public ActionResult Index()
        {
            var teacherSubjects = db.TeacherSubjects.Include(t => t.Subject).Include(t => t.Teacher);

            //foreach(var i in teacherSubjects)
            //{
            //    i.
            //}
            return View(teacherSubjects.ToList());
        }

        // GET: TeacherSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherSubject teacherSubject = db.TeacherSubjects.Find(id);
            if (teacherSubject == null)
            {
                return HttpNotFound();
            }
            return View(teacherSubject);
        }

        // GET: TeacherSubjects/Create
        public ActionResult Create2()
        {
            string id = User.Identity.GetUserId();
            

          //  TeacherSubject teacherSubject = db.TeacherSubjects.Find(id);

            var currentTeacherSubjects = db.TeacherSubjects.Where(x => x.UserId == id)?.ToList();

            var subjectsIds = currentTeacherSubjects.Select(x => x.SubjectID).ToList();

            var dbSubjects = db.Subjects.Where(x => x.SubjectID != 0 && x.Name != null)?.ToList();


            if (dbSubjects == null || dbSubjects.Count < 1)
            {
                return HttpNotFound();
            }

            foreach (var i in subjectsIds)
            {
                if(dbSubjects.Any(x=>x.SubjectID==i))
                {
                    var item = dbSubjects.Where(x => x.SubjectID == i).FirstOrDefault();
                    dbSubjects.Remove(item);
                }                  
                //if (subjectsIds.Any(x => x == y.SubjectID))
                //    dbSubjects.Remove(y);
            }

            ViewBag.SubjectID = new SelectList(dbSubjects, "SubjectID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2([Bind(Include = "TeacherSubjectID,UserId,SubjectID")] TeacherSubject teacherSubject)
        {
            if (ModelState.IsValid)
            {
                teacherSubject.UserId = User.Identity.GetUserId(); //teacherUserId.UserId;
                db.TeacherSubjects.Add(teacherSubject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //List<ApplicationUser> teachers = db.Users.Where(x => x.Teacher.UserId != null).ToList();
            //var selectList = new List<SelectListItem>();
            //foreach (var t in teachers)
            //{
            //    selectList.Add(new SelectListItem
            //    {
            //        Value = t.NameSurname,
            //        Text = t.NameSurname
            //    });
            //}

            ////  if (flag == false)

            //ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", teacherSubject.SubjectID);
            ////  ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", teacherSubject.SubjectID);

            //ViewBag.UserId = selectList;
            //new SelectList(selectList, teacherSubject.UserId);
            // ViewBag.UserId = new SelectList(db.Teachers, "UserId", "Title", teacherSubject.UserId);
            return View(teacherSubject);
        }


        // GET: TeacherSubjects/Create
        public ActionResult Create()
        {
            bool flag = false;

            //List<ApplicationUser> teachers = db.Users.Where(x => x.Teacher.UserId != null).ToList();
            //var users = db.Users.Where(y => y.NameSurname == teacherSubject.UserId).FirstOrDefault();
            //var teacherUserId = db.Teachers.Where(x => x.UserId == users.Id).FirstOrDefault();

            //var tchersub = db.TeacherSubjects.Where(x => x.Teacher.ApplicationUser.NameSurname)
            //var users = db.Users.Where(y => y.NameSurname == teacherSubject.UserId).FirstOrDefault();

            //var teacherSubjects = db.TeacherSubjects.Where(x => x.UserId == users.Id);
            //if (teacherSubjects != null)
            //{
            //    var teacherSubjectsIdList = new List<int>();
            //    foreach (var i in teacherSubjects)
            //    {
            //        teacherSubjectsIdList.Add(i.SubjectID);
            //    }
            //    var subjectsList = new List<string>();
            //    foreach (var i in teacherSubjectsIdList)
            //    {
            //        var subject = db.Subjects.Where(x => x.SubjectID == i).FirstOrDefault().Name;
            //        subjectsList.Add(subject);
            //    }

            //    var subjectsselectList = new List<SelectListItem>();
            //    foreach (var s in subjectsList)
            //    {
            //        subjectsselectList.Add(new SelectListItem
            //        {
            //            Value = s,
            //            Text = s
            //        });
            //    }
            //    flag = true;
            //    ViewBag.SubjectID = subjectsselectList;
            //}

            if(flag==false)  ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name");

           // List<Student> studentsList = db.Students.Where(x => String.IsNullOrEmpty(x.Group.Name)).ToList();

            List<ApplicationUser> teachers = db.Users.Where(x => x.Teacher.UserId != null).ToList();
            var selectListTeachers = new List<SelectListItem>();
            foreach (var t in teachers)
            {
                selectListTeachers.Add(new SelectListItem
                {
                    Value = t.NameSurname,
                    Text = t.NameSurname
                });
            }

          //  new SelectList(db.Teachers, "UserId", "Title");
            ViewBag.UserId = selectListTeachers;// new SelectList(db.Users.Where(x=>x.Teacher.UserId != null), "UserId", );
            return View();
        }

        // POST: TeacherSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherSubjectID,UserId,SubjectID")] TeacherSubject teacherSubject)
        {
     //       bool flag = false;
            if (ModelState.IsValid)
            {
             //   var teacherSubjects = db.Teachers.Where(x => x.TeacherSubjects != null);

               // if(teacherSubject.SubjectID)

                var users = db.Users.Where(y => y.NameSurname == teacherSubject.UserId).FirstOrDefault();
              
                var teacherUserId = db.Teachers.Where(x => x.UserId == users.Id).FirstOrDefault();
                teacherSubject.UserId = teacherUserId.UserId;
                db.TeacherSubjects.Add(teacherSubject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<ApplicationUser> teachers = db.Users.Where(x => x.Teacher.UserId != null).ToList();
            var selectList = new List<SelectListItem>();
            foreach (var t in teachers)
            {
                selectList.Add(new SelectListItem
                {
                    Value = t.NameSurname,
                    Text = t.NameSurname
                });
            }

          //  if (flag == false)

                ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", teacherSubject.SubjectID);
          //  ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", teacherSubject.SubjectID);

            ViewBag.UserId = selectList;
            //new SelectList(selectList, teacherSubject.UserId);
           // ViewBag.UserId = new SelectList(db.Teachers, "UserId", "Title", teacherSubject.UserId);
            return View(teacherSubject);
        }

        // GET: TeacherSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherSubject teacherSubject = db.TeacherSubjects.Find(id);
            if (teacherSubject == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", teacherSubject.SubjectID);



            List<ApplicationUser> teachers = db.Users.Where(x => x.Teacher.UserId != null).ToList();
            var selectList = new List<SelectListItem>();
            foreach (var t in teachers)
            {
                selectList.Add(new SelectListItem
                {
                    Value = t.NameSurname,
                    Text = t.NameSurname
                });
            }

            ViewBag.UserId = selectList;// new SelectList(db.Teachers, "UserId", "Title", teacherSubject.UserId);

            return View(teacherSubject);
        }

        // POST: TeacherSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherSubjectID,UserId,SubjectID")] TeacherSubject teacherSubject)
        {
            if (ModelState.IsValid)
            {
                teacherSubject.UserId = db.Users.Where(x => x.NameSurname == teacherSubject.UserId).FirstOrDefault().Id;
                db.Entry(teacherSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", teacherSubject.SubjectID);


            List<ApplicationUser> teachers = db.Users.Where(x => x.Teacher.UserId != null).ToList();
            var selectList = new List<SelectListItem>();
            foreach (var t in teachers)
            {
                selectList.Add(new SelectListItem
                {
                    Value = t.NameSurname,
                    Text = t.NameSurname
                });
            }

            ViewBag.UserId = selectList;// new SelectList(db.Teachers, "UserId", "Title", teacherSubject.UserId);
            return View(teacherSubject);
        }

        // GET: TeacherSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherSubject teacherSubject = db.TeacherSubjects.Find(id);
            if (teacherSubject == null)
            {
                return HttpNotFound();
            }
            return View(teacherSubject);
        }

        // POST: TeacherSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeacherSubject teacherSubject = db.TeacherSubjects.Find(id);
            db.TeacherSubjects.Remove(teacherSubject);
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
