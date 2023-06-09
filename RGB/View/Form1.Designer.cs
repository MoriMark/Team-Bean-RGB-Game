﻿namespace RGB
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
            logoPicture = new PictureBox();
            layoutRobotsPanel = new TableLayoutPanel();
            labelNumOfPlayers = new Label();
            ((System.ComponentModel.ISupportInitialize)logoPicture).BeginInit();
            SuspendLayout();
            // 
            // buttonGameStart
            // 
            buttonGameStart.Anchor = AnchorStyles.None;
            buttonGameStart.BackColor = Color.White;
            buttonGameStart.FlatStyle = FlatStyle.Flat;
            buttonGameStart.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonGameStart.ForeColor = Color.Red;
            buttonGameStart.Location = new Point(333, 769);
            buttonGameStart.Name = "buttonGameStart";
            buttonGameStart.Size = new Size(240, 50);
            buttonGameStart.TabIndex = 0;
            buttonGameStart.Text = "Start Game";
            buttonGameStart.UseVisualStyleBackColor = false;
            buttonGameStart.Click += buttonGameStart_Click;
            // 
            // buttonHelp
            // 
            buttonHelp.Anchor = AnchorStyles.Top;
            buttonHelp.FlatStyle = FlatStyle.Flat;
            buttonHelp.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonHelp.ForeColor = Color.Green;
            buttonHelp.Location = new Point(333, 856);
            buttonHelp.Name = "buttonHelp";
            buttonHelp.Size = new Size(240, 50);
            buttonHelp.TabIndex = 1;
            buttonHelp.Text = "Help";
            buttonHelp.UseVisualStyleBackColor = true;
            buttonHelp.Click += buttonHelp_Click;
            // 
            // logoPicture
            // 
            logoPicture.BackColor = Color.White;
            logoPicture.BackgroundImage = Properties.Resources.teambeanlogo_cropped;
            logoPicture.BackgroundImageLayout = ImageLayout.Zoom;
            logoPicture.Location = new Point(0, 0);
            logoPicture.Name = "logoPicture";
            logoPicture.Size = new Size(890, 969);
            logoPicture.TabIndex = 10;
            logoPicture.TabStop = false;
            // 
            // layoutRobotsPanel
            // 
            layoutRobotsPanel.ColumnCount = 1;
            layoutRobotsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutRobotsPanel.Location = new Point(295, 358);
            layoutRobotsPanel.Margin = new Padding(0);
            layoutRobotsPanel.Name = "layoutRobotsPanel";
            layoutRobotsPanel.RowCount = 1;
            layoutRobotsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutRobotsPanel.Size = new Size(310, 314);
            layoutRobotsPanel.TabIndex = 11;
            // 
            // labelNumOfPlayers
            // 
            labelNumOfPlayers.AutoSize = true;
            labelNumOfPlayers.BackColor = Color.White;
            labelNumOfPlayers.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            labelNumOfPlayers.ForeColor = Color.Blue;
            labelNumOfPlayers.Location = new Point(352, 282);
            labelNumOfPlayers.Name = "labelNumOfPlayers";
            labelNumOfPlayers.Size = new Size(196, 30);
            labelNumOfPlayers.TabIndex = 12;
            labelNumOfPlayers.Text = "Number of Players";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = Properties.Resources.mainmenu;
            ClientSize = new Size(884, 961);
            Controls.Add(logoPicture);
            Controls.Add(labelNumOfPlayers);
            Controls.Add(layoutRobotsPanel);
            Controls.Add(buttonHelp);
            Controls.Add(buttonGameStart);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "RGB";
            ((System.ComponentModel.ISupportInitialize)logoPicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonGameStart;
        private Button buttonHelp;
        private PictureBox logoPicture;
        private TableLayoutPanel layoutRobotsPanel;
        private Label labelNumOfPlayers;
    }
}