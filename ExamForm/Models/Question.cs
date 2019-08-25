using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickExamLibrary.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int ExamId { get; set; }
        public string QuestionText { get; set; }

        private readonly List<Choice> _Choices = new List<Choice>();

        public IList<Choice> Choices { get { return _Choices; } }

        public void AddChoice(Choice choice)
        {
            _Choices.Add(choice);
        }
    }
}
