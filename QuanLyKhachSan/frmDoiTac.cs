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
    public partial class frmDoiTac : Form
    {
        private Boolean bThem = false;
        private Boolean bSua = false;
        private string strLuuMaSo = null;

        public frmDoiTac()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            bThem = true;
            txtMaSo.Text = "";
            txtTenDoiTac.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            txtGhiChu.Text = "";
            txtMaSo.Enabled = true;
            txtTenDoiTac.Enabled = true;
            txtDiaChi.Enabled = true;
            txtDienThoai.Enabled = true;
            txtGhiChu.Enabled = true;
            lstvDs.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnKhongLuu.Enabled = true;
            txtMaSo.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaSo.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Mã Đối Tác Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSo.Focus();
                return;
            }

            try
            {

                if (MessageBox.Show("Bạn Có Chắc Chắn Muốn Xoá Đối Tác Này Không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string strDoiTacXoa = "Select * From Doi_Tac Where MaSo='" + txtMaSo.Text.Trim() + "'";
                    SqlDataAdapter daDoiTacXoa = new SqlDataAdapter(strDoiTacXoa, clsDungChung.con);
                    DataSet dsDoiTacXoa = new DataSet();
                    daDoiTacXoa.Fill(dsDoiTacXoa, "Doi_Tac");
                    DataTable tbDoiTacXoa = dsDoiTacXoa.Tables["Doi_Tac"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daDoiTacXoa);
                    daDoiTacXoa.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowDoiTacXoa = tbDoiTacXoa.Rows[0];
                    rowDoiTacXoa.Delete();
                    daDoiTacXoa.Update(dsDoiTacXoa, "Doi_Tac");

                    tbDoiTacXoa.Dispose();
                    dsDoiTacXoa.Dispose();
                    daDoiTacXoa.Dispose();
                }

                btnKhongLuu_Click(sender, e);
                HienDoiTac();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void HienDoiTac()
        {
            try
            {
                string strCmdDoiTac = "Select * From Doi_Tac";
                SqlDataAdapter daDoiTac = new SqlDataAdapter(strCmdDoiTac, clsDungChung.con);
                DataSet dsDoiTac = new DataSet();
                daDoiTac.Fill(dsDoiTac, "Doi_Tac");
                DataTable tbDoiTac = dsDoiTac.Tables["Doi_Tac"];
                lstvDs.Items.Clear();
                foreach (DataRow r1 in tbDoiTac.Rows)
                {
                    ListViewItem item = new ListViewItem(r1["MaSo"].ToString().Trim());
                    item.SubItems.Add(r1["TenDoiTac"].ToString().Trim());
                    item.SubItems.Add(r1["DiaChi"].ToString().Trim());
                    item.SubItems.Add(r1["DienThoai"].ToString().Trim());
                    item.SubItems.Add(r1["GhiChu"].ToString().Trim());
                    lstvDs.Items.Add(item);

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

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaSo.Text != "")
            {
                bSua = true;
                strLuuMaSo = txtMaSo.Text.Trim();
                txtMaSo.Enabled = true;
                txtTenDoiTac.Enabled = true;
                txtDiaChi.Enabled = true;
                txtDienThoai.Enabled = true;
                txtGhiChu.Enabled = true;
                lstvDs.Enabled = false;
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
                btnLuu.Enabled = true;
                btnKhongLuu.Enabled = true;
                txtMaSo.Focus();
            }
            else
            {
                MessageBox.Show("Giá Trị Mã Đối Tác Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lstvDs.Focus();
            }
        }

        private void btnKhongLuu_Click(object sender, EventArgs e)
        {
            bThem = false;
            bSua = false;
            txtMaSo.Text = "";
            txtTenDoiTac.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "0";
            txtGhiChu.Text = "";
            txtMaSo.Enabled = false;
            txtTenDoiTac.Enabled = false;
            txtDienThoai.Enabled = false;
            txtDiaChi.Enabled = false;
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
                if (bThem)
                {
                    if (txtMaSo.Text.Equals(""))
                    {
                        MessageBox.Show("Giá Trị Mã Đối Tác Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaSo.Focus();
                        return;
                    }

                    if (txtTenDoiTac.Text.Equals(""))
                    {
                        MessageBox.Show("Giá Trị Tên Đối Tác Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTenDoiTac.Focus();
                        return;
                    }

                    //Kiem tra xem Ma so loai phong toan tai hay chua
                    string strDoiTac = "Select * From Doi_Tac Where MaSo='" + txtMaSo.Text.Trim() + "'";
                    SqlDataAdapter daDoiTac = new SqlDataAdapter(strDoiTac, clsDungChung.con);
                    DataSet dsDoiTac = new DataSet();
                    daDoiTac.Fill(dsDoiTac, "Doi_Tac");
                    DataTable tbDoiTac = dsDoiTac.Tables["Doi_Tac"];
                    if (tbDoiTac.DefaultView.Count > 0)
                    {
                        MessageBox.Show("Giá Trị Mã Số Đối Tác " + txtMaSo.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaSo.Focus();
                        return;
                    }
                    tbDoiTac.Dispose();
                    dsDoiTac.Dispose();
                    daDoiTac.Dispose();

                    string strDoiTacThem = "Select * From Doi_Tac";
                    SqlDataAdapter daDoiTacThem = new SqlDataAdapter(strDoiTacThem, clsDungChung.con);
                    DataSet dsDoiTacThem = new DataSet();
                    daDoiTacThem.Fill(dsDoiTacThem, "Doi_Tac");
                    DataTable tbDoiTacThem = dsDoiTacThem.Tables["Doi_Tac"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daDoiTacThem);
                    daDoiTacThem.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rwDoiTacThem = tbDoiTacThem.NewRow();
                    rwDoiTacThem["MaSo"] = txtMaSo.Text.Trim();
                    rwDoiTacThem["TenDoiTac"] = txtTenDoiTac.Text.Trim();
                    rwDoiTacThem["DiaChi"] = txtDiaChi.Text.Trim();
                    rwDoiTacThem["DienThoai"] = txtDienThoai.Text.Trim();
                    rwDoiTacThem["GhiChu"] = txtGhiChu.Text.Trim();
                    tbDoiTacThem.Rows.Add(rwDoiTacThem);
                    daDoiTacThem.Update(dsDoiTacThem, "Doi_Tac");

                    tbDoiTacThem.Dispose();
                    dsDoiTacThem.Dispose();
                    daDoiTacThem.Dispose();
                }

                if (bSua)
                {
                    if (txtMaSo.Text.Trim() != strLuuMaSo)
                    {
                        string strDoiTac = "Select * From Doi_Tac Where MaSo='" + txtMaSo.Text.Trim() + "'";
                        SqlDataAdapter daDoiTac = new SqlDataAdapter(strDoiTac, clsDungChung.con);
                        DataSet dsDoiTac = new DataSet();
                        daDoiTac.Fill(dsDoiTac, "Doi_Tac");
                        DataTable tbDoiTac = dsDoiTac.Tables["Doi_Tac"];
                        if (tbDoiTac.DefaultView.Count > 0)
                        {
                            MessageBox.Show("Giá Trị Mã Số Đối Tác " + txtMaSo.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMaSo.Focus();
                            return;
                        }
                        tbDoiTac.Dispose();
                        dsDoiTac.Dispose();
                        daDoiTac.Dispose();
                    }

                    string strDoiTacSua = "Select * From Doi_Tac Where MaSo='" + strLuuMaSo + "'";
                    SqlDataAdapter daDoiTacSua = new SqlDataAdapter(strDoiTacSua, clsDungChung.con);
                    DataSet dsDoiTacSua = new DataSet();
                    daDoiTacSua.Fill(dsDoiTacSua, "Doi_Tac");
                    DataTable tbDoiTacSua = dsDoiTacSua.Tables["Doi_Tac"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daDoiTacSua);
                    daDoiTacSua.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowDoiTacSua = tbDoiTacSua.Rows[0];
                    rowDoiTacSua.BeginEdit();
                    rowDoiTacSua["MaSo"] = txtMaSo.Text.Trim();
                    rowDoiTacSua["TenDoiTac"] = txtTenDoiTac.Text.Trim();
                    rowDoiTacSua["DiaChi"] = txtDiaChi.Text.Trim();
                    rowDoiTacSua["DienThoai"] = txtDienThoai.Text.Trim();
                    rowDoiTacSua["GhiChu"] = txtGhiChu.Text.Trim();
                    rowDoiTacSua.EndEdit();
                    daDoiTacSua.Update(dsDoiTacSua, "Doi_Tac");

                    tbDoiTacSua.Dispose();
                    dsDoiTacSua.Dispose();
                    dsDoiTacSua.Dispose();
                }

                btnKhongLuu_Click(sender, e);
                HienDoiTac();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void lstvDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item = lstvDs.FocusedItem;
            txtMaSo.Text = item.Text;
            txtTenDoiTac.Text = item.SubItems[1].Text;
            txtDiaChi.Text = item.SubItems[2].Text;
            txtDienThoai.Text = item.SubItems[3].Text;
            txtGhiChu.Text = item.SubItems[4].Text;
        }

        private void frmDoiTac_Load(object sender, EventArgs e)
        {
            HienDoiTac();
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
