using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels
{
   public class GradeRating
    {
        public int GradeRatingID { get; set; }

        public string TestType { get; set; }
        public float Wage { get; set; }

        public int SubjectID { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
