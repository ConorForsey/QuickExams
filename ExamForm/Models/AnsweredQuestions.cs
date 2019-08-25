using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickExamLibrary.Models
{
    public class AnsweredQuestions
    {
        public int AnsweredQuestionId { get; set; }
        public int ChoiceId { get; set; }
        public int QuestionId { get; set; }
        public int StudentExamId { get; set; }
    }
}
