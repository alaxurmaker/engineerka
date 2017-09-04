using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels
{
    public class Season
    {
        public int SeasonID { get; set; }

        public int Semester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }

        public virtual ICollection<FinalGrade> FinalGrades { get; set; }
        public virtual ICollection<StudentHistory> StudentHistories { get; set; }
    }
}
