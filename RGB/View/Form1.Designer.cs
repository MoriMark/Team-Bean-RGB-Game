namespace RGB
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
            buttonGameStart = new Button();
            buttonHelp = new Button();
            numOfRobots = new NumericUpDown();
            numOfTeams = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)numOfRobots).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numOfTeams).BeginInit();
            SuspendLayout();
            // 
            // buttonGameStart
            // 
            buttonGameStart.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonGameStart.Location = new Point(325, 600);
            buttonGameStart.Name = "buttonGameStart";
            buttonGameStart.Size = new Size(250, 50);
            buttonGameStart.TabIndex = 0;
            buttonGameStart.Text = "Játék Indítása";
            buttonGameStart.UseVisualStyleBackColor = true;
            buttonGameStart.Click += buttonGameStart_Click;
            // 
            // buttonHelp
            // 
            buttonHelp.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonHelp.Location = new Point(325, 700);
            buttonHelp.Name = "buttonHelp";
            buttonHelp.Size = new Size(250, 50);
            buttonHelp.TabIndex = 1;
            buttonHelp.Text = "Súgó";
            buttonHelp.UseVisualStyleBackColor = true;
            buttonHelp.Click += buttonHelp_Click;
            // 
            // numOfRobots
            // 
            numOfRobots.Location = new Point(482, 329);
            numOfRobots.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            numOfRobots.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numOfRobots.Name = "numOfRobots";
            numOfRobots.Size = new Size(55, 23);
            numOfRobots.TabIndex = 2;
            numOfRobots.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // numOfTeams
            // 
            numOfTeams.Location = new Point(482, 276);
            numOfTeams.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            numOfTeams.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numOfTeams.Name = "numOfTeams";
            numOfTeams.Size = new Size(55, 23);
            numOfTeams.TabIndex = 3;
            numOfTeams.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(325, 276);
            label1.Name = "label1";
            label1.Size = new Size(136, 21);
            label1.TabIndex = 4;
            label1.Text = "Csapatok száma:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(325, 329);
            label2.Name = "label2";
            label2.Size = new Size(131, 21);
            label2.TabIndex = 5;
            label2.Text = "Robotok száma:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 961);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(numOfTeams);
            Controls.Add(numOfRobots);
            Controls.Add(buttonHelp);
            Controls.Add(buttonGameStart);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            Text = "RGB";
            ((System.ComponentModel.ISupportInitialize)numOfRobots).EndInit();
            ((System.ComponentModel.ISupportInitialize)numOfTeams).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonGameStart;
        private Button buttonHelp;
        private NumericUpDown numOfRobots;
        private NumericUpDown numOfTeams;
        private Label label1;
        private Label label2;
    }
}