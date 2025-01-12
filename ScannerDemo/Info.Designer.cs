
namespace ScannerDemo
{
    partial class InfoDialog
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
            this.info_text = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // info_text
            // 
            this.info_text.AutoSize = true;
            this.info_text.Location = new System.Drawing.Point(67, 102);
            this.info_text.Name = "info_text";
            this.info_text.Size = new System.Drawing.Size(86, 31);
            this.info_text.TabIndex = 0;
            this.info_text.Text = "label1";
            // 
            // InfoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 279);
            this.Controls.Add(this.info_text);
            this.Name = "InfoDialog";
            this.Text = "Info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label info_text;
    }
}