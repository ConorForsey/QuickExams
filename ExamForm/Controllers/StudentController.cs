using QuickExamLibrary.DataAccessController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickExamLibrary.Controllers
{
    public class StudentController : ControllerBase
    {
        public StudentController()
        {
        }

        public void GetStudentsAvailableExams()
        {
            // if student has no score, show exam
            // get all questions, if answered question entry exists 
            // Need a new table for students choice?
        }
    }
}
