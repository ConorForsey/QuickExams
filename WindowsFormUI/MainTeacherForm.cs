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
    public partial class MainTeacherForm : Form
    {
        private TeacherController teacherController;

        // Courses Tab
        IList<Semester> mAllSemesters;
        IList<Course> mAllCourses;
        IList<Exam> mAllExams;

        // Create Exam tab
        List<TextBox> mChoiceTextBoxes;
        List<RadioButton> mChoiceRadioButtons;

        /// <summary>
        /// On teacher form load
        /// </summary>
        public MainTeacherForm()
        {
            InitializeComponent();

            teacherController = new TeacherController();

            // Courses Tab
            UpdateSemestersCombobox();
            UpdateCourseCombobox();

            // Checks user is logged in
            if(GlobalConfig.CurrentUser == null)
            {
                uiCourseTeacherTextBox.Text = "Please Login Again!";
                this.Hide();
                LoginForm login = new LoginForm();
                login.ShowDialog();
                this.Close();
            }
            else
            {
                uiCourseTeacherTextBox.Text = GlobalConfig.CurrentUser.FirstName + " " + GlobalConfig.CurrentUser.LastName;
            }

            // Create Exam Tab
            UpdateExamCourseCombobox();
            uiStartDateTimePicker.ShowCheckBox = true;
            uiEndDateTimePicker.ShowCheckBox = true;
            uiStartDateTimePicker.Checked = true;
            uiEndDateTimePicker.Checked = true;

            // Adds all the choice textboxes to a list
            mChoiceTextBoxes = new List<TextBox>();
            mChoiceTextBoxes.Add(uiAnswer1TextBox);
            mChoiceTextBoxes.Add(uiAnswer2TextBox);
            mChoiceTextBoxes.Add(uiAnswer3TextBox);
            mChoiceTextBoxes.Add(uiAnswer4TextBox);

            // Adds all the choice radio buttons to a list
            mChoiceRadioButtons = new List<RadioButton>();
            mChoiceRadioButtons.Add(uiCorrect1RadioButton);
            mChoiceRadioButtons.Add(uiCorrect2RadioButton);
            mChoiceRadioButtons.Add(uiCorrect3RadioButton);
            mChoiceRadioButtons.Add(uiCorrect4RadioButton);
        }

        // Methods
        private async Task GetCourses()
        {
            mAllCourses = await teacherController.SQLConnector().GetAllCoursesAsync();
        } // Updates the courses list with the most recent data
        private async Task GetSemesters()
        {
            mAllSemesters = await teacherController.SQLConnector().GetAllSemestersAsync();
        } // Updates the semesters list with the most recent data
        private async Task GetExamsForCourse(int courseId)
        {
            mAllExams = await teacherController.SQLConnector().GetExamByCourseIdAsync(courseId);
        }

        // Courses Tab
        /// <summary>
        /// Create new semester
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void uiSemesterCreateButton_Click(object sender, EventArgs e)
        {
            if (await teacherController.SQLConnector().NewSemesterAsync(uiStartDateTimePicker.Text, uiEndDateTimePicker.Text))
            {
                UpdateSemestersCombobox();
            }
            else
            {
                MessageBox.Show("Problem Creating New Semester!");
            }               
        }

        /// <summary>
        /// Create new course with the teacher set as the teacher currently logged in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void uiCreateCourseButton_Click(object sender, EventArgs e)
        {
            if(CoursesValidation())
            {
                int semesterId = mAllSemesters[uiSemesterComboBox.SelectedIndex].SemesterId;
                if(await teacherController.SQLConnector().NewCourseAsync(uiCourseTitleTextBox.Text, GlobalConfig.CurrentUser.UserId, semesterId))
                {
                    MessageBox.Show("Course Added!");
                    UpdateCourseCombobox();
                }
            }                       
        }

        /// <summary>
        /// Updates the data in the semesters combobox
        /// </summary>
        private async void UpdateSemestersCombobox()
        {
            await GetSemesters();
            foreach (Semester s in mAllSemesters)
            {
                uiSemesterComboBox.Items.Add(s.StartDate.ToString("dd-MM-yyyy") + " To " + s.EndDate.ToString("dd-MM-yyyy"));
            }
        }

        /// <summary>
        /// Updates the data in the view all courses listview
        /// </summary>
        private async void UpdateCourseCombobox()
        {
            uiAllCoursesListView.Items.Clear();
            await GetCourses();

            foreach (Course c in mAllCourses)
            {
                foreach (Semester s in mAllSemesters)
                {
                    if(c.SemesterId == s.SemesterId)
                    {
                        ListViewItem courseItem = new ListViewItem(c.CourseTitle);
                        courseItem.SubItems.Add(c.TeacherId.ToString());
                        courseItem.SubItems.Add(s.StartDate.ToString("dd-MM-yyyy"));
                        courseItem.SubItems.Add(s.EndDate.ToString("dd-MM-yyyy"));
                        uiAllCoursesListView.Items.Add(courseItem);
                    }                  
                }            
            }
        }

        /// <summary>
        /// Validates the user inputs before adding the data to the database
        /// </summary>
        /// <returns></returns>
        private bool CoursesValidation()
        {
            bool valid = true;

            if (uiCourseTitleTextBox.Text.Length >= 0 || uiCourseTitleTextBox.Text.Length > 100)
            {
                valid = false;
            }

            if(uiSemesterComboBox.SelectedIndex == -1)
            {
                valid = false;
            }

            if(GlobalConfig.CurrentUser == null)
            {
                valid = false;
            }

            return valid;
        }

        // Create Exam Tab
        /// <summary>
        /// Create new exam
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void uiCreateExamButton_Click(object sender, EventArgs e)
        {          
            if (CreateExamValidation())
            {
                Exam exam = new Exam();
                if (uiStartDateTimePicker.Checked == true)
                {
                    exam.StartTime = uiStartDateTimePicker.Value;
                } else exam.StartTime = null;

                if (uiEndDateTimePicker.Checked == true)
                {
                    exam.EndTime = uiEndDateTimePicker.Value;
                } else exam.EndTime = null;

                exam.Title = uiExamTitleTextBox.Text;
                exam.CourseId = mAllCourses[uiCourseComboBox.SelectedIndex].CourseId;

                await teacherController.SQLConnector().NewExamAsync(exam.Title, exam.CourseId, exam.StartTime, exam.EndTime);
            }           
        }

        private async void uiAddQuestionButton_Click(object sender, EventArgs e)
        {
            if (CreateQuestionValidation())
            {
                uiAddQuestionButton.Enabled = false;
                int questionId = await teacherController.SQLConnector().AddQuestionAsync(mAllExams[uiExamComboBox.SelectedIndex].ExamId, uiQuestionTextTextBox.Text);
                await teacherController.AddChoice(questionId, mChoiceTextBoxes, mChoiceRadioButtons);
                MessageBox.Show("Question Added!");
                ClearQuestion();
                uiAddQuestionButton.Enabled = true; ;

            }
            else MessageBox.Show("Make sure all fields are filled");        
        }

        private bool CreateQuestionValidation()
        {
            bool valid = true;
            if(uiQuestionCourseCombobox.SelectedIndex == -1)
            {
                valid = false;
            }

            if(uiExamComboBox.SelectedIndex == -1)
            {
                valid = false;
            }

            if (string.IsNullOrEmpty(uiQuestionTextTextBox.Text) || uiQuestionTextTextBox.Text.Length > 100)
            {
                valid = false;
            }

            if (string.IsNullOrEmpty(uiAnswer1TextBox.Text) || uiAnswer1TextBox.Text.Length > 100)
            {
                valid = false;
            }

            if (string.IsNullOrEmpty(uiAnswer2TextBox.Text) || uiAnswer2TextBox.Text.Length > 100)
            {
                valid = false;         
            }

            if (string.IsNullOrEmpty(uiAnswer3TextBox.Text) || uiAnswer3TextBox.Text.Length > 100)
            {
                valid = false;
            }

            if (string.IsNullOrEmpty(uiAnswer4TextBox.Text) || uiAnswer4TextBox.Text.Length > 100)
            {
                valid = false;
            }

            return valid;
        }

        private void uiClearQuestionButton_Click(object sender, EventArgs e)
        {
            ClearQuestion();
        }

        private void ClearQuestion()
        {
            uiQuestionTextTextBox.Text = "";
            uiAnswer1TextBox.Text = "";
            uiAnswer2TextBox.Text = "";
            uiAnswer4TextBox.Text = "";
            uiAnswer3TextBox.Text = "";
        }

        /// <summary>
        /// Updates the course combo box
        /// </summary>
        private async void UpdateExamCourseCombobox()
        {
            uiCourseComboBox.Items.Clear();
            uiQuestionCourseCombobox.Items.Clear();
            await GetCourses();

            foreach (Course c in mAllCourses)
            {
                uiCourseComboBox.Items.Add(c.CourseId.ToString() + ": " + c.CourseTitle);
                uiQuestionCourseCombobox.Items.Add(c.CourseId.ToString() + ": " + c.CourseTitle);
            }
        }

        /// <summary>
        /// Updates the Exams combobox with the most recent data
        /// </summary>
        private async void UpdateExamCombobox()
        {
            uiExamComboBox.Items.Clear();
            uiExamComboBox.Text = "";
            await GetExamsForCourse(mAllCourses[uiQuestionCourseCombobox.SelectedIndex].CourseId);
            foreach (Exam e in mAllExams)
            {
                uiExamComboBox.Items.Add(e.ExamId + ": " + e.Title);
            }
            
        }

        /// <summary>
        /// Validates the create exam input
        /// </summary>
        /// <returns></returns>
        private bool CreateExamValidation()
        {
            bool valid = true;

            if(uiCourseComboBox.SelectedIndex == -1)
            {
                valid = false;
            }
            
            if(uiExamTitleTextBox.Text.Length <= 0 || uiExamTitleTextBox.Text.Length > 100)
            {
                valid = false;
            }

            return valid;
        }

        private void UiQuestionCourseCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateExamCombobox();
        }

        private void UiQuestionDeleteButton_Click(object sender, EventArgs e)
        {

        }

        private void UiUpdateQuestionButton_Click(object sender, EventArgs e)
        {

        }

        private void UiExamEditCourseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UiExamEditComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // View Enrollment Tab



        // View Exams Tab



        // Results Tab



        // Settings Tab



        // Navigation Buttons
        private void UiNavDashboardButton_Click(object sender, EventArgs e)
        {
            uiTeacherMainTabControl.SelectedTab = uiDashboardTabPage;
        }

        private void UiNavEnrollmentButton_Click(object sender, EventArgs e)
        {
            uiTeacherMainTabControl.SelectedTab = uiEnrollmentTabPage;
        }

        private void UiNavExamButton_Click(object sender, EventArgs e)
        {
            uiTeacherMainTabControl.SelectedTab = uiExamTabPage;
        }

        private void UiNavCoursesButton_Click(object sender, EventArgs e)
        {
            uiTeacherMainTabControl.SelectedTab = uiCoursesTabPage;
        }

        private void UiNavResultsButton_Click(object sender, EventArgs e)
        {
            uiTeacherMainTabControl.SelectedTab = uiViewResultsTabPage;
        }

        private void UiNavSettingsButton_Click(object sender, EventArgs e)
        {
            uiTeacherMainTabControl.SelectedTab = uiSetttingsTabPage;
        }

        private void UiNavLogoutButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Logout", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                GlobalConfig.CurrentUser = null;
                this.Hide();
                LoginForm login = new LoginForm();
                login.ShowDialog();
                this.Close();
            }         
        }

        // Settings Tab
        private async void UiNewPasswordButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(uiNewPassExistingTextBox.Text) || string.IsNullOrEmpty(uiNewPassTextBox.Text) || string.IsNullOrEmpty(uiNewPassConfirmTextBox.Text))
            {
                if (await teacherController.ChangePassword(uiNewPassExistingTextBox.Text, uiNewPassTextBox.Text, uiNewPassConfirmTextBox.Text))
                {
                    uiNewPassExistingTextBox.Clear();
                    uiNewPassTextBox.Clear();
                    uiNewPassConfirmTextBox.Clear();
                    MessageBox.Show("Password Change successful!");
                }
                else MessageBox.Show("Please check the new password and the confirm password match");
            }
            else MessageBox.Show("Please fill all required fields");
        }
    }
}
