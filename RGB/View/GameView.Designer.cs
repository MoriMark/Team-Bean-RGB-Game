namespace RGB.View
{
    partial class GameView
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            dataGridViewMessages = new DataGridView();
            Robot = new DataGridViewTextBoxColumn();
            Message = new DataGridViewImageColumn();
            listScores = new ListBox();
            tableTask3 = new TableLayoutPanel();
            tableTask2 = new TableLayoutPanel();
            tableTask1 = new TableLayoutPanel();
            button7 = new Button();
            button8 = new Button();
            testLabel = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            remaningTimeLabel = new Label();
            remaningTimeBar = new ProgressBar();
            tableLayoutPanelButtons = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMessages).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(57, 849);
            button1.Name = "button1";
            button1.Size = new Size(100, 100);
            button1.TabIndex = 0;
            button1.Text = "Mozog";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Location = new Point(188, 849);
            button2.Name = "button2";
            button2.Size = new Size(100, 100);
            button2.TabIndex = 1;
            button2.Text = "Forog";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button3.Location = new Point(320, 849);
            button3.Name = "button3";
            button3.Size = new Size(100, 100);
            button3.TabIndex = 2;
            button3.Text = "Összekapcsol";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button4.Location = new Point(458, 849);
            button4.Name = "button4";
            button4.Size = new Size(100, 100);
            button4.TabIndex = 3;
            button4.Text = "Szétkapcsol";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button5.Location = new Point(591, 849);
            button5.Name = "button5";
            button5.Size = new Size(100, 100);
            button5.TabIndex = 4;
            button5.Text = "Lekapcsol";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button6.Location = new Point(727, 849);
            button6.Name = "button6";
            button6.Size = new Size(100, 100);
            button6.TabIndex = 5;
            button6.Text = "Felkapcsol";
            button6.UseVisualStyleBackColor = true;
            // 
            // dataGridViewMessages
            // 
            dataGridViewMessages.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMessages.Columns.AddRange(new DataGridViewColumn[] { Robot, Message });
            dataGridViewMessages.Location = new Point(595, 323);
            dataGridViewMessages.Name = "dataGridViewMessages";
            dataGridViewMessages.RowHeadersWidth = 51;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewMessages.RowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewMessages.RowTemplate.Height = 25;
            dataGridViewMessages.Size = new Size(250, 360);
            dataGridViewMessages.TabIndex = 6;
            // 
            // Robot
            // 
            Robot.HeaderText = "Robot";
            Robot.MinimumWidth = 6;
            Robot.Name = "Robot";
            Robot.Width = 125;
            // 
            // Message
            // 
            Message.HeaderText = "Message";
            Message.MinimumWidth = 6;
            Message.Name = "Message";
            Message.Resizable = DataGridViewTriState.True;
            Message.SortMode = DataGridViewColumnSortMode.Automatic;
            Message.Width = 125;
            // 
            // listScores
            // 
            listScores.FormattingEnabled = true;
            listScores.ItemHeight = 15;
            listScores.Location = new Point(595, 689);
            listScores.Name = "listScores";
            listScores.Size = new Size(250, 154);
            listScores.TabIndex = 7;
            // 
            // tableTask3
            // 
            tableTask3.ColumnCount = 2;
            tableTask3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableTask3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableTask3.Location = new Point(595, 118);
            tableTask3.Name = "tableTask3";
            tableTask3.RowCount = 1;
            tableTask3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableTask3.Size = new Size(250, 150);
            tableTask3.TabIndex = 11;
            // 
            // tableTask2
            // 
            tableTask2.ColumnCount = 2;
            tableTask2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableTask2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableTask2.Location = new Point(320, 118);
            tableTask2.Name = "tableTask2";
            tableTask2.RowCount = 1;
            tableTask2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableTask2.Size = new Size(250, 150);
            tableTask2.TabIndex = 12;
            // 
            // tableTask1
            // 
            tableTask1.ColumnCount = 2;
            tableTask1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableTask1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableTask1.Location = new Point(38, 118);
            tableTask1.Name = "tableTask1";
            tableTask1.RowCount = 1;
            tableTask1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableTask1.Size = new Size(250, 150);
            tableTask1.TabIndex = 13;
            // 
            // button7
            // 
            button7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button7.Location = new Point(38, 12);
            button7.Name = "button7";
            button7.Size = new Size(100, 100);
            button7.TabIndex = 14;
            button7.Text = "Küldés";
            button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button8.Location = new Point(745, 12);
            button8.Name = "button8";
            button8.Size = new Size(100, 100);
            button8.TabIndex = 15;
            button8.Text = "Térkép";
            button8.UseVisualStyleBackColor = true;
            // 
            // testLabel
            // 
            testLabel.AutoSize = true;
            testLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            testLabel.Location = new Point(411, 75);
            testLabel.Name = "testLabel";
            testLabel.Size = new Size(31, 17);
            testLabel.TabIndex = 16;
            testLabel.Text = "test";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.582089F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 91.41791F));
            tableLayoutPanel1.Controls.Add(remaningTimeLabel, 0, 0);
            tableLayoutPanel1.Controls.Add(remaningTimeBar, 1, 0);
            tableLayoutPanel1.Location = new Point(41, 274);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(804, 37);
            tableLayoutPanel1.TabIndex = 18;
            // 
            // remaningTimeLabel
            // 
            remaningTimeLabel.AutoSize = true;
            remaningTimeLabel.Dock = DockStyle.Fill;
            remaningTimeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            remaningTimeLabel.Location = new Point(3, 0);
            remaningTimeLabel.Name = "remaningTimeLabel";
            remaningTimeLabel.Size = new Size(63, 37);
            remaningTimeLabel.TabIndex = 0;
            remaningTimeLabel.Text = "Time";
            remaningTimeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // remaningTimeBar
            // 
            remaningTimeBar.Dock = DockStyle.Fill;
            remaningTimeBar.Location = new Point(72, 3);
            remaningTimeBar.Maximum = 300;
            remaningTimeBar.Name = "remaningTimeBar";
            remaningTimeBar.Size = new Size(729, 31);
            remaningTimeBar.Step = -1;
            remaningTimeBar.TabIndex = 1;
            remaningTimeBar.Value = 300;
            // 
            // tableLayoutPanelButtons
            // 
            tableLayoutPanelButtons.ColumnCount = 1;
            tableLayoutPanelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelButtons.Location = new Point(41, 323);
            tableLayoutPanelButtons.Margin = new Padding(0);
            tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            tableLayoutPanelButtons.RowCount = 1;
            tableLayoutPanelButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelButtons.Size = new Size(520, 520);
            tableLayoutPanelButtons.TabIndex = 19;
            // 
            // GameView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 961);
            Controls.Add(tableLayoutPanelButtons);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(testLabel);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(tableTask1);
            Controls.Add(tableTask2);
            Controls.Add(tableTask3);
            Controls.Add(listScores);
            Controls.Add(dataGridViewMessages);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "GameView";
            StartPosition = FormStartPosition.CenterParent;
            Text = "RGB";
            ((System.ComponentModel.ISupportInitialize)dataGridViewMessages).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private DataGridView dataGridViewMessages;
        private DataGridViewTextBoxColumn Robot;
        private DataGridViewImageColumn Message;
        private ListBox listScores;
        private TableLayoutPanel tableTask3;
        private TableLayoutPanel tableTask2;
        private TableLayoutPanel tableTask1;
        private Button button7;
        private Button button8;
        private Label testLabel;
        private TableLayoutPanel tableLayoutPanel1;
        private Label remaningTimeLabel;
        private ProgressBar remaningTimeBar;
        private TableLayoutPanel tableLayoutPanelButtons;
    }
}