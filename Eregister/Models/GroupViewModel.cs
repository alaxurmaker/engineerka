using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eregister.Models
{
    public class GroupViewModel
    {
        // public Group Group { get; set; }
        public int? GroupID { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string ShortDescription { get; set; }
        public string StudentId { get; set; }

        public StudentsListViewModel StudentsListViewModel { get; set; }
        public List<StudentsListViewModel> StudentsListView { get; set; }

        public IEnumerable<SelectListItem> StudentsSelectList { get; set; }
        public string SelectedStudent { get; set; }
    }

    public class StudentsListViewModel
    {
        public string NameSurname { get; set; }
        public string Pesel { get; set; }
        public DateTime? JoinDate { get; set; }
    }
}