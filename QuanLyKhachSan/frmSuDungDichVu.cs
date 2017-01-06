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
    public partial class frmSuDungDichVu : Form
    {
        public frmSuDungDichVu()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSuDungDichVu_Load(object sender, EventArgs e)
        {
            txtMaDK.Text = clsDungChung.strLuuMaDK;
            txtSoPhong.Text = clsDungChung.strLuuSoPhong;
            txtMaKH.Text = clsDungChung.strLuuMaKH;  
            dpickNgayDV.Value = DateTime.Now;
            HienDV();
        }

        private void HienDV()
        {
            try
            {
                string strCmdDichVu = "Select * From Dich_Vu";
                SqlDataAdapter daDichVu = new SqlDataAdapter(strCmdDichVu, clsDungChung.con);
                DataSet dsDichVu = new DataSet();
                daDichVu.Fill(dsDichVu, "Dich_Vu");
                DataTable tbDichVu = dsDichVu.Tables["Dich_Vu"];
                cboLoaiDichVu.Items.Clear();
                foreach (DataRow r1 in tbDichVu.Rows)
                {
                    cboLoaiDichVu.Items.Add(r1["MaDV"].ToString() + ". " + r1["TenDV"].ToString());
                }
                dsDichVu.Dispose();
                daDichVu.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void cboLoaiDichVu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboLoaiDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clsCacHam h = new clsCacHam();
                string strSqlDonVT = "Select * From Dich_Vu Where MaDV='" + cboLoaiDichVu.Text.Substring(0, cboLoaiDichVu.Text.IndexOf(".")) + "'";
                SqlDataAdapter daDonVT = new SqlDataAdapter(strSqlDonVT, clsDungChung.con);
                DataSet dsDonVT = new DataSet();
                daDonVT.Fill(dsDonVT, "Dich_Vu");
                DataTable tbDonVT = dsDonVT.Tables["Dich_Vu"];
                if (tbDonVT.DefaultView.Count > 0)
                {
                    DataRow r = tbDonVT.Rows[0];
                    txtDonVT.Text = r["DonVT"].ToString();
                    txtDonGia.Text = h.chendau(Convert.ToInt32(r["DonGia"]).ToString());
                    numSoLuong.Enabled = true;
                }
                tbDonVT.Dispose();
                dsDonVT.Dispose();
                daDonVT.Dispose();

                txtThanhTien.Text = Convert.ToString(Int32.Parse(h.loaidau(txtDonGia.Text)) * numSoLuong.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (cboLoaiDichVu.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Loại Dịch Vụ Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiDichVu.Focus();
                return;
            }

            if (numSoLuong.Value == 0)
            {
                MessageBox.Show("Giá Trị Số Lượng Không Thể Bằng 0, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSoLuong.Focus();
                return;
            }

            try
            {
                clsCacHam h = new clsCacHam();
                string sqlSuDungDV = "Select * From SuDung_DichVu";
                SqlDataAdapter daSuDungDV = new SqlDataAdapter(sqlSuDungDV, clsDungChung.con);
                DataSet dsSuDungDV = new DataSet();
                daSuDungDV.Fill(dsSuDungDV, "SuDung_DichVu");
                DataTable tbSuDungDV = dsSuDungDV.Tables["SuDung_DichVu"];
                SqlCommandBuilder cmdBuildDV = new SqlCommandBuilder(daSuDungDV);
                daSuDungDV.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rwSuDungDV = tbSuDungDV.NewRow();
                rwSuDungDV["NgaySD"] = dpickNgayDV.Value;
                rwSuDungDV["MaDK"] = txtMaDK.Text.Trim();
                rwSuDungDV["MaKH"] = txtMaKH.Text.Trim();
                rwSuDungDV["TenDV"] = cboLoaiDichVu.Text.Substring(cboLoaiDichVu.Text.IndexOf(".") + 1, cboLoaiDichVu.Text.Length - (cboLoaiDichVu.Text.IndexOf(".")+1)); //cboLoaiDichVu.Text.IndexOf("."), cboLoaiDichVu.Text.Length - cboLoaiDichVu.Text.IndexOf("."));
                rwSuDungDV["DonVT"] = txtDonVT.Text.Trim();
                rwSuDungDV["SoLuong"] = numSoLuong.Value;
                rwSuDungDV["DonGia"] = h.loaidau(txtDonGia.Text.Trim());
                rwSuDungDV["ThanhTien"] = h.loaidau(txtThanhTien.Text.Trim());
                tbSuDungDV.Rows.Add(rwSuDungDV);
                daSuDungDV.Update(dsSuDungDV, "SuDung_DichVu");

                tbSuDungDV.Dispose();
                dsSuDungDV.Dispose();
                daSuDungDV.Dispose();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            if (txtDonGia.Text.Length > 0)
            {
                clsCacHam h = new clsCacHam();
                txtDonGia.Text = h.chendau(h.loaidau(txtDonGia.Text));
            }
        }

        private void txtThanhTien_TextChanged(object sender, EventArgs e)
        {
            if (txtThanhTien.Text.Length > 0)
            {
                clsCacHam h = new clsCacHam();
                txtThanhTien.Text = h.chendau(h.loaidau(txtThanhTien.Text));
            }
        }

        private void numSoLuong_ValueChanged(object sender, EventArgs e)
        {
            clsCacHam h=new clsCacHam();
            txtThanhTien.Text=Convert.ToString(Int32.Parse(h.loaidau(txtDonGia.Text)) * numSoLuong.Value);
        }

    }
}
