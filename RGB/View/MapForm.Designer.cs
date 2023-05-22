namespace RGB.View
{
    partial class MapForm
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
            mapTable = new TableLayoutPanel();
            mapLabel = new Label();
            SuspendLayout();
            // 
            // mapTable
            // 
            mapTable.ColumnCount = 1;
            mapTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mapTable.Location = new Point(8, 52);
            mapTable.Margin = new Padding(0);
            mapTable.Name = "mapTable";
            mapTable.RowCount = 1;
            mapTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            mapTable.Size = new Size(650, 650);
            mapTable.TabIndex = 0;
            // 
            // mapLabel
            // 
            mapLabel.AutoSize = true;
            mapLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            mapLabel.Location = new Point(237, 9);
            mapLabel.Name = "mapLabel";
            mapLabel.Size = new Size(0, 21);
            mapLabel.TabIndex = 1;
            // 
            // MapForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(667, 711);
            Controls.Add(mapLabel);
            Controls.Add(mapTable);
            Name = "MapForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Map";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel mapTable;
        private Label mapLabel;
    }
}