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
    public partial class frmDsPhong : Form
    {
        private Boolean bThem=false;
        private Boolean bSua=false;
        private string strLuuMaSo = null;

        public frmDsPhong()
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
            txtSoPhong.Text = "";
            txtGhiChu.Text = "";
            txtSoPhong.Enabled = true;
            txtGhiChu.Enabled = true;
            lstvDs.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnKhongLuu.Enabled = true;
            txtSoPhong.Focus();
        }

        private void HienLoaiPhong()
        {
            try
            {
                string StrComdLoaiPhong = "Select * From Loai_Phong";
                SqlDataAdapter daLoaiPhong = new SqlDataAdapter(StrComdLoaiPhong, clsDungChung.con);
                DataSet dsLoaiPhong = new DataSet();
                daLoaiPhong.Fill(dsLoaiPhong, "Loai_Phong");
                DataTable tbLoaiPhong = dsLoaiPhong.Tables["Loai_Phong"];
                cboLoaiPhong.Items.Clear();
                foreach (DataRow r1 in tbLoaiPhong.Rows)
                {
                    cboLoaiPhong.Items.Add(r1["MaLoai"].ToString().Trim() + ". " + r1["LoaiPhong"].ToString().Trim());
                }
                if (cboLoaiPhong.Items.Count > 0)
                {
                    cboLoaiPhong.Text = cboLoaiPhong.Items[0].ToString();
                }
                tbLoaiPhong.Dispose();
                dsLoaiPhong.Dispose();
                daLoaiPhong.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void frmDsPhong_Load(object sender, EventArgs e)
        {
            HienLoaiPhong();
            HienDsPhong();
        }

        private void HienDsPhong()
        {
            try
            {
                string strComdDsPhong = "Select * From So_Phong Where MaLoai='" + cboLoaiPhong.Text.Substring(0, cboLoaiPhong.Text.IndexOf(".")) + "'";
                SqlDataAdapter daDsPhong = new SqlDataAdapter(strComdDsPhong, clsDungChung.con);
                DataSet dsDsPhong = new DataSet();
                daDsPhong.Fill(dsDsPhong, "So_Phong");
                DataTable tbDsPhong = dsDsPhong.Tables["So_Phong"];
                lstvDs.Items.Clear();
                foreach (DataRow r2 in tbDsPhong.Rows)
                {
                    ListViewItem item = new ListViewItem(r2["SoPhong"].ToString());
                    item.SubItems.Add(r2["MaLoai"].ToString());
                    item.SubItems.Add(r2["GhiChu"].ToString());
                    lstvDs.Items.Add(item);
                }

                tbDsPhong.Dispose();
                dsDsPhong.Dispose();
                daDsPhong.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void cboLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            HienDsPhong();
        }

        private void cboLoaiPhong_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtSoPhong.Text != "")
            {
                bSua = true;
                strLuuMaSo = txtSoPhong.Text.Trim();
                txtSoPhong.Enabled = true;
                txtGhiChu.Enabled = true;
                lstvDs.Enabled = false;
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
                btnLuu.Enabled = true;
                btnKhongLuu.Enabled = true;
                txtSoPhong.Focus();
            }
            else
            {
                MessageBox.Show("Giá Trị Số Phòng Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lstvDs.Focus();
            }
        }

        private void lstvDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item = lstvDs.FocusedItem;
            txtSoPhong.Text = item.Text;
            //cboLoaiPhong.Text = item.SubItems[2].Text;
            txtGhiChu.Text = item.SubItems[2].Text;
        }

        private void btnKhongLuu_Click(object sender, EventArgs e)
        {
            bThem = false;
            bSua = false;
            txtSoPhong.Text = "";
            txtGhiChu.Text = "";
            txtSoPhong.Enabled = false;
            txtGhiChu.Enabled = false;
            lstvDs.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnKhongLuu.Enabled = false;
            cboLoaiPhong.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string strLuuLoaiPhong = null;
            try
            {
                if (bThem)
                {
                    if (txtSoPhong.Text.Equals(""))
                    {
                        MessageBox.Show("Giá Trị Số Phòng Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSoPhong.Focus();
                        return;
                    }

                    //Kiem tra xem Ma So phong toan tai hay chua
                    string strSoPhong1 = "Select * From So_Phong Where SoPhong='" + txtSoPhong.Text.Trim() + "'";
                    SqlDataAdapter daSoPhong1 = new SqlDataAdapter(strSoPhong1, clsDungChung.con);
                    DataSet dsSoPhong1 = new DataSet();
                    daSoPhong1.Fill(dsSoPhong1, "So_Phong");
                    DataTable tbSoPhong1 = dsSoPhong1.Tables["So_Phong"];
                    if (tbSoPhong1.DefaultView.Count > 0)
                    {
                        MessageBox.Show("Giá Trị Số Phòng " + txtSoPhong.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSoPhong.Focus();
                        return;
                    }
                    tbSoPhong1.Dispose();
                    dsSoPhong1.Dispose();
                    daSoPhong1.Dispose();

                    string strSoPhongThem = "Select * From So_Phong";
                    SqlDataAdapter daSoPhongThem = new SqlDataAdapter(strSoPhongThem, clsDungChung.con);
                    DataSet dsSoPhongThem = new DataSet();
                    daSoPhongThem.Fill(dsSoPhongThem, "So_Phong");
                    DataTable tbSoPhongThem = dsSoPhongThem.Tables["So_Phong"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daSoPhongThem);
                    daSoPhongThem.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rwSoPhongThem = tbSoPhongThem.NewRow();
                    rwSoPhongThem["SoPhong"] = txtSoPhong.Text.Trim();
                    if (cboLoaiPhong.Text.IndexOf(".") == -1)
                        strLuuLoaiPhong = cboLoaiPhong.Text;
                    else
                        strLuuLoaiPhong = cboLoaiPhong.Text.Substring(0, cboLoaiPhong.Text.IndexOf("."));
                    rwSoPhongThem["MaLoai"] = strLuuLoaiPhong;
                    rwSoPhongThem["TinhTrang"] = 0;
                    rwSoPhongThem["GhiChu"] = txtGhiChu.Text;
                    tbSoPhongThem.Rows.Add(rwSoPhongThem);
                    daSoPhongThem.Update(dsSoPhongThem, "So_Phong");

                    tbSoPhongThem.Dispose();
                    dsSoPhongThem.Dispose();
                    daSoPhongThem.Dispose();

                }

                if (bSua)
                {
                    if (txtSoPhong.Text.Trim() != strLuuMaSo)
                    {
                        string strSoPhong1 = "Select * From So_Phong Where SoPhong='" + txtSoPhong.Text.Trim() + "'";
                        SqlDataAdapter daSoPhong1 = new SqlDataAdapter(strSoPhong1, clsDungChung.con);
                        DataSet dsSoPhong1 = new DataSet();
                        daSoPhong1.Fill(dsSoPhong1, "So_Phong");
                        DataTable tbSoPhong1 = dsSoPhong1.Tables["So_Phong"];
                        if (tbSoPhong1.DefaultView.Count > 0)
                        {
                            MessageBox.Show("Giá Trị Số Phòng " + txtSoPhong.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtSoPhong.Focus();
                            return;
                        }
                        tbSoPhong1.Dispose();
                        dsSoPhong1.Dispose();
                        daSoPhong1.Dispose();
                    }

                    string strSoPhongSua = "Select * From So_Phong Where SoPhong='" + strLuuMaSo + "'";
                    SqlDataAdapter daSoPhongSua = new SqlDataAdapter(strSoPhongSua, clsDungChung.con);
                    DataSet dsSoPhongSua = new DataSet();
                    daSoPhongSua.Fill(dsSoPhongSua, "So_Phong");
                    DataTable tbSoPhongSua = dsSoPhongSua.Tables["So_Phong"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daSoPhongSua);
                    daSoPhongSua.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowSoPhongSua = tbSoPhongSua.Rows[0];
                    rowSoPhongSua.BeginEdit();
                    rowSoPhongSua["SoPhong"] = txtSoPhong.Text.Trim();
                    if (cboLoaiPhong.Text.IndexOf(".") == -1)
                        strLuuLoaiPhong = cboLoaiPhong.Text;
                    else
                        strLuuLoaiPhong = cboLoaiPhong.Text.Substring(0, cboLoaiPhong.Text.IndexOf("."));
                    rowSoPhongSua["MaLoai"] = strLuuLoaiPhong;
                    rowSoPhongSua["TinhTrang"] = 0;
                    rowSoPhongSua["GhiChu"] = txtGhiChu.Text.Trim();
                    rowSoPhongSua.EndEdit();
                    daSoPhongSua.Update(dsSoPhongSua, "So_Phong");

                    tbSoPhongSua.Dispose();
                    dsSoPhongSua.Dispose();
                    dsSoPhongSua.Dispose();
                }

                btnKhongLuu_Click(sender, e);
                HienDsPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtSoPhong.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Số Phòng Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoPhong.Focus();
                return;
            }

            try
            {
                if (MessageBox.Show("Bạn Có Chắc Chắn Muốn Xoá Số Phòng Này Không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string strSoPhongXoa = "Select * From So_Phong Where SoPhong='" + txtSoPhong.Text.Trim() + "'";
                    SqlDataAdapter daSoPhongXoa = new SqlDataAdapter(strSoPhongXoa, clsDungChung.con);
                    DataSet dsSoPhongXoa = new DataSet();
                    daSoPhongXoa.Fill(dsSoPhongXoa, "So_Phong");
                    DataTable tbSoPhongXoa = dsSoPhongXoa.Tables["So_Phong"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daSoPhongXoa);
                    daSoPhongXoa.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowSoPhongXoa = tbSoPhongXoa.Rows[0];
                    rowSoPhongXoa.Delete();
                    daSoPhongXoa.Update(dsSoPhongXoa, "So_Phong");
                    tbSoPhongXoa.Dispose();
                    dsSoPhongXoa.Dispose();
                    daSoPhongXoa.Dispose();
                }

                btnKhongLuu_Click(sender, e);
                HienDsPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }
    }
}
