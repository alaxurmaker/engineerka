using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eregister
{
    public class FinalGrade
    {
        public int FinalGradeID { get; set; }

        public int GradeValue { get; set; }
        public string TextValue { get; set; }
        public ConductGrade? ConductGrade { get; set; }
        public string Description { get; set; }

        [ForeignKey("StudentGrade")]
        public int StudentGradeID { get; set; }

        [ForeignKey("Season")]
        public int SeasonID { get; set; }

        public virtual StudentGrade StudentGrade { get; set; }
        public virtual Season Season { get; set; }
    }

    public enum ConductGrade
    {
        [Display(Name = "Niedostateczny")]
        niedostateczny=1,
        [Display(Name = "Dopuszczający")]
        dopuszczający=2,
        [Display(Name = "Dostateczny")]
        dostateczny=3,
        [Display(Name = "Dobry")]
        dobry=4,
        [Display(Name = "Bardzo dobry")]
        bardzodobry=5,
        [Display(Name = "Celujący")]
        celujacy=6
    }
}
