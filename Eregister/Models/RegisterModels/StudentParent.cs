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
    public class StudentParent
    {
        [Key]
        public int StudentParentID { get; set; }

        
       // public string UserId { get; set; }

        [ForeignKey("Student")]
        public string StudentID { get; set; }

        [ForeignKey("Parent")]
        public string ParentID { get; set; }

        public virtual Student Student { get; set; }
        public virtual Parent Parent { get; set; }
        //  public virtual Parent Parent { get; set; }
        //  public virtual Student Student { get; set; }

    }
}
