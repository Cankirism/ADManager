using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADManager
{
    public partial class AdminListesi : Form
    {
        private BusinessUser bluser;
        private string _adminGrupName;
        public AdminListesi()
        {
           
            InitializeComponent();
        }

        private void GetAdminsBtn_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            GetAdmins();
        }

        /// <summary>
        ///  Seçilen admin grubuna üye kullanıcı listesini gridview'e aktarır.
        /// </summary>
        private void GetAdmins()
        {
            ExceptionCatcher(() =>
            {
                int _selectedIndex = comboBox1.SelectedIndex;

                if (_selectedIndex != -1)
                {
                    bluser = new BusinessUser();
                    _adminGrupName = comboBox1.SelectedItem.ToString();
                    List<string> adminListesi = bluser.ListAdmins(_adminGrupName);

                    for (int i = 0; i < adminListesi.Count; i++)
                    {
                        dataGridView1.Rows.Add(i + 1, adminListesi[i], _adminGrupName);
                    }
                }
            }); 
        }

        private void ExceptionCatcher(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (DirectoryServicesCOMException ex)
            {
                MessageBox.Show(ex.Message);
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void SetDataGridSettings()
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void SetGridColums()
        {
            for (int a = 1; a <= 2; a++)

            {
                dataGridView1.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void AdminListesi_Load(object sender, EventArgs e)
        {
            SetDataGridSettings();
            SetGridColums();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
