namespace ADManager
{
    partial class ComputerInfoForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GetirBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.DiskLbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.RamLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CpuLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ErrLbl = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ErrLbl);
            this.groupBox1.Controls.Add(this.GetirBtn);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.DiskLbl);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.RamLbl);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.CpuLbl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.Location = new System.Drawing.Point(22, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(796, 194);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Donanım Bilgisi";
            // 
            // GetirBtn
            // 
            this.GetirBtn.Location = new System.Drawing.Point(10, 159);
            this.GetirBtn.Name = "GetirBtn";
            this.GetirBtn.Size = new System.Drawing.Size(139, 29);
            this.GetirBtn.TabIndex = 11;
            this.GetirBtn.Text = "Bilgileri Getir";
            this.GetirBtn.UseVisualStyleBackColor = true;
            this.GetirBtn.Click += new System.EventHandler(this.GetirBtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label7.Location = new System.Drawing.Point(62, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 19);
            this.label7.TabIndex = 10;
         //   this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 159);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 19);
            this.label8.TabIndex = 9;
            // 
            // DiskLbl
            // 
            this.DiskLbl.AutoSize = true;
            this.DiskLbl.BackColor = System.Drawing.SystemColors.ControlDark;
            this.DiskLbl.Location = new System.Drawing.Point(62, 120);
            this.DiskLbl.Name = "DiskLbl";
            this.DiskLbl.Size = new System.Drawing.Size(37, 19);
            this.DiskLbl.TabIndex = 8;
            this.DiskLbl.Text = "Null";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 19);
            this.label6.TabIndex = 7;
            this.label6.Text = "DISK";
            // 
            // RamLbl
            // 
            this.RamLbl.AutoSize = true;
            this.RamLbl.BackColor = System.Drawing.SystemColors.ControlDark;
            this.RamLbl.Location = new System.Drawing.Point(62, 82);
            this.RamLbl.Name = "RamLbl";
            this.RamLbl.Size = new System.Drawing.Size(37, 19);
            this.RamLbl.TabIndex = 6;
            this.RamLbl.Text = "Null";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "RAM";
            // 
            // CpuLbl
            // 
            this.CpuLbl.AutoSize = true;
            this.CpuLbl.BackColor = System.Drawing.SystemColors.ControlDark;
            this.CpuLbl.Location = new System.Drawing.Point(62, 51);
            this.CpuLbl.Name = "CpuLbl";
            this.CpuLbl.Size = new System.Drawing.Size(37, 19);
            this.CpuLbl.TabIndex = 4;
            this.CpuLbl.Text = "Null";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "CPU ";
            // 
            // ErrLbl
            // 
            this.ErrLbl.AutoSize = true;
            this.ErrLbl.Location = new System.Drawing.Point(186, 164);
            this.ErrLbl.Name = "ErrLbl";
            this.ErrLbl.Size = new System.Drawing.Size(0, 19);
            this.ErrLbl.TabIndex = 12;
            // 
            // ComputerInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 440);
            this.Controls.Add(this.groupBox1);
            this.Name = "ComputerInfoForm";
            this.Text = "ComputerInfoForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CpuLbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label DiskLbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label RamLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button GetirBtn;
        private System.Windows.Forms.Label ErrLbl;
    }
}