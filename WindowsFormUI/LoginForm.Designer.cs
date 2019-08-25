namespace WindowsFormUI
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiUsernameTextBox = new System.Windows.Forms.TextBox();
            this.uiPasswordTextBox = new System.Windows.Forms.TextBox();
            this.uiLoginButton = new System.Windows.Forms.Button();
            this.uiUsernameLabel = new System.Windows.Forms.Label();
            this.uiPasswordLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.uiMessageLabel = new System.Windows.Forms.Label();
            this.uiRegisterButton = new System.Windows.Forms.Button();
            this.uiForgotPassButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uiUsernameTextBox
            // 
            this.uiUsernameTextBox.Location = new System.Drawing.Point(168, 84);
            this.uiUsernameTextBox.Name = "uiUsernameTextBox";
            this.uiUsernameTextBox.Size = new System.Drawing.Size(148, 20);
            this.uiUsernameTextBox.TabIndex = 0;
            // 
            // uiPasswordTextBox
            // 
            this.uiPasswordTextBox.Location = new System.Drawing.Point(168, 118);
            this.uiPasswordTextBox.Name = "uiPasswordTextBox";
            this.uiPasswordTextBox.PasswordChar = '*';
            this.uiPasswordTextBox.Size = new System.Drawing.Size(148, 20);
            this.uiPasswordTextBox.TabIndex = 1;
            // 
            // uiLoginButton
            // 
            this.uiLoginButton.Location = new System.Drawing.Point(198, 155);
            this.uiLoginButton.Name = "uiLoginButton";
            this.uiLoginButton.Size = new System.Drawing.Size(89, 25);
            this.uiLoginButton.TabIndex = 4;
            this.uiLoginButton.Text = "Login";
            this.uiLoginButton.UseVisualStyleBackColor = true;
            this.uiLoginButton.Click += new System.EventHandler(this.uiLoginButton_Click);
            // 
            // uiUsernameLabel
            // 
            this.uiUsernameLabel.AutoSize = true;
            this.uiUsernameLabel.Location = new System.Drawing.Point(104, 87);
            this.uiUsernameLabel.Name = "uiUsernameLabel";
            this.uiUsernameLabel.Size = new System.Drawing.Size(58, 13);
            this.uiUsernameLabel.TabIndex = 5;
            this.uiUsernameLabel.Text = "User name";
            // 
            // uiPasswordLabel
            // 
            this.uiPasswordLabel.AutoSize = true;
            this.uiPasswordLabel.Location = new System.Drawing.Point(109, 121);
            this.uiPasswordLabel.Name = "uiPasswordLabel";
            this.uiPasswordLabel.Size = new System.Drawing.Size(53, 13);
            this.uiPasswordLabel.TabIndex = 6;
            this.uiPasswordLabel.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(124, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 37);
            this.label1.TabIndex = 7;
            this.label1.Text = "QUICK EXAMS";
            // 
            // uiMessageLabel
            // 
            this.uiMessageLabel.AutoSize = true;
            this.uiMessageLabel.Enabled = false;
            this.uiMessageLabel.Location = new System.Drawing.Point(322, 125);
            this.uiMessageLabel.Name = "uiMessageLabel";
            this.uiMessageLabel.Size = new System.Drawing.Size(161, 13);
            this.uiMessageLabel.TabIndex = 8;
            this.uiMessageLabel.Text = "Incorrect Username or Password";
            this.uiMessageLabel.Visible = false;
            // 
            // uiRegisterButton
            // 
            this.uiRegisterButton.Location = new System.Drawing.Point(198, 186);
            this.uiRegisterButton.Name = "uiRegisterButton";
            this.uiRegisterButton.Size = new System.Drawing.Size(89, 23);
            this.uiRegisterButton.TabIndex = 9;
            this.uiRegisterButton.Text = "Register";
            this.uiRegisterButton.UseVisualStyleBackColor = true;
            this.uiRegisterButton.Click += new System.EventHandler(this.uiRegisterButton_Click);
            // 
            // uiForgotPassButton
            // 
            this.uiForgotPassButton.FlatAppearance.BorderSize = 0;
            this.uiForgotPassButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.uiForgotPassButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiForgotPassButton.Location = new System.Drawing.Point(189, 215);
            this.uiForgotPassButton.Name = "uiForgotPassButton";
            this.uiForgotPassButton.Size = new System.Drawing.Size(107, 23);
            this.uiForgotPassButton.TabIndex = 10;
            this.uiForgotPassButton.Text = "Forgot Password?";
            this.uiForgotPassButton.UseVisualStyleBackColor = true;
            this.uiForgotPassButton.Click += new System.EventHandler(this.UiForgotPassButton_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.uiForgotPassButton);
            this.Controls.Add(this.uiRegisterButton);
            this.Controls.Add(this.uiMessageLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uiPasswordLabel);
            this.Controls.Add(this.uiUsernameLabel);
            this.Controls.Add(this.uiLoginButton);
            this.Controls.Add(this.uiPasswordTextBox);
            this.Controls.Add(this.uiUsernameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.Text = "Quick Exams";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox uiUsernameTextBox;
        private System.Windows.Forms.TextBox uiPasswordTextBox;
        private System.Windows.Forms.Button uiLoginButton;
        private System.Windows.Forms.Label uiUsernameLabel;
        private System.Windows.Forms.Label uiPasswordLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label uiMessageLabel;
        private System.Windows.Forms.Button uiRegisterButton;
        private System.Windows.Forms.Button uiForgotPassButton;
    }
}

