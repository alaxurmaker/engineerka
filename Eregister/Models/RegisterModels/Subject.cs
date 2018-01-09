using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eregister
{
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }

        public string Name { get; set; }
        public string Level { get; set; }
        public string Description { get; set; }

       // [ForeignKey("StudentSubject")]
      //  public int StudentSubjectID { get; set; }

        public virtual ICollection<StudentSubject> StudentSubjects { get; set;}

        public virtual ICollection<GradeRating> GradeRatings { get; set; }
       // public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }
    }
}
