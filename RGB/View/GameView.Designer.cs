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
            listScores = new ListBox();
            tableTask3 = new TableLayoutPanel();
            tableTask2 = new TableLayoutPanel();
            tableTask1 = new TableLayoutPanel();
            sendButton = new Button();
            mapButton = new Button();
            testLabel = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            remaningTimeLabel = new Label();
            remaningTimeBar = new ProgressBar();
            tableLayoutPanelButtons = new TableLayoutPanel();
            actionButtons = new TableLayoutPanel();
            alertLabel = new Label();
            roundLabel = new Label();
            moveLabel = new Label();
            teamMessagePanel = new TableLayoutPanel();
            alertAndSymbols = new TableLayoutPanel();
            symbolLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            alertAndSymbols.SuspendLayout();
            SuspendLayout();
            // 
            // listScores
            // 
            listScores.FormattingEnabled = true;
            listScores.ItemHeight = 15;
            listScores.Location = new Point(595, 734);
            listScores.Name = "listScores";
            listScores.Size = new Size(250, 109);
            listScores.TabIndex = 7;
            // 
            // tableTask3
            // 
            tableTask3.ColumnCount = 2;
            tableTask3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableTask3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableTask3.Location = new Point(642, 145);
            tableTask3.Name = "tableTask3";
            tableTask3.RowCount = 1;
            tableTask3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableTask3.Size = new Size(200, 100);
            tableTask3.TabIndex = 11;
            // 
            // tableTask2
            // 
            tableTask2.ColumnCount = 2;
            tableTask2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableTask2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableTask2.Location = new Point(345, 142);
            tableTask2.Name = "tableTask2";
            tableTask2.RowCount = 1;
            tableTask2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableTask2.Size = new Size(200, 100);
            tableTask2.TabIndex = 12;
            // 
            // tableTask1
            // 
            tableTask1.ColumnCount = 2;
            tableTask1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableTask1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableTask1.Location = new Point(38, 142);
            tableTask1.Name = "tableTask1";
            tableTask1.RowCount = 1;
            tableTask1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableTask1.Size = new Size(200, 100);
            tableTask1.TabIndex = 13;
            // 
            // sendButton
            // 
            sendButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            sendButton.Location = new Point(629, 39);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(100, 100);
            sendButton.TabIndex = 14;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = true;
            // 
            // mapButton
            // 
            mapButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            mapButton.Location = new Point(745, 39);
            mapButton.Name = "mapButton";
            mapButton.Size = new Size(100, 100);
            mapButton.TabIndex = 15;
            mapButton.Text = "Map";
            mapButton.UseVisualStyleBackColor = true;
            // 
            // testLabel
            // 
            testLabel.AutoSize = true;
            testLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            testLabel.Location = new Point(38, 95);
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
            tableLayoutPanel1.Location = new Point(41, 251);
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
            tableLayoutPanelButtons.Location = new Point(41, 291);
            tableLayoutPanelButtons.Margin = new Padding(0);
            tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            tableLayoutPanelButtons.RowCount = 1;
            tableLayoutPanelButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelButtons.Size = new Size(551, 552);
            tableLayoutPanelButtons.TabIndex = 19;
            // 
            // actionButtons
            // 
            actionButtons.ColumnCount = 1;
            actionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            actionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            actionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            actionButtons.Location = new Point(44, 849);
            actionButtons.Margin = new Padding(0);
            actionButtons.Name = "actionButtons";
            actionButtons.RowCount = 1;
            actionButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            actionButtons.Size = new Size(801, 100);
            actionButtons.TabIndex = 20;
            // 
            // alertLabel
            // 
            alertLabel.Anchor = AnchorStyles.None;
            alertLabel.AutoSize = true;
            alertLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            alertLabel.Location = new Point(320, 6);
            alertLabel.Name = "alertLabel";
            alertLabel.Size = new Size(62, 30);
            alertLabel.TabIndex = 21;
            alertLabel.Text = "Alert";
            // 
            // roundLabel
            // 
            roundLabel.AutoSize = true;
            roundLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            roundLabel.Location = new Point(38, 52);
            roundLabel.Name = "roundLabel";
            roundLabel.Size = new Size(57, 21);
            roundLabel.TabIndex = 22;
            roundLabel.Text = "Turn 0";
            // 
            // moveLabel
            // 
            moveLabel.AutoSize = true;
            moveLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            moveLabel.Location = new Point(38, 74);
            moveLabel.Name = "moveLabel";
            moveLabel.Size = new Size(66, 21);
            moveLabel.TabIndex = 23;
            moveLabel.Text = "Move 0";
            // 
            // teamMessagePanel
            // 
            teamMessagePanel.ColumnCount = 1;
            teamMessagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            teamMessagePanel.Location = new Point(595, 291);
            teamMessagePanel.Margin = new Padding(0);
            teamMessagePanel.Name = "teamMessagePanel";
            teamMessagePanel.RowCount = 1;
            teamMessagePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            teamMessagePanel.Size = new Size(250, 432);
            teamMessagePanel.TabIndex = 24;
            // 
            // alertAndSymbols
            // 
            alertAndSymbols.ColumnCount = 1;
            alertAndSymbols.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            alertAndSymbols.Controls.Add(symbolLayoutPanel, 0, 0);
            alertAndSymbols.Location = new Point(211, 39);
            alertAndSymbols.Margin = new Padding(0);
            alertAndSymbols.Name = "alertAndSymbols";
            alertAndSymbols.RowCount = 1;
            alertAndSymbols.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            alertAndSymbols.Size = new Size(350, 100);
            alertAndSymbols.TabIndex = 25;
            // 
            // symbolLayoutPanel
            // 
            symbolLayoutPanel.ColumnCount = 1;
            symbolLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            symbolLayoutPanel.Dock = DockStyle.Fill;
            symbolLayoutPanel.Location = new Point(0, 0);
            symbolLayoutPanel.Margin = new Padding(0);
            symbolLayoutPanel.Name = "symbolLayoutPanel";
            symbolLayoutPanel.RowCount = 1;
            symbolLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            symbolLayoutPanel.Size = new Size(350, 100);
            symbolLayoutPanel.TabIndex = 22;
            // 
            // GameView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 961);
            Controls.Add(alertLabel);
            Controls.Add(alertAndSymbols);
            Controls.Add(teamMessagePanel);
            Controls.Add(moveLabel);
            Controls.Add(roundLabel);
            Controls.Add(actionButtons);
            Controls.Add(tableLayoutPanelButtons);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(testLabel);
            Controls.Add(mapButton);
            Controls.Add(sendButton);
            Controls.Add(tableTask1);
            Controls.Add(tableTask2);
            Controls.Add(tableTask3);
            Controls.Add(listScores);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "GameView";
            StartPosition = FormStartPosition.CenterParent;
            Text = "RGB";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            alertAndSymbols.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListBox listScores;
        private TableLayoutPanel tableTask3;
        private TableLayoutPanel tableTask2;
        private TableLayoutPanel tableTask1;
        private Button sendButton;
        private Button mapButton;
        private Label testLabel;
        private TableLayoutPanel tableLayoutPanel1;
        private Label remaningTimeLabel;
        private ProgressBar remaningTimeBar;
        private TableLayoutPanel tableLayoutPanelButtons;
        private TableLayoutPanel actionButtons;
        private Label alertLabel;
        private Label roundLabel;
        private Label moveLabel;
        private TableLayoutPanel teamMessagePanel;
        private TableLayoutPanel alertAndSymbols;
        private TableLayoutPanel symbolLayoutPanel;
    }
}