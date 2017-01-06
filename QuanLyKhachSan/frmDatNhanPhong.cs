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
    public partial class frmDatNhanPhong : Form
    {
        public frmDatNhanPhong()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled =true; 
        }

        private void txtCMNDPassport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true; 
        }

        private void numdownNguoiLon_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cboGioiTinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; 
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {

            if (dpickNgayDi.Value <= dpickNgayDen.Value)
            {
                MessageBox.Show("Giá Trị Ngày Đi Không Thể Nhỏ Hơn Ngày Đến, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dpickNgayDi.Focus();
                return;
            }

            if (numdownNguoiLon.Value == 0)
            {
                MessageBox.Show("Giá Trị Ô Người Lớn Phải Lớn Hơn 1, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numdownNguoiLon.Focus();
                return;
            }

            if (txtHoTen.Text.Equals("") && cboDoiTac.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Đối Tác Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDoiTac.Focus();
                return;
            }

            try
            {
                clsCacHam h = new clsCacHam();
                txtMaKH.Text = h.BoKhoangTrang(DateTime.Now.ToString());
                string sqlKhachHangThem = "Select * From Khach_Hang";
                SqlDataAdapter daKhachHangThem = new SqlDataAdapter(sqlKhachHangThem, clsDungChung.con);
                DataSet dsKhachHangThem = new DataSet();
                daKhachHangThem.Fill(dsKhachHangThem, "Khach_Hang");
                DataTable tbKhachHangThem = dsKhachHangThem.Tables["Khach_Hang"];
                SqlCommandBuilder cmdBuildKH = new SqlCommandBuilder(daKhachHangThem);
                daKhachHangThem.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rwKhachHangThem = tbKhachHangThem.NewRow();
                rwKhachHangThem["MaKH"] = txtMaKH.Text;
                rwKhachHangThem["HoTen"] = txtHoTen.Text.Trim();
                rwKhachHangThem["GioiTinh"] = cboGioiTinh.Text.Trim();
                rwKhachHangThem["NgaySinh"] = dpickNgaySinh.Value;
                rwKhachHangThem["NoiSinh"] = txtNoiSinh.Text.Trim();
                rwKhachHangThem["DiaChi"] = txtDiaChi.Text.Trim();
                rwKhachHangThem["DienThoai"] = txtDienThoai.Text.Trim();
                rwKhachHangThem["Mail"] = txtMail.Text.Trim();
                rwKhachHangThem["CMND_PP"] = txtCMNDPassport.Text.Trim();
                rwKhachHangThem["NoiCap"] = txtNoiCap.Text.Trim();
                rwKhachHangThem["QuocTich"] = cboQuocTich.Text.Trim();
                rwKhachHangThem["PassPort"] = txtPassPort.Text.Trim();
                rwKhachHangThem["NoiCapPass"] = txtNoiCapPassPort.Text.Trim();
                rwKhachHangThem["YeuCau"] = txtYeuCau.Text.Trim();
                tbKhachHangThem.Rows.Add(rwKhachHangThem);
                daKhachHangThem.Update(dsKhachHangThem, "Khach_Hang");

                tbKhachHangThem.Dispose();
                dsKhachHangThem.Dispose();
                daKhachHangThem.Dispose();

                if (txtDuaTruoc.Text.Equals(""))
                {
                    txtDuaTruoc.Text = "0";
                }

                string strDangKyThem = "Select * From Dang_Ky";
                SqlDataAdapter daDangKyThem = new SqlDataAdapter(strDangKyThem, clsDungChung.con);
                DataSet dsDangKyThem = new DataSet();
                daDangKyThem.Fill(dsDangKyThem, "Dang_Ky");
                DataTable tbDangKyThem = dsDangKyThem.Tables["Dang_Ky"];
                SqlCommandBuilder cmdBuildDK = new SqlCommandBuilder(daDangKyThem);
                daDangKyThem.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rwDangKyThem = tbDangKyThem.NewRow();
                rwDangKyThem["MaDK"] = txtMaDK.Text.Trim();
                rwDangKyThem["NgayDK"] = DateTime.Now.ToShortDateString();
                rwDangKyThem["MaKH"] = txtMaKH.Text;
                rwDangKyThem["NgayDen"] = dpickNgayDen.Value;
                rwDangKyThem["GioDen"] = dpickGioDen.Value;
                rwDangKyThem["NgayDi"] = dpickNgayDi.Value;
                rwDangKyThem["NguoiLon"] = numdownNguoiLon.Value;
                rwDangKyThem["TreEm"] = numdownTreEm.Value;
                rwDangKyThem["DoiTac"] = cboDoiTac.Text;
                rwDangKyThem["SoPhong"] = txtSoPhong.Text.Trim();
                rwDangKyThem["DuaTruoc"] = txtDuaTruoc.Text.Trim();
                rwDangKyThem["TrangThai"] = 1;
                rwDangKyThem["PhiDoiPhong"] =0;
                tbDangKyThem.Rows.Add(rwDangKyThem);
                daDangKyThem.Update(dsDangKyThem, "Dang_Ky");

                tbDangKyThem.Dispose();
                dsDangKyThem.Dispose();
                daDangKyThem.Dispose();

                string sqlSoPhong = "Select * From So_Phong Where SoPhong='" + txtSoPhong.Text.Trim() + "'";
                SqlDataAdapter daSoPhong = new SqlDataAdapter(sqlSoPhong, clsDungChung.con);
                DataSet dsSoPhong = new DataSet();
                daSoPhong.Fill(dsSoPhong, "So_Phong");
                DataTable tbSoPhong = dsSoPhong.Tables["So_Phong"];
                SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daSoPhong);
                daSoPhong.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rowSoPhongSua = tbSoPhong.Rows[0];
                rowSoPhongSua.BeginEdit();
                rowSoPhongSua["TinhTrang"] = 1;
                rowSoPhongSua.EndEdit();
                daSoPhong.Update(dsSoPhong, "So_Phong");

                tbSoPhong.Dispose();
                dsSoPhong.Dispose();
                daSoPhong.Dispose();

                btnDatPhong.Enabled = false;
                btnNhanPhong.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void DsDoiTac()
        {
            try
            {
                string strCmdDoiTac = "Select * From Doi_Tac";
                SqlDataAdapter daDoiTac = new SqlDataAdapter(strCmdDoiTac, clsDungChung.con);
                DataSet dsDoiTac = new DataSet();
                daDoiTac.Fill(dsDoiTac, "Doi_Tac");
                DataTable tbDoiTac = dsDoiTac.Tables["Doi_Tac"];
                cboDoiTac.Items.Clear();
                foreach (DataRow r1 in tbDoiTac.Rows)
                {
                    cboDoiTac.Items.Add(r1["TenDoiTac"].ToString());
                }

                tbDoiTac.Dispose();
                dsDoiTac.Dispose();
                daDoiTac.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void frmDatNhanPhong_Load(object sender, EventArgs e)
        {
            txtMaDK.Text = DateTime.Now.ToString();
            txtSoPhong.Text = clsDungChung.strLuuSoPhong;
            
            DsDoiTac();
        }

        private void txtDuaTruoc_TextChanged(object sender, EventArgs e)
        {
            if (txtDuaTruoc.Text.Length > 0)
            {
                clsCacHam h = new clsCacHam();
                txtDuaTruoc.Text = h.chendau(h.loaidau(txtDuaTruoc.Text));
                txtDuaTruoc.SelectionStart = txtDuaTruoc.Text.Length + 1; 
            }

        }

        private void btnNhanPhong_Click(object sender, EventArgs e)
        {
            if (txtHoTen.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Họ Tên Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return; 
            }

            if (cboQuocTich.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Quốc Tịch Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboQuocTich.Focus();
                return;
            }

            if (txtDiaChi.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Địa Chỉ Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }

            if (txtCMNDPassport.Text.Equals("") && txtPassPort.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị CMND Hoặc Passport Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCMNDPassport.Focus();
                return;
            }

            try
            {
                string sqlKhachSua = "Select * From Khach_Hang Where MaKH='" + txtMaKH.Text.Trim() + "'";
                SqlDataAdapter daKhachSua = new SqlDataAdapter(sqlKhachSua, clsDungChung.con);
                DataSet dsKhachSua = new DataSet();
                daKhachSua.Fill(dsKhachSua, "Khach_Hang");
                DataTable tbKhachSua = dsKhachSua.Tables["Khach_Hang"];
                SqlCommandBuilder cmdBuild2 = new SqlCommandBuilder(daKhachSua);
                daKhachSua.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rowKhachSua = tbKhachSua.Rows[0];
                rowKhachSua.BeginEdit();
                rowKhachSua["HoTen"] = txtHoTen.Text;
                rowKhachSua["GioiTinh"] = cboGioiTinh.Text;
                rowKhachSua["QuocTich"] = cboQuocTich.Text;
                rowKhachSua["NgaySinh"] = dpickNgaySinh.Value;
                rowKhachSua["NoiSinh"] = txtNoiSinh.Text;
                rowKhachSua["DiaChi"] = txtDiaChi.Text;
                rowKhachSua["DienThoai"] = txtDienThoai.Text;
                rowKhachSua["Mail"] = txtMail.Text;
                rowKhachSua["CMND_PP"] = txtCMNDPassport.Text;
                rowKhachSua["NoiCap"] = txtNoiCap.Text;
                rowKhachSua["PassPort"] = txtPassPort.Text;
                rowKhachSua["NoiCapPass"] = txtNoiCapPassPort.Text;
                rowKhachSua["YeuCau"] = txtYeuCau.Text;
                rowKhachSua.EndEdit();
                daKhachSua.Update(dsKhachSua, "Khach_Hang");

                tbKhachSua.Dispose();
                dsKhachSua.Dispose();
                daKhachSua.Dispose();

                string sqlDKSua = "Select * From Dang_Ky Where MaDK='" + txtMaDK.Text.Trim() + "'";
                SqlDataAdapter daDKSua = new SqlDataAdapter(sqlDKSua, clsDungChung.con);
                DataSet dsDKSua = new DataSet();
                daDKSua.Fill(dsDKSua, "Dang_Ky");
                DataTable tbDKSua = dsDKSua.Tables["Dang_Ky"];
                SqlCommandBuilder cmdBuild3 = new SqlCommandBuilder(daDKSua);
                daDKSua.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rowDKSua = tbDKSua.Rows[0];
                rowDKSua.BeginEdit();
                rowDKSua["NgayDen"] = dpickNgayDen.Value;
                rowDKSua["GioDen"] = dpickGioDen.Value;
                rowDKSua["NgayDi"] = dpickNgayDi.Value;
                rowDKSua["NguoiLon"] = numdownNguoiLon.Value;
                rowDKSua["TreEm"] = numdownTreEm.Value;
                rowDKSua["DoiTac"] = cboDoiTac.Text;
                rowDKSua["DuaTruoc"] = txtDuaTruoc.Text;
                rowDKSua.EndEdit();
                daDKSua.Update(dsDKSua, "Dang_Ky");

                tbDKSua.Dispose();
                dsDKSua.Dispose();
                daDKSua.Dispose();

                string sqlSoPhong = "Select * From So_Phong Where SoPhong='" + txtSoPhong.Text.Trim() + "'";
                SqlDataAdapter daSoPhong = new SqlDataAdapter(sqlSoPhong, clsDungChung.con);
                DataSet dsSoPhong = new DataSet();
                daSoPhong.Fill(dsSoPhong, "So_Phong");
                DataTable tbSoPhong = dsSoPhong.Tables["So_Phong"];
                SqlCommandBuilder cmdBuild1 = new SqlCommandBuilder(daSoPhong);
                daSoPhong.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rowSoPhongSua = tbSoPhong.Rows[0];
                rowSoPhongSua.BeginEdit();
                rowSoPhongSua["TinhTrang"] = 2;
                rowSoPhongSua.EndEdit();
                daSoPhong.Update(dsSoPhong, "So_Phong");

                tbSoPhong.Dispose();
                dsSoPhong.Dispose();
                daSoPhong.Dispose();

                string sqlDK = "Select * From Dang_Ky Where SoPhong='" + txtSoPhong.Text.Trim() + "' And TrangThai=1";
                SqlDataAdapter daDK = new SqlDataAdapter(sqlDK, clsDungChung.con);
                DataSet dsDK = new DataSet();
                daDK.Fill(dsDK, "Dang_Ky");
                DataTable tbDK = dsDK.Tables["Dang_Ky"];
                SqlCommandBuilder cmdBuild6 = new SqlCommandBuilder(daDK);
                daDK.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rowDKSua1 = tbDK.Rows[0];
                rowDKSua1.BeginEdit();
                rowDKSua1["TrangThai"] = 2;
                rowDKSua1.EndEdit();
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

        private void txtPassPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true; 
        }

        private void txtDuaTruoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true; 
        }
    }
}
