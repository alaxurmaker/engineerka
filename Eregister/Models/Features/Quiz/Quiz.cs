using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eregister.Models.Features.Quiz
{
    public class Quiz
    {
        public int QuizID { get; set; }

        public string Question { get; set; }
        public int Correct { get; set; }

        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }

        public int? Score { get; set; }

    }
}