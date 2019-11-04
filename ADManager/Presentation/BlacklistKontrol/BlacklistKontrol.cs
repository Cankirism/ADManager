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
    public partial class BlacklistKontrol : MetroFramework.Forms.MetroForm
    {

       
        private  List<AbuseProperties> _blacklist;
        public BlacklistKontrol()
        {
            InitializeComponent();
          
           
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            metroGrid1.DataSource = null;
            metroGrid1.Columns.Clear();

            AbuseVeriAl();

            if (metroGrid1.Rows.Count == 0)
            {
                metroLabel3.Text = $"{metroTextBox2.Text} ip'si son {metroTextBox1.Text} gün içerisinde hiç raporlanmamış";
            }

            else
            {
                SetLabels();
            }
        }

        private void AbuseVeriAl()
        {
            ExceptionCacther(() =>
            {
               _blacklist = AbuseService.AbuseApiConnect(metroTextBox2.Text, Convert.ToInt32(metroTextBox1.Text));
              
                metroGrid1.DataSource = _blacklist;
                metroGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }); 
        }
        private void SetLabels()
        {
            metroLabel3.Text = $"{metroTextBox2.Text} ip'si son {metroTextBox1.Text} gün içerisinde {metroGrid1.Rows.Count}  kez raporlandı.";
            metroLabel6.Text = metroGrid1.Rows[0].Cells[4].Value.ToString();
            metroLabel8.Text = metroGrid1.Rows[0].Cells[7].Value.ToString();
            materialRaisedButton2.Visible = true;
        }

        private void ExceptionCacther(Action action)
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

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            ListtoDtConverter dtConverter = new ListtoDtConverter();

            Report rapor = new Report(dtConverter.ConverToDataTable(_blacklist));
            MessageBox.Show(rapor.ReportGridviewToExcel());
          
        }
    }
}
