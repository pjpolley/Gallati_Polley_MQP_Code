namespace Sender
{
    partial class ContinuousNeuralNetForm
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
            this.button2 = new System.Windows.Forms.Button();
            this.Reader = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(65, 24);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(140, 47);
            this.TestButton.TabIndex = 0;
            this.TestButton.Text = "Test";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(65, 132);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 47);
            this.button2.TabIndex = 1;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Reader
            // 
            this.Reader.Location = new System.Drawing.Point(65, 79);
            this.Reader.Name = "Reader";
            this.Reader.Size = new System.Drawing.Size(140, 47);
            this.Reader.TabIndex = 2;
            this.Reader.Text = "Read BCI";
            this.Reader.UseVisualStyleBackColor = true;
            this.Reader.Click += new System.EventHandler(this.Reader_Click);
            // 
            // ContinuousNeuralNetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.Reader);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.TestButton);
            this.Name = "ContinuousNeuralNetForm";
            this.Text = "ContinuousNeuralNetForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Reader;
    }
}