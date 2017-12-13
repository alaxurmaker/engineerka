using Eregister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eregister
{
    public class StudentHistory
    {
        public int StudentHistoryID { get; set; }

        public string Status { get; set; }

        public int SeasonID { get; set; }
        public int StudentID { get; set; }

        public virtual Season Season { get; set; }
        public virtual Student Student { get; set; }
    }
}
