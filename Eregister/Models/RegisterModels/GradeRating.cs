using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eregister
{
   public class GradeRating
    {
        [Key]
        public int GradeRatingID { get; set; }

        public string TestType { get; set; }
        public float Wage { get; set; }

        [ForeignKey("Subject")]
        public int SubjectID { get; set; }

        
        public virtual Subject Subject { get; set; }
    }
}
