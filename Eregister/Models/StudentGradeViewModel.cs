using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eregister.Models
{
    public class StudentGradeViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string Subject { get; set; }
        public string Pesel { get; set; }
        public string GroupName { get; set; }
        public string Grade { get; set; }
        public string EndGrade { get; set; }
        public string Grades { get; set; }
        public string Group { get; set; }
        public int? SubjectId { get; set; }

        public string StudentsGrades { get; set; }
        public List<Student> StudentsList { get; set; }
    }
}