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
    public partial class AdminUye : Form
    {
        // Kullanıcıya Admin Yetkisi Verdiğimiz UI
        private readonly string samAccountName;
        public AdminUye(string samAccountName)
        {
            this.samAccountName = samAccountName;
            InitializeComponent();
        }

        private void TekSecBtn_Click(object sender, EventArgs e)
        {
            TekAktar();
        }
        public void TekAktar()
        {
            listBox3.Items.Add(listBox2.SelectedItem.ToString());
        }

        public void HepsiniAktar()
        {
            foreach (var group in listBox2.Items)

                listBox3.Items.Add(group);
        }
        private void HepsiSecBtn_Click(object sender, EventArgs e)
        {
            HepsiniAktar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
        }
        private void KydtBtn_Click(object sender, EventArgs e)
        {
            AddUserToAdminGroup();
        }

        // Kullanıcıyı seçilen Admin Grubuna atar.
        public void AddUserToAdminGroup()
        {

            if (listBox3.Items.Count != 0)
            {
                AdminExceptionCatcher(() =>
                {
                    string groupName = listBox3.Items[0].ToString();
                    BusinessUser blUser = new BusinessUser();
                    MessageBox.Show(blUser.AddUserToAdminGroup(samAccountName, groupName));
                });

            }

            else
                MessageBox.Show("Grup Seçilmedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void AdminExceptionCatcher(Action action)
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
    }
}
