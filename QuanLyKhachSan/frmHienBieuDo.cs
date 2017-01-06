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
    public partial class frmHienBieuDo : Form
    {
        public frmHienBieuDo()
        {
            InitializeComponent();
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
            frmBaoCaoHieuSuatPhong fHieuSuat = new frmBaoCaoHieuSuatPhong();
            fHieuSuat.ShowDialog();
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
