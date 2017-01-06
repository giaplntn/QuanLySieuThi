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
    public partial class frmNhom : Form
    {
        public frmNhom()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            clsDungChung.bNhomThem = true;
            frmPhanNhom fPhanNhom = new frmPhanNhom();
            fPhanNhom.ShowDialog();
            clsDungChung.bNhomThem = false;
            txtMaNhom.Text = "";
            HienNhom();
        }

        private void HienNhom()
        {
            try
            {
                string strComdNhom = "Select MaNhom,TenNhom From Nhom_Quyen";
                SqlDataAdapter daNhom = new SqlDataAdapter(strComdNhom, clsDungChung.con);
                DataSet dsNhom = new DataSet();
                daNhom.Fill(dsNhom, "Nhom_Quyen");
                DataTable tbNhom = dsNhom.Tables["Nhom_Quyen"];
                lstvDs.Items.Clear();
                foreach (DataRow r1 in tbNhom.Rows)
                {
                    ListViewItem item = new ListViewItem(r1["MaNhom"].ToString());
                    item.SubItems.Add(r1["TenNhom"].ToString());
                    lstvDs.Items.Add(item);
                }
                tbNhom.Dispose();
                dsNhom.Dispose();
                daNhom.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void frmNhom_Load(object sender, EventArgs e)
        {
            HienNhom();
        }

        private void lstvDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item = lstvDs.FocusedItem;
            txtMaNhom.Text = item.Text;
            clsDungChung.strLuuMaNhom = item.Text;
            clsDungChung.strLuuTenNhom = item.SubItems[1].Text;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaNhom.Text == "")
            {
                MessageBox.Show("Giá Trị Mã Nhóm Chưa Được Chọn, Vui Lòng Kiểm Tra Lại..", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lstvDs.Focus();
                return;
            }

            clsDungChung.bNhomSua = true;
            frmPhanNhom fPhanNhom = new frmPhanNhom();
            fPhanNhom.ShowDialog();
            clsDungChung.bNhomSua = false;
            txtMaNhom.Text = "";
            HienNhom();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNhom.Text.Equals("01"))
            {
                MessageBox.Show("Giá Trị Mã Nhóm Giám Đốc Không Thể Xoá, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lstvDs.Focus();
                return;
            }

            if (txtMaNhom.Text == "")
            {
                MessageBox.Show("Giá Trị Mã Nhóm Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lstvDs.Focus();
                return;
            }
            try
            {
                string strNhomXoa = "Select * From Nhom_Quyen Where MaNhom='" + txtMaNhom.Text.Trim() + "'";
                SqlDataAdapter daNhomXoa = new SqlDataAdapter(strNhomXoa, clsDungChung.con);
                DataSet dsNhomXoa = new DataSet();
                daNhomXoa.Fill(dsNhomXoa, "Nhom_Quyen");
                DataTable tbNhomXoa = dsNhomXoa.Tables["Nhom_Quyen"];
                SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daNhomXoa);
                daNhomXoa.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rowNhomXoa = tbNhomXoa.Rows[0];
                if (MessageBox.Show("Bạn Có Chắc Chắn Muốn Xoá Nhóm Này Không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    rowNhomXoa.Delete();
                    daNhomXoa.Update(dsNhomXoa, "Nhom_Quyen");
                }
                tbNhomXoa.Dispose();
                dsNhomXoa.Dispose();
                daNhomXoa.Dispose();
                HienNhom();
                txtMaNhom.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }
    }
}
