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
            SuspendLayout();
            // 
            // buttonBack
            // 
            buttonBack.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonBack.Location = new Point(175, 600);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new Size(250, 50);
            buttonBack.TabIndex = 0;
            buttonBack.Text = "Vissza";
            buttonBack.UseVisualStyleBackColor = true;
            buttonBack.Click += buttonBack_Click;
            // 
            // HelpForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 761);
            Controls.Add(buttonBack);
            Name = "HelpForm";
            Text = "RGB Súgó";
            ResumeLayout(false);
        }

        #endregion

        private Button buttonBack;
    }
}