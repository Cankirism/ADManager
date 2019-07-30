using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADManager
{
    public partial class ComputerForm : Form
    {
        private BusinessComputer _computerBl;
        private DataTable _computerList;
        private DataTable _searchedUserList;
        private string _computerName;
        // Form move parameters
        private int tagMove, MouseX, MouseY;
        public ComputerForm()
        {  
            InitializeComponent();
            _computerList = new DataTable();
            Control.CheckForIllegalCrossThreadCalls = false;
            _computerBl = new BusinessComputer();
        }
        private void ComputerForm_Load(object sender, EventArgs e)
        {
            SetDataGridSettings();
            System.Windows.Forms.ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(this.SearchTxt, "Cihaz İp ya da Cihaz  adını giriniz");
        }
        private void SetDataGridSettings()
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
        private void HepsiniGetirBtn_Click(object sender, EventArgs e)
        {
            label2.Text = "Kayıtlar getiriliyor. Lütfen bekleyiniz ...";
            pictureBox1.Visible = true;
            backgroundWorker1.RunWorkerAsync();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            GetAllComputers();
        }

        // Veritabanından tüm bilgisayar bilgilerini getirir
        private void GetAllComputers()
        {

            ExceptionCatcher(() => {
                _computerList = _computerBl.FillGridView("HepsiniGetirBtn", SearchTxt.Text);
            });

        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FillGridForAll();
        }

        // AD veritabanından çekilen tüm pc bilgileri ile grid'i doldurur.
        private void FillGridForAll()
        {
            ExceptionCatcher(() => {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = _computerList;

                for (int a = 1; a <= 5; a++)
                {
                    dataGridView1.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                label2.Text = $"{dataGridView1.Rows.Count} adet kayıt listelendi";
                pictureBox1.Visible = false;
            });
        }

        private void AraBtn_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(SearchTxt.Text)))
            {
                backgroundWorker2.RunWorkerAsync();
                pictureBox1.Visible = true;
                label2.Text = "Kayıtlar getiriliyor, lütfen bekleyiniz..."; 
            }
            else
            {
                MessageBox.Show("Lütfen ip ya da cihaz ismi giriniz", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            FillGridViewForSearched();
        }

        //Aranılan cihaz bilgisini alıp, gridview'de gösterir.
        private void FillGridViewForSearched()
        {
            ExceptionCatcher(() =>
            {
                _searchedUserList = new DataTable();
                _searchedUserList = _computerBl.FillGridView("AraBtn", SearchTxt.Text);

            });
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGridView1.Columns.Clear();
            pictureBox1.Visible = false;
            dataGridView1.DataSource = _searchedUserList;
            for (int a = 1; a <= 5; a++)
            {
                dataGridView1.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            label2.Text = $"{dataGridView1.Rows.Count} adet kayıt listelendi.";
        }

        // Hata yakalama metodumuz 
        private void ExceptionCatcher(Action action)
        {
            try
            {
                action.Invoke();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FormCloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // TR: Formu hareket ettirmek için kullanılan methodlar.
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            tagMove = 1;
            MouseX  = e.X;
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

        private void ExcelBtn_Click(object sender, EventArgs e)
        {
            DataGridToExcel();
        }

        //Raporlama
        public void DataGridToExcel()
        {
            if (dataGridView1.DataSource != null)
            {
                DataTable computerDataTable = (DataTable)dataGridView1.DataSource;
                Report report = new Report(computerDataTable);
               MessageBox.Show(report.ReportGridviewToExcel());

            }

            else

                MessageBox.Show("Lütfen Liste Oluşturunuz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int currentMouseRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                if (currentMouseRow < 0)

                {
                    MessageBox.Show("Lütfen bir satır seçiniz ");
                }

                cm.MenuItems.Add("Yeniden Başlat",RebootComputer);
                cm.MenuItems.Add("Kapat", ShutDownComputer);
                cm.MenuItems.Add("Bilgisayar Bilgileri", GetComputerInfo);
                cm.Show(dataGridView1,new Point(e.X,e.Y));
                }
        }

        private void RebootComputer(object sender, EventArgs e)
        {
            try
            {
                _computerName = dataGridView1.SelectedCells[1].Value.ToString();
                _computerBl = new BusinessComputer();
                _computerBl.RebootComputer(_computerName);
            }

            catch (ArgumentOutOfRangeException )
            {
                MessageBox.Show("Lütfen tüm satırı seçiniz","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }  
        }

        private void ShutDownComputer(object sender, EventArgs e)
        {
            try
            {
                _computerName = dataGridView1.SelectedCells[1].Value.ToString();
                _computerBl = new BusinessComputer(); 
                _computerBl.ShutDownComputer(_computerName);
            }

            catch (ArgumentOutOfRangeException )
            {
                MessageBox.Show("Lütfen tüm satırı seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void GetComputerInfo(object sender, EventArgs e)
        {
            try
            {
                string ipAddress = dataGridView1.SelectedCells[1].Value.ToString();
                ComputerInfoForm compForm = new ComputerInfoForm(ipAddress);
                compForm.Show();
            }
            catch (ArgumentOutOfRangeException )
            {
                MessageBox.Show("Lütfen tüm satırı seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }   
        }
       
        private void SearchTxt_MouseDown(object sender, MouseEventArgs e)
        {
            SearchTxt.Clear();
        }
    }
}
