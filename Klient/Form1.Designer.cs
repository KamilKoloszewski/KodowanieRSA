namespace Klient
{
    partial class Klient
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
            this.button1 = new System.Windows.Forms.Button();
            this.przychodzaceKlient = new System.Windows.Forms.ListBox();
            this.tekstKlient = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(110, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Połącz";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // przychodzaceKlient
            // 
            this.przychodzaceKlient.FormattingEnabled = true;
            this.przychodzaceKlient.Location = new System.Drawing.Point(12, 42);
            this.przychodzaceKlient.Name = "przychodzaceKlient";
            this.przychodzaceKlient.Size = new System.Drawing.Size(173, 160);
            this.przychodzaceKlient.TabIndex = 1;
            // 
            // tekstKlient
            // 
            this.tekstKlient.Location = new System.Drawing.Point(12, 229);
            this.tekstKlient.Name = "tekstKlient";
            this.tekstKlient.Size = new System.Drawing.Size(173, 20);
            this.tekstKlient.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(205, 229);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Wyślij";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Klient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tekstKlient);
            this.Controls.Add(this.przychodzaceKlient);
            this.Controls.Add(this.button1);
            this.Name = "Klient";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Klient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox przychodzaceKlient;
        private System.Windows.Forms.TextBox tekstKlient;
        private System.Windows.Forms.Button button2;
    }
}

