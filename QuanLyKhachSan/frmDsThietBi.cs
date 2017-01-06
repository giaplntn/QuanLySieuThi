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
    public partial class frmDsThietBi : Form
    {
        private Boolean bThem = false;
        private Boolean bSua = false;
        private string strLuuMaTB = null;
        public frmDsThietBi()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            bThem = true;
            txtMaTB.Text = "";
            txtTenTB.Text = "";
            txtDonVT.Text = "";
            txtGhiChu.Text = "";
            txtMaTB.Enabled = true;
            txtTenTB.Enabled = true;
            txtDonVT.Enabled = true;
            txtGhiChu.Enabled = true;
            lstvDs.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnKhongLuu.Enabled = true;
            txtMaTB.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HienThietBi()
        {
            try
            {
                string strCmdThietBi = "Select * From Thiet_Bi";
                SqlDataAdapter daThietBi = new SqlDataAdapter(strCmdThietBi, clsDungChung.con);
                DataSet dsThietBi = new DataSet();
                daThietBi.Fill(dsThietBi, "Thiet_Bi");
                DataTable tbThietBi = dsThietBi.Tables["Thiet_Bi"];
                lstvDs.Items.Clear();
                foreach (DataRow r1 in tbThietBi.Rows)
                {
                    ListViewItem item = new ListViewItem(r1["MaTB"].ToString().Trim());
                    item.SubItems.Add(r1["TenTB"].ToString().Trim());
                    item.SubItems.Add(r1["DonVT"].ToString().Trim());
                    item.SubItems.Add(r1["GhiChu"].ToString().Trim());
                    lstvDs.Items.Add(item);

                }
                dsThietBi.Dispose();
                daThietBi.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaTB.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Mã Thiết Bị Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaTB.Focus();
                return;
            }

            try
            {
                if (MessageBox.Show("Bạn Có Chắc Chắn Muốn Xoá Thiết Bị Này Không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string strThietBiXoa = "Select * From Thiet_Bi Where MaTB='" + txtMaTB.Text.Trim() + "'";
                    SqlDataAdapter daThietBiXoa = new SqlDataAdapter(strThietBiXoa, clsDungChung.con);
                    DataSet dsThietBiXoa = new DataSet();
                    daThietBiXoa.Fill(dsThietBiXoa, "Thiet_Bi");
                    DataTable tbThietBiXoa = dsThietBiXoa.Tables["Thiet_Bi"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daThietBiXoa);
                    daThietBiXoa.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowThietBiXoa = tbThietBiXoa.Rows[0];
                    rowThietBiXoa.Delete();
                    daThietBiXoa.Update(dsThietBiXoa, "Thiet_Bi");

                    tbThietBiXoa.Dispose();
                    dsThietBiXoa.Dispose();
                    daThietBiXoa.Dispose();
                }

                btnKhongLuu_Click(sender, e);
                HienThietBi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaTB.Text != "")
            {
                bSua = true;
                strLuuMaTB = txtMaTB.Text.Trim();
                txtMaTB.Enabled = true;
                txtTenTB.Enabled = true;
                txtDonVT.Enabled = true;
                txtGhiChu.Enabled = true; 
                lstvDs.Enabled = false;
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
                btnLuu.Enabled = true;
                btnKhongLuu.Enabled = true;
                txtMaTB.Focus();
            }
            else
            {
                MessageBox.Show("Giá Trị Mã Thiết Bị Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lstvDs.Focus();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (bThem)
                {
                    if (txtMaTB.Text.Equals(""))
                    {
                        MessageBox.Show("Giá Trị Mã Thiết Bị Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaTB.Focus();
                        return;
                    }

                    if (txtTenTB.Text.Equals(""))
                    {
                        MessageBox.Show("Giá Trị Tên Thiết Bị Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTenTB.Focus();
                        return;
                    }

                    //Kiem tra xem Ma so loai phong toan tai hay chua
                    string strThietBi = "Select * From Thiet_Bi Where MaTB='" + txtMaTB.Text.Trim() + "'";
                    SqlDataAdapter daThietBi = new SqlDataAdapter(strThietBi, clsDungChung.con);
                    DataSet dsThietBi = new DataSet();
                    daThietBi.Fill(dsThietBi, "Thiet_Bi");
                    DataTable tbThietBi = dsThietBi.Tables["Thiet_Bi"];
                    if (tbThietBi.DefaultView.Count > 0)
                    {
                        MessageBox.Show("Giá Trị Mã Số Thiết Bị " + txtMaTB.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaTB.Focus();
                        return;
                    }
                    tbThietBi.Dispose();
                    dsThietBi.Dispose();
                    daThietBi.Dispose();

                    string strThietBiThem = "Select * From Thiet_Bi";
                    SqlDataAdapter daThietBiThem = new SqlDataAdapter(strThietBiThem, clsDungChung.con);
                    DataSet dsThietBiThem = new DataSet();
                    daThietBiThem.Fill(dsThietBiThem, "Thiet_Bi");
                    DataTable tbThietBiThem = dsThietBiThem.Tables["Thiet_Bi"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daThietBiThem);
                    daThietBiThem.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rwThietBiThem = tbThietBiThem.NewRow();
                    rwThietBiThem["MaTB"] = txtMaTB.Text.Trim();
                    rwThietBiThem["TenTB"] = txtTenTB.Text.Trim();
                    rwThietBiThem["DonVT"] = txtDonVT.Text.Trim();
                    rwThietBiThem["GhiChu"] = txtGhiChu.Text.Trim();
                    tbThietBiThem.Rows.Add(rwThietBiThem);
                    daThietBiThem.Update(dsThietBiThem, "Thiet_Bi");

                    tbThietBiThem.Dispose();
                    dsThietBiThem.Dispose();
                    daThietBiThem.Dispose();
                }

                if (bSua)
                {
                    if (txtMaTB.Text.Trim() != strLuuMaTB)
                    {
                        string strThietBi = "Select * From Thiet_Bi Where MaTB='" + txtMaTB.Text.Trim() + "'";
                        SqlDataAdapter daThietBi = new SqlDataAdapter(strThietBi, clsDungChung.con);
                        DataSet dsThietBi = new DataSet();
                        daThietBi.Fill(dsThietBi, "Thiet_Bi");
                        DataTable tbThietBi = dsThietBi.Tables["Thiet_Bi"];
                        if (tbThietBi.DefaultView.Count > 0)
                        {
                            MessageBox.Show("Giá Trị Mã Số Thiết Bị " + txtMaTB.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMaTB.Focus();
                            return;
                        }
                        tbThietBi.Dispose();
                        dsThietBi.Dispose();
                        daThietBi.Dispose();
                    }

                    string strThietBiSua = "Select * From Thiet_Bi Where MaTB='" + strLuuMaTB + "'";
                    SqlDataAdapter daThietBiSua = new SqlDataAdapter(strThietBiSua, clsDungChung.con);
                    DataSet dsThietBiSua = new DataSet();
                    daThietBiSua.Fill(dsThietBiSua, "Thiet_Bi");
                    DataTable tbThietBiSua = dsThietBiSua.Tables["Thiet_Bi"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daThietBiSua);
                    daThietBiSua.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowThietBiSua = tbThietBiSua.Rows[0];
                    rowThietBiSua.BeginEdit();
                    rowThietBiSua["MaTB"] = txtMaTB.Text.Trim();
                    rowThietBiSua["TenTB"] = txtTenTB.Text.Trim();
                    rowThietBiSua["DonVT"] = txtDonVT.Text.Trim();
                    rowThietBiSua["GhiChu"] = txtGhiChu.Text.Trim();
                    rowThietBiSua.EndEdit();
                    daThietBiSua.Update(dsThietBiSua, "Thiet_Bi");

                    tbThietBiSua.Dispose();
                    dsThietBiSua.Dispose();
                    dsThietBiSua.Dispose();
                }

                btnKhongLuu_Click(sender, e);
                HienThietBi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void btnKhongLuu_Click(object sender, EventArgs e)
        {
            bThem = false;
            bSua = false;
            txtMaTB.Text = "";
            txtTenTB.Text = "";
            txtDonVT.Text = "";
            txtGhiChu.Text = "";
            txtMaTB.Enabled = false;
            txtTenTB.Enabled = false;
            txtDonVT.Enabled = false;
            txtGhiChu.Enabled = false;
            lstvDs.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnKhongLuu.Enabled = false;
        }

        private void lstvDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item = lstvDs.FocusedItem;
            txtMaTB.Text = item.Text;
            txtTenTB.Text = item.SubItems[1].Text;
            txtDonVT.Text = item.SubItems[2].Text;
            txtGhiChu.Text = item.SubItems[3].Text;
        }

        private void frmThietBi_Load(object sender, EventArgs e)
        {
            HienThietBi();
        }
    }
}
