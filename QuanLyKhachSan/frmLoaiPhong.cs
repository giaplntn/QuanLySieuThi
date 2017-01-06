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
    public partial class frmLoaiPhong : Form
    {
        private Boolean bThem = false;
        private Boolean bSua = false;
        private string strLuuMaSo = null;

        public frmLoaiPhong()
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
            txtLoaiPhong.Text = "";
            txtDonGia.Text = "0";
            txtGhiChu.Text = "";
            txtMaSo.Enabled = true;
            txtLoaiPhong.Enabled = true;
            txtDonGia.Enabled = true;
            txtGhiChu.Enabled = true;
            lstvDs.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnKhongLuu.Enabled = true;
            txtMaSo.Focus();
        }

        private void HienLoaiPhong()
        {
            try
            {
                clsCacHam h = new clsCacHam();
                string strCmdLoaiPhong = "Select * From Loai_Phong";
                SqlDataAdapter daLoaiPhong = new SqlDataAdapter(strCmdLoaiPhong, clsDungChung.con);
                DataSet dsLoaiPhong = new DataSet();
                daLoaiPhong.Fill(dsLoaiPhong, "Loai_Phong");
                DataTable tbLoaiPhong = dsLoaiPhong.Tables["Loai_Phong"];
                lstvDs.Items.Clear();
                foreach (DataRow r1 in tbLoaiPhong.Rows)
                {
                    ListViewItem item = new ListViewItem(r1["MaLoai"].ToString().Trim());
                    item.SubItems.Add(r1["LoaiPhong"].ToString());
                    item.SubItems.Add(h.chendau((Convert.ToInt32(r1["DonGia"])).ToString()));
                    item.SubItems.Add(r1["GhiChu"].ToString().Trim());
                    lstvDs.Items.Add(item);

                }
                dsLoaiPhong.Dispose();
                daLoaiPhong.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void frmLoaiPhong_Load(object sender, EventArgs e)
        {
            HienLoaiPhong();
        }

        private void btnKhongLuu_Click(object sender, EventArgs e)
        {
            bThem = false;
            bSua = false;
            txtMaSo.Text = "";
            txtLoaiPhong.Text = "";
            txtDonGia.Text = "0";
            txtGhiChu.Text = "";
            txtMaSo.Enabled = false;
            txtLoaiPhong.Enabled = false;
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
            clsCacHam h = new clsCacHam();
            try
            {
                if (bThem)
                {
                    if (txtMaSo.Text.Equals(""))
                    {
                        MessageBox.Show("Giá Trị Mã Phòng Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaSo.Focus();
                        return;
                    }

                    if (txtLoaiPhong.Text.Equals(""))
                    {
                        MessageBox.Show("Giá Trị Loại Phòng Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLoaiPhong.Focus();
                        return;
                    }

                    if (txtDonGia.Text.Equals(""))
                    {
                        txtDonGia.Text = "0";
                    }

                    //Kiem tra xem Ma so loai phong toan tai hay chua
                    string strLoaiPhong = "Select * From Loai_Phong Where MaLoai='" + txtMaSo.Text.Trim() + "'";
                    SqlDataAdapter daLoaiPhong = new SqlDataAdapter(strLoaiPhong, clsDungChung.con);
                    DataSet dsLoaiPhong = new DataSet();
                    daLoaiPhong.Fill(dsLoaiPhong, "Loai_Phong");
                    DataTable tbLoaiPhong = dsLoaiPhong.Tables["Loai_Phong"];
                    if (tbLoaiPhong.DefaultView.Count > 0)
                    {
                        MessageBox.Show("Giá Trị Mã Số " + txtMaSo.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaSo.Focus();
                        return;
                    }
                    tbLoaiPhong.Dispose();
                    dsLoaiPhong.Dispose();
                    daLoaiPhong.Dispose();

                    string strLoaiPhongThem = "Select * From Loai_Phong";
                    SqlDataAdapter daLoaiPhongThem = new SqlDataAdapter(strLoaiPhongThem, clsDungChung.con);
                    DataSet dsLoaiPhongThem = new DataSet();
                    daLoaiPhongThem.Fill(dsLoaiPhongThem, "Loai_Phong");
                    DataTable tbLoaiPhongThem = dsLoaiPhongThem.Tables["Loai_Phong"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daLoaiPhongThem);
                    daLoaiPhongThem.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rwLoaiPhongThem = tbLoaiPhongThem.NewRow();
                    rwLoaiPhongThem["MaLoai"] = txtMaSo.Text.Trim();
                    rwLoaiPhongThem["LoaiPhong"] = txtLoaiPhong.Text.Trim();
                    rwLoaiPhongThem["DonGia"] = h.loaidau(txtDonGia.Text.Trim());
                    rwLoaiPhongThem["GhiChu"] = txtGhiChu.Text.Trim();
                    tbLoaiPhongThem.Rows.Add(rwLoaiPhongThem);
                    daLoaiPhongThem.Update(dsLoaiPhongThem, "Loai_Phong");

                    tbLoaiPhongThem.Dispose();
                    dsLoaiPhongThem.Dispose();
                    daLoaiPhongThem.Dispose();
                }

                if (bSua)
                {
                    if (txtMaSo.Text.Trim() != strLuuMaSo)
                    {
                        string strLoaiPhong = "Select * From Loai_Phong Where MaLoai='" + txtMaSo.Text.Trim() + "'";
                        SqlDataAdapter daLoaiPhong = new SqlDataAdapter(strLoaiPhong, clsDungChung.con);
                        DataSet dsLoaiPhong = new DataSet();
                        daLoaiPhong.Fill(dsLoaiPhong, "Loai_Phong");
                        DataTable tbLoaiPhong = dsLoaiPhong.Tables["Loai_Phong"];
                        if (tbLoaiPhong.DefaultView.Count > 0)
                        {
                            MessageBox.Show("Giá Trị Mã Số " + txtMaSo.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMaSo.Focus();
                            return;
                        }
                        tbLoaiPhong.Dispose();
                        dsLoaiPhong.Dispose();
                        daLoaiPhong.Dispose();
                    }

                    string strLoaiPhongSua = "Select * From Loai_Phong Where MaLoai='" + strLuuMaSo + "'";
                    SqlDataAdapter daLoaiPhongSua = new SqlDataAdapter(strLoaiPhongSua, clsDungChung.con);
                    DataSet dsLoaiPhongSua = new DataSet();
                    daLoaiPhongSua.Fill(dsLoaiPhongSua, "Loai_Phong");
                    DataTable tbLoaiPhongSua = dsLoaiPhongSua.Tables["Loai_Phong"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daLoaiPhongSua);
                    daLoaiPhongSua.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowLoaiPhongSua = tbLoaiPhongSua.Rows[0];
                    rowLoaiPhongSua.BeginEdit();
                    rowLoaiPhongSua["MaLoai"] = txtMaSo.Text.Trim();
                    rowLoaiPhongSua["LoaiPhong"] = txtLoaiPhong.Text.Trim();
                    rowLoaiPhongSua["DonGia"] = h.loaidau(txtDonGia.Text.Trim());
                    rowLoaiPhongSua["GhiChu"] = txtGhiChu.Text.Trim();
                    rowLoaiPhongSua.EndEdit();
                    daLoaiPhongSua.Update(dsLoaiPhongSua, "Loai_Phong");

                    tbLoaiPhongSua.Dispose();
                    dsLoaiPhongSua.Dispose();
                    dsLoaiPhongSua.Dispose();
                }

                btnKhongLuu_Click(sender, e);
                HienLoaiPhong();
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
                txtLoaiPhong.Enabled = true;
                txtDonGia.Enabled = true;
                lstvDs.Enabled = false;
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
                btnLuu.Enabled = true;
                btnKhongLuu.Enabled = true;
                txtLoaiPhong.Focus();
            }
            else
            {
                MessageBox.Show("Giá Trị Mã Số Loại phòng Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lstvDs.Focus();
            }
        }

        private void lstvDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item = lstvDs.FocusedItem;
            txtMaSo.Text = item.Text;
            txtLoaiPhong.Text = item.SubItems[1].Text;
            txtDonGia.Text = item.SubItems[2].Text;
            txtGhiChu.Text = item.SubItems[3].Text;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaSo.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Mã Loại Phòng Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSo.Focus();
                return; 
            }
            try
            {
                if (MessageBox.Show("Bạn Có Chắc Chắn Muốn Xoá Loại Phòng Này Không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string strLoaiPhongXoa = "Select * From Loai_Phong Where MaLoai='" + txtMaSo.Text.Trim() + "'";
                    SqlDataAdapter daLoaiPhongXoa = new SqlDataAdapter(strLoaiPhongXoa, clsDungChung.con);
                    DataSet dsLoaiPhongXoa = new DataSet();
                    daLoaiPhongXoa.Fill(dsLoaiPhongXoa, "Loai_Phong");
                    DataTable tbLoaiPhongXoa = dsLoaiPhongXoa.Tables["Loai_Phong"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daLoaiPhongXoa);
                    daLoaiPhongXoa.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowLoaiPhongXoa = tbLoaiPhongXoa.Rows[0];
                    rowLoaiPhongXoa.Delete();
                    daLoaiPhongXoa.Update(dsLoaiPhongXoa, "Loai_Phong");

                    tbLoaiPhongXoa.Dispose();
                    dsLoaiPhongXoa.Dispose();
                    daLoaiPhongXoa.Dispose();
                }

                btnKhongLuu_Click(sender, e);
                HienLoaiPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
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
                clsCacHam h=new clsCacHam();
                txtDonGia.Text = h.chendau(h.loaidau(txtDonGia.Text));
                txtDonGia.SelectionStart = txtDonGia.Text.Length + 1;
            }
        }
    }
}
