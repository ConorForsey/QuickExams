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
    public partial class MainStudentForm : Form
    {
        private TabControl uiStudentTabControl;
        private TabPage uiDashboardTabPage;
        private TabPage uiEnrollmentTabPage;
        private TabPage uiTakeExamTabPage;
        private TabPage uiGadesTabPage;
        private TabPage uiSettingsTabPage;
        private Button uiNavLogoutButton;
        private Button uiNavSettingsButton;
        private Button uiNavResultsButton;
        private Button uiNavGradesButton;
        private Button uiNavExamsButton;
        private Button uiNavEnrollButton;
        private Button uiNavDashboardButton;
        private MonthCalendar monthCalendar1;
        private Label label11;
        private Label uiEnrollCourseLabel;
        private Label label6;
        private Label label5;
        private ListView uiEnrollCoursesListView;
        private ColumnHeader uiCourseIdColumnHeader;
        private ColumnHeader uiCourseTitleColumnHeader;
        private ColumnHeader uiCourseTeacherColumnHeader;
        private ListView uiExamTakeListView;
        private ColumnHeader uiExamStartTimeColumnHeader;
        private ColumnHeader uiExamExamIdColumnHeader;
        private Button uiEnrollButton;
        private TextBox textBox1;

        private StudentController studentController;
        private ColumnHeader uiTeacherLastNameColumnHeader;
        private ColumnHeader uiTeacherEmailColumnHeader;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListView uiEnrollCurrentCouresListView;
        private ColumnHeader uiEnrolledCourseIdColumnHeader3;
        private ColumnHeader uiEnrolledCourseTitleColumnHeader3;
        private ColumnHeader uiExamCourseTitleColumnHeader;
        private Button uiTakeExamButton;
        private ColumnHeader uiExamExamTitleColumnHeader;

        private IEnumerable<User> teachers;
        private IEnumerable<Course> courses;
        private ColumnHeader uiEnrollSemesterStartColumnHeader;
        private ColumnHeader uiEnrollSemesterEndColumnHeader;
        private Label label8;
        private Button button2;
        private Button button1;
        private IEnumerable<Course> enrolledCourses;
        private ColumnHeader uiExamEndTimeColumnHeader;
        private Label uiSettingsLabel;
        private TextBox uiNewPassConfirmTextBox;
        private TextBox uiNewPassTextBox;
        private Button uiNewPasswordButton;
        private Label uiNewPassConfirmLabel;
        private Label uiNewPassLabel;
        private TextBox uiNewPassExistingTextBox;
        private Label uiNewPaddExistingLabel;
        private IEnumerable<Exam> incompleteExams;

        public MainStudentForm()
        {
            InitializeComponent();
            studentController = new StudentController();          
        }

        private void MainStudentForm_VisibleChanged(object sender, EventArgs e)
        {
            // Enrollment Tab
            UpdateEnrollCoursesListView();
            UpdateCurrentlyEnrolled();

            // Take Exam Tab
            UpdateAvailableExams();
        }

        /// <summary>
        /// Gets the most recent version of the courses table from the database
        /// </summary>
        private async Task GetAllCourses()
        {
            courses = await studentController.SQLConnector().GetAllCoursesAsync();
        }

        private async Task GetTeachers()
        {
            teachers = await studentController.SQLConnector().GetAllTeachersAsync();
        }

        private async Task GetEnrolledCourses()
        {
            if(GlobalConfig.CurrentUser != null)
            {
                enrolledCourses = await studentController.SQLConnector().GetCoursesByUserId(GlobalConfig.CurrentUser.UserId);
            }          
        }

        // Enrollement page
        private void UiEnrollButton_Click(object sender, EventArgs e)
        {
            EnrollStudent();
        }
        private void UiEnrollCoursesListView_DoubleClick(object sender, EventArgs e)
        {
            EnrollStudent();
        }

        /// <summary>
        /// Enrolls the current logged in student in the the selected course
        /// </summary>
        private async void EnrollStudent()
        {
            if(uiEnrollCoursesListView.SelectedItems.Count > 0)
            {
                if (await studentController.SQLConnector().EnrollStudentAsync(Convert.ToInt32(uiEnrollCoursesListView.SelectedItems[0].SubItems[1].Text), GlobalConfig.CurrentUser.UserId))
                {
                    MessageBox.Show("You Have Been Enrolled!");
                    UpdateCurrentlyEnrolled();                
                }
                else MessageBox.Show("Enrollment Failed!");
            }
            
            
        }

        /// <summary>
        /// Fetches all courses from the courses table
        /// </summary>
        private async void UpdateEnrollCoursesListView()
        {
            uiEnrollCoursesListView.Items.Clear();
            await GetAllCourses();
            await GetTeachers();
            foreach (Course c in courses)
            {
                foreach (User t in teachers)
                {
                    if(c.TeacherId == t.UserId)
                    {
                        ListViewItem courseItem = new ListViewItem(c.CourseTitle);
                        courseItem.SubItems.Add(c.CourseId.ToString());
                        courseItem.SubItems.Add(c.TeacherId.ToString());
                        courseItem.SubItems.Add(t.LastName);
                        courseItem.SubItems.Add(t.Email);
                        uiEnrollCoursesListView.Items.Add(courseItem);
                    }
                }               
            }
        }

        /// <summary>
        /// Updates the currently enrolled listview with the courses the current user is enrolled into
        /// </summary>
        private async void UpdateCurrentlyEnrolled()
        {
            uiEnrollCurrentCouresListView.Items.Clear();
            await GetEnrolledCourses();
            foreach (Course enrolled in enrolledCourses)
            {
                Semester semester = await studentController.SQLConnector().GetSemesterAsync(enrolled.SemesterId);

                ListViewItem enrolledItem = new ListViewItem(enrolled.CourseTitle);
                enrolledItem.SubItems.Add(enrolled.CourseId.ToString());
                enrolledItem.SubItems.Add(semester.StartDate.ToString());
                enrolledItem.SubItems.Add(semester.EndDate.ToString());
                uiEnrollCurrentCouresListView.Items.Add(enrolledItem);
            }        
        }

        // Take Exam Tab
        private void UiTakeExamButton_Click(object sender, EventArgs e)
        {
            TakeExam();
        }

        private void UiExamTakeListView_DoubleClick(object sender, EventArgs e)
        {
            TakeExam();

        }

        /// <summary>
        /// Fetches the exams that have no been completed by the current user and display them using a listview
        /// </summary>
        private async void UpdateAvailableExams()
        {
            uiExamTakeListView.Items.Clear();

            if(GlobalConfig.CurrentUser != null)
            {
                incompleteExams = await studentController.SQLConnector().GetIncompleteExamsAsync(GlobalConfig.CurrentUser.UserId);

                foreach (Exam e in incompleteExams)
                {
                    foreach (Course c in courses)
                    {
                        if (e.CourseId == c.CourseId)
                        {
                            ListViewItem exam = new ListViewItem(e.ExamId.ToString());
                            exam.SubItems.Add(e.Title);
                            exam.SubItems.Add(c.CourseTitle);
                            if (e.StartTime == null)
                            {
                                exam.SubItems.Add("No Start Time");
                            }
                            else exam.SubItems.Add(e.StartTime.ToString());
                            if (e.EndTime == null)
                            {
                                exam.SubItems.Add("No End Time");
                            }
                            else exam.SubItems.Add(e.EndTime.ToString());
                            uiExamTakeListView.Items.Add(exam);
                        }
                    }
                }
            }                      
        }

        private void TakeExam()
        {
            if (uiExamTakeListView.SelectedItems.Count > 0)
            {
                ExamForm examForm = new ExamForm((Convert.ToInt32(uiExamTakeListView.SelectedItems[0].SubItems[0].Text)),this);
                this.Hide();
                examForm.ShowDialog();
            }
        }

        // Navigation buttons
        private void UiNavDashboardButton_Click(object sender, EventArgs e)
        {
            uiStudentTabControl.SelectedTab = uiDashboardTabPage;
        }

        private void UiNavEnrollButton_Click(object sender, EventArgs e)
        {
            uiStudentTabControl.SelectedTab = uiEnrollmentTabPage;
        }

        private void UiNavExamsButton_Click(object sender, EventArgs e)
        {
            uiStudentTabControl.SelectedTab = uiTakeExamTabPage;
        }

        private void UiNavGradesButton_Click(object sender, EventArgs e)
        {
            uiStudentTabControl.SelectedTab = uiGadesTabPage;
        }

        private void UiNavSettingsButton_Click(object sender, EventArgs e)
        {
            uiStudentTabControl.SelectedTab = uiSettingsTabPage;
        }

        /// <summary>
        /// Logs the current user out of the system and return them to the login page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (await studentController.ChangePassword(uiNewPassExistingTextBox.Text, uiNewPassTextBox.Text, uiNewPassConfirmTextBox.Text))
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

        /// <summary>
        /// Form design.cs file is missing so initialized components here
        /// </summary>
        private void InitializeComponent()
        {
            this.uiStudentTabControl = new System.Windows.Forms.TabControl();
            this.uiDashboardTabPage = new System.Windows.Forms.TabPage();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.uiEnrollmentTabPage = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.uiEnrollCurrentCouresListView = new System.Windows.Forms.ListView();
            this.uiEnrolledCourseTitleColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uiEnrolledCourseIdColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uiEnrollSemesterStartColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uiEnrollSemesterEndColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.uiEnrollButton = new System.Windows.Forms.Button();
            this.uiEnrollCoursesListView = new System.Windows.Forms.ListView();
            this.uiCourseTitleColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uiCourseIdColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uiCourseTeacherColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uiTeacherLastNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uiTeacherEmailColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uiEnrollCourseLabel = new System.Windows.Forms.Label();
            this.uiTakeExamTabPage = new System.Windows.Forms.TabPage();
            this.uiTakeExamButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.uiExamTakeListView = new System.Windows.Forms.ListView();
            this.uiExamExamIdColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uiExamExamTitleColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uiExamCourseTitleColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uiExamStartTimeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uiExamEndTimeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uiGadesTabPage = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.uiSettingsTabPage = new System.Windows.Forms.TabPage();
            this.uiNavLogoutButton = new System.Windows.Forms.Button();
            this.uiNavSettingsButton = new System.Windows.Forms.Button();
            this.uiNavResultsButton = new System.Windows.Forms.Button();
            this.uiNavGradesButton = new System.Windows.Forms.Button();
            this.uiNavExamsButton = new System.Windows.Forms.Button();
            this.uiNavEnrollButton = new System.Windows.Forms.Button();
            this.uiNavDashboardButton = new System.Windows.Forms.Button();
            this.uiSettingsLabel = new System.Windows.Forms.Label();
            this.uiNewPassConfirmTextBox = new System.Windows.Forms.TextBox();
            this.uiNewPassTextBox = new System.Windows.Forms.TextBox();
            this.uiNewPasswordButton = new System.Windows.Forms.Button();
            this.uiNewPassConfirmLabel = new System.Windows.Forms.Label();
            this.uiNewPassLabel = new System.Windows.Forms.Label();
            this.uiNewPassExistingTextBox = new System.Windows.Forms.TextBox();
            this.uiNewPaddExistingLabel = new System.Windows.Forms.Label();
            this.uiStudentTabControl.SuspendLayout();
            this.uiDashboardTabPage.SuspendLayout();
            this.uiEnrollmentTabPage.SuspendLayout();
            this.uiTakeExamTabPage.SuspendLayout();
            this.uiGadesTabPage.SuspendLayout();
            this.uiSettingsTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiStudentTabControl
            // 
            this.uiStudentTabControl.Controls.Add(this.uiDashboardTabPage);
            this.uiStudentTabControl.Controls.Add(this.uiEnrollmentTabPage);
            this.uiStudentTabControl.Controls.Add(this.uiTakeExamTabPage);
            this.uiStudentTabControl.Controls.Add(this.uiGadesTabPage);
            this.uiStudentTabControl.Controls.Add(this.uiSettingsTabPage);
            this.uiStudentTabControl.Location = new System.Drawing.Point(187, -1);
            this.uiStudentTabControl.Name = "uiStudentTabControl";
            this.uiStudentTabControl.SelectedIndex = 0;
            this.uiStudentTabControl.Size = new System.Drawing.Size(800, 500);
            this.uiStudentTabControl.TabIndex = 0;
            // 
            // uiDashboardTabPage
            // 
            this.uiDashboardTabPage.Controls.Add(this.monthCalendar1);
            this.uiDashboardTabPage.Controls.Add(this.label11);
            this.uiDashboardTabPage.Controls.Add(this.textBox1);
            this.uiDashboardTabPage.Location = new System.Drawing.Point(4, 22);
            this.uiDashboardTabPage.Name = "uiDashboardTabPage";
            this.uiDashboardTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.uiDashboardTabPage.Size = new System.Drawing.Size(792, 474);
            this.uiDashboardTabPage.TabIndex = 0;
            this.uiDashboardTabPage.Text = "Dashboard";
            this.uiDashboardTabPage.UseVisualStyleBackColor = true;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(538, 33);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(194, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Notifications";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(25, 33);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(460, 162);
            this.textBox1.TabIndex = 6;
            // 
            // uiEnrollmentTabPage
            // 
            this.uiEnrollmentTabPage.Controls.Add(this.label3);
            this.uiEnrollmentTabPage.Controls.Add(this.uiEnrollCurrentCouresListView);
            this.uiEnrollmentTabPage.Controls.Add(this.label1);
            this.uiEnrollmentTabPage.Controls.Add(this.uiEnrollButton);
            this.uiEnrollmentTabPage.Controls.Add(this.uiEnrollCoursesListView);
            this.uiEnrollmentTabPage.Controls.Add(this.uiEnrollCourseLabel);
            this.uiEnrollmentTabPage.Location = new System.Drawing.Point(4, 22);
            this.uiEnrollmentTabPage.Name = "uiEnrollmentTabPage";
            this.uiEnrollmentTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.uiEnrollmentTabPage.Size = new System.Drawing.Size(792, 474);
            this.uiEnrollmentTabPage.TabIndex = 1;
            this.uiEnrollmentTabPage.Text = "Enrollment";
            this.uiEnrollmentTabPage.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Current Enrolled Courses";
            // 
            // uiEnrollCurrentCouresListView
            // 
            this.uiEnrollCurrentCouresListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.uiEnrollCurrentCouresListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.uiEnrolledCourseTitleColumnHeader3,
            this.uiEnrolledCourseIdColumnHeader3,
            this.uiEnrollSemesterStartColumnHeader,
            this.uiEnrollSemesterEndColumnHeader});
            this.uiEnrollCurrentCouresListView.FullRowSelect = true;
            this.uiEnrollCurrentCouresListView.GridLines = true;
            this.uiEnrollCurrentCouresListView.HideSelection = false;
            this.uiEnrollCurrentCouresListView.Location = new System.Drawing.Point(3, 62);
            this.uiEnrollCurrentCouresListView.Name = "uiEnrollCurrentCouresListView";
            this.uiEnrollCurrentCouresListView.Size = new System.Drawing.Size(337, 188);
            this.uiEnrollCurrentCouresListView.TabIndex = 6;
            this.uiEnrollCurrentCouresListView.UseCompatibleStateImageBehavior = false;
            this.uiEnrollCurrentCouresListView.View = System.Windows.Forms.View.Details;
            // 
            // uiEnrolledCourseTitleColumnHeader3
            // 
            this.uiEnrolledCourseTitleColumnHeader3.Text = "Course Title";
            this.uiEnrolledCourseTitleColumnHeader3.Width = 98;
            // 
            // uiEnrolledCourseIdColumnHeader3
            // 
            this.uiEnrolledCourseIdColumnHeader3.Text = "Course ID";
            // 
            // uiEnrollSemesterStartColumnHeader
            // 
            this.uiEnrollSemesterStartColumnHeader.Text = "Start Date";
            this.uiEnrollSemesterStartColumnHeader.Width = 84;
            // 
            // uiEnrollSemesterEndColumnHeader
            // 
            this.uiEnrollSemesterEndColumnHeader.Text = "End Date";
            this.uiEnrollSemesterEndColumnHeader.Width = 90;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 36);
            this.label1.TabIndex = 5;
            this.label1.Text = "Course Enrollment";
            // 
            // uiEnrollButton
            // 
            this.uiEnrollButton.Location = new System.Drawing.Point(471, 448);
            this.uiEnrollButton.Name = "uiEnrollButton";
            this.uiEnrollButton.Size = new System.Drawing.Size(75, 23);
            this.uiEnrollButton.TabIndex = 4;
            this.uiEnrollButton.Text = "Enroll";
            this.uiEnrollButton.UseVisualStyleBackColor = true;
            this.uiEnrollButton.Click += new System.EventHandler(this.UiEnrollButton_Click);
            // 
            // uiEnrollCoursesListView
            // 
            this.uiEnrollCoursesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.uiCourseTitleColumnHeader,
            this.uiCourseIdColumnHeader,
            this.uiCourseTeacherColumnHeader,
            this.uiTeacherLastNameColumnHeader,
            this.uiTeacherEmailColumnHeader});
            this.uiEnrollCoursesListView.FullRowSelect = true;
            this.uiEnrollCoursesListView.GridLines = true;
            this.uiEnrollCoursesListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.uiEnrollCoursesListView.HideSelection = false;
            this.uiEnrollCoursesListView.Location = new System.Drawing.Point(261, 254);
            this.uiEnrollCoursesListView.MultiSelect = false;
            this.uiEnrollCoursesListView.Name = "uiEnrollCoursesListView";
            this.uiEnrollCoursesListView.Size = new System.Drawing.Size(520, 188);
            this.uiEnrollCoursesListView.TabIndex = 2;
            this.uiEnrollCoursesListView.UseCompatibleStateImageBehavior = false;
            this.uiEnrollCoursesListView.View = System.Windows.Forms.View.Details;
            this.uiEnrollCoursesListView.DoubleClick += new System.EventHandler(this.UiEnrollCoursesListView_DoubleClick);
            // 
            // uiCourseTitleColumnHeader
            // 
            this.uiCourseTitleColumnHeader.Text = "Course Title";
            this.uiCourseTitleColumnHeader.Width = 100;
            // 
            // uiCourseIdColumnHeader
            // 
            this.uiCourseIdColumnHeader.Text = "Course ID";
            this.uiCourseIdColumnHeader.Width = 66;
            // 
            // uiCourseTeacherColumnHeader
            // 
            this.uiCourseTeacherColumnHeader.Text = "Teacher ID";
            this.uiCourseTeacherColumnHeader.Width = 69;
            // 
            // uiTeacherLastNameColumnHeader
            // 
            this.uiTeacherLastNameColumnHeader.Text = "Teacher LastName";
            this.uiTeacherLastNameColumnHeader.Width = 110;
            // 
            // uiTeacherEmailColumnHeader
            // 
            this.uiTeacherEmailColumnHeader.Text = "Teacher Email Address";
            this.uiTeacherEmailColumnHeader.Width = 170;
            // 
            // uiEnrollCourseLabel
            // 
            this.uiEnrollCourseLabel.AutoSize = true;
            this.uiEnrollCourseLabel.Location = new System.Drawing.Point(412, 238);
            this.uiEnrollCourseLabel.Name = "uiEnrollCourseLabel";
            this.uiEnrollCourseLabel.Size = new System.Drawing.Size(192, 13);
            this.uiEnrollCourseLabel.TabIndex = 0;
            this.uiEnrollCourseLabel.Text = "Select an available course to enroll into";
            // 
            // uiTakeExamTabPage
            // 
            this.uiTakeExamTabPage.Controls.Add(this.uiTakeExamButton);
            this.uiTakeExamTabPage.Controls.Add(this.label2);
            this.uiTakeExamTabPage.Controls.Add(this.uiExamTakeListView);
            this.uiTakeExamTabPage.Location = new System.Drawing.Point(4, 22);
            this.uiTakeExamTabPage.Name = "uiTakeExamTabPage";
            this.uiTakeExamTabPage.Size = new System.Drawing.Size(792, 474);
            this.uiTakeExamTabPage.TabIndex = 2;
            this.uiTakeExamTabPage.Text = "Take Exam";
            this.uiTakeExamTabPage.UseVisualStyleBackColor = true;
            // 
            // uiTakeExamButton
            // 
            this.uiTakeExamButton.Location = new System.Drawing.Point(349, 283);
            this.uiTakeExamButton.Name = "uiTakeExamButton";
            this.uiTakeExamButton.Size = new System.Drawing.Size(115, 43);
            this.uiTakeExamButton.TabIndex = 15;
            this.uiTakeExamButton.Text = "Take Selected Test";
            this.uiTakeExamButton.UseVisualStyleBackColor = true;
            this.uiTakeExamButton.Click += new System.EventHandler(this.UiTakeExamButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(251, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Select available exam and take when you are ready";
            // 
            // uiExamTakeListView
            // 
            this.uiExamTakeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.uiExamExamIdColumnHeader,
            this.uiExamExamTitleColumnHeader,
            this.uiExamCourseTitleColumnHeader,
            this.uiExamStartTimeColumnHeader,
            this.uiExamEndTimeColumnHeader});
            this.uiExamTakeListView.FullRowSelect = true;
            this.uiExamTakeListView.GridLines = true;
            this.uiExamTakeListView.Location = new System.Drawing.Point(20, 62);
            this.uiExamTakeListView.Name = "uiExamTakeListView";
            this.uiExamTakeListView.Size = new System.Drawing.Size(470, 176);
            this.uiExamTakeListView.TabIndex = 13;
            this.uiExamTakeListView.UseCompatibleStateImageBehavior = false;
            this.uiExamTakeListView.View = System.Windows.Forms.View.Details;
            this.uiExamTakeListView.DoubleClick += new System.EventHandler(this.UiExamTakeListView_DoubleClick);
            // 
            // uiExamExamIdColumnHeader
            // 
            this.uiExamExamIdColumnHeader.Text = "Exam Id";
            this.uiExamExamIdColumnHeader.Width = 58;
            // 
            // uiExamExamTitleColumnHeader
            // 
            this.uiExamExamTitleColumnHeader.Text = "Exam Title";
            this.uiExamExamTitleColumnHeader.Width = 70;
            // 
            // uiExamCourseTitleColumnHeader
            // 
            this.uiExamCourseTitleColumnHeader.Text = "Course Title";
            this.uiExamCourseTitleColumnHeader.Width = 71;
            // 
            // uiExamStartTimeColumnHeader
            // 
            this.uiExamStartTimeColumnHeader.Text = "Start Time";
            // 
            // uiExamEndTimeColumnHeader
            // 
            this.uiExamEndTimeColumnHeader.Text = "End Time";
            // 
            // uiGadesTabPage
            // 
            this.uiGadesTabPage.Controls.Add(this.label8);
            this.uiGadesTabPage.Controls.Add(this.button2);
            this.uiGadesTabPage.Controls.Add(this.button1);
            this.uiGadesTabPage.Controls.Add(this.label6);
            this.uiGadesTabPage.Controls.Add(this.label5);
            this.uiGadesTabPage.Location = new System.Drawing.Point(4, 22);
            this.uiGadesTabPage.Name = "uiGadesTabPage";
            this.uiGadesTabPage.Size = new System.Drawing.Size(792, 474);
            this.uiGadesTabPage.TabIndex = 3;
            this.uiGadesTabPage.Text = "Grades";
            this.uiGadesTabPage.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(307, 220);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Download as csv also?";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(129, 179);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(149, 35);
            this.button2.TabIndex = 3;
            this.button2.Text = "Download selected result";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(129, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "Download all results";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(307, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Use ITextSharp for pdf";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(190, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Download results";
            // 
            // uiSettingsTabPage
            // 
            this.uiSettingsTabPage.Controls.Add(this.uiNewPassExistingTextBox);
            this.uiSettingsTabPage.Controls.Add(this.uiNewPaddExistingLabel);
            this.uiSettingsTabPage.Controls.Add(this.uiSettingsLabel);
            this.uiSettingsTabPage.Controls.Add(this.uiNewPassConfirmTextBox);
            this.uiSettingsTabPage.Controls.Add(this.uiNewPassTextBox);
            this.uiSettingsTabPage.Controls.Add(this.uiNewPasswordButton);
            this.uiSettingsTabPage.Controls.Add(this.uiNewPassConfirmLabel);
            this.uiSettingsTabPage.Controls.Add(this.uiNewPassLabel);
            this.uiSettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.uiSettingsTabPage.Name = "uiSettingsTabPage";
            this.uiSettingsTabPage.Size = new System.Drawing.Size(792, 474);
            this.uiSettingsTabPage.TabIndex = 4;
            this.uiSettingsTabPage.Text = "Settings";
            this.uiSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // uiNavLogoutButton
            // 
            this.uiNavLogoutButton.Location = new System.Drawing.Point(8, 433);
            this.uiNavLogoutButton.Name = "uiNavLogoutButton";
            this.uiNavLogoutButton.Size = new System.Drawing.Size(173, 64);
            this.uiNavLogoutButton.TabIndex = 18;
            this.uiNavLogoutButton.Text = "Logout";
            this.uiNavLogoutButton.UseVisualStyleBackColor = true;
            this.uiNavLogoutButton.Click += new System.EventHandler(this.UiNavLogoutButton_Click);
            // 
            // uiNavSettingsButton
            // 
            this.uiNavSettingsButton.Location = new System.Drawing.Point(8, 363);
            this.uiNavSettingsButton.Name = "uiNavSettingsButton";
            this.uiNavSettingsButton.Size = new System.Drawing.Size(173, 64);
            this.uiNavSettingsButton.TabIndex = 17;
            this.uiNavSettingsButton.Text = "Settings";
            this.uiNavSettingsButton.UseVisualStyleBackColor = true;
            this.uiNavSettingsButton.Click += new System.EventHandler(this.UiNavSettingsButton_Click);
            // 
            // uiNavResultsButton
            // 
            this.uiNavResultsButton.Location = new System.Drawing.Point(8, 293);
            this.uiNavResultsButton.Name = "uiNavResultsButton";
            this.uiNavResultsButton.Size = new System.Drawing.Size(173, 64);
            this.uiNavResultsButton.TabIndex = 16;
            this.uiNavResultsButton.Text = "-----------";
            this.uiNavResultsButton.UseVisualStyleBackColor = true;
            // 
            // uiNavGradesButton
            // 
            this.uiNavGradesButton.Location = new System.Drawing.Point(8, 223);
            this.uiNavGradesButton.Name = "uiNavGradesButton";
            this.uiNavGradesButton.Size = new System.Drawing.Size(173, 64);
            this.uiNavGradesButton.TabIndex = 15;
            this.uiNavGradesButton.Text = "View Grades";
            this.uiNavGradesButton.UseVisualStyleBackColor = true;
            this.uiNavGradesButton.Click += new System.EventHandler(this.UiNavGradesButton_Click);
            // 
            // uiNavExamsButton
            // 
            this.uiNavExamsButton.Location = new System.Drawing.Point(8, 153);
            this.uiNavExamsButton.Name = "uiNavExamsButton";
            this.uiNavExamsButton.Size = new System.Drawing.Size(173, 64);
            this.uiNavExamsButton.TabIndex = 14;
            this.uiNavExamsButton.Text = "Exams";
            this.uiNavExamsButton.UseVisualStyleBackColor = true;
            this.uiNavExamsButton.Click += new System.EventHandler(this.UiNavExamsButton_Click);
            // 
            // uiNavEnrollButton
            // 
            this.uiNavEnrollButton.Location = new System.Drawing.Point(8, 83);
            this.uiNavEnrollButton.Name = "uiNavEnrollButton";
            this.uiNavEnrollButton.Size = new System.Drawing.Size(173, 64);
            this.uiNavEnrollButton.TabIndex = 13;
            this.uiNavEnrollButton.Text = "Enroll";
            this.uiNavEnrollButton.UseVisualStyleBackColor = true;
            this.uiNavEnrollButton.Click += new System.EventHandler(this.UiNavEnrollButton_Click);
            // 
            // uiNavDashboardButton
            // 
            this.uiNavDashboardButton.Location = new System.Drawing.Point(8, 12);
            this.uiNavDashboardButton.Name = "uiNavDashboardButton";
            this.uiNavDashboardButton.Size = new System.Drawing.Size(173, 64);
            this.uiNavDashboardButton.TabIndex = 12;
            this.uiNavDashboardButton.Text = "Dashboard";
            this.uiNavDashboardButton.UseVisualStyleBackColor = true;
            this.uiNavDashboardButton.Click += new System.EventHandler(this.UiNavDashboardButton_Click);
            // 
            // uiSettingsLabel
            // 
            this.uiSettingsLabel.AutoSize = true;
            this.uiSettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiSettingsLabel.Location = new System.Drawing.Point(3, 2);
            this.uiSettingsLabel.Name = "uiSettingsLabel";
            this.uiSettingsLabel.Size = new System.Drawing.Size(113, 31);
            this.uiSettingsLabel.TabIndex = 13;
            this.uiSettingsLabel.Text = "Settings";
            // 
            // uiNewPassConfirmTextBox
            // 
            this.uiNewPassConfirmTextBox.Location = new System.Drawing.Point(136, 123);
            this.uiNewPassConfirmTextBox.Name = "uiNewPassConfirmTextBox";
            this.uiNewPassConfirmTextBox.Size = new System.Drawing.Size(194, 20);
            this.uiNewPassConfirmTextBox.TabIndex = 12;
            // 
            // uiNewPassTextBox
            // 
            this.uiNewPassTextBox.Location = new System.Drawing.Point(136, 93);
            this.uiNewPassTextBox.Name = "uiNewPassTextBox";
            this.uiNewPassTextBox.Size = new System.Drawing.Size(194, 20);
            this.uiNewPassTextBox.TabIndex = 11;
            // 
            // uiNewPasswordButton
            // 
            this.uiNewPasswordButton.Location = new System.Drawing.Point(136, 149);
            this.uiNewPasswordButton.Name = "uiNewPasswordButton";
            this.uiNewPasswordButton.Size = new System.Drawing.Size(96, 32);
            this.uiNewPasswordButton.TabIndex = 10;
            this.uiNewPasswordButton.Text = "Confirm Change";
            this.uiNewPasswordButton.UseVisualStyleBackColor = true;
            this.uiNewPasswordButton.Click += new System.EventHandler(this.UiNewPasswordButton_Click);
            // 
            // uiNewPassConfirmLabel
            // 
            this.uiNewPassConfirmLabel.AutoSize = true;
            this.uiNewPassConfirmLabel.Location = new System.Drawing.Point(38, 126);
            this.uiNewPassConfirmLabel.Name = "uiNewPassConfirmLabel";
            this.uiNewPassConfirmLabel.Size = new System.Drawing.Size(90, 13);
            this.uiNewPassConfirmLabel.TabIndex = 9;
            this.uiNewPassConfirmLabel.Text = "Confirm password";
            // 
            // uiNewPassLabel
            // 
            this.uiNewPassLabel.AutoSize = true;
            this.uiNewPassLabel.Location = new System.Drawing.Point(38, 96);
            this.uiNewPassLabel.Name = "uiNewPassLabel";
            this.uiNewPassLabel.Size = new System.Drawing.Size(78, 13);
            this.uiNewPassLabel.TabIndex = 8;
            this.uiNewPassLabel.Text = "New Password";
            // 
            // uiNewPassExistingTextBox
            // 
            this.uiNewPassExistingTextBox.Location = new System.Drawing.Point(136, 62);
            this.uiNewPassExistingTextBox.Name = "uiNewPassExistingTextBox";
            this.uiNewPassExistingTextBox.Size = new System.Drawing.Size(194, 20);
            this.uiNewPassExistingTextBox.TabIndex = 15;
            // 
            // uiNewPaddExistingLabel
            // 
            this.uiNewPaddExistingLabel.AutoSize = true;
            this.uiNewPaddExistingLabel.Location = new System.Drawing.Point(38, 65);
            this.uiNewPaddExistingLabel.Name = "uiNewPaddExistingLabel";
            this.uiNewPaddExistingLabel.Size = new System.Drawing.Size(92, 13);
            this.uiNewPaddExistingLabel.TabIndex = 14;
            this.uiNewPaddExistingLabel.Text = "Existing Password";
            // 
            // MainStudentForm
            // 
            this.ClientSize = new System.Drawing.Size(984, 506);
            this.ControlBox = false;
            this.Controls.Add(this.uiNavLogoutButton);
            this.Controls.Add(this.uiNavSettingsButton);
            this.Controls.Add(this.uiNavResultsButton);
            this.Controls.Add(this.uiNavGradesButton);
            this.Controls.Add(this.uiNavExamsButton);
            this.Controls.Add(this.uiNavEnrollButton);
            this.Controls.Add(this.uiNavDashboardButton);
            this.Controls.Add(this.uiStudentTabControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainStudentForm";
            this.ShowIcon = false;
            this.Text = "Quick Exams";
            this.VisibleChanged += new System.EventHandler(this.MainStudentForm_VisibleChanged);
            this.uiStudentTabControl.ResumeLayout(false);
            this.uiDashboardTabPage.ResumeLayout(false);
            this.uiDashboardTabPage.PerformLayout();
            this.uiEnrollmentTabPage.ResumeLayout(false);
            this.uiEnrollmentTabPage.PerformLayout();
            this.uiTakeExamTabPage.ResumeLayout(false);
            this.uiTakeExamTabPage.PerformLayout();
            this.uiGadesTabPage.ResumeLayout(false);
            this.uiGadesTabPage.PerformLayout();
            this.uiSettingsTabPage.ResumeLayout(false);
            this.uiSettingsTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

    }
}
