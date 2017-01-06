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
    public partial class frmDoanhThuDichVu : Form
    {
        public frmDoanhThuDichVu()
        {
            InitializeComponent();
        }

        private void frmDoanhThuDichVu_Load(object sender, EventArgs e)
        {
            try
            {
                crtDoanhThuDichVu aa = new crtDoanhThuDichVu();
                aa.Load(Application.StartupPath + "\\crtHieuSuatPhong.rpt");
                aa.DataDefinition.RecordSelectionFormula = "{Hoa_Don.NgayTT} in Date(" + clsDungChung.datTuNgay.Year + "," + clsDungChung.datTuNgay.Month + "," + clsDungChung.datTuNgay.Day + ") to Date(" + clsDungChung.datDenNgay.Year + "," + clsDungChung.datDenNgay.Month + "," + clsDungChung.datDenNgay.Day + ")"; 
                crystalReportViewer1.ReportSource = aa;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
