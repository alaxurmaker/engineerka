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
    public class StudentHistory
    {
        [Key]
        public int StudentHistoryID { get; set; }

        public string Status { get; set; }

        [ForeignKey("Season")]
        public int SeasonID { get; set; }

        [ForeignKey("Student")]
        public string UserId { get; set; }

        public virtual Season Season { get; set; }
        public virtual Student Student { get; set; }
    }
}
