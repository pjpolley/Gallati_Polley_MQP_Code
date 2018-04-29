namespace Sender
{
    partial class UnsureNetworkForm
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
            this.TestButton = new System.Windows.Forms.Button();
            this.TrainButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.logButton = new System.Windows.Forms.Button();
            this.DefaultPositionsBox = new System.Windows.Forms.ListBox();
            this.PositionTextBox = new System.Windows.Forms.TextBox();
            this.currentGoalLabel = new System.Windows.Forms.Label();
            this.FocusTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FocusButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(12, 26);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(108, 46);
            this.TestButton.TabIndex = 0;
            this.TestButton.Text = "Test";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // TrainButton
            // 
            this.TrainButton.Location = new System.Drawing.Point(13, 78);
            this.TrainButton.Name = "TrainButton";
            this.TrainButton.Size = new System.Drawing.Size(108, 46);
            this.TrainButton.TabIndex = 1;
            this.TrainButton.Text = "Train";
            this.TrainButton.UseVisualStyleBackColor = true;
            this.TrainButton.Click += new System.EventHandler(this.TrainButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(13, 130);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(108, 46);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Save Data";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // logButton
            // 
            this.logButton.Location = new System.Drawing.Point(138, 190);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(134, 46);
            this.logButton.TabIndex = 3;
            this.logButton.Text = "Log Position";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // DefaultPositionsBox
            // 
            this.DefaultPositionsBox.FormattingEnabled = true;
            this.DefaultPositionsBox.Location = new System.Drawing.Point(138, 78);
            this.DefaultPositionsBox.Name = "DefaultPositionsBox";
            this.DefaultPositionsBox.Size = new System.Drawing.Size(134, 95);
            this.DefaultPositionsBox.TabIndex = 4;
            this.DefaultPositionsBox.SelectedIndexChanged += new System.EventHandler(this.DefaultPositionsBox_SelectedIndexChanged);
            // 
            // PositionTextBox
            // 
            this.PositionTextBox.Location = new System.Drawing.Point(138, 26);
            this.PositionTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.PositionTextBox.Name = "PositionTextBox";
            this.PositionTextBox.ReadOnly = true;
            this.PositionTextBox.Size = new System.Drawing.Size(60, 20);
            this.PositionTextBox.TabIndex = 30;
            // 
            // currentGoalLabel
            // 
            this.currentGoalLabel.AutoSize = true;
            this.currentGoalLabel.Location = new System.Drawing.Point(144, 48);
            this.currentGoalLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentGoalLabel.Name = "currentGoalLabel";
            this.currentGoalLabel.Size = new System.Drawing.Size(44, 13);
            this.currentGoalLabel.TabIndex = 31;
            this.currentGoalLabel.Text = "Position";
            // 
            // FocusTextBox
            // 
            this.FocusTextBox.Location = new System.Drawing.Point(212, 26);
            this.FocusTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.FocusTextBox.Name = "FocusTextBox";
            this.FocusTextBox.ReadOnly = true;
            this.FocusTextBox.Size = new System.Drawing.Size(60, 20);
            this.FocusTextBox.TabIndex = 32;
            this.FocusTextBox.TextChanged += new System.EventHandler(this.FocusTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(221, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Focus";
            // 
            // FocusButton
            // 
            this.FocusButton.Location = new System.Drawing.Point(13, 190);
            this.FocusButton.Name = "FocusButton";
            this.FocusButton.Size = new System.Drawing.Size(108, 46);
            this.FocusButton.TabIndex = 34;
            this.FocusButton.Text = "Configure Focus";
            this.FocusButton.UseVisualStyleBackColor = true;
            this.FocusButton.Click += new System.EventHandler(this.FocusButton_Click);
            // 
            // UnsureNetworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.FocusButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FocusTextBox);
            this.Controls.Add(this.currentGoalLabel);
            this.Controls.Add(this.PositionTextBox);
            this.Controls.Add(this.DefaultPositionsBox);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.TrainButton);
            this.Controls.Add(this.TestButton);
            this.Name = "UnsureNetworkForm";
            this.Text = "UnsureNetworkForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Button TrainButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.ListBox DefaultPositionsBox;
        private System.Windows.Forms.TextBox PositionTextBox;
        private System.Windows.Forms.Label currentGoalLabel;
        private System.Windows.Forms.TextBox FocusTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button FocusButton;
    }
}