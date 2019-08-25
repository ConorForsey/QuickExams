using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickExamLibrary.Models
{
    public class Choice
    {
        public int ChoiceId { get; set; }
        public int QuestionId { get; set; }
        public string ChoiceText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
