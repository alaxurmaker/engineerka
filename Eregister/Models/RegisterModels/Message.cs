using Eregister.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eregister
{
    public class Message
    {
        public int MessageID { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public string UserFrom { get; set; }
        public string From { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public bool IsOn { get; set; }
        public DateTime? AddDate { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}