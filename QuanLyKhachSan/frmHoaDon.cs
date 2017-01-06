using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;  
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class frmHoaDon : Form
    {
        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                ParameterDiscreteValue ParaDisValue;
                ParameterValues ParaValue;

                ReportDocument crtHoaDon = new ReportDocument();
                crtHoaDon.Load(Application.StartupPath + "\\crtHoaDon.rpt");
                ParameterFieldDefinition ParaFildDef;
                ParaFildDef = crtHoaDon.DataDefinition.ParameterFields["MaKH"];
                ParaValue = new ParameterValues();
                ParaDisValue = new ParameterDiscreteValue();
                ParaDisValue.Value = clsDungChung.strLuuMaKH;
                ParaValue.Add(ParaDisValue);
                ParaFildDef.ApplyCurrentValues(ParaValue);
                crytViewHienDs.ReportSource = crtHoaDon;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void crytViewHienDs_Load(object sender, EventArgs e)
        {

        }
    }
}
