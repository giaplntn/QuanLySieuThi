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
    public partial class frmDsDichVu : Form
    {
        private Boolean bThem = false;
        private Boolean bSua = false;
        private string strLuuMaDV = null;

        public frmDsDichVu()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            bThem = true;
            txtMaDV.Text = "";
            txtTenDV.Text = "";
            txtDonVT.Text = "";
            txtDonGia.Text = "0";
            txtGhiChu.Text = "";
            txtMaDV.Enabled = true;
            txtTenDV.Enabled = true;
            txtDonVT.Enabled = true;
            txtDonGia.Enabled = true;
            txtGhiChu.Enabled = true;
            lstvDs.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnKhongLuu.Enabled = true;
            txtMaDV.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaDV.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Mã Dịch Vụ Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDV.Focus();
                return;
            }

            try
            {
                if (MessageBox.Show("Bạn Có Chắc Chắn Muốn Xoá Dịch Vụ Này Không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string strDichVuXoa = "Select * From Dich_Vu Where MaDV='" + txtMaDV.Text.Trim() + "'";
                    SqlDataAdapter daDichVuXoa = new SqlDataAdapter(strDichVuXoa, clsDungChung.con);
                    DataSet dsDichVuXoa = new DataSet();
                    daDichVuXoa.Fill(dsDichVuXoa, "Dich_Vu");
                    DataTable tbDichVuXoa = dsDichVuXoa.Tables["Dich_Vu"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daDichVuXoa);
                    daDichVuXoa.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowDichVuXoa = tbDichVuXoa.Rows[0];
                    rowDichVuXoa.Delete();
                    daDichVuXoa.Update(dsDichVuXoa, "Dich_Vu");

                    tbDichVuXoa.Dispose();
                    dsDichVuXoa.Dispose();
                    daDichVuXoa.Dispose();
                }

                btnKhongLuu_Click(sender, e);
                HienDichVu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void HienDichVu()
        {
            try
            {
                clsCacHam h = new clsCacHam();
                string strCmdDichVu = "Select * From Dich_Vu";
                SqlDataAdapter daDichVu = new SqlDataAdapter(strCmdDichVu, clsDungChung.con);
                DataSet dsDichVu = new DataSet();
                daDichVu.Fill(dsDichVu, "Dich_Vu");
                DataTable tbDichVu = dsDichVu.Tables["Dich_Vu"];
                lstvDs.Items.Clear();
                foreach (DataRow r1 in tbDichVu.Rows)
                {
                    ListViewItem item = new ListViewItem(r1["MaDV"].ToString().Trim());
                    item.SubItems.Add(r1["TenDV"].ToString().Trim());
                    item.SubItems.Add(r1["DonVT"].ToString().Trim());
                    item.SubItems.Add(h.chendau((Convert.ToInt32(r1["DonGia"])).ToString().Trim()));
                    item.SubItems.Add(r1["GhiChu"].ToString().Trim());
                    lstvDs.Items.Add(item);

                }
                dsDichVu.Dispose();
                daDichVu.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaDV.Text != "")
            {
                bSua = true;
                strLuuMaDV = txtMaDV.Text.Trim();
                txtMaDV.Enabled = true;
                txtTenDV.Enabled = true;
                txtDonVT.Enabled = true;
                txtDonGia.Enabled = true; 
                txtGhiChu.Enabled = true;
                lstvDs.Enabled = false;
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
                btnLuu.Enabled = true;
                btnKhongLuu.Enabled = true;
                txtMaDV.Focus();
            }
            else
            {
                MessageBox.Show("Giá Trị Mã Dịch Vụ Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lstvDs.Focus();
            }
        }

        private void btnKhongLuu_Click(object sender, EventArgs e)
        {
            bThem = false;
            bSua = false;
            txtMaDV.Text = "";
            txtTenDV.Text = "";
            txtDonVT.Text = "";
            txtDonGia.Text = "0";
            txtGhiChu.Text = "";
            txtMaDV.Enabled = false;
            txtTenDV.Enabled = false;
            txtDonVT.Enabled = false;
            txtDonGia.Enabled = false;
            txtGhiChu.Enabled = false;
            lstvDs.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnKhongLuu.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                clsCacHam h = new clsCacHam();
                if (bThem)
                {
                    if (txtMaDV.Text.Equals(""))
                    {
                        MessageBox.Show("Giá Trị Mã Dịch Vụ Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaDV.Focus();
                        return;
                    }

                    if (txtTenDV.Text.Equals(""))
                    {
                        MessageBox.Show("Giá Trị Tên Dịch Vụ Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTenDV.Focus();
                        return;
                    }

                    if (txtDonGia.Text.Equals(""))
                    {
                        txtDonGia.Text = "0";
                    }

                    //Kiem tra xem Ma so loai phong toan tai hay chua
                    string strDichVu = "Select * From Dich_Vu Where MaDV='" + txtMaDV.Text.Trim() + "'";
                    SqlDataAdapter daDichVu = new SqlDataAdapter(strDichVu, clsDungChung.con);
                    DataSet dsDichVu = new DataSet();
                    daDichVu.Fill(dsDichVu, "Dich_Vu");
                    DataTable tbDichVu = dsDichVu.Tables["Dich_Vu"];
                    if (tbDichVu.DefaultView.Count > 0)
                    {
                        MessageBox.Show("Giá Trị Mã Số Dịch Vụ " + txtMaDV.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaDV.Focus();
                        return;
                    }
                    tbDichVu.Dispose();
                    dsDichVu.Dispose();
                    daDichVu.Dispose();

                    string strDichVuThem = "Select * From Dich_Vu";
                    SqlDataAdapter daDichVuThem = new SqlDataAdapter(strDichVuThem, clsDungChung.con);
                    DataSet dsDichVuThem = new DataSet();
                    daDichVuThem.Fill(dsDichVuThem, "Dich_Vu");
                    DataTable tbDichVuThem = dsDichVuThem.Tables["Dich_Vu"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daDichVuThem);
                    daDichVuThem.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rwDichVuThem = tbDichVuThem.NewRow();
                    rwDichVuThem["MaDV"] = txtMaDV.Text.Trim();
                    rwDichVuThem["TenDV"] = txtTenDV.Text.Trim();
                    rwDichVuThem["DonVT"] = txtDonVT.Text.Trim();
                    rwDichVuThem["DonGia"] = h.loaidau(txtDonGia.Text.Trim());
                    rwDichVuThem["GhiChu"] = txtGhiChu.Text.Trim();
                    tbDichVuThem.Rows.Add(rwDichVuThem);
                    daDichVuThem.Update(dsDichVuThem, "Dich_Vu");

                    tbDichVuThem.Dispose();
                    dsDichVuThem.Dispose();
                    daDichVuThem.Dispose();
                }

                if (bSua)
                {
                    if (txtMaDV.Text.Trim() != strLuuMaDV)
                    {
                        string strDichVu = "Select * From Dich_Vu Where MaDV='" + txtMaDV.Text.Trim() + "'";
                        SqlDataAdapter daDichVu = new SqlDataAdapter(strDichVu, clsDungChung.con);
                        DataSet dsDichVu = new DataSet();
                        daDichVu.Fill(dsDichVu, "Dich_Vu");
                        DataTable tbDichVu = dsDichVu.Tables["Dich_Vu"];
                        if (tbDichVu.DefaultView.Count > 0)
                        {
                            MessageBox.Show("Giá Trị Mã Số Dịch Vụ " + txtMaDV.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMaDV.Focus();
                            return;
                        }
                        tbDichVu.Dispose();
                        dsDichVu.Dispose();
                        daDichVu.Dispose();
                    }

                    string strDichVuSua = "Select * From Dich_Vu Where MaDV='" + strLuuMaDV + "'";
                    SqlDataAdapter daDichVuSua = new SqlDataAdapter(strDichVuSua, clsDungChung.con);
                    DataSet dsDichVuSua = new DataSet();
                    daDichVuSua.Fill(dsDichVuSua, "Dich_Vu");
                    DataTable tbDichVuSua = dsDichVuSua.Tables["Dich_Vu"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daDichVuSua);
                    daDichVuSua.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowDichVuSua = tbDichVuSua.Rows[0];
                    rowDichVuSua.BeginEdit();
                    rowDichVuSua["MaDV"] = txtMaDV.Text.Trim();
                    rowDichVuSua["TenDV"] = txtTenDV.Text.Trim();
                    rowDichVuSua["DonVT"] = txtDonVT.Text.Trim();
                    rowDichVuSua["DonGia"] = h.loaidau(txtDonGia.Text.Trim());
                    rowDichVuSua["GhiChu"] = txtGhiChu.Text.Trim();
                    rowDichVuSua.EndEdit();
                    daDichVuSua.Update(dsDichVuSua, "Dich_Vu");

                    tbDichVuSua.Dispose();
                    dsDichVuSua.Dispose();
                    dsDichVuSua.Dispose();
                }

                btnKhongLuu_Click(sender, e);
                HienDichVu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void lstvDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item = lstvDs.FocusedItem;
            txtMaDV.Text = item.Text;
            txtTenDV.Text = item.SubItems[1].Text;
            txtDonVT.Text = item.SubItems[2].Text;
            txtDonGia.Text = item.SubItems[3].Text;
            txtGhiChu.Text = item.SubItems[4].Text;
        }

        private void frmDsDichVu_Load(object sender, EventArgs e)
        {
            HienDichVu();
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            if (txtDonGia.Text.Length > 0)
            {
                clsCacHam h = new clsCacHam();
                txtDonGia.Text = h.chendau(h.loaidau(txtDonGia.Text));
                txtDonGia.SelectionStart = txtDonGia.Text.Length + 1;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
