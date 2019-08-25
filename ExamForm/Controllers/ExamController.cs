using QuickExamLibrary.DataAccessController;
using QuickExamLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickExamLibrary.Controllers
{
    public class ExamController : ControllerBase
    {
        private List<AnsweredQuestions> mAnsweredQuestions = new List<AnsweredQuestions>();

        public ExamController()
        {
            
        }

        public void AnswerQuestion(int questionNumber, int choiceId, int questionId)
        {
            int questionIndex = questionNumber - 1;
            if(mAnsweredQuestions.Count <= questionIndex)
            {
                AnsweredQuestions answered = new AnsweredQuestions();
                answered.ChoiceId = choiceId;
                answered.QuestionId = questionId;
                mAnsweredQuestions.Add(answered);                
            }
            else
            {
                mAnsweredQuestions[questionIndex].ChoiceId = choiceId;
                mAnsweredQuestions[questionIndex].QuestionId = questionId;
            }
            
        }

        public async Task CompleteExam(int examId)
        {
            List<Task<bool>> tasks = new List<Task<bool>>();
            int studentExamId = await SQLConnector().FinishExamAsync(GlobalConfig.CurrentUser.UserId, examId);

            foreach (AnsweredQuestions answeredQuestions in mAnsweredQuestions)
            {
                tasks.Add(SQLConnector().AnswerQuestionAsync(answeredQuestions.ChoiceId, answeredQuestions.QuestionId, studentExamId));
            }
            await Task.WhenAll(tasks);
        }
    }
}
