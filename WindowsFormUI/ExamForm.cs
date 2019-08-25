using QuickExamLibrary;
using QuickExamLibrary.Controllers;
using QuickExamLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormUI
{
    public partial class ExamForm : Form
    {
        private ExamController mExamController;
        private MainStudentForm mStudentForm;
        private IList<Question> mQuestions;
        private List<RadioButton> mSelectionRadioButtons;
        private int mCurrentQuestionNumber = 1;
        private int mExamId;

        public ExamForm(int examId, MainStudentForm mainStudentForm)
        {
            this.Text = "Quick Exams - Student: " + GlobalConfig.CurrentUser.FirstName + " " + GlobalConfig.CurrentUser.LastName + " - User ID: " + GlobalConfig.CurrentUser.UserId;
            InitializeComponent();
            mExamController = new ExamController();
            mStudentForm = mainStudentForm;
            mExamId = examId;
            mSelectionRadioButtons = new List<RadioButton>();
            mSelectionRadioButtons.Add(uiAnswer1RadioButton);
            mSelectionRadioButtons.Add(uiAnswer2RadioButton);
            mSelectionRadioButtons.Add(uiAnswer3RadioButton);
            mSelectionRadioButtons.Add(uiAnswer4RadioButton);
        }

        private async void ExamForm_Load(object sender, EventArgs e)
        {
            mQuestions = await mExamController.SQLConnector().GetQuestionsAsync(mExamId);
            PopulateQuestionInterface();
        }

        private void UpdateCurrentQuestionTextBox()
        {
            uiNumberQsTextBox.Text = (mCurrentQuestionNumber).ToString() + "/" + mQuestions.Count().ToString();
        }
        private void PopulateQuestionInterface()
        {
            UpdateCurrentQuestionTextBox();
            Question question = mQuestions[mCurrentQuestionNumber-1];
            uiQuestionTextTextBox.Text = question.QuestionText;
            uiAnswer1TextBox.Text = question.Choices[0].ChoiceText;
            uiAnswer2TextBox.Text = question.Choices[1].ChoiceText;
            uiAnswer3TextBox.Text = question.Choices[2].ChoiceText;
            uiAnswer4TextBox.Text = question.Choices[3].ChoiceText;

            uiAnswer1RadioButton.Checked = false;
            uiAnswer2RadioButton.Checked = false;
            uiAnswer3RadioButton.Checked = false;
            uiAnswer4RadioButton.Checked = false;

        }

        private void UiBackQuestionButton_Click(object sender, EventArgs e)
        {
            if (mCurrentQuestionNumber > 1)
            {
                mCurrentQuestionNumber--;
                PopulateQuestionInterface();
            }
        }

        private async void UiNextQuestionButton_Click(object sender, EventArgs e)
        {
            if (mCurrentQuestionNumber <= mQuestions.Count)
            { 
                if (uiAnswer1RadioButton.Checked || uiAnswer2RadioButton.Checked || uiAnswer3RadioButton.Checked || uiAnswer4RadioButton.Checked)
                {
                    if (mCurrentQuestionNumber == mQuestions.Count)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to finish the exam?", "Finish Exam", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            mExamController.AnswerQuestion(mCurrentQuestionNumber, ChoiceSelection(), mQuestions[mCurrentQuestionNumber - 1].QuestionId);
                            await mExamController.CompleteExam(mExamId);
                            ReturnToMain();
                        }                       
                    }
                    else
                    {                      
                        mExamController.AnswerQuestion(mCurrentQuestionNumber, ChoiceSelection(), mQuestions[mCurrentQuestionNumber-1].QuestionId);
                        mCurrentQuestionNumber++;
                        PopulateQuestionInterface();
                        if(mCurrentQuestionNumber == mQuestions.Count)
                        {
                            uiNextQuestionButton.Text = "Complete Exam";
                        }
                        
                    }                   
                }
                else MessageBox.Show("Select an option before continuing!");
            }  
        }

        private int ChoiceSelection()
        {
            int selectionIndex = -1;
            for (int i = 0; i < mSelectionRadioButtons.Count; i++)
            {
                if (mSelectionRadioButtons[i].Checked)
                {
                    selectionIndex = mQuestions[mCurrentQuestionNumber-1].Choices[i].ChoiceId;
                }
            }
            return selectionIndex;
        }

        private void ReturnToMain()
        {
            this.Hide();
            mStudentForm.Show();
            this.Close();
        }

    }
}
