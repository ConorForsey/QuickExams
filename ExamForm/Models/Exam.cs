using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickExamLibrary.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public Exam()
        {

        }


    }
}
