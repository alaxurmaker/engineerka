using Eregister.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eregister
{
    public class StudentSubject
    {
        [Key]
        public int StudentSubjectID { get; set; }

        [ForeignKey("Student")]
        public string UserId { get; set; }

        [ForeignKey("Subject")]
        public int SubjectID { get; set; }

        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }

    }
}