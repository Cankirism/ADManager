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
    public partial class ou : Form
    {
        public ou()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OUClass ou = new OUClass();
            foreach (var a in ou.GetAllOU())
            {
                listBox1.Items.Add(a);
            }

        }
    }
}
