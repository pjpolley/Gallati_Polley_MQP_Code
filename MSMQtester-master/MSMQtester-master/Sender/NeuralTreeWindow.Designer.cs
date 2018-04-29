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
            this.setNumberOfPositionsPerLayerButton = new System.Windows.Forms.Button();
            this.handDelayButton = new System.Windows.Forms.Button();
            this.positionsPerLayerBox = new System.Windows.Forms.TextBox();
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
            this.currentGoalBox = new System.Windows.Forms.TextBox();
            this.currentGoalLabel = new System.Windows.Forms.Label();
            this.currentInputLabel = new System.Windows.Forms.Label();
            this.currentInputBox = new System.Windows.Forms.TextBox();
            this.maxValueLabel = new System.Windows.Forms.Label();
            this.maxValueTextBox = new System.Windows.Forms.TextBox();
            this.minValueLabel = new System.Windows.Forms.Label();
            this.minValueTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // NeuronTreeView
            // 
            this.NeuronTreeView.Location = new System.Drawing.Point(6, 6);
            this.NeuronTreeView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.NeuronTreeView.Name = "NeuronTreeView";
            this.NeuronTreeView.Size = new System.Drawing.Size(384, 633);
            this.NeuronTreeView.TabIndex = 0;
            this.NeuronTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NeuronTreeView_NodeClicked);
            // 
            // setHandPositionButton
            // 
            this.setHandPositionButton.Location = new System.Drawing.Point(631, 354);
            this.setHandPositionButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.setHandPositionButton.Name = "setHandPositionButton";
            this.setHandPositionButton.Size = new System.Drawing.Size(132, 54);
            this.setHandPositionButton.TabIndex = 1;
            this.setHandPositionButton.Text = "Save Hand Position";
            this.setHandPositionButton.UseVisualStyleBackColor = true;
            this.setHandPositionButton.Click += new System.EventHandler(this.setHandPositionButton_Click);
            // 
            // AddAnotherLayerButton
            // 
            this.AddAnotherLayerButton.Location = new System.Drawing.Point(389, 22);
            this.AddAnotherLayerButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AddAnotherLayerButton.Name = "AddAnotherLayerButton";
            this.AddAnotherLayerButton.Size = new System.Drawing.Size(191, 64);
            this.AddAnotherLayerButton.TabIndex = 2;
            this.AddAnotherLayerButton.Text = "Add layer";
            this.AddAnotherLayerButton.UseVisualStyleBackColor = true;
            this.AddAnotherLayerButton.Click += new System.EventHandler(this.AddAnotherLayerButton_Click);
            // 
            // changeNameButton
            // 
            this.changeNameButton.Location = new System.Drawing.Point(389, 178);
            this.changeNameButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.changeNameButton.Name = "changeNameButton";
            this.changeNameButton.Size = new System.Drawing.Size(191, 54);
            this.changeNameButton.TabIndex = 3;
            this.changeNameButton.Text = "Change Name";
            this.changeNameButton.UseVisualStyleBackColor = true;
            this.changeNameButton.Click += new System.EventHandler(this.changeNameButton_Click);
            // 
            // desiredNameBox
            // 
            this.desiredNameBox.Location = new System.Drawing.Point(391, 158);
            this.desiredNameBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.desiredNameBox.Name = "desiredNameBox";
            this.desiredNameBox.Size = new System.Drawing.Size(192, 20);
            this.desiredNameBox.TabIndex = 4;
            this.desiredNameBox.Text = "Name Here";
            this.desiredNameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.desiredNameBox_KeyDown);
            // 
            // setNumberOfPositionsPerLayerButton
            // 
            this.setNumberOfPositionsPerLayerButton.Location = new System.Drawing.Point(388, 257);
            this.setNumberOfPositionsPerLayerButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.setNumberOfPositionsPerLayerButton.Name = "setNumberOfPositionsPerLayerButton";
            this.setNumberOfPositionsPerLayerButton.Size = new System.Drawing.Size(191, 47);
            this.setNumberOfPositionsPerLayerButton.TabIndex = 5;
            this.setNumberOfPositionsPerLayerButton.Text = "Set number of positions per layer";
            this.setNumberOfPositionsPerLayerButton.UseVisualStyleBackColor = true;
            this.setNumberOfPositionsPerLayerButton.Click += new System.EventHandler(this.setNumberOfPositionsPerLayerButton_Click);
            // 
            // handDelayButton
            // 
            this.handDelayButton.Location = new System.Drawing.Point(388, 328);
            this.handDelayButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.handDelayButton.Name = "handDelayButton";
            this.handDelayButton.Size = new System.Drawing.Size(191, 44);
            this.handDelayButton.TabIndex = 6;
            this.handDelayButton.Text = "Set delay for hand movement (milliseconds)";
            this.handDelayButton.UseVisualStyleBackColor = true;
            this.handDelayButton.Click += new System.EventHandler(this.handDelayButton_Click);
            // 
            // positionsPerLayerBox
            // 
            this.positionsPerLayerBox.Location = new System.Drawing.Point(389, 237);
            this.positionsPerLayerBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.positionsPerLayerBox.Name = "positionsPerLayerBox";
            this.positionsPerLayerBox.Size = new System.Drawing.Size(192, 20);
            this.positionsPerLayerBox.TabIndex = 7;
            this.positionsPerLayerBox.Text = "4";
            this.positionsPerLayerBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionsPerLayerBox_KeyDown);
            // 
            // handDelayBox
            // 
            this.handDelayBox.Location = new System.Drawing.Point(388, 308);
            this.handDelayBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.handDelayBox.Name = "handDelayBox";
            this.handDelayBox.Size = new System.Drawing.Size(192, 20);
            this.handDelayBox.TabIndex = 8;
            this.handDelayBox.Text = "200";
            this.handDelayBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handDelayBox_KeyDown);
            // 
            // removeLayerButton
            // 
            this.removeLayerButton.Location = new System.Drawing.Point(389, 90);
            this.removeLayerButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.removeLayerButton.Name = "removeLayerButton";
            this.removeLayerButton.Size = new System.Drawing.Size(190, 64);
            this.removeLayerButton.TabIndex = 9;
            this.removeLayerButton.Text = "Remove layer under selected one";
            this.removeLayerButton.UseVisualStyleBackColor = true;
            this.removeLayerButton.Click += new System.EventHandler(this.removeLayerButton_Click);
            // 
            // hardResetButton
            // 
            this.hardResetButton.Location = new System.Drawing.Point(391, 598);
            this.hardResetButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.hardResetButton.Name = "hardResetButton";
            this.hardResetButton.Size = new System.Drawing.Size(186, 39);
            this.hardResetButton.TabIndex = 10;
            this.hardResetButton.Text = "Factory Reset Command Structure";
            this.hardResetButton.UseVisualStyleBackColor = true;
            this.hardResetButton.Click += new System.EventHandler(this.hardResetButton_Click);
            // 
            // saveCommandStructure
            // 
            this.saveCommandStructure.Location = new System.Drawing.Point(389, 376);
            this.saveCommandStructure.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.saveCommandStructure.Name = "saveCommandStructure";
            this.saveCommandStructure.Size = new System.Drawing.Size(189, 64);
            this.saveCommandStructure.TabIndex = 11;
            this.saveCommandStructure.Text = "Save Command Structure";
            this.saveCommandStructure.UseVisualStyleBackColor = true;
            this.saveCommandStructure.Click += new System.EventHandler(this.saveCommandStructure_Click);
            // 
            // CommandStructureModificationBox
            // 
            this.CommandStructureModificationBox.AutoSize = true;
            this.CommandStructureModificationBox.Location = new System.Drawing.Point(390, 7);
            this.CommandStructureModificationBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CommandStructureModificationBox.Name = "CommandStructureModificationBox";
            this.CommandStructureModificationBox.Size = new System.Drawing.Size(160, 13);
            this.CommandStructureModificationBox.TabIndex = 12;
            this.CommandStructureModificationBox.Text = "Command Structure Modification";
            // 
            // HandPositionModificationBox
            // 
            this.HandPositionModificationBox.AutoSize = true;
            this.HandPositionModificationBox.Location = new System.Drawing.Point(628, 7);
            this.HandPositionModificationBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.HandPositionModificationBox.Name = "HandPositionModificationBox";
            this.HandPositionModificationBox.Size = new System.Drawing.Size(133, 13);
            this.HandPositionModificationBox.TabIndex = 13;
            this.HandPositionModificationBox.Text = "Hand Position Modification";
            // 
            // ThumbSelectButton
            // 
            this.ThumbSelectButton.Location = new System.Drawing.Point(631, 23);
            this.ThumbSelectButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ThumbSelectButton.Name = "ThumbSelectButton";
            this.ThumbSelectButton.Size = new System.Drawing.Size(132, 29);
            this.ThumbSelectButton.TabIndex = 14;
            this.ThumbSelectButton.Text = "Select Thumb";
            this.ThumbSelectButton.UseVisualStyleBackColor = true;
            this.ThumbSelectButton.Click += new System.EventHandler(this.ThumbSelectButton_Click);
            // 
            // IndexSelectButton
            // 
            this.IndexSelectButton.Location = new System.Drawing.Point(631, 55);
            this.IndexSelectButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.IndexSelectButton.Name = "IndexSelectButton";
            this.IndexSelectButton.Size = new System.Drawing.Size(132, 29);
            this.IndexSelectButton.TabIndex = 15;
            this.IndexSelectButton.Text = "Select Index Finger";
            this.IndexSelectButton.UseVisualStyleBackColor = true;
            this.IndexSelectButton.Click += new System.EventHandler(this.IndexSelectButton_Click);
            // 
            // MiddleSelectButton
            // 
            this.MiddleSelectButton.Location = new System.Drawing.Point(631, 86);
            this.MiddleSelectButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MiddleSelectButton.Name = "MiddleSelectButton";
            this.MiddleSelectButton.Size = new System.Drawing.Size(132, 29);
            this.MiddleSelectButton.TabIndex = 16;
            this.MiddleSelectButton.Text = "Select Middle Finger";
            this.MiddleSelectButton.UseVisualStyleBackColor = true;
            this.MiddleSelectButton.Click += new System.EventHandler(this.MiddleSelectButton_Click);
            // 
            // RingSelectButton
            // 
            this.RingSelectButton.Location = new System.Drawing.Point(631, 118);
            this.RingSelectButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RingSelectButton.Name = "RingSelectButton";
            this.RingSelectButton.Size = new System.Drawing.Size(132, 29);
            this.RingSelectButton.TabIndex = 17;
            this.RingSelectButton.Text = "Select Ring Finger";
            this.RingSelectButton.UseVisualStyleBackColor = true;
            this.RingSelectButton.Click += new System.EventHandler(this.RingSelectButton_Click);
            // 
            // PinkySelectButton
            // 
            this.PinkySelectButton.Location = new System.Drawing.Point(631, 150);
            this.PinkySelectButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PinkySelectButton.Name = "PinkySelectButton";
            this.PinkySelectButton.Size = new System.Drawing.Size(132, 29);
            this.PinkySelectButton.TabIndex = 18;
            this.PinkySelectButton.Text = "Select Pinky";
            this.PinkySelectButton.UseVisualStyleBackColor = true;
            this.PinkySelectButton.Click += new System.EventHandler(this.PinkySelectButton_Click);
            // 
            // InnerJointButton
            // 
            this.InnerJointButton.Location = new System.Drawing.Point(630, 264);
            this.InnerJointButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.InnerJointButton.Name = "InnerJointButton";
            this.InnerJointButton.Size = new System.Drawing.Size(132, 29);
            this.InnerJointButton.TabIndex = 21;
            this.InnerJointButton.Text = "Select Inner Joint";
            this.InnerJointButton.UseVisualStyleBackColor = true;
            this.InnerJointButton.Click += new System.EventHandler(this.InnerJointButton_Click);
            // 
            // MiddleJointButton
            // 
            this.MiddleJointButton.Location = new System.Drawing.Point(631, 231);
            this.MiddleJointButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MiddleJointButton.Name = "MiddleJointButton";
            this.MiddleJointButton.Size = new System.Drawing.Size(132, 29);
            this.MiddleJointButton.TabIndex = 20;
            this.MiddleJointButton.Text = "Select Middle Joint";
            this.MiddleJointButton.UseVisualStyleBackColor = true;
            this.MiddleJointButton.Click += new System.EventHandler(this.MiddleJointButton_Click);
            // 
            // OuterJointButton
            // 
            this.OuterJointButton.Location = new System.Drawing.Point(630, 198);
            this.OuterJointButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OuterJointButton.Name = "OuterJointButton";
            this.OuterJointButton.Size = new System.Drawing.Size(132, 29);
            this.OuterJointButton.TabIndex = 19;
            this.OuterJointButton.Text = "Select Outer Joint";
            this.OuterJointButton.UseVisualStyleBackColor = true;
            this.OuterJointButton.Click += new System.EventHandler(this.OuterJointButton_Click);
            // 
            // DesiredAngleInput
            // 
            this.DesiredAngleInput.Location = new System.Drawing.Point(677, 310);
            this.DesiredAngleInput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DesiredAngleInput.Name = "DesiredAngleInput";
            this.DesiredAngleInput.Size = new System.Drawing.Size(43, 20);
            this.DesiredAngleInput.TabIndex = 22;
            this.DesiredAngleInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DesiredAngleInput_KeyDown);
            // 
            // currentlyModifyingBox
            // 
            this.currentlyModifyingBox.AutoSize = true;
            this.currentlyModifyingBox.Location = new System.Drawing.Point(628, 295);
            this.currentlyModifyingBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentlyModifyingBox.Name = "currentlyModifyingBox";
            this.currentlyModifyingBox.Size = new System.Drawing.Size(124, 13);
            this.currentlyModifyingBox.TabIndex = 23;
            this.currentlyModifyingBox.Text = "Thumb Outer Joint Angle";
            // 
            // DecreaseAngleButton
            // 
            this.DecreaseAngleButton.Font = new System.Drawing.Font("Webdings", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.DecreaseAngleButton.Location = new System.Drawing.Point(649, 307);
            this.DecreaseAngleButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DecreaseAngleButton.Name = "DecreaseAngleButton";
            this.DecreaseAngleButton.Size = new System.Drawing.Size(24, 43);
            this.DecreaseAngleButton.TabIndex = 24;
            this.DecreaseAngleButton.Text = "3";
            this.DecreaseAngleButton.UseVisualStyleBackColor = true;
            this.DecreaseAngleButton.Click += new System.EventHandler(this.DecreaseAngleButton_Click);
            // 
            // IncreaseAngleButton
            // 
            this.IncreaseAngleButton.Font = new System.Drawing.Font("Webdings", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.IncreaseAngleButton.Location = new System.Drawing.Point(722, 310);
            this.IncreaseAngleButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.IncreaseAngleButton.Name = "IncreaseAngleButton";
            this.IncreaseAngleButton.Size = new System.Drawing.Size(26, 40);
            this.IncreaseAngleButton.TabIndex = 25;
            this.IncreaseAngleButton.Text = "4";
            this.IncreaseAngleButton.UseVisualStyleBackColor = true;
            this.IncreaseAngleButton.Click += new System.EventHandler(this.IncreaseAngleButton_Click);
            // 
            // beginControllingHandButton
            // 
            this.beginControllingHandButton.Location = new System.Drawing.Point(610, 412);
            this.beginControllingHandButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.beginControllingHandButton.Name = "beginControllingHandButton";
            this.beginControllingHandButton.Size = new System.Drawing.Size(176, 34);
            this.beginControllingHandButton.TabIndex = 26;
            this.beginControllingHandButton.Text = "Configure hand for controlling";
            this.beginControllingHandButton.UseVisualStyleBackColor = true;
            this.beginControllingHandButton.Click += new System.EventHandler(this.beginControllingHandButton_Click);
            // 
            // iterateButton
            // 
            this.iterateButton.Location = new System.Drawing.Point(610, 450);
            this.iterateButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.iterateButton.Name = "iterateButton";
            this.iterateButton.Size = new System.Drawing.Size(176, 55);
            this.iterateButton.TabIndex = 27;
            this.iterateButton.Text = "Move hand";
            this.iterateButton.UseVisualStyleBackColor = true;
            this.iterateButton.Click += new System.EventHandler(this.iterateButton_Click);
            // 
            // setCurrentPosition
            // 
            this.setCurrentPosition.Location = new System.Drawing.Point(677, 331);
            this.setCurrentPosition.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.setCurrentPosition.Name = "setCurrentPosition";
            this.setCurrentPosition.Size = new System.Drawing.Size(41, 19);
            this.setCurrentPosition.TabIndex = 28;
            this.setCurrentPosition.Text = "Set";
            this.setCurrentPosition.UseVisualStyleBackColor = true;
            // 
            // currentGoalBox
            // 
            this.currentGoalBox.Location = new System.Drawing.Point(609, 522);
            this.currentGoalBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.currentGoalBox.Name = "currentGoalBox";
            this.currentGoalBox.ReadOnly = true;
            this.currentGoalBox.Size = new System.Drawing.Size(65, 20);
            this.currentGoalBox.TabIndex = 29;
            // 
            // currentGoalLabel
            // 
            this.currentGoalLabel.AutoSize = true;
            this.currentGoalLabel.Location = new System.Drawing.Point(608, 506);
            this.currentGoalLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentGoalLabel.Name = "currentGoalLabel";
            this.currentGoalLabel.Size = new System.Drawing.Size(66, 13);
            this.currentGoalLabel.TabIndex = 30;
            this.currentGoalLabel.Text = "Current Goal";
            // 
            // currentInputLabel
            // 
            this.currentInputLabel.AutoSize = true;
            this.currentInputLabel.Location = new System.Drawing.Point(720, 506);
            this.currentInputLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentInputLabel.Name = "currentInputLabel";
            this.currentInputLabel.Size = new System.Drawing.Size(68, 13);
            this.currentInputLabel.TabIndex = 32;
            this.currentInputLabel.Text = "Current Input";
            // 
            // currentInputBox
            // 
            this.currentInputBox.Location = new System.Drawing.Point(722, 522);
            this.currentInputBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.currentInputBox.Name = "currentInputBox";
            this.currentInputBox.ReadOnly = true;
            this.currentInputBox.Size = new System.Drawing.Size(65, 20);
            this.currentInputBox.TabIndex = 31;
            // 
            // maxValueLabel
            // 
            this.maxValueLabel.AutoSize = true;
            this.maxValueLabel.Location = new System.Drawing.Point(504, 506);
            this.maxValueLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.maxValueLabel.Name = "maxValueLabel";
            this.maxValueLabel.Size = new System.Drawing.Size(64, 13);
            this.maxValueLabel.TabIndex = 36;
            this.maxValueLabel.Text = "Current Max";
            // 
            // maxValueTextBox
            // 
            this.maxValueTextBox.Location = new System.Drawing.Point(506, 522);
            this.maxValueTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.maxValueTextBox.Name = "maxValueTextBox";
            this.maxValueTextBox.ReadOnly = true;
            this.maxValueTextBox.Size = new System.Drawing.Size(65, 20);
            this.maxValueTextBox.TabIndex = 35;
            // 
            // minValueLabel
            // 
            this.minValueLabel.AutoSize = true;
            this.minValueLabel.Location = new System.Drawing.Point(391, 506);
            this.minValueLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.minValueLabel.Name = "minValueLabel";
            this.minValueLabel.Size = new System.Drawing.Size(61, 13);
            this.minValueLabel.TabIndex = 34;
            this.minValueLabel.Text = "Current Min";
            // 
            // minValueTextBox
            // 
            this.minValueTextBox.Location = new System.Drawing.Point(394, 522);
            this.minValueTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.minValueTextBox.Name = "minValueTextBox";
            this.minValueTextBox.ReadOnly = true;
            this.minValueTextBox.Size = new System.Drawing.Size(65, 20);
            this.minValueTextBox.TabIndex = 33;
            // 
            // NeuralTreeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 552);
            this.Controls.Add(this.maxValueLabel);
            this.Controls.Add(this.maxValueTextBox);
            this.Controls.Add(this.minValueLabel);
            this.Controls.Add(this.minValueTextBox);
            this.Controls.Add(this.currentInputLabel);
            this.Controls.Add(this.currentInputBox);
            this.Controls.Add(this.currentGoalLabel);
            this.Controls.Add(this.currentGoalBox);
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
            this.Controls.Add(this.positionsPerLayerBox);
            this.Controls.Add(this.handDelayButton);
            this.Controls.Add(this.setNumberOfPositionsPerLayerButton);
            this.Controls.Add(this.desiredNameBox);
            this.Controls.Add(this.changeNameButton);
            this.Controls.Add(this.AddAnotherLayerButton);
            this.Controls.Add(this.setHandPositionButton);
            this.Controls.Add(this.NeuronTreeView);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.Button setNumberOfPositionsPerLayerButton;
        private System.Windows.Forms.Button handDelayButton;
        private System.Windows.Forms.TextBox positionsPerLayerBox;
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
        private System.Windows.Forms.TextBox currentGoalBox;
        private System.Windows.Forms.Label currentGoalLabel;
        private System.Windows.Forms.Label currentInputLabel;
        private System.Windows.Forms.TextBox currentInputBox;
        private System.Windows.Forms.Label maxValueLabel;
        private System.Windows.Forms.TextBox maxValueTextBox;
        private System.Windows.Forms.Label minValueLabel;
        private System.Windows.Forms.TextBox minValueTextBox;
    }
}