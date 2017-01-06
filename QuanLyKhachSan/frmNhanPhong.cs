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
    public partial class frmNhanPhong : Form
    {
        public frmNhanPhong()
        {
            InitializeComponent();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            try
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
                SqlCommandBuilder cmdBuild4 = new SqlCommandBuilder(daDK);
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
                MessageBox.Show("Lỗi : " + ex.Message, "Thông Báo");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNhanPhong_Load(object sender, EventArgs e)
        {
            try
            {
                txtMaDK.Text = clsDungChung.strLuuMaDK;
                txtMaKH.Text = clsDungChung.strLuuMaKH;
                string strSqlDK = "Select * From Dang_Ky Where MaDK='" + txtMaDK.Text.Trim() + "'";
                SqlDataAdapter daDK = new SqlDataAdapter(strSqlDK, clsDungChung.con);
                DataSet dsDK = new DataSet();
                daDK.Fill(dsDK, "Dang_Ky");
                DataTable tbDK = dsDK.Tables["Dang_Ky"];
                if (tbDK.DefaultView.Count > 0)
                {
                    DataRow r = tbDK.Rows[0];
                    dpickNgayDen.Value = Convert.ToDateTime(r["NgayDen"]);
                    dpickGioDen.Value = Convert.ToDateTime(r["GioDen"]);
                    dpickNgayDi.Value = Convert.ToDateTime(r["NgayDi"]);
                    numdownNguoiLon.Value = Convert.ToInt32(r["NguoiLon"]);
                    numdownTreEm.Value = Convert.ToInt32(r["TreEm"]);
                    txtSoPhong.Text = r["SoPhong"].ToString();
                    cboDoiTac.Text = r["DoiTac"].ToString();
                    txtDuaTruoc.Text = Convert.ToInt32(r["DuaTruoc"]).ToString();
                }

                tbDK.Dispose();
                dsDK.Dispose();
                daDK.Dispose();

                string sqlKhachHang = "Select * From Khach_Hang Where MaKH='" + txtMaKH.Text.Trim() + "'";
                SqlDataAdapter daKhachHang = new SqlDataAdapter(sqlKhachHang, clsDungChung.con);
                DataSet dsKhachHang = new DataSet();
                daKhachHang.Fill(dsKhachHang, "Khach_Hang");
                DataTable tbKhachHang = dsKhachHang.Tables["Khach_Hang"];
                if (tbKhachHang.DefaultView.Count > 0)
                {
                    DataRow r1 = tbKhachHang.Rows[0];
                    txtHoTen.Text = r1["HoTen"].ToString();
                    cboGioiTinh.Text = r1["GioiTinh"].ToString();
                    cboQuocTich.Text = r1["QuocTich"].ToString();
                    dpickNgaySinh.Value = Convert.ToDateTime(r1["NgaySinh"]);
                    txtNoiSinh.Text = r1["NoiSinh"].ToString();
                    txtDiaChi.Text = r1["DiaChi"].ToString();
                    txtDienThoai.Text = r1["DienThoai"].ToString();
                    txtMail.Text = r1["Mail"].ToString();
                    txtCMNDPassport.Text = r1["CMND_PP"].ToString();
                    txtNoiCap.Text = r1["NoiCap"].ToString();
                    txtPassPort.Text = r1["PassPort"].ToString();
                    txtNoiCapPassPort.Text = r1["NoiCapPass"].ToString();
                    txtYeuCau.Text = r1["YeuCau"].ToString();
                }
                tbKhachHang.Dispose();
                dsKhachHang.Dispose();
                daKhachHang.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Thông Báo");
            }
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true; 
        }

        private void txtCMNDPassport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true; 
        }

        private void txtDuaTruoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true; 
        }

        private void cboGioiTinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; 
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

        private void txtPassPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true; 
        }

    }
}
