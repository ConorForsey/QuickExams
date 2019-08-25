using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickExamLibrary.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int CourseId    { get; set; }
        public int StudentId { get; set; }
    }    
}

