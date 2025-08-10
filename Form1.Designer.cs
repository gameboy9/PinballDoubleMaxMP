namespace PinballDoubleMaxMP
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			playerCount = new NumericUpDown();
			roundCount = new NumericUpDown();
			minBreak = new NumericUpDown();
			flexBreak = new RadioButton();
			massBreak = new RadioButton();
			longBreak1 = new CheckBox();
			longBreak2 = new CheckBox();
			break1Rounds = new NumericUpDown();
			breakLabel = new Label();
			break1Minutes = new NumericUpDown();
			label5 = new Label();
			break2Minutes = new NumericUpDown();
			break2Rounds = new NumericUpDown();
			runSimulation = new Button();
			machineCount = new NumericUpDown();
			label6 = new Label();
			((System.ComponentModel.ISupportInitialize)playerCount).BeginInit();
			((System.ComponentModel.ISupportInitialize)roundCount).BeginInit();
			((System.ComponentModel.ISupportInitialize)minBreak).BeginInit();
			((System.ComponentModel.ISupportInitialize)break1Rounds).BeginInit();
			((System.ComponentModel.ISupportInitialize)break1Minutes).BeginInit();
			((System.ComponentModel.ISupportInitialize)break2Minutes).BeginInit();
			((System.ComponentModel.ISupportInitialize)break2Rounds).BeginInit();
			((System.ComponentModel.ISupportInitialize)machineCount).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(36, 22);
			label1.Name = "label1";
			label1.Size = new Size(133, 20);
			label1.TabIndex = 1;
			label1.Text = "Number Of Players";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(36, 67);
			label2.Name = "label2";
			label2.Size = new Size(136, 20);
			label2.TabIndex = 2;
			label2.Text = "Number Of Rounds";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(36, 114);
			label3.Name = "label3";
			label3.Size = new Size(262, 20);
			label3.TabIndex = 4;
			label3.Text = "Minimum break between rounds (min)";
			// 
			// playerCount
			// 
			playerCount.Location = new Point(397, 20);
			playerCount.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
			playerCount.Name = "playerCount";
			playerCount.Size = new Size(79, 27);
			playerCount.TabIndex = 6;
			playerCount.Value = new decimal(new int[] { 150, 0, 0, 0 });
			// 
			// roundCount
			// 
			roundCount.Location = new Point(397, 65);
			roundCount.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
			roundCount.Name = "roundCount";
			roundCount.Size = new Size(79, 27);
			roundCount.TabIndex = 7;
			roundCount.Value = new decimal(new int[] { 20, 0, 0, 0 });
			// 
			// minBreak
			// 
			minBreak.Location = new Point(397, 112);
			minBreak.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
			minBreak.Name = "minBreak";
			minBreak.Size = new Size(79, 27);
			minBreak.TabIndex = 8;
			minBreak.Value = new decimal(new int[] { 5, 0, 0, 0 });
			// 
			// flexBreak
			// 
			flexBreak.AutoSize = true;
			flexBreak.Checked = true;
			flexBreak.Location = new Point(36, 200);
			flexBreak.Name = "flexBreak";
			flexBreak.Size = new Size(165, 24);
			flexBreak.TabIndex = 9;
			flexBreak.TabStop = true;
			flexBreak.Text = "Flexible Long Breaks";
			flexBreak.UseVisualStyleBackColor = true;
			// 
			// massBreak
			// 
			massBreak.AutoSize = true;
			massBreak.Location = new Point(227, 200);
			massBreak.Name = "massBreak";
			massBreak.Size = new Size(147, 24);
			massBreak.TabIndex = 10;
			massBreak.Text = "Mass Long Breaks";
			massBreak.UseVisualStyleBackColor = true;
			massBreak.CheckedChanged += massBreak_CheckedChanged;
			// 
			// longBreak1
			// 
			longBreak1.AutoSize = true;
			longBreak1.Checked = true;
			longBreak1.CheckState = CheckState.Checked;
			longBreak1.Location = new Point(38, 289);
			longBreak1.Name = "longBreak1";
			longBreak1.Size = new Size(117, 24);
			longBreak1.TabIndex = 11;
			longBreak1.Text = "Long Break 1";
			longBreak1.UseVisualStyleBackColor = true;
			// 
			// longBreak2
			// 
			longBreak2.AutoSize = true;
			longBreak2.Checked = true;
			longBreak2.CheckState = CheckState.Checked;
			longBreak2.Location = new Point(38, 332);
			longBreak2.Name = "longBreak2";
			longBreak2.Size = new Size(117, 24);
			longBreak2.TabIndex = 12;
			longBreak2.Text = "Long Break 2";
			longBreak2.UseVisualStyleBackColor = true;
			// 
			// break1Rounds
			// 
			break1Rounds.Location = new Point(191, 284);
			break1Rounds.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
			break1Rounds.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			break1Rounds.Name = "break1Rounds";
			break1Rounds.Size = new Size(80, 27);
			break1Rounds.TabIndex = 13;
			break1Rounds.Value = new decimal(new int[] { 6, 0, 0, 0 });
			// 
			// breakLabel
			// 
			breakLabel.AutoSize = true;
			breakLabel.Location = new Point(191, 251);
			breakLabel.Name = "breakLabel";
			breakLabel.Size = new Size(106, 20);
			breakLabel.TabIndex = 14;
			breakLabel.Text = "Rounds Played";
			// 
			// break1Minutes
			// 
			break1Minutes.Location = new Point(332, 284);
			break1Minutes.Maximum = new decimal(new int[] { 199, 0, 0, 0 });
			break1Minutes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			break1Minutes.Name = "break1Minutes";
			break1Minutes.Size = new Size(80, 27);
			break1Minutes.TabIndex = 16;
			break1Minutes.Value = new decimal(new int[] { 35, 0, 0, 0 });
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(332, 251);
			label5.Name = "label5";
			label5.Size = new Size(106, 20);
			label5.TabIndex = 17;
			label5.Text = "Duration (min)";
			// 
			// break2Minutes
			// 
			break2Minutes.Location = new Point(332, 329);
			break2Minutes.Maximum = new decimal(new int[] { 199, 0, 0, 0 });
			break2Minutes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			break2Minutes.Name = "break2Minutes";
			break2Minutes.Size = new Size(80, 27);
			break2Minutes.TabIndex = 19;
			break2Minutes.Value = new decimal(new int[] { 70, 0, 0, 0 });
			// 
			// break2Rounds
			// 
			break2Rounds.Location = new Point(191, 329);
			break2Rounds.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
			break2Rounds.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
			break2Rounds.Name = "break2Rounds";
			break2Rounds.Size = new Size(80, 27);
			break2Rounds.TabIndex = 18;
			break2Rounds.Value = new decimal(new int[] { 13, 0, 0, 0 });
			// 
			// runSimulation
			// 
			runSimulation.Location = new Point(181, 385);
			runSimulation.Name = "runSimulation";
			runSimulation.Size = new Size(148, 29);
			runSimulation.TabIndex = 20;
			runSimulation.Text = "Run Simulation";
			runSimulation.UseVisualStyleBackColor = true;
			runSimulation.Click += runSimulation_Click;
			// 
			// machineCount
			// 
			machineCount.Location = new Point(397, 157);
			machineCount.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
			machineCount.Name = "machineCount";
			machineCount.Size = new Size(79, 27);
			machineCount.TabIndex = 22;
			machineCount.Value = new decimal(new int[] { 50, 0, 0, 0 });
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(36, 159);
			label6.Name = "label6";
			label6.Size = new Size(147, 20);
			label6.TabIndex = 21;
			label6.Text = "Number of Machines";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(519, 443);
			Controls.Add(machineCount);
			Controls.Add(label6);
			Controls.Add(runSimulation);
			Controls.Add(break2Minutes);
			Controls.Add(break2Rounds);
			Controls.Add(label5);
			Controls.Add(break1Minutes);
			Controls.Add(breakLabel);
			Controls.Add(break1Rounds);
			Controls.Add(longBreak2);
			Controls.Add(longBreak1);
			Controls.Add(massBreak);
			Controls.Add(flexBreak);
			Controls.Add(minBreak);
			Controls.Add(roundCount);
			Controls.Add(playerCount);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			Name = "Form1";
			Text = "Double Max Matchplay Simulator";
			((System.ComponentModel.ISupportInitialize)playerCount).EndInit();
			((System.ComponentModel.ISupportInitialize)roundCount).EndInit();
			((System.ComponentModel.ISupportInitialize)minBreak).EndInit();
			((System.ComponentModel.ISupportInitialize)break1Rounds).EndInit();
			((System.ComponentModel.ISupportInitialize)break1Minutes).EndInit();
			((System.ComponentModel.ISupportInitialize)break2Minutes).EndInit();
			((System.ComponentModel.ISupportInitialize)break2Rounds).EndInit();
			((System.ComponentModel.ISupportInitialize)machineCount).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private Label label2;
		private Label label3;
		private NumericUpDown playerCount;
		private NumericUpDown roundCount;
		private NumericUpDown minBreak;
		private RadioButton flexBreak;
		private RadioButton massBreak;
		private CheckBox longBreak1;
		private CheckBox longBreak2;
		private NumericUpDown break1Rounds;
		private Label breakLabel;
		private NumericUpDown break1Minutes;
		private Label label5;
		private NumericUpDown break2Minutes;
		private NumericUpDown break2Rounds;
		private Button runSimulation;
		private NumericUpDown machineCount;
		private Label label6;
	}
}
