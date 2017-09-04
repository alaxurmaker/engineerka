using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels
{
    public class StudentParent
    {
        public int StudentParentID { get; set; }

        public int StudentID { get; set; }
        public int ParentID { get; set; }

        public virtual Parent Parent { get; set; }
        public virtual Student Student { get; set; }
        
    }
}
