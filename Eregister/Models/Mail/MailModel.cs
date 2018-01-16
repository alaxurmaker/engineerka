using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eregister.Models.Mail
{
    public class MailModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}