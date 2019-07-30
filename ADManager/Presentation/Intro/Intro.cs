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
    public partial class Intro : Form
    {
    
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }
       
        public Intro()
        {
            InitializeComponent();
            this.ControlBox = false;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            rectangleShape2.Width += 1;
            if (rectangleShape2.Width == rectangleShape1.Width-5)
            {
                Giris giris = new Giris();
                giris.Show();
                this.Close();
            }
          
        }
    }
}
