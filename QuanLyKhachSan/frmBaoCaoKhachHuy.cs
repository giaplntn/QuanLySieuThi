﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class frmBaoCaoKhachHuy : Form
    {
        public frmBaoCaoKhachHuy()
        {
            InitializeComponent();
        }

        private void frmBaoCaoKhachHuy_Load(object sender, EventArgs e)
        {
            try
            {
                ReportDocument aa = new ReportDocument();
                aa.Load(Application.StartupPath + "\\crtKhachHuyDangKy.rpt");
                aa.DataDefinition.RecordSelectionFormula = "{Huy_DK.NgayHuy} in Date(" + clsDungChung.datTuNgay.Year + "," + clsDungChung.datTuNgay.Month + "," + clsDungChung.datTuNgay.Day + ") to Date(" + clsDungChung.datDenNgay.Year + "," + clsDungChung.datDenNgay.Month + "," + clsDungChung.datDenNgay.Day + ")"; 
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
