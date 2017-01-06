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
    public partial class frmHienDoanhThuDV : Form
    {
        public frmHienDoanhThuDV()
        {
            InitializeComponent();
        }

        private void frmHienDoanhThuDV_Load(object sender, EventArgs e)
        {

        }

        private void radNgay_CheckedChanged(object sender, EventArgs e)
        {
            dPickDenNgay.Value = DateTime.Now;   
        }

        private void radThang_CheckedChanged(object sender, EventArgs e)
        {
            dPickDenNgay.Value = dPichTuNgay.Value.AddMonths(1);   
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            clsDungChung.datTuNgay = dPichTuNgay.Value;
            clsDungChung.datDenNgay = dPickDenNgay.Value;
            this.Hide();
            frmDoanhThuDichVu fBaoCaoDV = new frmDoanhThuDichVu();
            fBaoCaoDV.ShowDialog();
            this.Close();
        }
    }
}
