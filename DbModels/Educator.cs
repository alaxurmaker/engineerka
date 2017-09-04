using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels
{
   public class Educator
    {
        public int EducatorID { get; set; }

        public int TeacherID { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        // public virtual ICollection<Classroom> Classrooms { get; set; }
    }
}
