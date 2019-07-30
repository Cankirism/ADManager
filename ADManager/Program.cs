using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            #if DEBUG 
            using (var myAppContext = new MyApplicationContext(new Giris()))
            {
                try
                {
                    Application.Run(myAppContext);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } 
             
            }
           #else

           using (var myAppContext = new MyApplicationContext(new Intro()))
            {
                try
                {
                    Application.Run(myAppContext);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } 
             

            }

         #endif
        }
    }
}
