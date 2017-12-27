using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eregister.Models
{
    public class AlertContent
    {
        public int AlertContentID { get; set; }
        public string Content { get; set; }

        public virtual ICollection<Alert> Alerts { get; set; }
    }
}