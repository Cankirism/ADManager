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
    public partial class PasswordChange : Form
    {

        // Kullanıcı Parola değiştirme işlemini yapan UIform
        private readonly string samAccountName;
        private BusinessUser blUser;
        public PasswordChange(string samAccountName )
        {
            this.samAccountName = samAccountName;
            blUser = new BusinessUser();
            InitializeComponent();
        }

        private void DegistirBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var changePasswordDialog = MessageBox.Show(samAccountName + "Kullanıcısının parolasını değiştirmek istiyor musunuz ? ", "Dikkat !", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (changePasswordDialog == DialogResult.Yes)
                {
                    MessageBox.Show(blUser.ResetUserPassword(samAccountName, newPassTxt.Text));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }   
        }
    }
}
