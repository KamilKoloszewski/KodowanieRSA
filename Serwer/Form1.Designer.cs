namespace Serwer
{
    partial class Serwer
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
            this.button2 = new System.Windows.Forms.Button();
            this.tekstSerwer = new System.Windows.Forms.TextBox();
            this.przychodzaceSerwer = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(114, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(194, 223);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Wyślij";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tekstSerwer
            // 
            this.tekstSerwer.Location = new System.Drawing.Point(12, 226);
            this.tekstSerwer.Name = "tekstSerwer";
            this.tekstSerwer.Size = new System.Drawing.Size(176, 20);
            this.tekstSerwer.TabIndex = 2;
            // 
            // przychodzaceSerwer
            // 
            this.przychodzaceSerwer.FormattingEnabled = true;
            this.przychodzaceSerwer.Location = new System.Drawing.Point(13, 45);
            this.przychodzaceSerwer.Name = "przychodzaceSerwer";
            this.przychodzaceSerwer.Size = new System.Drawing.Size(176, 160);
            this.przychodzaceSerwer.TabIndex = 3;
            // 
            // Serwer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.przychodzaceSerwer);
            this.Controls.Add(this.tekstSerwer);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Serwer";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tekstSerwer;
        private System.Windows.Forms.ListBox przychodzaceSerwer;
    }
}

