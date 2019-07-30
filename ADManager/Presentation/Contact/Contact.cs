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
    public partial class Contact : Form
    {
        public Contact()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"https://goo.gl/maps/r4s8apUzuyexKGRj7");
        }
    }
}
