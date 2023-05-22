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
            tableTaskView = new TableLayoutPanel();
            mapmodeNormalButton = new Button();
            mapmodeGroupButton = new Button();
            scoreTable = new TableLayoutPanel();
            redTeamScore = new TableLayoutPanel();
            greenTeamScore = new TableLayoutPanel();
            blueTeamScore = new TableLayoutPanel();
            yellowTeamScore = new TableLayoutPanel();
            redTeamPicture = new PictureBox();
            greenTeamPicture = new PictureBox();
            blueTeamPicture = new PictureBox();
            yellowTeamPicture = new PictureBox();
            labelRedTeamScore = new Label();
            labelGreenTeamScore = new Label();
            label1 = new Label();
            labelYellowTeamScore = new Label();
            tableLayoutPanel1.SuspendLayout();
            alertAndSymbols.SuspendLayout();
            scoreTable.SuspendLayout();
            redTeamScore.SuspendLayout();
            greenTeamScore.SuspendLayout();
            blueTeamScore.SuspendLayout();
            yellowTeamScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)redTeamPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)greenTeamPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)blueTeamPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)yellowTeamPicture).BeginInit();
            SuspendLayout();
            // 
            // sendButton
            // 
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            sendButton.ForeColor = Color.Green;
            sendButton.Location = new Point(639, 39);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(100, 100);
            sendButton.TabIndex = 14;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = true;
            // 
            // mapButton
            // 
            mapButton.FlatStyle = FlatStyle.Flat;
            mapButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            mapButton.ForeColor = Color.Blue;
            mapButton.Location = new Point(741, 39);
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
            tableLayoutPanel1.Location = new Point(41, 263);
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
            remaningTimeLabel.ForeColor = Color.Green;
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
            tableLayoutPanelButtons.Location = new Point(44, 303);
            tableLayoutPanelButtons.Margin = new Padding(0);
            tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            tableLayoutPanelButtons.RowCount = 1;
            tableLayoutPanelButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelButtons.Size = new Size(540, 540);
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
            alertLabel.BackColor = Color.Transparent;
            alertLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            alertLabel.Location = new Point(412, 9);
            alertLabel.Name = "alertLabel";
            alertLabel.Size = new Size(62, 30);
            alertLabel.TabIndex = 21;
            alertLabel.Text = "Alert";
            // 
            // roundLabel
            // 
            roundLabel.AutoSize = true;
            roundLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            roundLabel.ForeColor = Color.Red;
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
            moveLabel.ForeColor = Color.Blue;
            moveLabel.Location = new Point(38, 74);
            moveLabel.Name = "moveLabel";
            moveLabel.Size = new Size(66, 21);
            moveLabel.TabIndex = 23;
            moveLabel.Text = "Move 0";
            // 
            // teamMessagePanel
            // 
            teamMessagePanel.BackColor = Color.White;
            teamMessagePanel.ColumnCount = 1;
            teamMessagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            teamMessagePanel.Location = new Point(595, 303);
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
            alertAndSymbols.Location = new Point(286, 39);
            alertAndSymbols.Margin = new Padding(0);
            alertAndSymbols.Name = "alertAndSymbols";
            alertAndSymbols.RowCount = 1;
            alertAndSymbols.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            alertAndSymbols.Size = new Size(350, 100);
            alertAndSymbols.TabIndex = 25;
            // 
            // symbolLayoutPanel
            // 
            symbolLayoutPanel.BackColor = Color.White;
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
            // tableTaskView
            // 
            tableTaskView.BackColor = Color.White;
            tableTaskView.ColumnCount = 1;
            tableTaskView.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableTaskView.Location = new Point(41, 145);
            tableTaskView.Name = "tableTaskView";
            tableTaskView.RowCount = 1;
            tableTaskView.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableTaskView.Size = new Size(800, 100);
            tableTaskView.TabIndex = 26;
            // 
            // mapmodeNormalButton
            // 
            mapmodeNormalButton.BackgroundImage = Properties.Resources.normalMode;
            mapmodeNormalButton.FlatStyle = FlatStyle.Flat;
            mapmodeNormalButton.ForeColor = Color.Blue;
            mapmodeNormalButton.Location = new Point(113, 39);
            mapmodeNormalButton.Margin = new Padding(0);
            mapmodeNormalButton.Name = "mapmodeNormalButton";
            mapmodeNormalButton.Size = new Size(50, 50);
            mapmodeNormalButton.TabIndex = 27;
            mapmodeNormalButton.UseVisualStyleBackColor = true;
            mapmodeNormalButton.Click += mapmodeNormalButton_Click;
            // 
            // mapmodeGroupButton
            // 
            mapmodeGroupButton.BackgroundImage = Properties.Resources.groupMode;
            mapmodeGroupButton.FlatStyle = FlatStyle.Flat;
            mapmodeGroupButton.ForeColor = Color.Green;
            mapmodeGroupButton.Location = new Point(113, 89);
            mapmodeGroupButton.Margin = new Padding(0);
            mapmodeGroupButton.Name = "mapmodeGroupButton";
            mapmodeGroupButton.Size = new Size(50, 50);
            mapmodeGroupButton.TabIndex = 28;
            mapmodeGroupButton.UseVisualStyleBackColor = true;
            mapmodeGroupButton.Click += mapmodeGroupButton_Click;
            // 
            // scoreTable
            // 
            scoreTable.ColumnCount = 2;
            scoreTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            scoreTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            scoreTable.Controls.Add(redTeamScore, 0, 0);
            scoreTable.Controls.Add(greenTeamScore, 1, 0);
            scoreTable.Controls.Add(blueTeamScore, 0, 1);
            scoreTable.Controls.Add(yellowTeamScore, 1, 1);
            scoreTable.Location = new Point(595, 738);
            scoreTable.Name = "scoreTable";
            scoreTable.RowCount = 2;
            scoreTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            scoreTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            scoreTable.Size = new Size(250, 108);
            scoreTable.TabIndex = 29;
            // 
            // redTeamScore
            // 
            redTeamScore.ColumnCount = 2;
            redTeamScore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            redTeamScore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            redTeamScore.Controls.Add(redTeamPicture, 0, 0);
            redTeamScore.Controls.Add(labelRedTeamScore, 1, 0);
            redTeamScore.Location = new Point(0, 0);
            redTeamScore.Margin = new Padding(0);
            redTeamScore.Name = "redTeamScore";
            redTeamScore.RowCount = 1;
            redTeamScore.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            redTeamScore.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            redTeamScore.Size = new Size(125, 54);
            redTeamScore.TabIndex = 0;
            // 
            // greenTeamScore
            // 
            greenTeamScore.ColumnCount = 2;
            greenTeamScore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            greenTeamScore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            greenTeamScore.Controls.Add(greenTeamPicture, 0, 0);
            greenTeamScore.Controls.Add(labelGreenTeamScore, 1, 0);
            greenTeamScore.Location = new Point(125, 0);
            greenTeamScore.Margin = new Padding(0);
            greenTeamScore.Name = "greenTeamScore";
            greenTeamScore.RowCount = 1;
            greenTeamScore.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            greenTeamScore.Size = new Size(125, 54);
            greenTeamScore.TabIndex = 1;
            // 
            // blueTeamScore
            // 
            blueTeamScore.ColumnCount = 2;
            blueTeamScore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            blueTeamScore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            blueTeamScore.Controls.Add(blueTeamPicture, 0, 0);
            blueTeamScore.Controls.Add(label1, 1, 0);
            blueTeamScore.Location = new Point(0, 54);
            blueTeamScore.Margin = new Padding(0);
            blueTeamScore.Name = "blueTeamScore";
            blueTeamScore.RowCount = 1;
            blueTeamScore.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            blueTeamScore.Size = new Size(125, 54);
            blueTeamScore.TabIndex = 2;
            // 
            // yellowTeamScore
            // 
            yellowTeamScore.ColumnCount = 2;
            yellowTeamScore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            yellowTeamScore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            yellowTeamScore.Controls.Add(yellowTeamPicture, 0, 0);
            yellowTeamScore.Controls.Add(labelYellowTeamScore, 1, 0);
            yellowTeamScore.Location = new Point(125, 54);
            yellowTeamScore.Margin = new Padding(0);
            yellowTeamScore.Name = "yellowTeamScore";
            yellowTeamScore.RowCount = 1;
            yellowTeamScore.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            yellowTeamScore.Size = new Size(125, 54);
            yellowTeamScore.TabIndex = 3;
            // 
            // redTeamPicture
            // 
            redTeamPicture.BackColor = Color.Transparent;
            redTeamPicture.BackgroundImage = Properties.Resources.red_down;
            redTeamPicture.BackgroundImageLayout = ImageLayout.Zoom;
            redTeamPicture.Dock = DockStyle.Fill;
            redTeamPicture.Location = new Point(0, 0);
            redTeamPicture.Margin = new Padding(0);
            redTeamPicture.Name = "redTeamPicture";
            redTeamPicture.Size = new Size(62, 54);
            redTeamPicture.TabIndex = 0;
            redTeamPicture.TabStop = false;
            // 
            // greenTeamPicture
            // 
            greenTeamPicture.BackColor = Color.Transparent;
            greenTeamPicture.BackgroundImage = Properties.Resources.green_down;
            greenTeamPicture.BackgroundImageLayout = ImageLayout.Zoom;
            greenTeamPicture.Dock = DockStyle.Fill;
            greenTeamPicture.Location = new Point(0, 0);
            greenTeamPicture.Margin = new Padding(0);
            greenTeamPicture.Name = "greenTeamPicture";
            greenTeamPicture.Size = new Size(62, 54);
            greenTeamPicture.TabIndex = 0;
            greenTeamPicture.TabStop = false;
            // 
            // blueTeamPicture
            // 
            blueTeamPicture.BackColor = Color.Transparent;
            blueTeamPicture.BackgroundImage = Properties.Resources.blue_down;
            blueTeamPicture.BackgroundImageLayout = ImageLayout.Zoom;
            blueTeamPicture.Dock = DockStyle.Fill;
            blueTeamPicture.Location = new Point(0, 0);
            blueTeamPicture.Margin = new Padding(0);
            blueTeamPicture.Name = "blueTeamPicture";
            blueTeamPicture.Size = new Size(62, 54);
            blueTeamPicture.TabIndex = 0;
            blueTeamPicture.TabStop = false;
            // 
            // yellowTeamPicture
            // 
            yellowTeamPicture.BackColor = Color.Transparent;
            yellowTeamPicture.BackgroundImage = Properties.Resources.yellow_down;
            yellowTeamPicture.BackgroundImageLayout = ImageLayout.Zoom;
            yellowTeamPicture.Dock = DockStyle.Fill;
            yellowTeamPicture.Location = new Point(0, 0);
            yellowTeamPicture.Margin = new Padding(0);
            yellowTeamPicture.Name = "yellowTeamPicture";
            yellowTeamPicture.Size = new Size(62, 54);
            yellowTeamPicture.TabIndex = 0;
            yellowTeamPicture.TabStop = false;
            // 
            // labelRedTeamScore
            // 
            labelRedTeamScore.Anchor = AnchorStyles.None;
            labelRedTeamScore.AutoSize = true;
            labelRedTeamScore.BackColor = Color.Transparent;
            labelRedTeamScore.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            labelRedTeamScore.ForeColor = Color.Red;
            labelRedTeamScore.Location = new Point(84, 16);
            labelRedTeamScore.Name = "labelRedTeamScore";
            labelRedTeamScore.Size = new Size(19, 21);
            labelRedTeamScore.TabIndex = 1;
            labelRedTeamScore.Text = "0";
            // 
            // labelGreenTeamScore
            // 
            labelGreenTeamScore.Anchor = AnchorStyles.None;
            labelGreenTeamScore.AutoSize = true;
            labelGreenTeamScore.BackColor = Color.Transparent;
            labelGreenTeamScore.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            labelGreenTeamScore.ForeColor = Color.Green;
            labelGreenTeamScore.Location = new Point(84, 16);
            labelGreenTeamScore.Name = "labelGreenTeamScore";
            labelGreenTeamScore.Size = new Size(19, 21);
            labelGreenTeamScore.TabIndex = 1;
            labelGreenTeamScore.Text = "0";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Blue;
            label1.Location = new Point(84, 16);
            label1.Name = "label1";
            label1.Size = new Size(19, 21);
            label1.TabIndex = 1;
            label1.Text = "0";
            // 
            // labelYellowTeamScore
            // 
            labelYellowTeamScore.Anchor = AnchorStyles.None;
            labelYellowTeamScore.AutoSize = true;
            labelYellowTeamScore.BackColor = Color.Transparent;
            labelYellowTeamScore.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            labelYellowTeamScore.ForeColor = Color.Goldenrod;
            labelYellowTeamScore.Location = new Point(84, 16);
            labelYellowTeamScore.Name = "labelYellowTeamScore";
            labelYellowTeamScore.Size = new Size(19, 21);
            labelYellowTeamScore.TabIndex = 1;
            labelYellowTeamScore.Text = "0";
            // 
            // GameView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(884, 961);
            Controls.Add(scoreTable);
            Controls.Add(mapmodeGroupButton);
            Controls.Add(mapmodeNormalButton);
            Controls.Add(tableTaskView);
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
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "GameView";
            StartPosition = FormStartPosition.CenterParent;
            Text = "RGB";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            alertAndSymbols.ResumeLayout(false);
            scoreTable.ResumeLayout(false);
            redTeamScore.ResumeLayout(false);
            redTeamScore.PerformLayout();
            greenTeamScore.ResumeLayout(false);
            greenTeamScore.PerformLayout();
            blueTeamScore.ResumeLayout(false);
            blueTeamScore.PerformLayout();
            yellowTeamScore.ResumeLayout(false);
            yellowTeamScore.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)redTeamPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)greenTeamPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)blueTeamPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)yellowTeamPicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
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
        private TableLayoutPanel tableTaskView;
        private Button mapmodeNormalButton;
        private Button mapmodeGroupButton;
        private TableLayoutPanel scoreTable;
        private TableLayoutPanel redTeamScore;
        private PictureBox redTeamPicture;
        private TableLayoutPanel greenTeamScore;
        private TableLayoutPanel blueTeamScore;
        private TableLayoutPanel yellowTeamScore;
        private PictureBox greenTeamPicture;
        private PictureBox blueTeamPicture;
        private PictureBox yellowTeamPicture;
        private Label labelRedTeamScore;
        private Label labelGreenTeamScore;
        private Label label1;
        private Label labelYellowTeamScore;
    }
}