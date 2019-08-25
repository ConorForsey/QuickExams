using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickExamLibrary.Models
{
    public class StudentExam
    {
        public int StudentExamId { get; set; }
        public int UserId { get; set; }
        public int ExamId { get; set; }
    }
}
