
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.DirectoryServices;

namespace ADManager
{
    public partial class UserForm : Form
    {

        #region Parametreler
        private string _searchedName;
        private string _logonName;
        private string _logonPass;
        private string _deletedUserSamName;
        private DataTable _searchedUserDT;
        // Form move parameters.
        private int tagMove, MouseX, MouseY;
        private BusinessUser _blUser;
        private DataTable _allUserDT;
        #endregion

        public UserForm()
        {
            _logonName = Giris.UserName;
            _logonPass = Giris.UserPassword;
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void CreateUser_Load(object sender, EventArgs e)
        {
            GetOU();
            SetDataGridSettings();
            SetStartParameters();
        }

        // Domain sunucudan yapısal birim - OU listesini alır.
        private void GetOU()
        {
            OUClass ou = new OUClass();
            foreach (var yp in ou.GetAllOU())
            {
                OuCombo.Items.Add(yp);
            }

        }

        // Datagrid satır-sütun ayarlarını set eder.
        public void SetDataGridSettings()
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }
        private void SetStartParameters()
        {
            _logonName = Giris.UserName;
            _logonPass = Giris.UserPassword;
            _allUserDT = new DataTable();
            _blUser = new BusinessUser();
            _searchedUserDT = new DataTable();
        }

        // Kullanıcı Adı textBox'ı  (name.surname) otomatik olarak dolduran method.
        private void SurnameTxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(String.IsNullOrEmpty(NameTxt.Text)))

            {

                UserNameTxt.Text = EnglishChar.ConvertTRCharToENChar($"{NameTxt.Text}.{SurnameTxt.Text}".ToLower()); 
            }
                
        }
        /// <summary>
        /// DataGrid üzerinde Context Menu Ayarlama
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            //TR: Üzerinde bulunduğumuz satırın numarasını alıyoruz.
            int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;

            if (e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                // Satır Seçilmiş mi 
                if (currentMouseOverRow < 0)
                {
                    MessageBox.Show("Lütfen bir satır seçiniz");
                }

                // Menüleri ekleme
                cm.MenuItems.Add(new MenuItem("Kullanıcı Üyeliklerini Göster", GetUserGroup));
                cm.MenuItems.Add(new MenuItem("Kullanıcıyı Pasife Al", DisableUser));
                cm.MenuItems.Add(new MenuItem("Admin Yetkisi Ver", AdminYetkisiVer));
                cm.MenuItems.Add(new MenuItem("Kullanıcı Sil", datagridview1_kullaniciSil));
                cm.MenuItems.Add(new MenuItem("Parola Sıfırla", ParolaSifirla));
                cm.Show(dataGridView1, new Point(e.X, e.Y));
            }
        }
 
        // Seçilen Kullanıcının Üyelik Grup Listesinin getirir. 
        private void GetUserGroup(object sender, EventArgs e)
        {
            ExceptionCatcher(() =>
            {
                IEnumerable<string> userGroupList;
                var usersMemFrm = new UsersMemFrm();
                string samName = dataGridView1.SelectedCells[2].Value.ToString();
                userGroupList = _blUser.GetUserGroups(samName);
                usersMemFrm.PrintGroups(userGroupList);
                usersMemFrm.Show();
            });

        }

        private void DisableUser(object sender, EventArgs e)
        {
            ExceptionCatcher(() =>
            {
                string samAccountName = dataGridView1.SelectedCells[2].Value.ToString();
                string disableState = _blUser.DisableUser(samAccountName);
                MessageBox.Show(disableState);
            });

        }
        public void AdminYetkisiVer(object sender, EventArgs e)
        {
            ExceptionCatcher(() =>
            {
                string samName = dataGridView1.SelectedCells[2].Value.ToString();
                var adminYetkiForm = new AdminUye(samName);
                adminYetkiForm.Show();
            });
        }
        /// <summary>
        /// DataGrid üzerinde sağ tık menulerde 'Kullanici Sil' seçildiğinde çalışan method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datagridview1_kullaniciSil(object sender, EventArgs e)
        {
            try
            {
                _deletedUserSamName = dataGridView1.SelectedCells[2].Value.ToString(); // Gridview'de seçilen satırın samAccountName değerini seçer
                if (_deletedUserSamName != string.Empty)
                {
                    var deleteUserDialog = MessageBox.Show(_deletedUserSamName + " Kullanıcısını silmek istiyor musunuz", "Uyarı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (deleteUserDialog == DialogResult.Yes)
                    {
                        _blUser.DeleteUser(_deletedUserSamName);
                        MessageBox.Show("Kullanıcı Başarıyla Silindi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            catch (ArgumentOutOfRangeException)
            {
                ShowMessage("Lütfen tüm Satırı Seçiniz");
            }
        }

        private void ParolaSifirla(object sender, EventArgs e)
        {
            try

            {
                string samName = dataGridView1.SelectedCells[2].Value.ToString();
                PasswordChange pwChange = new PasswordChange(samName);
                pwChange.Show();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Lütfen tüm satırı seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void AraBtn_Click(object sender, EventArgs e)
        {

            SearchPersonel();
            LblUserCount.Visible = true;
            LblUserCount.Text = $"{dataGridView1.Rows.Count.ToString()} Kayıt Listelendi.";
            SetGridviewColumns();
        }

        // Kullanıcı arama
        private void SearchPersonel()
        {
            ExceptionCatcher(() =>
            {
                _searchedName =EnglishChar.ConvertTRCharToENChar(SearchTxt.Text.ToUpper());
                IEnumerable<UsersProperties> userList = _blUser.SearchUser(_searchedName);
                _searchedUserDT = _blUser.SetSearchedPersonDT(userList);
                dataGridView1.Columns.Clear();

                if (_searchedUserDT.Rows.Count == 0)
                {
                    MessageBox.Show("Kayıt Bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                dataGridView1.DataSource = _searchedUserDT;
                _allUserDT = new DataTable();
            });

        }

        private void GetAllBtn_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            bgWorker.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            GetAllUser();
        }

        // Tüm Kullanıcıları listeleyen method.
        private void GetAllUser()
        {
            ExceptionCatcher(() =>
            {
                _allUserDT = _blUser.GetAllAdUsers();
            });
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FillWithAllUsers();
            pictureBox1.Visible = false;
        }

        // Tüm kullanıcı listesi ile Gridview 'i doldurur.
        private void FillWithAllUsers()
        {

            ExceptionCatcher(() =>
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = _allUserDT;
                LblUserCount.Visible = true;
                LblUserCount.Text = $"{dataGridView1.Rows.Count.ToString()} Kayıt Listelendi";
                SetGridviewColumns();
            });

        }

        private void KaydetBtn_Click(object sender, EventArgs e)
        {
            if (NameTxt.Text == string.Empty || SurnameTxt.Text == string.Empty || PasswordTxt.Text == string.Empty)

                MessageBox.Show("Lütfen tüm alanları doldurunuz");

            else SaveUserData();
           
            
              
        }


        // Kullanıcı Kayıt 
        private void SaveUserData()
        {

            ExceptionCatcher(() =>
            {
                string userNameEnglishChar = EnglishChar.ConvertTRCharToENChar(UserNameTxt.Text);
                var userFormInputs = new UserFormInputs(NameTxt.Text, SurnameTxt.Text,userNameEnglishChar, PasswordTxt.Text, true,(OuCombo.SelectedIndex>=0)? OuCombo.SelectedItem.ToString():null);
                _blUser.CreateUserAccount(userFormInputs);
                MessageBox.Show($"{_blUser.ResponseMessage}", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });

        }
        private void ExcelBtn_Click(object sender, EventArgs e)
        {
            DataGridToExcel();
        }

        // Datagrid üzerindeki verileri excele aktarır.
        private void DataGridToExcel()
        {
            if (dataGridView1.DataSource != null)
            {
                DataTable userData = (DataTable)dataGridView1.DataSource;
                Report userReport = new Report(userData);
                MessageBox.Show(userReport.ReportGridviewToExcel());

            }
            else
            {
                MessageBox.Show("Aktarılacak kaynakta veri yok");
            }

        }

        private void ExceptionCatcher(Action action)
        {
            try
            {
                action.Invoke();
            }

            catch (PrincipalExistsException ex)
            {
                ShowMessage(ex.Message);

            }

            catch (PasswordException)
            {
                ShowMessage("Girilen Parola, Parola İlkelerini karşılamıyor");
            }

            catch (UnauthorizedAccessException)
            {
                ShowMessage("Bu İşlemi Yapma Yetkiniz Yok");
            }

            catch (PrincipalException ex)
            {
                ShowMessage(ex.Message);
            }

            catch (ArgumentOutOfRangeException ex)
            {
                ShowMessage(ex.Message);
            }

            catch (DirectoryServicesCOMException comExc)
            {
                ShowMessage(comExc.Message);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }

        }

        private void SetGridviewColumns()
        {
            for (int a = 1; a <= 7; a++)

            {
                dataGridView1.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

        }

        private void ShowMessage(string mesaj)
        {
            MessageBox.Show($"{mesaj}", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FormCloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Minimize Form on Panel Right Click. 
        // TR: Formun en üstünde sağ tık yaparak formu simge durumuna alıyoruz
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                cm.MenuItems.Add(new MenuItem("Simge durumuna al ", MinimizeForm));
                cm.Show(panel1, new Point(e.X, e.Y));
            }
        }

        private void MinimizeForm(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }
        private void ClearTxtBtn_Click_1(object sender, EventArgs e)
        {

            foreach (Control c in groupBox1.Controls)
            {
                if (c is TextBox)
                    c.Text = string.Empty;
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void adminListbtn_Click_1(object sender, EventArgs e)
        {
            AdminListesi adminListeForm = new AdminListesi();

            adminListeForm.Show();
        }

        #region FormMove
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            tagMove = 1;
            MouseX = e.X;
            MouseY = e.Y;
        }

        

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (tagMove == 1)
                this.SetDesktopLocation((MousePosition.X - MouseX), (MousePosition.Y - MouseY));
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            tagMove = 0;
        }

        #endregion
    }

    /// <summary>
    /// userForm üzerinde
    /// </summary>
    public class UserFormData
    {
        public string userName { get; set; }

        public string userPassword { get; set; }

        public string name { get; set; }

        public string surName { get; set; }

        public bool isUserAktif { get; set; }

    }
}
