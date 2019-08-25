using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickExamLibrary.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int TeacherId { get; set; }
        public int SemesterId { get; set; }       
    }
}
