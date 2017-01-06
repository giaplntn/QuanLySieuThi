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
    public partial class frmTrangThietBi : Form
    {

        private Boolean bThem = false;

        public frmTrangThietBi()
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
            cboLoaiThietBi.Text = "";
            txtDonVT.Text = "";
            numSoLuong.Value = 0;
            cboLoaiThietBi.Enabled = true;
            numSoLuong.Enabled = true;
            lstvDs.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnKhongLuu.Enabled = true;
            cboSoPhong.Focus();
        }

        private void HienTrangThietBi()
        {
            try
            {
                string strCmdTrangThietBi = "Select * From Trang_ThietBi Where SoPhong='" + cboSoPhong.Text.Trim() + "'";
                SqlDataAdapter daTrangThietBi = new SqlDataAdapter(strCmdTrangThietBi, clsDungChung.con);
                DataSet dsTrangThietBi = new DataSet();
                daTrangThietBi.Fill(dsTrangThietBi, "Trang_ThietBi");
                DataTable tbTrangThietBi = dsTrangThietBi.Tables["Trang_ThietBi"];
                lstvDs.Items.Clear();
                foreach (DataRow r1 in tbTrangThietBi.Rows)
                {
                    ListViewItem item = new ListViewItem(r1["MaSo"].ToString().Trim());
                    string SqlTenTB = "Select * From Thiet_Bi Where MaTB='" + r1["MaTB"].ToString().Trim() + "'";
                    SqlDataAdapter daTenTB = new SqlDataAdapter(SqlTenTB, clsDungChung.con);
                    DataSet dsTenTB = new DataSet();
                    daTenTB.Fill(dsTenTB, "Thiet_Bi");
                    DataTable tbTenTB = dsTenTB.Tables["Thiet_Bi"];
                    DataRow row = tbTenTB.Rows[0];
                    item.SubItems.Add(row["TenTB"].ToString().Trim());
                    tbTenTB.Dispose();
                    dsTenTB.Dispose();
                    daTenTB.Dispose();

                    item.SubItems.Add(r1["DonVT"].ToString().Trim());
                    item.SubItems.Add(r1["SoLuong"].ToString().Trim());
                    lstvDs.Items.Add(item);

                }
                tbTrangThietBi.Dispose();
                dsTrangThietBi.Dispose();
                daTrangThietBi.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void frmTrangThietBi_Load(object sender, EventArgs e)
        {
            HienDsPhong();
            if (cboSoPhong.Items.Count > 0)
            {
                cboSoPhong.Text = cboSoPhong.Items[0].ToString();   
            }
            HienDsThietBi();
            HienTrangThietBi();
        }

        private void HienDsPhong()
        {
            try
            {
                string strComdDsPhong = "Select * From So_Phong";
                SqlDataAdapter daDsPhong = new SqlDataAdapter(strComdDsPhong, clsDungChung.con);
                DataSet dsDsPhong = new DataSet();
                daDsPhong.Fill(dsDsPhong, "So_Phong");
                DataTable tbDsPhong = dsDsPhong.Tables["So_Phong"];
                cboSoPhong.Items.Clear();
                foreach (DataRow r2 in tbDsPhong.Rows)
                {
                    cboSoPhong.Items.Add(r2["SoPhong"].ToString());
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

        private void HienDsThietBi()
        {
            try
            {
                string strComdDsThietBi = "Select * From Thiet_Bi";
                SqlDataAdapter daDsThietBi = new SqlDataAdapter(strComdDsThietBi, clsDungChung.con);
                DataSet dsDsThietBi = new DataSet();
                daDsThietBi.Fill(dsDsThietBi, "Thiet_Bi");
                DataTable tbDsThietBi = dsDsThietBi.Tables["Thiet_Bi"];
                cboLoaiThietBi.Items.Clear();
                foreach (DataRow r2 in tbDsThietBi.Rows)
                {
                    cboLoaiThietBi.Items.Add(r2["MaTB"].ToString() + ". " + r2["TenTB"].ToString());
                }

                tbDsThietBi.Dispose();
                dsDsThietBi.Dispose();
                daDsThietBi.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void cboSoPhong_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; 
        }

        private void cboLoaiThietBi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strSqlThietBi = "Select * From Thiet_Bi Where MaTB='" + cboLoaiThietBi.Text.Substring(0, cboLoaiThietBi.Text.IndexOf(".")) + "'";
                SqlDataAdapter daThietBi = new SqlDataAdapter(strSqlThietBi, clsDungChung.con);
                DataSet dsThietBi = new DataSet();
                daThietBi.Fill(dsThietBi, "Thiet_Bi");
                DataTable tbThietBi = dsThietBi.Tables["Thiet_Bi"];
                if (tbThietBi.DefaultView.Count > 0)
                {
                    DataRow r = tbThietBi.Rows[0];
                    txtDonVT.Text = r["DonVT"].ToString();
                }
                tbThietBi.Dispose();
                dsThietBi.Dispose();
                daThietBi.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void cboSoPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            HienTrangThietBi();
        }

        private void btnKhongLuu_Click(object sender, EventArgs e)
        {
            bThem = false;
            cboLoaiThietBi.Text = "";
            txtMaSo.Text = "";
            txtDonVT.Text = "";
            numSoLuong.Value = 0;
            cboLoaiThietBi.Enabled = false;
            numSoLuong.Enabled = false;
            lstvDs.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnKhongLuu.Enabled =false;
            lstvDs.Focus(); 
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (bThem)
                {
                    if (cboLoaiThietBi.Text.Equals(""))
                    {
                        MessageBox.Show("Giá Trị Thiết Bị Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboLoaiThietBi.Focus();
                        return;
                    }

                    if (numSoLuong.Value == 0)
                    {
                        MessageBox.Show("Giá Trị Số Lượng Không Thể Bằng 0, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        numSoLuong.Focus();
                        return;
                    }

                    //Kiem tra xem Thiet Bi phong đã tồn tại hay chua
                    string strThietBi = "Select * From Trang_ThietBi Where MaTB='" + cboLoaiThietBi.Text.Substring(0, cboLoaiThietBi.Text.IndexOf(".")) + "' And SoPhong='" + cboSoPhong.Text.Trim() + "'";
                    SqlDataAdapter daThietBi = new SqlDataAdapter(strThietBi, clsDungChung.con);
                    DataSet dsThietBi = new DataSet();
                    daThietBi.Fill(dsThietBi, "Trang_ThietBi");
                    DataTable tbThietBi = dsThietBi.Tables["Trang_ThietBi"];
                    if (tbThietBi.DefaultView.Count > 0)
                    {
                        MessageBox.Show("Giá Trị Thiết Bị " + cboLoaiThietBi.Text.Trim() + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboLoaiThietBi.Focus();
                        return;
                    }
                    tbThietBi.Dispose();
                    dsThietBi.Dispose();
                    daThietBi.Dispose();

                    string strThietBiThem = "Select * From Trang_ThietBi";
                    SqlDataAdapter daThietBiThem = new SqlDataAdapter(strThietBiThem, clsDungChung.con);
                    DataSet dsThietBiThem = new DataSet();
                    daThietBiThem.Fill(dsThietBiThem, "Trang_ThietBi");
                    DataTable tbThietBiThem = dsThietBiThem.Tables["Trang_ThietBi"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daThietBiThem);
                    daThietBiThem.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rwThietBiThem = tbThietBiThem.NewRow();
                    rwThietBiThem["SoPhong"] = cboSoPhong.Text.Trim();
                    rwThietBiThem["MaTB"] = cboLoaiThietBi.Text.Substring(0, cboLoaiThietBi.Text.IndexOf("."));
                    rwThietBiThem["DonVT"] = txtDonVT.Text.Trim();
                    rwThietBiThem["SoLuong"] = numSoLuong.Value;
                    tbThietBiThem.Rows.Add(rwThietBiThem);
                    daThietBiThem.Update(dsThietBiThem, "Trang_ThietBi");

                    tbThietBiThem.Dispose();
                    dsThietBiThem.Dispose();
                    daThietBiThem.Dispose();

                }
                btnKhongLuu_Click(sender, e);
                HienTrangThietBi();
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
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaSo.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Mã Thiết Bị Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lstvDs.Focus();
                return;
            }

            try
            {
                if (MessageBox.Show("Bạn Có Chắc Chắn Muốn Xoá Trang Thiết Bị Phòng Này Không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string strThietBiXoa = "Select * From Trang_ThietBi Where MaSo='" + txtMaSo.Text.Trim() + "'";
                    SqlDataAdapter daThietBiXoa = new SqlDataAdapter(strThietBiXoa, clsDungChung.con);
                    DataSet dsThietBiXoa = new DataSet();
                    daThietBiXoa.Fill(dsThietBiXoa, "Trang_ThietBi");
                    DataTable tbThietBiXoa = dsThietBiXoa.Tables["Trang_ThietBi"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daThietBiXoa);
                    daThietBiXoa.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowThietBiXoa = tbThietBiXoa.Rows[0];
                    rowThietBiXoa.Delete();
                    daThietBiXoa.Update(dsThietBiXoa, "Trang_ThietBi");
                    tbThietBiXoa.Dispose();
                    dsThietBiXoa.Dispose();

                    daThietBiXoa.Dispose();
                }

                btnKhongLuu_Click(sender, e);
                HienTrangThietBi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }
    }
}
