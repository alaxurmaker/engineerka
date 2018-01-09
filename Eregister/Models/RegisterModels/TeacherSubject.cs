using Eregister.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eregister
{
    public class TeacherSubject
    {
        public int TeacherSubjectID { get; set; }  

        [ForeignKey("Teacher")]
        public string UserId { get; set; }

        [ForeignKey("Subject")]
        public int SubjectID { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
