using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class frmHienKhachHuy : Form
    {
        public frmHienKhachHuy()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radNgay_CheckedChanged(object sender, EventArgs e)
        {
            dPickDenNgay.Value = DateTime.Now;
        }

        private void radThang_CheckedChanged(object sender, EventArgs e)
        {
            dPickDenNgay.Value = dPichTuNgay.Value.AddMonths(1); 
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            clsDungChung.datTuNgay = dPichTuNgay.Value;
            clsDungChung.datDenNgay = dPickDenNgay.Value;
            this.Hide();
            frmBaoCaoKhachHuy fBaoCaoHuy = new frmBaoCaoKhachHuy();
            fBaoCaoHuy.ShowDialog();
            this.Close();
        }
    }
}
