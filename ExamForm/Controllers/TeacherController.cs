using QuickExamLibrary.DataAccessController;
using QuickExamLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickExamLibrary.Controllers
{
    public class TeacherController : ControllerBase
    {
        public TeacherController()
        {
        }

        public async Task<bool> AddChoice(int questionId, List<TextBox> choiceTextBoxes, List<RadioButton> choiceRadioButtons)
        {
            List<Task<bool>> tasks = new List<Task<bool>>();

            for (int h = 0; h < choiceTextBoxes.Count; h++)
            {
                tasks.Add(SQLConnector().AddChoiceAsync(questionId, choiceTextBoxes[h].Text, choiceRadioButtons[h].Checked));
            }
            await Task.WhenAll(tasks);
            return true;
        }       

    }
}
