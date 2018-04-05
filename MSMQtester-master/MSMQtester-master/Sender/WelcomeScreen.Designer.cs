﻿namespace Sender
{
    partial class WelcomeScreen
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
            this.NeuralTreeButton = new System.Windows.Forms.Button();
            this.NeuralNetButton = new System.Windows.Forms.Button();
            this.BasicFunctionalityButton = new System.Windows.Forms.Button();
            this.clearDataButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NeuralTreeButton
            // 
            this.NeuralTreeButton.Location = new System.Drawing.Point(66, 36);
            this.NeuralTreeButton.Name = "NeuralTreeButton";
            this.NeuralTreeButton.Size = new System.Drawing.Size(216, 148);
            this.NeuralTreeButton.TabIndex = 0;
            this.NeuralTreeButton.Text = "Neural Tree";
            this.NeuralTreeButton.UseVisualStyleBackColor = true;
            this.NeuralTreeButton.Click += new System.EventHandler(this.NeuralTreeButton_Click);
            // 
            // NeuralNetButton
            // 
            this.NeuralNetButton.Location = new System.Drawing.Point(288, 36);
            this.NeuralNetButton.Name = "NeuralNetButton";
            this.NeuralNetButton.Size = new System.Drawing.Size(216, 148);
            this.NeuralNetButton.TabIndex = 1;
            this.NeuralNetButton.Text = "Neural Net";
            this.NeuralNetButton.UseVisualStyleBackColor = true;
            // 
            // BasicFunctionalityButton
            // 
            this.BasicFunctionalityButton.Location = new System.Drawing.Point(510, 36);
            this.BasicFunctionalityButton.Name = "BasicFunctionalityButton";
            this.BasicFunctionalityButton.Size = new System.Drawing.Size(216, 148);
            this.BasicFunctionalityButton.TabIndex = 2;
            this.BasicFunctionalityButton.Text = "Basic Functionality";
            this.BasicFunctionalityButton.UseVisualStyleBackColor = true;
            this.BasicFunctionalityButton.Click += new System.EventHandler(this.BasicFunctionalityButton_Click);
            // 
            // clearDataButton
            // 
            this.clearDataButton.Location = new System.Drawing.Point(288, 290);
            this.clearDataButton.Name = "clearDataButton";
            this.clearDataButton.Size = new System.Drawing.Size(216, 148);
            this.clearDataButton.TabIndex = 3;
            this.clearDataButton.Text = "Clear All Data";
            this.clearDataButton.UseVisualStyleBackColor = true;
            // 
            // WelcomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.clearDataButton);
            this.Controls.Add(this.BasicFunctionalityButton);
            this.Controls.Add(this.NeuralNetButton);
            this.Controls.Add(this.NeuralTreeButton);
            this.Name = "WelcomeScreen";
            this.Text = "WelcomeScreen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button NeuralTreeButton;
        private System.Windows.Forms.Button NeuralNetButton;
        private System.Windows.Forms.Button BasicFunctionalityButton;
        private System.Windows.Forms.Button clearDataButton;
    }
}