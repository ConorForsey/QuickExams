using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickExamLibrary.Models
{
    public class Semester
    {
        public int SemesterId { get; set; }
        // Format: YYYY-MM-DD
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
