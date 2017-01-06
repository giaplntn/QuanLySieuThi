using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class frmHuyDangKy : Form
    {
        public frmHuyDangKy()
        {
            InitializeComponent();
        }

        private void frmHuyDangKy_Load(object sender, EventArgs e)
        {
            txtMaDK.Text = clsDungChung.strLuuMaDK;
            txtMaKH.Text = clsDungChung.strLuuMaKH;
            txtNgayHuy.Text = DateTime.Now.ToShortDateString();
            txtGioHuy.Text = DateTime.Now.ToShortTimeString();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            try
            {
                string strHuy_DK = "Select * From Huy_DK";
                SqlDataAdapter daHuy_DK = new SqlDataAdapter(strHuy_DK, clsDungChung.con);
                DataSet dsHuy_DK = new DataSet();
                daHuy_DK.Fill(dsHuy_DK, "Huy_DK");
                DataTable tbHuy_DK = dsHuy_DK.Tables["Huy_DK"];
                SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daHuy_DK);
                daHuy_DK.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rwHuy_DK = tbHuy_DK.NewRow();
                rwHuy_DK["MaDK"] = txtMaDK.Text.Trim();
                rwHuy_DK["MaKH"] = txtMaKH.Text.Trim();
                rwHuy_DK["NgayHuy"] = txtNgayHuy.Text.Trim();
                rwHuy_DK["GioHuy"] = txtGioHuy.Text.Trim();
                rwHuy_DK["LyDo"] = txtLyDo.Text.Trim();
                tbHuy_DK.Rows.Add(rwHuy_DK);
                daHuy_DK.Update(dsHuy_DK, "Huy_DK");

                tbHuy_DK.Dispose();
                dsHuy_DK.Dispose();
                daHuy_DK.Dispose();


                string sqlSoPhong = "Select * From So_Phong Where SoPhong='" + clsDungChung.strLuuSoPhong + "'";
                SqlDataAdapter daSoPhong = new SqlDataAdapter(sqlSoPhong, clsDungChung.con);
                DataSet dsSoPhong = new DataSet();
                daSoPhong.Fill(dsSoPhong, "So_Phong");
                DataTable tbSoPhong = dsSoPhong.Tables["So_Phong"];
                SqlCommandBuilder cmdBuild1 = new SqlCommandBuilder(daSoPhong);
                daSoPhong.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rowSoPhongSua = tbSoPhong.Rows[0];
                rowSoPhongSua.BeginEdit();
                rowSoPhongSua["TinhTrang"] = 0;
                rowSoPhongSua.EndEdit();
                daSoPhong.Update(dsSoPhong, "So_Phong");

                tbSoPhong.Dispose();
                dsSoPhong.Dispose();
                daSoPhong.Dispose();

                string sqlDK = "Select * From Dang_Ky Where SoPhong='" + clsDungChung.strLuuSoPhong + "' And TrangThai=1";
                SqlDataAdapter daDK = new SqlDataAdapter(sqlDK, clsDungChung.con);
                DataSet dsDK = new DataSet();
                daDK.Fill(dsDK, "Dang_Ky");
                DataTable tbDK = dsDK.Tables["Dang_Ky"];
                SqlCommandBuilder cmdBuild2 = new SqlCommandBuilder(daDK);
                daDK.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rowDKSua = tbDK.Rows[0];
                rowDKSua.BeginEdit();
                rowDKSua["TrangThai"] = 3;
                rowDKSua.EndEdit();
                daDK.Update(dsDK, "Dang_Ky");

                tbDK.Dispose();
                dsDK.Dispose();
                daDK.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
