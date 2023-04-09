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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)numOfRobots).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numOfTeams).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // buttonGameStart
            // 
            buttonGameStart.Anchor = AnchorStyles.None;
            buttonGameStart.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonGameStart.Location = new Point(317, 563);
            buttonGameStart.Name = "buttonGameStart";
            buttonGameStart.Size = new Size(250, 50);
            buttonGameStart.TabIndex = 0;
            buttonGameStart.Text = "Játék Indítása";
            buttonGameStart.UseVisualStyleBackColor = true;
            buttonGameStart.Click += buttonGameStart_Click;
            // 
            // buttonHelp
            // 
            buttonHelp.Anchor = AnchorStyles.Top;
            buttonHelp.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonHelp.Location = new Point(317, 634);
            buttonHelp.Name = "buttonHelp";
            buttonHelp.Size = new Size(250, 50);
            buttonHelp.TabIndex = 1;
            buttonHelp.Text = "Súgó";
            buttonHelp.UseVisualStyleBackColor = true;
            buttonHelp.Click += buttonHelp_Click;
            // 
            // numOfRobots
            // 
            numOfRobots.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            numOfRobots.Location = new Point(449, 280);
            numOfRobots.Margin = new Padding(10);
            numOfRobots.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            numOfRobots.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numOfRobots.Name = "numOfRobots";
            numOfRobots.Size = new Size(55, 29);
            numOfRobots.TabIndex = 2;
            numOfRobots.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // numOfTeams
            // 
            numOfTeams.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            numOfTeams.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            numOfTeams.Location = new Point(449, 231);
            numOfTeams.Margin = new Padding(10);
            numOfTeams.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            numOfTeams.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numOfTeams.Name = "numOfTeams";
            numOfTeams.Size = new Size(55, 29);
            numOfTeams.TabIndex = 3;
            numOfTeams.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(293, 239);
            label1.Margin = new Padding(10);
            label1.Name = "label1";
            label1.Size = new Size(136, 21);
            label1.TabIndex = 4;
            label1.Text = "Csapatok száma:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(298, 280);
            label2.Margin = new Padding(10);
            label2.Name = "label2";
            label2.Size = new Size(131, 21);
            label2.TabIndex = 5;
            label2.Text = "Robotok száma:";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(numOfRobots, 1, 1);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(numOfTeams, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(878, 540);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(buttonGameStart, 0, 1);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel1, 0, 0);
            tableLayoutPanel2.Controls.Add(buttonHelp, 0, 2);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 86.41304F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 13.586957F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 329F));
            tableLayoutPanel2.Size = new Size(884, 961);
            tableLayoutPanel2.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 961);
            Controls.Add(tableLayoutPanel2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            Text = "RGB";
            ((System.ComponentModel.ISupportInitialize)numOfRobots).EndInit();
            ((System.ComponentModel.ISupportInitialize)numOfTeams).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button buttonGameStart;
        private Button buttonHelp;
        private NumericUpDown numOfRobots;
        private NumericUpDown numOfTeams;
        private Label label1;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}