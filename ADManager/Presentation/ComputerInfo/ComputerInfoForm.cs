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
    public partial class ComputerInfoForm : Form
    {
        private readonly string _ipAddress;

        public ComputerInfoForm(string _ipAddress)
        {
            this._ipAddress = _ipAddress;
            InitializeComponent();
        }

        private void GetirBtn_Click(object sender, EventArgs e)
        {
            CpuLbl.Text = string.Empty;
            ComputerInfo computerInfo = new ComputerInfo(_ipAddress);
            List<string> cpuList = computerInfo.GetCpuInfo();
            if (computerInfo._errState)
            {
                MessageBox.Show(computerInfo.InfoErr);
            }

            else
            {
                foreach (var cpu in cpuList)

                    SetCpuLabel(cpu);
            }
           
        }

        private void SetCpuLabel(string cpu)
        {
            CpuLbl.Text += cpu;

        }
    }
}
