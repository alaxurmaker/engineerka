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
    public class GroupsController : BaseController//Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Assign(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }


                Group group = db.Groups.Find(id);
                group.OwnsEducator = true;
                var teacherId = User.Identity.GetUserId();
                int x = id.Value;
                Teacher entity = db.Teachers.Where(s => s.ApplicationUser.Id == teacherId).FirstOrDefault();
                //entity.ApplicationUser.
                entity.ApplicationUser.Id = teacherId;
                entity.GroupID = x;
                db.Entry(entity).State = EntityState.Modified;

                db.SaveChanges();
               
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
            //  db.SaveChanges();

            return RedirectToAction("CreateClass", new { id = id }); //View("CreateClass");
        }


        public ActionResult IndexDetails(int? groupId)
        {
            GroupViewModel model = new GroupViewModel();
            List<Group> myGroup = db.Groups.Where(x => x.GroupID == groupId).ToList();

            model.Name = myGroup.FirstOrDefault().Name;
            model.Year = myGroup.FirstOrDefault().Year;
            model.ShortDescription = myGroup.FirstOrDefault().ShortDescription;
            model.StudentsListView = new List<StudentsListViewModel>();

            foreach (var m in myGroup)
            {
                model.StudentsListView.Add(new StudentsListViewModel()
                {
                    NameSurname = m.Students.FirstOrDefault().ApplicationUser.NameSurname,
                    JoinDate = m.Students.FirstOrDefault().JoinDate,
                    Pesel = m.Students.FirstOrDefault().Pesel
                });
            }
            return View(model);
        }


        //GET
        public ActionResult CreateClass(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group group = db.Groups.Find(id);

            GroupViewModel model = new GroupViewModel();

            var teacher = db.Teachers?.Where(x => x.GroupID == id)?.FirstOrDefault()?.ApplicationUser?.NameSurname;

            List<Student> studentsList = db.Students.Where(x => String.IsNullOrEmpty(x.Group.Name)).ToList();

            var selectList = new List<SelectListItem>();
            foreach (var s in studentsList)
            {
                selectList.Add(new SelectListItem
                {
                    Value = s.ApplicationUser.NameSurname,
                    Text = s.ApplicationUser.NameSurname
                });
            }

            model.GroupID = id;// != null ? id : 2;
            model.Name = group.Name;

            model.Teacher =  !String.IsNullOrEmpty(teacher) ? teacher : "Brak";

            model.Year = group.Year;
            model.ShortDescription = group.ShortDescription;
            model.StudentsSelectList = selectList;

            model.StudentsListView = new List<StudentsListViewModel>();
            model.StudentsListView = CreateStudentsListViewModel(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Groups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClass(GroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var x = model.SelectedStudent.ToString();

                var student = db.Students.Where(y => y.ApplicationUser.NameSurname == x).FirstOrDefault();
                var studentToUpdate = db.Students.Where(u => u.ApplicationUser.Id == student.UserId).FirstOrDefault();

                var now = new DateTime();
                   now = DateTime.Now;

                studentToUpdate.ApplicationUser.Id = student.UserId;
                studentToUpdate.GroupID = model.GroupID.Value;
                studentToUpdate.GroupName = model.Name;
                studentToUpdate.JoinDate = now;

                db.SaveChanges();

                return RedirectToAction("CreateClass", "Groups", new { id = model.GroupID.Value });
            }

            return View(model);
        }


        // GET: Groups
        public ActionResult Index()
        {
            var teacherGroup = db.Teachers.Where(x => x.GroupID != null).ToList();
            return View(db.Groups.ToList());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,Name,Year,ShortDescription")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,Name,Year,ShortDescription")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
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










        public List<StudentsListViewModel> CreateStudentsListViewModel(int? id)
        {
           // GroupViewModel model = new GroupViewModel();

            //List<Group> myGroup = db.Groups.Where(x => x.GroupID == id).ToList();

            List<Student> students = db.Students.Where(y => y.Group.GroupID == id).ToList();

            GroupViewModel grp = new GroupViewModel();
            List<StudentsListViewModel> studentsListView = new List<StudentsListViewModel>();
            // List<StudentsListViewModel> studentsListView = new List<StudentsListViewModel>();
            //studentsListView = null;

            //  var student = db.Students.Where(y => y.ApplicationUser.NameSurname == x).FirstOrDefault();
            if (students.Count() > 0)
            {
                studentsListView = new List<StudentsListViewModel>();

                foreach (var s in students)
                {
                    var newStudent = new StudentsListViewModel()
                    {
                        NameSurname = s.ApplicationUser.NameSurname != null ? s.ApplicationUser.NameSurname : "",
                        JoinDate = s.JoinDate != null ? s.JoinDate : new DateTime(10, 10, 10),// DateTime.Now ,
                        Pesel = s.Pesel != null ? s.Pesel : ""
                    };
                    studentsListView.Add(newStudent);
                        //studentsListView.Add(new StudentsListViewModel()
                        //{
                        //    NameSurname = s.ApplicationUser.NameSurname !=null ? s.ApplicationUser.NameSurname : "",//?.FirstOrDefault().ApplicationUser.NameSurname,  //.FirstOrDefault().ApplicationUser.NameSurname,
                        //    JoinDate = s.JoinDate !=null ? s.JoinDate : new DateTime(10,10,10),// DateTime.Now ,
                        //    Pesel = s.Pesel != null ? s.Pesel : ""
                        //});
                    
                }
            }


            return studentsListView;
        }
    }
}
