namespace RGB
{
    partial class HelpForm
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
            buttonBack = new Button();
            helpTable = new TableLayoutPanel();
            buttonWrap = new TableLayoutPanel();
            leftButton = new Button();
            rightButton = new Button();
            labelHelp = new Label();
            pictureHelp = new PictureBox();
            helpTitle = new Label();
            pageCounter = new Label();
            helpTable.SuspendLayout();
            buttonWrap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureHelp).BeginInit();
            SuspendLayout();
            // 
            // buttonBack
            // 
            buttonBack.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonBack.Location = new Point(562, 899);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new Size(130, 50);
            buttonBack.TabIndex = 0;
            buttonBack.Text = "Vissza";
            buttonBack.UseVisualStyleBackColor = true;
            buttonBack.Click += buttonBack_Click;
            // 
            // helpTable
            // 
            helpTable.ColumnCount = 1;
            helpTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            helpTable.Controls.Add(buttonWrap, 0, 2);
            helpTable.Controls.Add(labelHelp, 0, 1);
            helpTable.Controls.Add(pictureHelp, 0, 0);
            helpTable.Location = new Point(39, 40);
            helpTable.Name = "helpTable";
            helpTable.RowCount = 3;
            helpTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            helpTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 99F));
            helpTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));
            helpTable.Size = new Size(626, 853);
            helpTable.TabIndex = 1;
            // 
            // buttonWrap
            // 
            buttonWrap.ColumnCount = 2;
            buttonWrap.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonWrap.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 316F));
            buttonWrap.Controls.Add(leftButton, 0, 0);
            buttonWrap.Controls.Add(rightButton, 1, 0);
            buttonWrap.Dock = DockStyle.Fill;
            buttonWrap.Location = new Point(3, 744);
            buttonWrap.Name = "buttonWrap";
            buttonWrap.RowCount = 1;
            buttonWrap.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            buttonWrap.Size = new Size(620, 106);
            buttonWrap.TabIndex = 0;
            // 
            // leftButton
            // 
            leftButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            leftButton.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            leftButton.Location = new Point(201, 3);
            leftButton.Name = "leftButton";
            leftButton.Size = new Size(100, 100);
            leftButton.TabIndex = 0;
            leftButton.Text = "<";
            leftButton.UseVisualStyleBackColor = true;
            leftButton.Click += leftButton_Click;
            // 
            // rightButton
            // 
            rightButton.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            rightButton.Location = new Point(307, 3);
            rightButton.Name = "rightButton";
            rightButton.Size = new Size(100, 100);
            rightButton.TabIndex = 1;
            rightButton.Text = ">";
            rightButton.UseVisualStyleBackColor = true;
            rightButton.Click += rightButton_Click;
            // 
            // labelHelp
            // 
            labelHelp.AutoSize = true;
            labelHelp.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            labelHelp.Location = new Point(3, 642);
            labelHelp.Name = "labelHelp";
            labelHelp.Size = new Size(43, 17);
            labelHelp.TabIndex = 1;
            labelHelp.Text = "label1";
            // 
            // pictureHelp
            // 
            pictureHelp.BackgroundImageLayout = ImageLayout.Zoom;
            pictureHelp.Dock = DockStyle.Fill;
            pictureHelp.Location = new Point(3, 3);
            pictureHelp.Name = "pictureHelp";
            pictureHelp.Size = new Size(620, 636);
            pictureHelp.TabIndex = 2;
            pictureHelp.TabStop = false;
            // 
            // helpTitle
            // 
            helpTitle.AutoSize = true;
            helpTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            helpTitle.Location = new Point(317, 9);
            helpTitle.Name = "helpTitle";
            helpTitle.Size = new Size(57, 21);
            helpTitle.TabIndex = 2;
            helpTitle.Text = "label1";
            helpTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pageCounter
            // 
            pageCounter.AutoSize = true;
            pageCounter.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            pageCounter.Location = new Point(317, 914);
            pageCounter.Name = "pageCounter";
            pageCounter.Size = new Size(57, 21);
            pageCounter.TabIndex = 3;
            pageCounter.Text = "label1";
            pageCounter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // HelpForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(704, 961);
            Controls.Add(pageCounter);
            Controls.Add(helpTitle);
            Controls.Add(helpTable);
            Controls.Add(buttonBack);
            Name = "HelpForm";
            Text = "RGB Help";
            helpTable.ResumeLayout(false);
            helpTable.PerformLayout();
            buttonWrap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureHelp).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonBack;
        private TableLayoutPanel helpTable;
        private TableLayoutPanel buttonWrap;
        private Label labelHelp;
        private PictureBox pictureHelp;
        private Button leftButton;
        private Button rightButton;
        private Label helpTitle;
        private Label pageCounter;
    }
}