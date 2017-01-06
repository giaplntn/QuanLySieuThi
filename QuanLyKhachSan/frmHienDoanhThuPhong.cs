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
    public partial class frmHienDoanhThuPhong : Form
    {
        public frmHienDoanhThuPhong()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radThang_CheckedChanged(object sender, EventArgs e)
        {
            dPickDenNgay.Value = dPichTuNgay.Value.AddMonths(1);   
        }

        private void radNgay_CheckedChanged(object sender, EventArgs e)
        {
            dPickDenNgay.Value = DateTime.Now;   
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            clsDungChung.datTuNgay = dPichTuNgay.Value;
            clsDungChung.datDenNgay = dPickDenNgay.Value;
            this.Hide();
            frmBaoCaoDoanhThuPhong fBaoCaoDoanhThuPhong = new frmBaoCaoDoanhThuPhong();
            fBaoCaoDoanhThuPhong.ShowDialog();
            this.Close();
        }

        
    }
}
