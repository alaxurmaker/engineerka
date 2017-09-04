using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels
{
    public class Student
    {
        public int StudentID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
        public int Pesel { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime JoinDate { get; set; }
        public string Classroom { get; set; }

        public int AddressID { get; set; }
        public int UserID { get; set; }
        public int EducatorID { get; set; }

        public virtual Address Address { get; set; }       
        public virtual User User { get; set; }
        public virtual Educator Educator { get; set; }
        public virtual ICollection<StudentParent> StudentParents { get; set; }
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }
        public virtual ICollection<StudentHistory> StudentHistories { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

    }
}
