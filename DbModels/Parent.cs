using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels
{
    public class Parent
    {
        public int ParentID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public int AddressID { get; set; }
        public int UserID { get; set; }

        public virtual Address Address { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<StudentParent> StudentParents { get; set; }
    }
}
