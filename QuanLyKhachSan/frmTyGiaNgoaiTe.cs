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
    public partial class frmTyGiaNgoaiTe : Form
    {
        private Boolean bThem = false;
        private Boolean bSua = false;
        private string strLuuMaNT = null;

        public frmTyGiaNgoaiTe()
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
            txtMaNgoaiTe.Text = "";
            txtTenNgoaiTe.Text = "";
            txtTyGia.Text = "0";
            txtMaNgoaiTe.Enabled = true;
            txtTenNgoaiTe.Enabled = true;
            txtTyGia.Enabled = true;
            lstvDs.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnKhongLuu.Enabled = true;
            txtMaNgoaiTe.Focus();
        }

        private void HienTyGia()
        {
            try
            {
                clsCacHam h = new clsCacHam();
                string strCmdTyGia = "Select * From Ngoai_Te";
                SqlDataAdapter daTyGia = new SqlDataAdapter(strCmdTyGia, clsDungChung.con);
                DataSet dsTyGia = new DataSet();
                daTyGia.Fill(dsTyGia, "Ngoai_Te");
                DataTable tbTyGia = dsTyGia.Tables["Ngoai_Te"];
                lstvDs.Items.Clear();
                foreach (DataRow r1 in tbTyGia.Rows)
                {
                    ListViewItem item = new ListViewItem(r1["MaNT"].ToString().Trim());
                    item.SubItems.Add(r1["TenNT"].ToString());
                    item.SubItems.Add(h.chendau((Convert.ToInt32(r1["TyGia"])).ToString()));
                    lstvDs.Items.Add(item);

                }
                tbTyGia.Dispose();
                dsTyGia.Dispose();
                daTyGia.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void txtTyGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void frmTyGiaNgoaiTe_Load(object sender, EventArgs e)
        {
            HienTyGia();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNgoaiTe.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Mã Ngoại Tệ Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNgoaiTe.Focus();
                return;
            }

            try
            {
                if (MessageBox.Show("Bạn Có Chắc Chắn Muốn Xoá Ngoại Tệ Này Không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string strTyGiaXoa = "Select * From Ngoai_Te Where MaNT='" + txtMaNgoaiTe.Text.Trim() + "'";
                    SqlDataAdapter daTyGiaXoa = new SqlDataAdapter(strTyGiaXoa, clsDungChung.con);
                    DataSet dsTyGiaXoa = new DataSet();
                    daTyGiaXoa.Fill(dsTyGiaXoa, "Ngoai_Te");
                    DataTable tbTyGiaXoa = dsTyGiaXoa.Tables["Ngoai_Te"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daTyGiaXoa);
                    daTyGiaXoa.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowTyGiaXoa = tbTyGiaXoa.Rows[0];
                    rowTyGiaXoa.Delete();
                    daTyGiaXoa.Update(dsTyGiaXoa, "Ngoai_Te");

                    tbTyGiaXoa.Dispose();
                    dsTyGiaXoa.Dispose();
                    daTyGiaXoa.Dispose();
                }

                btnKhongLuu_Click(sender, e);
                HienTyGia();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaNgoaiTe.Text != "")
            {
                bSua = true;
                strLuuMaNT = txtMaNgoaiTe.Text.Trim();
                txtMaNgoaiTe.Enabled = true;
                txtTenNgoaiTe.Enabled = true;
                txtTyGia.Enabled = true;
                lstvDs.Enabled = false;
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
                btnLuu.Enabled = true;
                btnKhongLuu.Enabled = true;
                txtMaNgoaiTe.Focus();
            }
            else
            {
                MessageBox.Show("Giá Trị Mã Ngoại Tệ Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lstvDs.Focus();
            }
        }

        private void btnKhongLuu_Click(object sender, EventArgs e)
        {
            bThem = false;
            bSua = false;
            txtMaNgoaiTe.Text = "";
            txtTenNgoaiTe.Text = "";
            txtTyGia.Text = "0";
            txtMaNgoaiTe.Enabled = false;
            txtTenNgoaiTe.Enabled = false;
            txtTyGia.Enabled = false;
            lstvDs.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnKhongLuu.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            clsCacHam h = new clsCacHam();
            try
            {
                if (bThem)
                {
                    if (txtMaNgoaiTe.Text.Equals(""))
                    {
                        MessageBox.Show("Giá Trị Mã Ngoại Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaNgoaiTe.Focus();
                        return;
                    }

                    if (txtTenNgoaiTe.Text.Equals(""))
                    {
                        MessageBox.Show("Giá Trị Tên Ngoại Tê Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTenNgoaiTe.Focus();
                        return;
                    }

                    if (txtTyGia.Text.Equals(""))
                    {
                        txtTyGia.Text = "0";
                    }

                    //Kiem tra xem Ma so loai phong toan tai hay chua
                    string strTyGia = "Select * From Ngoai_Te Where MaNT='" + txtMaNgoaiTe.Text.Trim() + "'";
                    SqlDataAdapter daTyGia = new SqlDataAdapter(strTyGia, clsDungChung.con);
                    DataSet dsTyGia = new DataSet();
                    daTyGia.Fill(dsTyGia, "Ngoai_Te");
                    DataTable tbTyGia = dsTyGia.Tables["Ngoai_Te"];
                    if (tbTyGia.DefaultView.Count > 0)
                    {
                        MessageBox.Show("Giá Trị Mã Số Ngoại Tệ " + txtMaNgoaiTe.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaNgoaiTe.Focus();
                        return;
                    }
                    tbTyGia.Dispose();
                    dsTyGia.Dispose();
                    daTyGia.Dispose();

                    string strTyGiaThem = "Select * From Ngoai_Te";
                    SqlDataAdapter daTyGiaThem = new SqlDataAdapter(strTyGiaThem, clsDungChung.con);
                    DataSet dsTyGiaThem = new DataSet();
                    daTyGiaThem.Fill(dsTyGiaThem, "Ngoai_Te");
                    DataTable tbTyGiaThem = dsTyGiaThem.Tables["Ngoai_Te"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daTyGiaThem);
                    daTyGiaThem.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rwTyGiaThem = tbTyGiaThem.NewRow();
                    rwTyGiaThem["MaNT"] = txtMaNgoaiTe.Text.Trim();
                    rwTyGiaThem["TenNT"] = txtTenNgoaiTe.Text.Trim();
                    rwTyGiaThem["TyGia"] = h.loaidau(txtTyGia.Text.Trim());
                    tbTyGiaThem.Rows.Add(rwTyGiaThem);
                    daTyGiaThem.Update(dsTyGiaThem, "Ngoai_Te");

                    tbTyGiaThem.Dispose();
                    dsTyGiaThem.Dispose();
                    daTyGiaThem.Dispose();
                }

                if (bSua)
                {
                    if (txtMaNgoaiTe.Text.Trim() != strLuuMaNT)
                    {
                        string strTyGia = "Select * From Ngoai_Te Where MaNT='" + txtMaNgoaiTe.Text.Trim() + "'";
                        SqlDataAdapter daTyGia = new SqlDataAdapter(strTyGia, clsDungChung.con);
                        DataSet dsTyGia = new DataSet();
                        daTyGia.Fill(dsTyGia, "Ngoai_Te");
                        DataTable tbTyGia = dsTyGia.Tables["Ngoai_Te"];
                        if (tbTyGia.DefaultView.Count > 0)
                        {
                            MessageBox.Show("Giá Trị Mã Số Ngoại Tệ " + txtMaNgoaiTe.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMaNgoaiTe.Focus();
                            return;
                        }
                        tbTyGia.Dispose();
                        dsTyGia.Dispose();
                        daTyGia.Dispose();
                    }

                    string strTyGiaSua = "Select * From Ngoai_Te Where MaNT='" + strLuuMaNT + "'";
                    SqlDataAdapter daTyGiaSua = new SqlDataAdapter(strTyGiaSua, clsDungChung.con);
                    DataSet dsTyGiaSua = new DataSet();
                    daTyGiaSua.Fill(dsTyGiaSua, "Ngoai_Te");
                    DataTable tbTyGiaSua = dsTyGiaSua.Tables["Ngoai_Te"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daTyGiaSua);
                    daTyGiaSua.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowTyGiaSua = tbTyGiaSua.Rows[0];
                    rowTyGiaSua.BeginEdit();
                    rowTyGiaSua["MaNT"] = txtMaNgoaiTe.Text.Trim();
                    rowTyGiaSua["TenNT"] = txtTenNgoaiTe.Text.Trim();
                    rowTyGiaSua["TyGia"] = h.loaidau(txtTyGia.Text.Trim());
                    rowTyGiaSua.EndEdit();
                    daTyGiaSua.Update(dsTyGiaSua, "Ngoai_Te");

                    tbTyGiaSua.Dispose();
                    dsTyGiaSua.Dispose();
                    dsTyGiaSua.Dispose();
                }

                btnKhongLuu_Click(sender, e);
                HienTyGia();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void lstvDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item = lstvDs.FocusedItem;
            txtMaNgoaiTe.Text = item.Text;
            txtTenNgoaiTe.Text = item.SubItems[1].Text;
            txtTyGia.Text = item.SubItems[2].Text;
        }

        private void txtTyGia_TextChanged(object sender, EventArgs e)
        {
            if (txtTyGia.Text.Length > 0)
            {
                clsCacHam h=new clsCacHam();
                txtTyGia.Text = h.chendau(h.loaidau(txtTyGia.Text));
                txtTyGia.SelectionStart = txtTyGia.Text.Length + 1; 
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
