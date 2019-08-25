namespace WindowsFormUI
{
    partial class ExamForm
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
            this.uiNumberQsLabel = new System.Windows.Forms.Label();
            this.uiNumberQsTextBox = new System.Windows.Forms.TextBox();
            this.uiNextQuestionButton = new System.Windows.Forms.Button();
            this.uiBackQuestionButton = new System.Windows.Forms.Button();
            this.uiQuestionGroupBox = new System.Windows.Forms.GroupBox();
            this.uiHelpLabel = new System.Windows.Forms.Label();
            this.uiAnswer4Label = new System.Windows.Forms.Label();
            this.uiAnswer3Label = new System.Windows.Forms.Label();
            this.uiAnswer2Label = new System.Windows.Forms.Label();
            this.uiAnswer1Label = new System.Windows.Forms.Label();
            this.uiQuestionTextLabel = new System.Windows.Forms.Label();
            this.uiQuestionTextTextBox = new System.Windows.Forms.TextBox();
            this.uiAnswer4RadioButton = new System.Windows.Forms.RadioButton();
            this.uiAnswer1TextBox = new System.Windows.Forms.TextBox();
            this.uiAnswer3RadioButton = new System.Windows.Forms.RadioButton();
            this.uiAnswer1RadioButton = new System.Windows.Forms.RadioButton();
            this.uiAnswer2RadioButton = new System.Windows.Forms.RadioButton();
            this.uiAnswer2TextBox = new System.Windows.Forms.TextBox();
            this.uiAnswer4TextBox = new System.Windows.Forms.TextBox();
            this.uiAnswer3TextBox = new System.Windows.Forms.TextBox();
            this.uiQuestionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiNumberQsLabel
            // 
            this.uiNumberQsLabel.AutoSize = true;
            this.uiNumberQsLabel.Location = new System.Drawing.Point(554, 9);
            this.uiNumberQsLabel.Name = "uiNumberQsLabel";
            this.uiNumberQsLabel.Size = new System.Drawing.Size(112, 13);
            this.uiNumberQsLabel.TabIndex = 23;
            this.uiNumberQsLabel.Text = "Number of Questions: ";
            // 
            // uiNumberQsTextBox
            // 
            this.uiNumberQsTextBox.Location = new System.Drawing.Point(669, 6);
            this.uiNumberQsTextBox.Name = "uiNumberQsTextBox";
            this.uiNumberQsTextBox.ReadOnly = true;
            this.uiNumberQsTextBox.Size = new System.Drawing.Size(103, 20);
            this.uiNumberQsTextBox.TabIndex = 22;
            // 
            // uiNextQuestionButton
            // 
            this.uiNextQuestionButton.Location = new System.Drawing.Point(257, 363);
            this.uiNextQuestionButton.Name = "uiNextQuestionButton";
            this.uiNextQuestionButton.Size = new System.Drawing.Size(91, 36);
            this.uiNextQuestionButton.TabIndex = 28;
            this.uiNextQuestionButton.Text = "Next";
            this.uiNextQuestionButton.UseVisualStyleBackColor = true;
            this.uiNextQuestionButton.Click += new System.EventHandler(this.UiNextQuestionButton_Click);
            // 
            // uiBackQuestionButton
            // 
            this.uiBackQuestionButton.Location = new System.Drawing.Point(108, 363);
            this.uiBackQuestionButton.Name = "uiBackQuestionButton";
            this.uiBackQuestionButton.Size = new System.Drawing.Size(91, 36);
            this.uiBackQuestionButton.TabIndex = 27;
            this.uiBackQuestionButton.Text = "Back";
            this.uiBackQuestionButton.UseVisualStyleBackColor = true;
            this.uiBackQuestionButton.Click += new System.EventHandler(this.UiBackQuestionButton_Click);
            // 
            // uiQuestionGroupBox
            // 
            this.uiQuestionGroupBox.Controls.Add(this.uiNextQuestionButton);
            this.uiQuestionGroupBox.Controls.Add(this.uiBackQuestionButton);
            this.uiQuestionGroupBox.Controls.Add(this.uiHelpLabel);
            this.uiQuestionGroupBox.Controls.Add(this.uiAnswer4Label);
            this.uiQuestionGroupBox.Controls.Add(this.uiAnswer3Label);
            this.uiQuestionGroupBox.Controls.Add(this.uiAnswer2Label);
            this.uiQuestionGroupBox.Controls.Add(this.uiAnswer1Label);
            this.uiQuestionGroupBox.Controls.Add(this.uiQuestionTextLabel);
            this.uiQuestionGroupBox.Controls.Add(this.uiQuestionTextTextBox);
            this.uiQuestionGroupBox.Controls.Add(this.uiAnswer4RadioButton);
            this.uiQuestionGroupBox.Controls.Add(this.uiAnswer1TextBox);
            this.uiQuestionGroupBox.Controls.Add(this.uiAnswer3RadioButton);
            this.uiQuestionGroupBox.Controls.Add(this.uiAnswer1RadioButton);
            this.uiQuestionGroupBox.Controls.Add(this.uiAnswer2RadioButton);
            this.uiQuestionGroupBox.Controls.Add(this.uiAnswer2TextBox);
            this.uiQuestionGroupBox.Controls.Add(this.uiAnswer4TextBox);
            this.uiQuestionGroupBox.Controls.Add(this.uiAnswer3TextBox);
            this.uiQuestionGroupBox.Location = new System.Drawing.Point(105, 12);
            this.uiQuestionGroupBox.Name = "uiQuestionGroupBox";
            this.uiQuestionGroupBox.Size = new System.Drawing.Size(443, 405);
            this.uiQuestionGroupBox.TabIndex = 26;
            this.uiQuestionGroupBox.TabStop = false;
            // 
            // uiHelpLabel
            // 
            this.uiHelpLabel.AutoSize = true;
            this.uiHelpLabel.Location = new System.Drawing.Point(3, 145);
            this.uiHelpLabel.Name = "uiHelpLabel";
            this.uiHelpLabel.Size = new System.Drawing.Size(205, 13);
            this.uiHelpLabel.TabIndex = 28;
            this.uiHelpLabel.Text = "Select one option from the choices below.";
            // 
            // uiAnswer4Label
            // 
            this.uiAnswer4Label.AutoSize = true;
            this.uiAnswer4Label.Location = new System.Drawing.Point(6, 312);
            this.uiAnswer4Label.Name = "uiAnswer4Label";
            this.uiAnswer4Label.Size = new System.Drawing.Size(51, 13);
            this.uiAnswer4Label.TabIndex = 27;
            this.uiAnswer4Label.Text = "Answer 4";
            // 
            // uiAnswer3Label
            // 
            this.uiAnswer3Label.AutoSize = true;
            this.uiAnswer3Label.Location = new System.Drawing.Point(6, 264);
            this.uiAnswer3Label.Name = "uiAnswer3Label";
            this.uiAnswer3Label.Size = new System.Drawing.Size(51, 13);
            this.uiAnswer3Label.TabIndex = 26;
            this.uiAnswer3Label.Text = "Answer 3";
            // 
            // uiAnswer2Label
            // 
            this.uiAnswer2Label.AutoSize = true;
            this.uiAnswer2Label.Location = new System.Drawing.Point(6, 219);
            this.uiAnswer2Label.Name = "uiAnswer2Label";
            this.uiAnswer2Label.Size = new System.Drawing.Size(51, 13);
            this.uiAnswer2Label.TabIndex = 25;
            this.uiAnswer2Label.Text = "Answer 2";
            // 
            // uiAnswer1Label
            // 
            this.uiAnswer1Label.AutoSize = true;
            this.uiAnswer1Label.Location = new System.Drawing.Point(6, 167);
            this.uiAnswer1Label.Name = "uiAnswer1Label";
            this.uiAnswer1Label.Size = new System.Drawing.Size(51, 13);
            this.uiAnswer1Label.TabIndex = 24;
            this.uiAnswer1Label.Text = "Answer 1";
            // 
            // uiQuestionTextLabel
            // 
            this.uiQuestionTextLabel.AutoSize = true;
            this.uiQuestionTextLabel.Location = new System.Drawing.Point(6, 16);
            this.uiQuestionTextLabel.Name = "uiQuestionTextLabel";
            this.uiQuestionTextLabel.Size = new System.Drawing.Size(49, 13);
            this.uiQuestionTextLabel.TabIndex = 12;
            this.uiQuestionTextLabel.Text = "Question";
            // 
            // uiQuestionTextTextBox
            // 
            this.uiQuestionTextTextBox.Location = new System.Drawing.Point(6, 32);
            this.uiQuestionTextTextBox.Multiline = true;
            this.uiQuestionTextTextBox.Name = "uiQuestionTextTextBox";
            this.uiQuestionTextTextBox.ReadOnly = true;
            this.uiQuestionTextTextBox.Size = new System.Drawing.Size(413, 83);
            this.uiQuestionTextTextBox.TabIndex = 21;
            // 
            // uiAnswer4RadioButton
            // 
            this.uiAnswer4RadioButton.AutoSize = true;
            this.uiAnswer4RadioButton.Location = new System.Drawing.Point(334, 329);
            this.uiAnswer4RadioButton.Name = "uiAnswer4RadioButton";
            this.uiAnswer4RadioButton.Size = new System.Drawing.Size(69, 17);
            this.uiAnswer4RadioButton.TabIndex = 20;
            this.uiAnswer4RadioButton.TabStop = true;
            this.uiAnswer4RadioButton.Text = "Answer 4";
            this.uiAnswer4RadioButton.UseVisualStyleBackColor = true;
            // 
            // uiAnswer1TextBox
            // 
            this.uiAnswer1TextBox.Location = new System.Drawing.Point(9, 183);
            this.uiAnswer1TextBox.Name = "uiAnswer1TextBox";
            this.uiAnswer1TextBox.ReadOnly = true;
            this.uiAnswer1TextBox.Size = new System.Drawing.Size(319, 20);
            this.uiAnswer1TextBox.TabIndex = 13;
            // 
            // uiAnswer3RadioButton
            // 
            this.uiAnswer3RadioButton.AutoSize = true;
            this.uiAnswer3RadioButton.Location = new System.Drawing.Point(334, 281);
            this.uiAnswer3RadioButton.Name = "uiAnswer3RadioButton";
            this.uiAnswer3RadioButton.Size = new System.Drawing.Size(69, 17);
            this.uiAnswer3RadioButton.TabIndex = 19;
            this.uiAnswer3RadioButton.TabStop = true;
            this.uiAnswer3RadioButton.Text = "Answer 3";
            this.uiAnswer3RadioButton.UseVisualStyleBackColor = true;
            // 
            // uiAnswer1RadioButton
            // 
            this.uiAnswer1RadioButton.AutoSize = true;
            this.uiAnswer1RadioButton.Location = new System.Drawing.Point(334, 184);
            this.uiAnswer1RadioButton.Name = "uiAnswer1RadioButton";
            this.uiAnswer1RadioButton.Size = new System.Drawing.Size(69, 17);
            this.uiAnswer1RadioButton.TabIndex = 14;
            this.uiAnswer1RadioButton.TabStop = true;
            this.uiAnswer1RadioButton.Text = "Answer 1";
            this.uiAnswer1RadioButton.UseVisualStyleBackColor = true;
            // 
            // uiAnswer2RadioButton
            // 
            this.uiAnswer2RadioButton.AutoSize = true;
            this.uiAnswer2RadioButton.Location = new System.Drawing.Point(334, 236);
            this.uiAnswer2RadioButton.Name = "uiAnswer2RadioButton";
            this.uiAnswer2RadioButton.Size = new System.Drawing.Size(69, 17);
            this.uiAnswer2RadioButton.TabIndex = 18;
            this.uiAnswer2RadioButton.TabStop = true;
            this.uiAnswer2RadioButton.Text = "Answer 2";
            this.uiAnswer2RadioButton.UseVisualStyleBackColor = true;
            // 
            // uiAnswer2TextBox
            // 
            this.uiAnswer2TextBox.Location = new System.Drawing.Point(9, 235);
            this.uiAnswer2TextBox.Name = "uiAnswer2TextBox";
            this.uiAnswer2TextBox.ReadOnly = true;
            this.uiAnswer2TextBox.Size = new System.Drawing.Size(319, 20);
            this.uiAnswer2TextBox.TabIndex = 15;
            // 
            // uiAnswer4TextBox
            // 
            this.uiAnswer4TextBox.Location = new System.Drawing.Point(9, 328);
            this.uiAnswer4TextBox.Name = "uiAnswer4TextBox";
            this.uiAnswer4TextBox.ReadOnly = true;
            this.uiAnswer4TextBox.Size = new System.Drawing.Size(319, 20);
            this.uiAnswer4TextBox.TabIndex = 17;
            // 
            // uiAnswer3TextBox
            // 
            this.uiAnswer3TextBox.Location = new System.Drawing.Point(9, 281);
            this.uiAnswer3TextBox.Name = "uiAnswer3TextBox";
            this.uiAnswer3TextBox.ReadOnly = true;
            this.uiAnswer3TextBox.Size = new System.Drawing.Size(319, 20);
            this.uiAnswer3TextBox.TabIndex = 16;
            // 
            // ExamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.uiQuestionGroupBox);
            this.Controls.Add(this.uiNumberQsLabel);
            this.Controls.Add(this.uiNumberQsTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExamForm";
            this.ShowIcon = false;
            this.Text = "ExamForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ExamForm_Load);
            this.uiQuestionGroupBox.ResumeLayout(false);
            this.uiQuestionGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label uiNumberQsLabel;
        private System.Windows.Forms.TextBox uiNumberQsTextBox;
        private System.Windows.Forms.Button uiNextQuestionButton;
        private System.Windows.Forms.Button uiBackQuestionButton;
        private System.Windows.Forms.GroupBox uiQuestionGroupBox;
        private System.Windows.Forms.Label uiHelpLabel;
        private System.Windows.Forms.Label uiAnswer4Label;
        private System.Windows.Forms.Label uiAnswer3Label;
        private System.Windows.Forms.Label uiAnswer2Label;
        private System.Windows.Forms.Label uiAnswer1Label;
        private System.Windows.Forms.Label uiQuestionTextLabel;
        private System.Windows.Forms.TextBox uiQuestionTextTextBox;
        private System.Windows.Forms.RadioButton uiAnswer4RadioButton;
        private System.Windows.Forms.TextBox uiAnswer1TextBox;
        private System.Windows.Forms.RadioButton uiAnswer3RadioButton;
        private System.Windows.Forms.RadioButton uiAnswer1RadioButton;
        private System.Windows.Forms.RadioButton uiAnswer2RadioButton;
        private System.Windows.Forms.TextBox uiAnswer2TextBox;
        private System.Windows.Forms.TextBox uiAnswer4TextBox;
        private System.Windows.Forms.TextBox uiAnswer3TextBox;
    }
}