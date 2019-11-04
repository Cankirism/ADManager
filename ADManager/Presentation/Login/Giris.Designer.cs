namespace ADManager
{
    partial class Giris
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Giris));
            this.GirisBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.TxtUser = new MetroFramework.Controls.MetroTextBox();
            this.TxtPass = new MetroFramework.Controls.MetroTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // GirisBtn
            // 
            this.GirisBtn.BackColor = System.Drawing.Color.SeaGreen;
            this.GirisBtn.FlatAppearance.BorderColor = System.Drawing.Color.SeaGreen;
            this.GirisBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.GirisBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GirisBtn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GirisBtn.Location = new System.Drawing.Point(66, 320);
            this.GirisBtn.Name = "GirisBtn";
            this.GirisBtn.Size = new System.Drawing.Size(212, 29);
            this.GirisBtn.TabIndex = 2;
            this.GirisBtn.Text = "Oturum Aç";
            this.GirisBtn.UseVisualStyleBackColor = false;
            this.GirisBtn.Click += new System.EventHandler(this.GirisBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(43, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(246, 178);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.SeaGreen;
            this.label3.Location = new System.Drawing.Point(294, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 22);
            this.label3.TabIndex = 7;
            this.label3.Text = "X";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // TxtUser
            // 
            // 
            // 
            // 
            this.TxtUser.CustomButton.Image = null;
            this.TxtUser.CustomButton.Location = new System.Drawing.Point(190, 1);
            this.TxtUser.CustomButton.Name = "";
            this.TxtUser.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.TxtUser.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.TxtUser.CustomButton.TabIndex = 1;
            this.TxtUser.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TxtUser.CustomButton.UseSelectable = true;
            this.TxtUser.CustomButton.Visible = false;
            this.TxtUser.Lines = new string[0];
            this.TxtUser.Location = new System.Drawing.Point(66, 249);
            this.TxtUser.MaxLength = 32767;
            this.TxtUser.Name = "TxtUser";
            this.TxtUser.PasswordChar = '\0';
            this.TxtUser.PromptText = "Domain Kullanıcı Adı";
            this.TxtUser.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TxtUser.SelectedText = "";
            this.TxtUser.SelectionLength = 0;
            this.TxtUser.SelectionStart = 0;
            this.TxtUser.ShortcutsEnabled = true;
            this.TxtUser.Size = new System.Drawing.Size(212, 23);
            this.TxtUser.TabIndex = 8;
            this.TxtUser.UseSelectable = true;
            this.TxtUser.WaterMark = "Domain Kullanıcı Adı";
            this.TxtUser.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.TxtUser.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // TxtPass
            // 
            // 
            // 
            // 
            this.TxtPass.CustomButton.Image = null;
            this.TxtPass.CustomButton.Location = new System.Drawing.Point(190, 1);
            this.TxtPass.CustomButton.Name = "";
            this.TxtPass.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.TxtPass.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.TxtPass.CustomButton.TabIndex = 1;
            this.TxtPass.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TxtPass.CustomButton.UseSelectable = true;
            this.TxtPass.CustomButton.Visible = false;
            this.TxtPass.Lines = new string[0];
            this.TxtPass.Location = new System.Drawing.Point(66, 278);
            this.TxtPass.MaxLength = 32767;
            this.TxtPass.Name = "TxtPass";
            this.TxtPass.PasswordChar = '*';
            this.TxtPass.PromptText = "Domain Parola";
            this.TxtPass.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TxtPass.SelectedText = "";
            this.TxtPass.SelectionLength = 0;
            this.TxtPass.SelectionStart = 0;
            this.TxtPass.ShortcutsEnabled = true;
            this.TxtPass.Size = new System.Drawing.Size(212, 23);
            this.TxtPass.TabIndex = 9;
            this.TxtPass.UseSelectable = true;
            this.TxtPass.WaterMark = "Domain Parola";
            this.TxtPass.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.TxtPass.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // Giris
            // 
            this.AcceptButton = this.GirisBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(326, 415);
            this.Controls.Add(this.TxtPass);
            this.Controls.Add(this.TxtUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.GirisBtn);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Giris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ADManagerLogin";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Giris_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Giris_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Giris_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button GirisBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MetroFramework.Controls.MetroTextBox TxtUser;
        private MetroFramework.Controls.MetroTextBox TxtPass;
    }
}