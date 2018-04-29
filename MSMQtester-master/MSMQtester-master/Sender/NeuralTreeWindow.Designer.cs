namespace Sender
{
    partial class NeuralTreeWindow
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
            this.NeuronTreeView = new System.Windows.Forms.TreeView();
            this.setHandPositionButton = new System.Windows.Forms.Button();
            this.AddAnotherLayerButton = new System.Windows.Forms.Button();
            this.changeNameButton = new System.Windows.Forms.Button();
            this.desiredNameBox = new System.Windows.Forms.TextBox();
            this.handDelayButton = new System.Windows.Forms.Button();
            this.handDelayBox = new System.Windows.Forms.TextBox();
            this.removeLayerButton = new System.Windows.Forms.Button();
            this.hardResetButton = new System.Windows.Forms.Button();
            this.saveCommandStructure = new System.Windows.Forms.Button();
            this.CommandStructureModificationBox = new System.Windows.Forms.Label();
            this.HandPositionModificationBox = new System.Windows.Forms.Label();
            this.ThumbSelectButton = new System.Windows.Forms.Button();
            this.IndexSelectButton = new System.Windows.Forms.Button();
            this.MiddleSelectButton = new System.Windows.Forms.Button();
            this.RingSelectButton = new System.Windows.Forms.Button();
            this.PinkySelectButton = new System.Windows.Forms.Button();
            this.InnerJointButton = new System.Windows.Forms.Button();
            this.MiddleJointButton = new System.Windows.Forms.Button();
            this.OuterJointButton = new System.Windows.Forms.Button();
            this.DesiredAngleInput = new System.Windows.Forms.TextBox();
            this.currentlyModifyingBox = new System.Windows.Forms.Label();
            this.DecreaseAngleButton = new System.Windows.Forms.Button();
            this.IncreaseAngleButton = new System.Windows.Forms.Button();
            this.beginControllingHandButton = new System.Windows.Forms.Button();
            this.iterateButton = new System.Windows.Forms.Button();
            this.setCurrentPosition = new System.Windows.Forms.Button();
            this.currentAverageBox = new System.Windows.Forms.TextBox();
            this.currentAverageLabel = new System.Windows.Forms.Label();
            this.currentInputLabel = new System.Windows.Forms.Label();
            this.currentInputBox = new System.Windows.Forms.TextBox();
            this.midpointValueLabel = new System.Windows.Forms.Label();
            this.midpointTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // NeuronTreeView
            // 
            this.NeuronTreeView.Location = new System.Drawing.Point(12, 12);
            this.NeuronTreeView.Margin = new System.Windows.Forms.Padding(4);
            this.NeuronTreeView.Name = "NeuronTreeView";
            this.NeuronTreeView.Size = new System.Drawing.Size(764, 1213);
            this.NeuronTreeView.TabIndex = 0;
            this.NeuronTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NeuronTreeView_NodeClicked);
            // 
            // setHandPositionButton
            // 
            this.setHandPositionButton.Location = new System.Drawing.Point(1262, 681);
            this.setHandPositionButton.Margin = new System.Windows.Forms.Padding(4);
            this.setHandPositionButton.Name = "setHandPositionButton";
            this.setHandPositionButton.Size = new System.Drawing.Size(264, 103);
            this.setHandPositionButton.TabIndex = 1;
            this.setHandPositionButton.Text = "Save Hand Position";
            this.setHandPositionButton.UseVisualStyleBackColor = true;
            this.setHandPositionButton.Click += new System.EventHandler(this.setHandPositionButton_Click);
            // 
            // AddAnotherLayerButton
            // 
            this.AddAnotherLayerButton.Location = new System.Drawing.Point(778, 42);
            this.AddAnotherLayerButton.Margin = new System.Windows.Forms.Padding(4);
            this.AddAnotherLayerButton.Name = "AddAnotherLayerButton";
            this.AddAnotherLayerButton.Size = new System.Drawing.Size(382, 123);
            this.AddAnotherLayerButton.TabIndex = 2;
            this.AddAnotherLayerButton.Text = "Add layer";
            this.AddAnotherLayerButton.UseVisualStyleBackColor = true;
            this.AddAnotherLayerButton.Click += new System.EventHandler(this.AddAnotherLayerButton_Click);
            // 
            // changeNameButton
            // 
            this.changeNameButton.Location = new System.Drawing.Point(778, 343);
            this.changeNameButton.Margin = new System.Windows.Forms.Padding(4);
            this.changeNameButton.Name = "changeNameButton";
            this.changeNameButton.Size = new System.Drawing.Size(382, 104);
            this.changeNameButton.TabIndex = 3;
            this.changeNameButton.Text = "Change Name";
            this.changeNameButton.UseVisualStyleBackColor = true;
            this.changeNameButton.Click += new System.EventHandler(this.changeNameButton_Click);
            // 
            // desiredNameBox
            // 
            this.desiredNameBox.Location = new System.Drawing.Point(782, 304);
            this.desiredNameBox.Margin = new System.Windows.Forms.Padding(4);
            this.desiredNameBox.Name = "desiredNameBox";
            this.desiredNameBox.Size = new System.Drawing.Size(380, 31);
            this.desiredNameBox.TabIndex = 4;
            this.desiredNameBox.Text = "Name Here";
            this.desiredNameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.desiredNameBox_KeyDown);
            // 
            // handDelayButton
            // 
            this.handDelayButton.Location = new System.Drawing.Point(776, 496);
            this.handDelayButton.Margin = new System.Windows.Forms.Padding(4);
            this.handDelayButton.Name = "handDelayButton";
            this.handDelayButton.Size = new System.Drawing.Size(382, 85);
            this.handDelayButton.TabIndex = 6;
            this.handDelayButton.Text = "Set delay for hand movement (milliseconds)";
            this.handDelayButton.UseVisualStyleBackColor = true;
            this.handDelayButton.Click += new System.EventHandler(this.handDelayButton_Click);
            // 
            // handDelayBox
            // 
            this.handDelayBox.Location = new System.Drawing.Point(778, 457);
            this.handDelayBox.Margin = new System.Windows.Forms.Padding(4);
            this.handDelayBox.Name = "handDelayBox";
            this.handDelayBox.Size = new System.Drawing.Size(380, 31);
            this.handDelayBox.TabIndex = 8;
            this.handDelayBox.Text = "10000";
            this.handDelayBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handDelayBox_KeyDown);
            // 
            // removeLayerButton
            // 
            this.removeLayerButton.Location = new System.Drawing.Point(778, 173);
            this.removeLayerButton.Margin = new System.Windows.Forms.Padding(4);
            this.removeLayerButton.Name = "removeLayerButton";
            this.removeLayerButton.Size = new System.Drawing.Size(380, 123);
            this.removeLayerButton.TabIndex = 9;
            this.removeLayerButton.Text = "Remove layer under selected one";
            this.removeLayerButton.UseVisualStyleBackColor = true;
            this.removeLayerButton.Click += new System.EventHandler(this.removeLayerButton_Click);
            // 
            // hardResetButton
            // 
            this.hardResetButton.Location = new System.Drawing.Point(1219, 991);
            this.hardResetButton.Margin = new System.Windows.Forms.Padding(4);
            this.hardResetButton.Name = "hardResetButton";
            this.hardResetButton.Size = new System.Drawing.Size(350, 133);
            this.hardResetButton.TabIndex = 10;
            this.hardResetButton.Text = "Factory Reset Command Structure";
            this.hardResetButton.UseVisualStyleBackColor = true;
            this.hardResetButton.Click += new System.EventHandler(this.hardResetButton_Click);
            // 
            // saveCommandStructure
            // 
            this.saveCommandStructure.Location = new System.Drawing.Point(1209, 803);
            this.saveCommandStructure.Margin = new System.Windows.Forms.Padding(4);
            this.saveCommandStructure.Name = "saveCommandStructure";
            this.saveCommandStructure.Size = new System.Drawing.Size(373, 137);
            this.saveCommandStructure.TabIndex = 11;
            this.saveCommandStructure.Text = "Save Command Structure";
            this.saveCommandStructure.UseVisualStyleBackColor = true;
            this.saveCommandStructure.Click += new System.EventHandler(this.saveCommandStructure_Click);
            // 
            // CommandStructureModificationBox
            // 
            this.CommandStructureModificationBox.AutoSize = true;
            this.CommandStructureModificationBox.Location = new System.Drawing.Point(780, 13);
            this.CommandStructureModificationBox.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CommandStructureModificationBox.Name = "CommandStructureModificationBox";
            this.CommandStructureModificationBox.Size = new System.Drawing.Size(324, 25);
            this.CommandStructureModificationBox.TabIndex = 12;
            this.CommandStructureModificationBox.Text = "Command Structure Modification";
            // 
            // HandPositionModificationBox
            // 
            this.HandPositionModificationBox.AutoSize = true;
            this.HandPositionModificationBox.Location = new System.Drawing.Point(1256, 13);
            this.HandPositionModificationBox.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.HandPositionModificationBox.Name = "HandPositionModificationBox";
            this.HandPositionModificationBox.Size = new System.Drawing.Size(268, 25);
            this.HandPositionModificationBox.TabIndex = 13;
            this.HandPositionModificationBox.Text = "Hand Position Modification";
            // 
            // ThumbSelectButton
            // 
            this.ThumbSelectButton.Location = new System.Drawing.Point(1262, 44);
            this.ThumbSelectButton.Margin = new System.Windows.Forms.Padding(4);
            this.ThumbSelectButton.Name = "ThumbSelectButton";
            this.ThumbSelectButton.Size = new System.Drawing.Size(264, 56);
            this.ThumbSelectButton.TabIndex = 14;
            this.ThumbSelectButton.Text = "Select Thumb";
            this.ThumbSelectButton.UseVisualStyleBackColor = true;
            this.ThumbSelectButton.Click += new System.EventHandler(this.ThumbSelectButton_Click);
            // 
            // IndexSelectButton
            // 
            this.IndexSelectButton.Location = new System.Drawing.Point(1262, 106);
            this.IndexSelectButton.Margin = new System.Windows.Forms.Padding(4);
            this.IndexSelectButton.Name = "IndexSelectButton";
            this.IndexSelectButton.Size = new System.Drawing.Size(264, 56);
            this.IndexSelectButton.TabIndex = 15;
            this.IndexSelectButton.Text = "Select Index Finger";
            this.IndexSelectButton.UseVisualStyleBackColor = true;
            this.IndexSelectButton.Click += new System.EventHandler(this.IndexSelectButton_Click);
            // 
            // MiddleSelectButton
            // 
            this.MiddleSelectButton.Location = new System.Drawing.Point(1262, 165);
            this.MiddleSelectButton.Margin = new System.Windows.Forms.Padding(4);
            this.MiddleSelectButton.Name = "MiddleSelectButton";
            this.MiddleSelectButton.Size = new System.Drawing.Size(264, 56);
            this.MiddleSelectButton.TabIndex = 16;
            this.MiddleSelectButton.Text = "Select Middle Finger";
            this.MiddleSelectButton.UseVisualStyleBackColor = true;
            this.MiddleSelectButton.Click += new System.EventHandler(this.MiddleSelectButton_Click);
            // 
            // RingSelectButton
            // 
            this.RingSelectButton.Location = new System.Drawing.Point(1262, 227);
            this.RingSelectButton.Margin = new System.Windows.Forms.Padding(4);
            this.RingSelectButton.Name = "RingSelectButton";
            this.RingSelectButton.Size = new System.Drawing.Size(264, 56);
            this.RingSelectButton.TabIndex = 17;
            this.RingSelectButton.Text = "Select Ring Finger";
            this.RingSelectButton.UseVisualStyleBackColor = true;
            this.RingSelectButton.Click += new System.EventHandler(this.RingSelectButton_Click);
            // 
            // PinkySelectButton
            // 
            this.PinkySelectButton.Location = new System.Drawing.Point(1262, 288);
            this.PinkySelectButton.Margin = new System.Windows.Forms.Padding(4);
            this.PinkySelectButton.Name = "PinkySelectButton";
            this.PinkySelectButton.Size = new System.Drawing.Size(264, 56);
            this.PinkySelectButton.TabIndex = 18;
            this.PinkySelectButton.Text = "Select Pinky";
            this.PinkySelectButton.UseVisualStyleBackColor = true;
            this.PinkySelectButton.Click += new System.EventHandler(this.PinkySelectButton_Click);
            // 
            // InnerJointButton
            // 
            this.InnerJointButton.Location = new System.Drawing.Point(1261, 508);
            this.InnerJointButton.Margin = new System.Windows.Forms.Padding(4);
            this.InnerJointButton.Name = "InnerJointButton";
            this.InnerJointButton.Size = new System.Drawing.Size(264, 56);
            this.InnerJointButton.TabIndex = 21;
            this.InnerJointButton.Text = "Select Inner Joint";
            this.InnerJointButton.UseVisualStyleBackColor = true;
            this.InnerJointButton.Click += new System.EventHandler(this.InnerJointButton_Click);
            // 
            // MiddleJointButton
            // 
            this.MiddleJointButton.Location = new System.Drawing.Point(1262, 444);
            this.MiddleJointButton.Margin = new System.Windows.Forms.Padding(4);
            this.MiddleJointButton.Name = "MiddleJointButton";
            this.MiddleJointButton.Size = new System.Drawing.Size(264, 56);
            this.MiddleJointButton.TabIndex = 20;
            this.MiddleJointButton.Text = "Select Middle Joint";
            this.MiddleJointButton.UseVisualStyleBackColor = true;
            this.MiddleJointButton.Click += new System.EventHandler(this.MiddleJointButton_Click);
            // 
            // OuterJointButton
            // 
            this.OuterJointButton.Location = new System.Drawing.Point(1261, 380);
            this.OuterJointButton.Margin = new System.Windows.Forms.Padding(4);
            this.OuterJointButton.Name = "OuterJointButton";
            this.OuterJointButton.Size = new System.Drawing.Size(264, 56);
            this.OuterJointButton.TabIndex = 19;
            this.OuterJointButton.Text = "Select Outer Joint";
            this.OuterJointButton.UseVisualStyleBackColor = true;
            this.OuterJointButton.Click += new System.EventHandler(this.OuterJointButton_Click);
            // 
            // DesiredAngleInput
            // 
            this.DesiredAngleInput.Location = new System.Drawing.Point(1354, 597);
            this.DesiredAngleInput.Margin = new System.Windows.Forms.Padding(4);
            this.DesiredAngleInput.Name = "DesiredAngleInput";
            this.DesiredAngleInput.Size = new System.Drawing.Size(82, 31);
            this.DesiredAngleInput.TabIndex = 22;
            this.DesiredAngleInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DesiredAngleInput_KeyDown);
            // 
            // currentlyModifyingBox
            // 
            this.currentlyModifyingBox.AutoSize = true;
            this.currentlyModifyingBox.Location = new System.Drawing.Point(1257, 568);
            this.currentlyModifyingBox.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.currentlyModifyingBox.Name = "currentlyModifyingBox";
            this.currentlyModifyingBox.Size = new System.Drawing.Size(250, 25);
            this.currentlyModifyingBox.TabIndex = 23;
            this.currentlyModifyingBox.Text = "Thumb Outer Joint Angle";
            // 
            // DecreaseAngleButton
            // 
            this.DecreaseAngleButton.Font = new System.Drawing.Font("Webdings", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.DecreaseAngleButton.Location = new System.Drawing.Point(1298, 590);
            this.DecreaseAngleButton.Margin = new System.Windows.Forms.Padding(4);
            this.DecreaseAngleButton.Name = "DecreaseAngleButton";
            this.DecreaseAngleButton.Size = new System.Drawing.Size(48, 83);
            this.DecreaseAngleButton.TabIndex = 24;
            this.DecreaseAngleButton.Text = "3";
            this.DecreaseAngleButton.UseVisualStyleBackColor = true;
            this.DecreaseAngleButton.Click += new System.EventHandler(this.DecreaseAngleButton_Click);
            // 
            // IncreaseAngleButton
            // 
            this.IncreaseAngleButton.Font = new System.Drawing.Font("Webdings", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.IncreaseAngleButton.Location = new System.Drawing.Point(1444, 597);
            this.IncreaseAngleButton.Margin = new System.Windows.Forms.Padding(4);
            this.IncreaseAngleButton.Name = "IncreaseAngleButton";
            this.IncreaseAngleButton.Size = new System.Drawing.Size(52, 76);
            this.IncreaseAngleButton.TabIndex = 25;
            this.IncreaseAngleButton.Text = "4";
            this.IncreaseAngleButton.UseVisualStyleBackColor = true;
            this.IncreaseAngleButton.Click += new System.EventHandler(this.IncreaseAngleButton_Click);
            // 
            // beginControllingHandButton
            // 
            this.beginControllingHandButton.Location = new System.Drawing.Point(778, 636);
            this.beginControllingHandButton.Margin = new System.Windows.Forms.Padding(4);
            this.beginControllingHandButton.Name = "beginControllingHandButton";
            this.beginControllingHandButton.Size = new System.Drawing.Size(384, 98);
            this.beginControllingHandButton.TabIndex = 26;
            this.beginControllingHandButton.Text = "Configure hand for controlling";
            this.beginControllingHandButton.UseVisualStyleBackColor = true;
            this.beginControllingHandButton.Click += new System.EventHandler(this.beginControllingHandButton_Click);
            // 
            // iterateButton
            // 
            this.iterateButton.Location = new System.Drawing.Point(778, 742);
            this.iterateButton.Margin = new System.Windows.Forms.Padding(4);
            this.iterateButton.Name = "iterateButton";
            this.iterateButton.Size = new System.Drawing.Size(384, 105);
            this.iterateButton.TabIndex = 27;
            this.iterateButton.Text = "Move hand";
            this.iterateButton.UseVisualStyleBackColor = true;
            this.iterateButton.Click += new System.EventHandler(this.iterateButton_Click);
            // 
            // setCurrentPosition
            // 
            this.setCurrentPosition.Location = new System.Drawing.Point(1354, 636);
            this.setCurrentPosition.Margin = new System.Windows.Forms.Padding(4);
            this.setCurrentPosition.Name = "setCurrentPosition";
            this.setCurrentPosition.Size = new System.Drawing.Size(82, 37);
            this.setCurrentPosition.TabIndex = 28;
            this.setCurrentPosition.Text = "Set";
            this.setCurrentPosition.UseVisualStyleBackColor = true;
            // 
            // currentAverageBox
            // 
            this.currentAverageBox.Location = new System.Drawing.Point(895, 956);
            this.currentAverageBox.Margin = new System.Windows.Forms.Padding(4);
            this.currentAverageBox.Name = "currentAverageBox";
            this.currentAverageBox.ReadOnly = true;
            this.currentAverageBox.Size = new System.Drawing.Size(126, 31);
            this.currentAverageBox.TabIndex = 29;
            // 
            // currentAverageLabel
            // 
            this.currentAverageLabel.AutoSize = true;
            this.currentAverageLabel.Location = new System.Drawing.Point(877, 927);
            this.currentAverageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.currentAverageLabel.Name = "currentAverageLabel";
            this.currentAverageLabel.Size = new System.Drawing.Size(169, 25);
            this.currentAverageLabel.TabIndex = 30;
            this.currentAverageLabel.Text = "Current Average";
            // 
            // currentInputLabel
            // 
            this.currentInputLabel.AutoSize = true;
            this.currentInputLabel.Location = new System.Drawing.Point(890, 991);
            this.currentInputLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.currentInputLabel.Name = "currentInputLabel";
            this.currentInputLabel.Size = new System.Drawing.Size(136, 25);
            this.currentInputLabel.TabIndex = 32;
            this.currentInputLabel.Text = "Current Input";
            // 
            // currentInputBox
            // 
            this.currentInputBox.Location = new System.Drawing.Point(895, 1028);
            this.currentInputBox.Margin = new System.Windows.Forms.Padding(4);
            this.currentInputBox.Name = "currentInputBox";
            this.currentInputBox.ReadOnly = true;
            this.currentInputBox.Size = new System.Drawing.Size(126, 31);
            this.currentInputBox.TabIndex = 31;
            // 
            // midpointValueLabel
            // 
            this.midpointValueLabel.AutoSize = true;
            this.midpointValueLabel.Location = new System.Drawing.Point(854, 863);
            this.midpointValueLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.midpointValueLabel.Name = "midpointValueLabel";
            this.midpointValueLabel.Size = new System.Drawing.Size(205, 25);
            this.midpointValueLabel.TabIndex = 34;
            this.midpointValueLabel.Text = "Configured Midpoint";
            // 
            // midpointTextBox
            // 
            this.midpointTextBox.Location = new System.Drawing.Point(895, 892);
            this.midpointTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.midpointTextBox.Name = "midpointTextBox";
            this.midpointTextBox.ReadOnly = true;
            this.midpointTextBox.Size = new System.Drawing.Size(126, 31);
            this.midpointTextBox.TabIndex = 33;
            // 
            // NeuralTreeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1684, 1230);
            this.Controls.Add(this.midpointValueLabel);
            this.Controls.Add(this.midpointTextBox);
            this.Controls.Add(this.currentInputLabel);
            this.Controls.Add(this.currentInputBox);
            this.Controls.Add(this.currentAverageLabel);
            this.Controls.Add(this.currentAverageBox);
            this.Controls.Add(this.setCurrentPosition);
            this.Controls.Add(this.iterateButton);
            this.Controls.Add(this.beginControllingHandButton);
            this.Controls.Add(this.IncreaseAngleButton);
            this.Controls.Add(this.DecreaseAngleButton);
            this.Controls.Add(this.currentlyModifyingBox);
            this.Controls.Add(this.DesiredAngleInput);
            this.Controls.Add(this.InnerJointButton);
            this.Controls.Add(this.MiddleJointButton);
            this.Controls.Add(this.OuterJointButton);
            this.Controls.Add(this.PinkySelectButton);
            this.Controls.Add(this.RingSelectButton);
            this.Controls.Add(this.MiddleSelectButton);
            this.Controls.Add(this.IndexSelectButton);
            this.Controls.Add(this.ThumbSelectButton);
            this.Controls.Add(this.HandPositionModificationBox);
            this.Controls.Add(this.CommandStructureModificationBox);
            this.Controls.Add(this.saveCommandStructure);
            this.Controls.Add(this.hardResetButton);
            this.Controls.Add(this.removeLayerButton);
            this.Controls.Add(this.handDelayBox);
            this.Controls.Add(this.handDelayButton);
            this.Controls.Add(this.desiredNameBox);
            this.Controls.Add(this.changeNameButton);
            this.Controls.Add(this.AddAnotherLayerButton);
            this.Controls.Add(this.setHandPositionButton);
            this.Controls.Add(this.NeuronTreeView);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NeuralTreeWindow";
            this.Text = "NeuralTreeWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView NeuronTreeView;
        private System.Windows.Forms.Button setHandPositionButton;
        private System.Windows.Forms.Button AddAnotherLayerButton;
        private System.Windows.Forms.Button changeNameButton;
        private System.Windows.Forms.TextBox desiredNameBox;
        private System.Windows.Forms.Button handDelayButton;
        private System.Windows.Forms.TextBox handDelayBox;
        private System.Windows.Forms.Button removeLayerButton;
        private System.Windows.Forms.Button hardResetButton;
        private System.Windows.Forms.Button saveCommandStructure;
        private System.Windows.Forms.Label CommandStructureModificationBox;
        private System.Windows.Forms.Label HandPositionModificationBox;
        private System.Windows.Forms.Button ThumbSelectButton;
        private System.Windows.Forms.Button IndexSelectButton;
        private System.Windows.Forms.Button MiddleSelectButton;
        private System.Windows.Forms.Button RingSelectButton;
        private System.Windows.Forms.Button PinkySelectButton;
        private System.Windows.Forms.Button InnerJointButton;
        private System.Windows.Forms.Button MiddleJointButton;
        private System.Windows.Forms.Button OuterJointButton;
        private System.Windows.Forms.TextBox DesiredAngleInput;
        private System.Windows.Forms.Label currentlyModifyingBox;
        private System.Windows.Forms.Button DecreaseAngleButton;
        private System.Windows.Forms.Button IncreaseAngleButton;
        private System.Windows.Forms.Button beginControllingHandButton;
        private System.Windows.Forms.Button iterateButton;
        private System.Windows.Forms.Button setCurrentPosition;
        private System.Windows.Forms.TextBox currentAverageBox;
        private System.Windows.Forms.Label currentAverageLabel;
        private System.Windows.Forms.Label currentInputLabel;
        private System.Windows.Forms.TextBox currentInputBox;
        private System.Windows.Forms.Label midpointValueLabel;
        private System.Windows.Forms.TextBox midpointTextBox;
    }
}