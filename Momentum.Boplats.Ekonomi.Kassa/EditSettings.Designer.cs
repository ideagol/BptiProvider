namespace Momentum.Boplats.Ekonomi.Kassa
{
    partial class EditSettings
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
            this.tcp = new System.Windows.Forms.TextBox();
            this.api = new System.Windows.Forms.TextBox();
            this.printer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.port = new System.Windows.Forms.TextBox();
            this.save = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.printerwindow = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tcp
            // 
            this.tcp.Location = new System.Drawing.Point(137, 13);
            this.tcp.Name = "tcp";
            this.tcp.Size = new System.Drawing.Size(265, 20);
            this.tcp.TabIndex = 0;
            // 
            // api
            // 
            this.api.Location = new System.Drawing.Point(137, 69);
            this.api.Name = "api";
            this.api.Size = new System.Drawing.Size(265, 20);
            this.api.TabIndex = 1;
            // 
            // printer
            // 
            this.printer.Location = new System.Drawing.Point(137, 97);
            this.printer.Name = "printer";
            this.printer.Size = new System.Drawing.Size(265, 20);
            this.printer.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Terminal IP-adress";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Boplats API url";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Skrivarnamn";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Terminal Port";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(137, 41);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(265, 20);
            this.port.TabIndex = 7;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(246, 150);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 9;
            this.save.Text = "Spara";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(327, 150);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 10;
            this.cancel.Text = "Avbryt";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // printerwindow
            // 
            this.printerwindow.AutoSize = true;
            this.printerwindow.Location = new System.Drawing.Point(16, 131);
            this.printerwindow.Name = "printerwindow";
            this.printerwindow.Size = new System.Drawing.Size(161, 17);
            this.printerwindow.TabIndex = 11;
            this.printerwindow.Text = "Öppna skrivarfönser vid start";
            this.printerwindow.UseVisualStyleBackColor = true;
            // 
            // EditSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 183);
            this.Controls.Add(this.printerwindow);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.save);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.port);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.printer);
            this.Controls.Add(this.api);
            this.Controls.Add(this.tcp);
            this.Name = "EditSettings";
            this.Text = "EditSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tcp;
        private System.Windows.Forms.TextBox api;
        private System.Windows.Forms.TextBox printer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.CheckBox printerwindow;
    }
}