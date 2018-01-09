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
    public class StudentGradesController : BaseController//Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult DeleteGrade(string userId, int position, int? subjectId)
        {
            StudentGrade studentGrade = db.StudentGrades.Where(x => x.UserId == userId).FirstOrDefault();
            string oceny = studentGrade.Grades;
            string str = oceny.Remove(position, 1);

            studentGrade.Grades = str;
            db.SaveChanges();

            return RedirectToAction("GoToGrades", new { id = subjectId.Value });
        }

        public ActionResult EndGrade(string id, int? subjectId, string userName)
        {
            StudentGrade studentGrade = db.StudentGrades.Where(x => x.UserId == id).FirstOrDefault();

            double average = 0;

            if (studentGrade != null && studentGrade.Grades != null)
            {
                string grades = studentGrade.Grades;
                int[] array = new int[grades.Length];
                int pos = 0;
                foreach (var s in grades)
                {
                    int x = (int)Char.GetNumericValue(s);
                    array[pos] = x;
                    pos++;
                }
                average = array.Average();
            }
            StudentGradeViewModel studentViewModel = new StudentGradeViewModel();
            studentViewModel.UserName = userName;
            studentViewModel.UserId = id;
            studentViewModel.SubjectId = subjectId.Value;
            studentViewModel.Grade = average != 0 ? average.ToString().Remove(4) : "brak";

            return View(studentViewModel);
        }

        public ActionResult AddGrade(string id, string grade, int? subjectId)
        {
            StudentGrade studentGrade = db.StudentGrades.Where(x=>x.UserId==id).FirstOrDefault();
            if(studentGrade==null)
            {
                StudentGrade newGrade = new StudentGrade();
                newGrade.UserId = id;
                newGrade.SubjectID = subjectId.Value;
                newGrade.Grades = grade;
                db.StudentGrades.Add(newGrade);
            }
            else
            {
                string currentGrades = studentGrade.Grades != null ? studentGrade.Grades : "";
                studentGrade.Grades = currentGrades + grade;
            }
        
            db.SaveChanges();
            return RedirectToAction("GoToGrades", new { id = subjectId });
        }

        public ActionResult AddEndGrade(string id, string grade, int? subjectId)
        {
            StudentGrade studentGrade = db.StudentGrades.Where(x => x.UserId == id).FirstOrDefault();

            studentGrade.Grade = grade;

            db.SaveChanges();
            return RedirectToAction("GoToGrades", new { id = subjectId });
        }



        // GET: StudentGrades
        public ActionResult MainIndex()
        {
            StudentGradeViewModel studentViewModel = new StudentGradeViewModel();
            
            var teacherGroups = db.Teachers?.Where(x => x.GroupID != null)?.FirstOrDefault().GroupID;
            Group ownGroup = new Group();
            if (teacherGroups != null)
            {
                ownGroup = db.Groups.Where(x => x.GroupID == teacherGroups).FirstOrDefault();
            }

            List<Student> studentsList = db.Students.Where(x => x.GroupName==ownGroup.Name).ToList();
            var student = db.Students.Where(x => x.GroupID == ownGroup.GroupID).FirstOrDefault().ApplicationUser.Id;

            studentViewModel.UserId = student;
            studentViewModel.GroupName = ownGroup.Name;

            studentViewModel.StudentsList = new List<Student>();
            studentViewModel.StudentsList = studentsList;

            return View(studentsList);
        }


        // GET: StudentGrades
        public ActionResult IndexForTeacher()
        {
            var teacherId = User.Identity.GetUserId();
            var mySubjects = db.TeacherSubjects.Where(x => x.UserId == teacherId).ToList();
            List<int> subjectsIds = new List<int>();

            foreach(var i in mySubjects)
            {
                subjectsIds.Add(i.SubjectID);
            }

            List<Subject> subjects = new List<Subject>();

            foreach(var i in subjectsIds)
            {
                var getSubject = db.Subjects.Where(x => x.SubjectID == i).FirstOrDefault();
                subjects.Add(getSubject);
            }
            return View(subjects);
        }

        public ActionResult GoToGrades(int? id)
        {          
            var students = db.StudentSubjects.Where(x => x.SubjectID == id.Value).ToList();

            List<StudentGradeViewModel> resultList = new List<StudentGradeViewModel>();
            var subject = db.Subjects.Where(x => x.SubjectID == id.Value).FirstOrDefault();
            var subjectInfo = subject.Name + " Poziom: " + subject.Level;
            foreach (var i in students)
            {
                StudentGradeViewModel student = new StudentGradeViewModel();
                var stuGrade = db.StudentGrades.Where(x => x.UserId == i.UserId).FirstOrDefault();

                int amount = 0;
                double average = 0;
                string Oceny = "brak";
               
                if (stuGrade!=null && stuGrade.Grades!=null)
                {
                    string grades = stuGrade.Grades;
                    int[] array = new int[grades.Length];
                    int pos = 0;
                    foreach (var s in grades)
                    {
                        int x = (int)Char.GetNumericValue(s);
                        amount += x;
                        array[pos] = x;
                        pos++;
                    }
                    Oceny = grades;
                    average = array.Average();
                }

                student.SubjectId = id.Value;
                student.Grade = average != 0 ? average.ToString().Substring(0, Math.Min(average.ToString().Length, 4)) : "brak";
                student.GroupName = subjectInfo;
                var studentName = db.Students.Where(x => x.UserId == i.UserId).FirstOrDefault();
                student.Grades = Oceny;
                student.EndGrade = stuGrade.Grade;
                student.UserName = studentName.ApplicationUser.NameSurname;
                student.Group = studentName.GroupName;
                student.UserId = i.UserId;

                resultList.Add(student);
            }
            if(resultList==null || resultList.Count==0)
            {
                resultList.Add(new StudentGradeViewModel() { GroupName = subjectInfo, Grades = null});
                resultList.GroupBy(x => x.Group);
                resultList.GroupBy(x => x.UserName);
            }

            return View(resultList);
        }


        // GET: StudentGrades
        public ActionResult Index()
        {
            var studentGrades = db.StudentGrades.Include(s => s.Student).Include(s => s.Subject);//.Where(x=>x.SubjectID==;
            return View(studentGrades.ToList());
        }

        // GET: StudentGrades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGrade studentGrade = db.StudentGrades.Find(id);
            if (studentGrade == null)
            {
                return HttpNotFound();
            }
            return View(studentGrade);
        }

        // GET: StudentGrades/Create
        public ActionResult Create()
        {
           // var students = db.Students
            ViewBag.UserId = new SelectList(db.Students, "UserId", "Pesel");
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name");
            return View();
        }

        // POST: StudentGrades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentGradeID,Grade,UserId,SubjectID")] StudentGrade studentGrade)
        {
            if (ModelState.IsValid)
            {
                db.StudentGrades.Add(studentGrade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Students, "UserId", "Pesel", studentGrade.UserId);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", studentGrade.SubjectID);
            return View(studentGrade);
        }

        // GET: StudentGrades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGrade studentGrade = db.StudentGrades.Find(id);
            if (studentGrade == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Students, "UserId", "Pesel", studentGrade.UserId);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", studentGrade.SubjectID);
            return View(studentGrade);
        }

        // POST: StudentGrades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentGradeID,Grade,UserId,SubjectID")] StudentGrade studentGrade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentGrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Students, "UserId", "Pesel", studentGrade.UserId);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", studentGrade.SubjectID);
            return View(studentGrade);
        }

        // GET: StudentGrades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGrade studentGrade = db.StudentGrades.Find(id);
            if (studentGrade == null)
            {
                return HttpNotFound();
            }
            return View(studentGrade);
        }

        // POST: StudentGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentGrade studentGrade = db.StudentGrades.Find(id);
            db.StudentGrades.Remove(studentGrade);
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
            List<Student> students = db.Students.Where(y => y.Group.GroupID == id).ToList();

            GroupViewModel grp = new GroupViewModel();
            List<StudentsListViewModel> studentsListView = new List<StudentsListViewModel>();

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
                }
            }


            return studentsListView;
        }
    }
}
