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
    public partial class frmDoiPhong : Form
    {
        public frmDoiPhong()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HienDsPhong()
        {
            try
            {
                string sqlSoPhong = "Select * From So_Phong Where TinhTrang=0";
                SqlDataAdapter daSoPhong = new SqlDataAdapter(sqlSoPhong, clsDungChung.con);
                DataSet dsSoPhong = new DataSet();
                daSoPhong.Fill(dsSoPhong, "So_Phong");
                DataTable tbSoPhong = dsSoPhong.Tables["So_Phong"];
                cboSoPhongChuyen.Items.Clear();
                foreach (DataRow r in tbSoPhong.Rows)
                {
                    cboSoPhongChuyen.Items.Add(r["SoPhong"].ToString());
                }
                tbSoPhong.Dispose();
                dsSoPhong.Dispose();
                daSoPhong.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void frmDoiPhong_Load(object sender, EventArgs e)
        {
            txtMaDK.Text = clsDungChung.strLuuMaDK;
            txtSoPhong.Text = clsDungChung.strLuuSoPhong;
            HienDsPhong();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (cboSoPhongChuyen.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Số Phòng Sẽ Chuyển Tới Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboSoPhongChuyen.Focus(); 
                return; 
            }
            try
            {
                string sqlDKSua = "Select * From Dang_Ky Where MaDK='" + txtMaDK.Text.Trim() + "'";
                SqlDataAdapter daDKSua = new SqlDataAdapter(sqlDKSua, clsDungChung.con);
                DataSet dsDKSua = new DataSet();
                daDKSua.Fill(dsDKSua, "Dang_Ky");
                DataTable tbDKSua = dsDKSua.Tables["Dang_Ky"];
                SqlCommandBuilder cmdBuild3 = new SqlCommandBuilder(daDKSua);
                daDKSua.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rowDKSua = tbDKSua.Rows[0];
                rowDKSua.BeginEdit();
                rowDKSua["SoPhong"] = cboSoPhongChuyen.Text.Trim();
                rowDKSua["NgayDen"] = DateTime.Now.ToShortDateString();

                TimeSpan s;
                DateTime a = DateTime.Now;
                DateTime b = clsDungChung.strLuuNgayDen;
                s = a - b;
                int intLuuNgay = Convert.ToInt32(s.TotalDays);

                if (intLuuNgay == 0)
                    intLuuNgay = intLuuNgay + 1;

                string SqlPhong = "Select * From So_Phong Where SoPhong='" + txtSoPhong.Text.Trim() + "'";
                SqlDataAdapter daPhong = new SqlDataAdapter(SqlPhong, clsDungChung.con);
                DataSet dsPhong = new DataSet();
                daPhong.Fill(dsPhong, "So_Phong");
                DataTable tbPhong = dsPhong.Tables["So_Phong"];
                DataRow rowKH = tbPhong.Rows[0];
                string strLuuMaLoaiPhong = rowKH["MaLoai"].ToString();
                tbPhong.Dispose();
                dsPhong.Dispose();
                daPhong.Dispose();

                string SqlLoaiPhong = "Select * From Loai_Phong Where MaLoai='" + strLuuMaLoaiPhong.Trim() + "'";
                SqlDataAdapter daLoaiPhong = new SqlDataAdapter(SqlLoaiPhong, clsDungChung.con);
                DataSet dsLoaiPhong = new DataSet();
                daLoaiPhong.Fill(dsLoaiPhong, "Loai_Phong");
                DataTable tbLoaiPhong = dsLoaiPhong.Tables["Loai_Phong"];
                DataRow rowLP = tbLoaiPhong.Rows[0];
                int intDonGiaPhong = Convert.ToInt32(rowLP["DonGia"]);
                tbLoaiPhong.Dispose();
                dsLoaiPhong.Dispose();
                daLoaiPhong.Dispose();

                int intTienDoiPhong = intLuuNgay * intDonGiaPhong;

                rowDKSua["PhiDoiPhong"] = intTienDoiPhong;
                rowDKSua.EndEdit();
                daDKSua.Update(dsDKSua, "Dang_Ky");

                tbDKSua.Dispose();
                dsDKSua.Dispose();
                daDKSua.Dispose();

                string sqlSoPhong1 = "Select * From So_Phong Where SoPhong='" + txtSoPhong.Text.Trim() + "'";
                SqlDataAdapter daSoPhong1 = new SqlDataAdapter(sqlSoPhong1, clsDungChung.con);
                DataSet dsSoPhong1 = new DataSet();
                daSoPhong1.Fill(dsSoPhong1, "So_Phong");
                DataTable tbSoPhong1 = dsSoPhong1.Tables["So_Phong"];
                SqlCommandBuilder cmdBuild1 = new SqlCommandBuilder(daSoPhong1);
                daSoPhong1.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rowSoPhong1Sua = tbSoPhong1.Rows[0];
                rowSoPhong1Sua.BeginEdit();
                rowSoPhong1Sua["TinhTrang"] = 0;
                rowSoPhong1Sua.EndEdit();
                daSoPhong1.Update(dsSoPhong1, "So_Phong");

                tbSoPhong1.Dispose();
                dsSoPhong1.Dispose();
                daSoPhong1.Dispose();

                string sqlSoPhong2 = "Select * From So_Phong Where SoPhong='" + cboSoPhongChuyen.Text.Trim() + "'";
                SqlDataAdapter daSoPhong2 = new SqlDataAdapter(sqlSoPhong2, clsDungChung.con);
                DataSet dsSoPhong2 = new DataSet();
                daSoPhong2.Fill(dsSoPhong2, "So_Phong");
                DataTable tbSoPhong2 = dsSoPhong2.Tables["So_Phong"];
                SqlCommandBuilder cmdBuild2 = new SqlCommandBuilder(daSoPhong2);
                daSoPhong2.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rowSoPhong2Sua = tbSoPhong2.Rows[0];
                rowSoPhong2Sua.BeginEdit();
                rowSoPhong2Sua["TinhTrang"] = 2;
                rowSoPhong2Sua.EndEdit();
                daSoPhong2.Update(dsSoPhong2, "So_Phong");

                tbSoPhong2.Dispose();
                dsSoPhong2.Dispose();
                daSoPhong2.Dispose();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void cboSoPhongChuyen_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; 
        }
    }
}
