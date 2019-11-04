using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices;
using System.Net;


namespace ADManager
{
    public partial class Giris : Form
    {
       private  int TogMove, Mvalx, Mvaly; // UI formunun hareket parametreleri.
     
        public static string  UserName { get; set; }
        public static string UserPassword { get; set; }
        public Giris()
        {
         
            InitializeComponent();  
        }
        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Giris_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        // Mouse hareket ettiğinde.
        private void Giris_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)

                this.SetDesktopLocation(MousePosition.X - Mvalx, MousePosition.Y - Mvaly);
        }

        /// <summary>
        ///  "Giriş" butonuna tıklandığında.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GirisBtn_Click(object sender, EventArgs e)
        {
            if (TxtUser.Text.Length !=0 && TxtPass.Text.Length != 0)
            {
                AuthOnServer();    
            }

            else
            {
                ShowErrorMessage("Tüm alanları doldurunuz");
            }
        }

        // Domain Sunucuda kimlik doğrulama
        private void AuthOnServer()
        {

            ExceptionCatcher(() =>
           {
               Authentication auth = new Authentication(TxtUser.Text, TxtPass.Text);
               bool isAuth = auth.IsAuthenticated();

               if (isAuth)
               {
                   UserName = TxtUser.Text;
                   UserPassword = TxtPass.Text;
                   Home homeForm = new Home();
                   homeForm.Show();
                   this.Close();
               }

           }
             );
           
        }

        // Hatalarımızı yakaladığımız meth
        private void ExceptionCatcher(Action action)
        {

            try
            {
                action.Invoke();
            }

           
            catch (DirectoryServicesCOMException ex)
            {
                ShowErrorMessage(ex.Message);
            }

            catch (System.Runtime.InteropServices.COMException)
            {
                ShowErrorMessage("Sunucuya bağlantı kurulamadı. Ağ bağlantısını kontrol ediniz");
            }
            catch (Exception ex)
            {

                ShowErrorMessage(ex.ToString());
            }
        }
        private void ShowErrorMessage(string mesaj)
        {
            MessageBox.Show($"{mesaj}", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void Giris_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1;
            Mvalx = e.X;
            Mvaly = e.Y;
        }
    }
}

