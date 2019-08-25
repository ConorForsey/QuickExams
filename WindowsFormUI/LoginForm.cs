using QuickExamLibrary;
using QuickExamLibrary.Models;
using QuickExamLibrary.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace WindowsFormUI
{
    public partial class LoginForm : Form
    {
        private LoginController loginController;
        public LoginForm()
        {
            InitializeComponent();
            loginController = new LoginController();
        }

        private async void uiLoginButton_Click(object sender, EventArgs e)
        {
            String username = uiUsernameTextBox.Text;
            String password = uiPasswordTextBox.Text;

            bool Authenticated = await loginController.SQLConnector().AuthenticateUserAsync(username, password);

            if (Authenticated)
            {
                GlobalConfig.CurrentUser = await loginController.SQLConnector().GetUserAsync(username, password);
                User currentUser = GlobalConfig.CurrentUser;

                switch (currentUser.RoleType)
                {
                    case RoleType.Student:
                        setMessageLabelVisible(false);
                        this.Hide();                                            //Hide Login form
                        MainStudentForm studentForm = new MainStudentForm();    //Initialise and declare form
                        studentForm.Text = "Quick Exams - Student: " + currentUser.FirstName + " " + currentUser.LastName + " - User ID: " + currentUser.UserId;
                        studentForm.ShowDialog();                               //Open form
                        this.Close();
                        break;
                    case RoleType.Teacher:
                        setMessageLabelVisible(false);
                        this.Hide();                                            //Hide Login form
                        MainTeacherForm teacherForm = new MainTeacherForm();    //Initialise and declare form
                        teacherForm.Text = "Quick Exams - Teacher: " + currentUser.FirstName + " " + currentUser.LastName + " - User ID: " + currentUser.UserId;
                        teacherForm.ShowDialog();                               //Open form
                        this.Close();                                           //Close login form
                        break;
                    case RoleType.Admin:

                        break;
                    default:
                        break;
                }
            }
            else setMessageLabelVisible(true);
        }

        // Hides Incorrect username/password label
        private void setMessageLabelVisible(bool visible)
        {
            uiMessageLabel.Visible = visible;
        }

        //Takes user to registration page
        private void uiRegisterButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterUser registerUser = new RegisterUser();
            registerUser.Text = "Quick Exams - Registration";
            registerUser.ShowDialog();
        }

        private async void UiForgotPassButton_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Please enter the email address for the account that you have forgotton the password for", "Forgotton Password", "", 0, 0);
            if (ValidateEmail(input))
            {
                if(await loginController.ForgotPassword(input))
                {
                    MessageBox.Show("New password has been sent");
                } else MessageBox.Show("There was a problem sending a new password");
            }
            else MessageBox.Show("Please check that you have input a valid email address");
        }

        private bool ValidateEmail(String emailAddress)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(emailAddress) && emailAddress.Length < 101)
                {
                    MailAddress m = new MailAddress(emailAddress);
                    return true;
                }
                else return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
