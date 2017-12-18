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
   public class Group
    {
        [Key]
        public int GroupID { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string ShortDescription { get; set; }

        // [ForeignKey("Student")]
        // public string UserId { get; set; }

        // [ForeignKey("ApplicationUser")]
        // public string UserId { get; set; }

        // public virtual ApplicationUser ApplicationUser { get; set; }
        //  public virtual Teacher Teacher { get; set; }
        //  public virtual Subject Subject { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        //public virtual Student Student { get; set; }

       // public virtual ICollection<GroupTimetable> GroupTimetables { get; set; }
       // public virtual ICollection<StudentGroup> StudentGroups { get; set; }
    }
}
