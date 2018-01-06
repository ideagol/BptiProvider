namespace Momentum.Boplats.Ekonomi.Kassa
{
    partial class PrinterWindow
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
            this.receipt = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // receipt
            // 
            this.receipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.receipt.Location = new System.Drawing.Point(0, 0);
            this.receipt.Name = "receipt";
            this.receipt.Size = new System.Drawing.Size(284, 561);
            this.receipt.TabIndex = 37;
            // 
            // PrinterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 561);
            this.ControlBox = false;
            this.Controls.Add(this.receipt);
            this.Name = "PrinterWindow";
            this.Text = "PrinterWindow";
            this.Load += new System.EventHandler(this.PrinterWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox receipt;
    }
}