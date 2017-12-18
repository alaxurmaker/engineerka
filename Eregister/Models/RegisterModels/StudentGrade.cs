using Eregister.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eregister
{
    public class StudentGrade
    {
        [Key]
        public int StudentGradeID { get; set; }

        public string Grade { get; set; }

        [ForeignKey("Student")]
        public string UserId { get; set; }

        [ForeignKey("Subject")]
        public int SubjectID { get; set; }

        public virtual ICollection<FinalGrade> FinalGrades { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }

    }
}
