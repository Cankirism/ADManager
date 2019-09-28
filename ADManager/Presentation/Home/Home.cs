using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices;
using System.Collections;
using System.Threading;

namespace ADManager
{

    public partial class Home : Form
    {
        // Form hareket parametreleri
        private int TogMove, MvalX, MvalY;
        // Aktif Dizin Kullanıcı adı- parolası
        private string _logonName;
        private string _logonPass;
        private DataTable _dtKullaniciListesi;
        private int rowCount = 0;
       
      
        public Home()
        {
            _logonName = Giris.UserName;
            _logonPass = Giris.UserPassword;
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            SetDataGridSettings();
            SetLabelSettings();
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.KullaniciBilgiGetirBtn, "Tüm Kullanıcıları Listele");
            ToolTip1.SetToolTip(this.CihazYonetimBtn, "Bilgisayar Yönetim Ekranı");
            ToolTip1.SetToolTip(this.KullaniciEkleBtn,"Kullanıcı Yönetim Ekranı");
            ToolTip1.SetToolTip(this.AboutBtn, "Uygulama Hakkında");
            ToolTip1.SetToolTip(this.GithubBtn, "uygulama Github hesabı");
            ToolTip1.SetToolTip(this.Contactbtn, "İletişim");
            toolTip1.SetToolTip(this.ExitBtn, "Çıkış");
            UserNameLbl.Text = _logonName;
        }

        /// <summary>
        /// DataGridview tasarımını yaptığımız methodumuz.
        /// </summary>
        public void SetDataGridSettings()
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        }
        /// <summary>
        /// Set Label Text.
        /// </summary>
        public void SetLabelSettings()
        {
            string date = DateTime.Today.ToLongDateString();
            label3.Text = $"Bağlantı Kuruldu : {System.Configuration.ConfigurationManager.AppSettings["path"]}";
            datelbl.Text = date;
        }

        private void KullaniciBilgiGetirBtn_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            backgroundWorker1.RunWorkerAsync();
            pictureBox4.Visible = true;
            LblRowCount.Visible = true;
            LblRowCount.Text = "Kullanıcılar Listeleniyor. Lütfen Bekleyiniz....";

        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            KullaniciListesiGetir();
        }

        // Aktif dizin sunucusundaki kullanıcıları getirir.
        private void KullaniciListesiGetir()
        {
            if (_dtKullaniciListesi == null)
            {
                var users = new BusinessUser();
                _dtKullaniciListesi = users.GetAllAdUsers();
                rowCount = _dtKullaniciListesi.Rows.Count;
            }

            else
            {
                LblRowCount.Text = $"{rowCount.ToString()} Kayıt Listelendi.";
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox4.Visible = false;
            FillGridview();
            SetLabelRowCountProperty();
        }

        //Aktif dizin sunucusundaki kullanıcılarını gridview'de gösterir.
        private void FillGridview()
        {
            try
            {
                dataGridView1.DataSource = _dtKullaniciListesi;
                for (int a = 1; a <= 6; a++)
                {
                    dataGridView1.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        private void SetLabelRowCountProperty()
        {
          
            LblRowCount.Text = $"{rowCount.ToString()} Kayıt Listelendi.";
        }

        private void KullaniciEkleBtn_Click(object sender, EventArgs e)
        {
            OpenForm(sender);
        }

        private void CihazYonetimBtn_Click(object sender, EventArgs e)
        {
            OpenForm(sender);
        }

        private void AboutBtn_Click(object sender, EventArgs e)
        {
            OpenForm(sender);
        }

        private void Contactbtn_Click(object sender, EventArgs e)
        {
            OpenForm(sender);
        }

        //Gösterilecek olan formu belirleyen method.
        private void OpenForm(object sender)
        {
            bool isOpen = false;
            string formName = string.Empty;

            switch (((Button)sender).Name)
            {
                case "KullaniciEkleBtn":
                    formName = "UserForm";
                    break;

                case "CihazYonetimBtn":
                    formName = "ComputerForm";
                    break;

                case "AboutBtn":
                    formName = "About";
                    break;

                case "Contactbtn":
                    formName = "Contact";
                    break;
                case "OUBtn":
                    formName = "ou";
                    break;
            }

            CheckOpenForm(formName, isOpen);
        }

        // Formun Show() edilmeden önce durumu kontrol edilir. Zaten SHow() edilmiş ise tekrar açılmaz.
        private void CheckOpenForm(string formName,bool isOpen )
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name == formName)
                {
                    ShowMessage("Bu Form Zaten Açık");
                    isOpen = true;
                }
            }

            if (!isOpen)
            {
                this.WindowState = FormWindowState.Minimized;
                switch (formName)
                {
                    case "UserForm":

                        ShowForm<UserForm>();
                        break;

                    case "ComputerForm":

                        ShowForm<ComputerForm>();
                        break;

                    case "About":
                        ShowForm<About>();
                        break;

                    case "Contact":
                        ShowForm<Contact>();
                        break;
                  
                }
            }
        }
        private void ShowForm<T>() where T : Form
        {
            var form = (Form)Activator.CreateInstance(typeof(T));
            form.Show();
        }

        private void ShowMessage(string gosterilecekMesaj)
        {
            MessageBox.Show(gosterilecekMesaj);
        }

        //  HOME IU formunun mouse ile taşınmasını sağlayan methodlar. 
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1;
            MvalX = e.X;
            MvalY = e.Y;
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)

                this.SetDesktopLocation(MousePosition.X - MvalX, MousePosition.Y - MvalY);
        }

        private void ExcelBtn_Click(object sender, EventArgs e)
        {
            Thread reportThread = new Thread(DataGridToExcel);
            reportThread.Start();
        }

        // Kullanıcı Listesinin Excel formatında raporlanmasını sağlar.
        private void DataGridToExcel()
        {
            if (dataGridView1.DataSource != null)
            {
                DataTable usersData = (DataTable)dataGridView1.DataSource;
                Report userReport = new Report(usersData);
                ShowMessage(userReport.ReportGridviewToExcel());
            }
            else
            {
                MessageBox.Show("Rapor alınacak grid boş olmamalı");
            }
        }

        //Close All Forms And Exit From Application. - TR: Home Form kapandığında tüm formlar da kapansın.
        private void FormCloseBtn_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                Application.OpenForms[i].Close();
        }

        private void GithubBtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"https://github.com/Cankirism");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenForm(sender);
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                Application.OpenForms[i].Close();
        }

        //Minimize Form
        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
