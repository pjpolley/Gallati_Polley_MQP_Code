namespace Sender
{
    partial class NeuralNetForm
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
            this.logButton = new System.Windows.Forms.Button();
            this.DefaultPositionsBox = new System.Windows.Forms.ListBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TestButton
            // 
            this.TestButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.TestButton.Location = new System.Drawing.Point(12, 24);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(99, 43);
            this.TestButton.TabIndex = 0;
            this.TestButton.Text = "Test";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // TrainButton
            // 
            this.TrainButton.Location = new System.Drawing.Point(12, 83);
            this.TrainButton.Name = "TrainButton";
            this.TrainButton.Size = new System.Drawing.Size(99, 43);
            this.TrainButton.TabIndex = 1;
            this.TrainButton.Text = "Train Network";
            this.TrainButton.UseVisualStyleBackColor = true;
            this.TrainButton.Click += new System.EventHandler(this.TrainButton_Click);
            // 
            // logButton
            // 
            this.logButton.Location = new System.Drawing.Point(138, 183);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(134, 43);
            this.logButton.TabIndex = 2;
            this.logButton.Text = "Log Position";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // DefaultPositionsBox
            // 
            this.DefaultPositionsBox.FormattingEnabled = true;
            this.DefaultPositionsBox.Location = new System.Drawing.Point(138, 24);
            this.DefaultPositionsBox.Name = "DefaultPositionsBox";
            this.DefaultPositionsBox.Size = new System.Drawing.Size(134, 134);
            this.DefaultPositionsBox.TabIndex = 3;
            this.DefaultPositionsBox.SelectedIndexChanged += new System.EventHandler(this.DefaultPositionsBox_SelectedIndexChanged);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(12, 183);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SaveButton.Size = new System.Drawing.Size(99, 43);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save Weights";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // NeuralNetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.DefaultPositionsBox);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.TrainButton);
            this.Controls.Add(this.TestButton);
            this.Name = "NeuralNetForm";
            this.Text = "NeuralNetForm";
            this.Load += new System.EventHandler(this.NeuralNetForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Button TrainButton;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.ListBox DefaultPositionsBox;
        private System.Windows.Forms.Button SaveButton;
    }
}