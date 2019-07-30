namespace ADManager
{
    partial class PasswordChange
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
            this.newPassTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DegistirBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newPassTxt
            // 
            this.newPassTxt.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newPassTxt.Location = new System.Drawing.Point(138, 40);
            this.newPassTxt.Name = "newPassTxt";
            this.newPassTxt.PasswordChar = '*';
            this.newPassTxt.Size = new System.Drawing.Size(225, 26);
            this.newPassTxt.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(37, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Yeni Parola :";
            // 
            // DegistirBtn
            // 
            this.DegistirBtn.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.DegistirBtn.Location = new System.Drawing.Point(288, 72);
            this.DegistirBtn.Name = "DegistirBtn";
            this.DegistirBtn.Size = new System.Drawing.Size(75, 27);
            this.DegistirBtn.TabIndex = 2;
            this.DegistirBtn.Text = "Değiştir";
            this.DegistirBtn.UseVisualStyleBackColor = true;
            this.DegistirBtn.Click += new System.EventHandler(this.DegistirBtn_Click);
            // 
            // PasswordChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(524, 128);
            this.Controls.Add(this.DegistirBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newPassTxt);
            this.Location = new System.Drawing.Point(277, 80);
            this.Name = "PasswordChange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parola Değiştirme";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox newPassTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DegistirBtn;
    }
}