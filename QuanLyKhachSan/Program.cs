using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyKhachSan
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
            //frmSplash fSplash = new frmSplash();
           // fSplash.ShowDialog(); 
            Application.Run(new frmDangNhap());
           //Application.Run(new frmMain());
        }
    }
}
 