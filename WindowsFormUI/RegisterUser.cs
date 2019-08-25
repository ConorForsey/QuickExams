using QuickExamLibrary;
using QuickExamLibrary.Models;
using QuickExamLibrary.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormUI
{
    public partial class RegisterUser : Form
    {
        private LoginController loginController;
        public RegisterUser()
        {
            InitializeComponent();
            loginController = new LoginController();
        }

        private async void uiRegisterButton_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                User newUser = new User(uiUsernameTextBox.Text, 
                    uiPasswordTextBox.Text, 
                    uiFirstnameTextBox.Text, 
                    uiLastnameTextBox.Text, 
                    uiEmailAddressTextBox.Text,
                    RoleType.Student);
              
                bool created = await loginController.SQLConnector().CreateNewUserAsync(newUser);
                if(created)
                {
                    MessageBox.Show("Student Successfully Registered!");
                    BackToLogin();                   
                }
                else
                {
                    MessageBox.Show("User Already Exists!");
                    uiUsernameTextBox.Text = "";
                    uiPasswordTextBox.Text = "";
                    uiFirstnameTextBox.Text = "";
                    uiLastnameTextBox.Text = "";
                    uiEmailAddressTextBox.Text = "";
                }
            }
        }

        /// <summary>
        /// Registration input validation
        /// </summary>
        /// <returns></returns>
        private bool validation()
        {
            bool output = true;

            if (uiFirstnameTextBox.Text.Length <= 0 || uiFirstnameTextBox.Text.Length > 100)
            {
                uiFirstnameInvalidLabel.Visible = true;
                output = false;
            }
            else uiFirstnameInvalidLabel.Visible = false;

            if (uiLastnameTextBox.Text.Length <= 0 || uiLastnameTextBox.Text.Length > 100)
            {
                uiLastnameInvalidLabel.Visible = true;
                output = false;
            }
            else uiLastnameInvalidLabel.Visible = false;

            if (uiUsernameTextBox.Text.Length <= 0 || uiUsernameTextBox.Text.Length > 100)
            {
                uiUsernameInvalidLabel.Visible = true;
                output = false;
            }
            else uiUsernameInvalidLabel.Visible = false;

            if (uiPasswordTextBox.Text.Length <= 0 || uiPasswordTextBox.Text.Length > 100)
            {
                uiPasswordInvalidLabel.Visible = true;
                output = false;
            }
            else uiPasswordInvalidLabel.Visible = false;

            if (!IsEmailValid(uiEmailAddressTextBox.Text))
            {
                uiEmailInvalidLabel.Visible = true;
                output = false;
            }
            else uiEmailInvalidLabel.Visible = false;

            return output;
        }

        /// <summary>
        /// Checks email input is a valid email address format
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        private bool IsEmailValid(string emailAddress)
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

        /// <summary>
        /// Takes the user back to the login screen
        /// </summary>
        private void BackToLogin()
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.ShowDialog();
            this.Close();
        }

        private void uiBackToLoginButton_Click(object sender, EventArgs e)
        {
            BackToLogin();
        }
    }
}
