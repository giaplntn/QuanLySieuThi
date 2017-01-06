using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Diagnostics; 
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    partial class frmTacGia : Form
    {
        public frmTacGia()
        {
            InitializeComponent();
            this.lblVersion.Text = "Version: " + Application.ProductVersion; 
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:toanthang9000@yahoo.com?subject=Hello");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.MicroWin83.com");
        }

        private void frmTacGia_Load(object sender, EventArgs e)
        {

        }
    }
}
