namespace MDIPaint
{
    partial class DocumentForm
    {

        private System.ComponentModel.IContainer components = null;

        /// <param name="disposing">
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.DoubleBuffered = true;
            this.Name = "DocumentForm";
            this.Text = "DocumentForm";
            this.Load += new System.EventHandler(this.DocumentForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}