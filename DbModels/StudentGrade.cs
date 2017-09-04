using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels
{
    public class StudentGrade
    {
        public int StudentGradeID { get; set; }
        public List<string> ListaOcenKar { get; set; }
        public List<string> ListaOcenOdp { get; set; }
        public List<string> ListaOcenSpr { get; set; }

        public int StudentID { get; set; }
        public int SubjectID { get; set; }

        public virtual ICollection<FinalGrade> FinalGrades { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }

    }
}
