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
    public class GroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult ClassList()
        {
            //GroupViewModel model = new GroupViewModel();
            //List<Group> myGroup = db.Groups.Where(x => x.GroupID == id).ToList();

            //model.StudentsListView = new List<StudentsListViewModel>();

            //foreach (var m in myGroup)
            //{
            //    model.StudentsListView.Add(new StudentsListViewModel()
            //    {
            //        NameSurname = m.Students.FirstOrDefault().ApplicationUser.NameSurname,
            //        JoinDate = m.Students.FirstOrDefault().JoinDate,
            //        Pesel = m.Students.FirstOrDefault().Pesel
            //    });
            //}
            return PartialView();
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
                // Student student = db.Students.Where(x => x.ApplicationUser.NameSurname.Equals(model.SelectedStudent)).FirstOrDefault();
                var x = model.SelectedStudent.ToString();

                var student = db.Students.Where(y => y.ApplicationUser.NameSurname == x).FirstOrDefault();
                Student studentToUpdate = db.Students.Where(u => u.UserId == student.UserId).SingleOrDefault();
                // student.GroupID = model.GroupID;
                // studentToUpdate.GroupName = model.Name;

                //  studentToUpdate.GroupID = model.GroupID.Value; // tu wywala
                studentToUpdate.GroupID = model.GroupID;
                studentToUpdate.GroupName = model.Name;
                

                var entity = db.Students.Find(studentToUpdate.UserId);
                //if (entity == null)
                //{
                //    return;
                //}

                db.Entry(entity).CurrentValues.SetValues(studentToUpdate);

                //   db.Students.Attach(student); // State = Unchanged
                //  student.GroupID = model.GroupID; // State = Modified, and only the FirstName property is dirty.
                // student.GroupID = model.GroupID;
                //    student.GroupName = model.Name;


                //db.Students.Attach(student);
                //student.GroupID = model.GroupID;
                //Group grp = new Group();
                //grp.GroupID = model.GroupID.Value;
                //grp.Name = model.Name;
                //student.Group = new Group();
                //student.Group = grp;


                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }








        // GET: Groups
        public ActionResult Index()
        {
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
            studentsListView = null;

            //  var student = db.Students.Where(y => y.ApplicationUser.NameSurname == x).FirstOrDefault();
            if (students.Count() > 0)
            {
                foreach (var s in students)
                {
                        studentsListView.Add(new StudentsListViewModel()
                        {
                            NameSurname = s.ApplicationUser.NameSurname !=null ? s.ApplicationUser.NameSurname : "",//?.FirstOrDefault().ApplicationUser.NameSurname,  //.FirstOrDefault().ApplicationUser.NameSurname,
                            JoinDate = s.JoinDate,
                            Pesel = s.Pesel != null ? s.Pesel : ""
                        });
                    
                }
            }


            return studentsListView;
        }
    }
}
