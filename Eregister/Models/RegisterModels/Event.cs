using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eregister
{
    public class Event
    {
        public int id { get; set; }
        public string text { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public int group_id { get; set; }
    }
}