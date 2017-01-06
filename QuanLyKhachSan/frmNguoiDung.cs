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
    public partial class frmNguoiDung : Form
    {
        public frmNguoiDung()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            clsDungChung.bNguoiDungThem = true;
            frmNguoiDungThemSua fNguoiDungThemSua = new frmNguoiDungThemSua();
            fNguoiDungThemSua.ShowDialog();
            clsDungChung.bNguoiDungThem = false;
            txtUserName.Text = "";
            HienDsNguoiDung();
        }

        private void HienDsNguoiDung()
        {
            try
            {
                string strComdNguoiDung = "Select * From Nguoi_Dung";
                SqlDataAdapter daNguoiDung = new SqlDataAdapter(strComdNguoiDung, clsDungChung.con);
                DataSet dsNguoiDung = new DataSet();
                daNguoiDung.Fill(dsNguoiDung, "Nguoi_Dung");
                DataTable tbNguoiDung = dsNguoiDung.Tables["Nguoi_Dung"];
                lstvDs.Items.Clear();
                foreach (DataRow r1 in tbNguoiDung.Rows)
                {
                    ListViewItem item = new ListViewItem(r1["UserName"].ToString());
                    item.SubItems.Add(r1["MaNhom"].ToString());
                    lstvDs.Items.Add(item);
                }
                tbNguoiDung.Dispose();
                dsNguoiDung.Dispose();
                daNguoiDung.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void frmNguoiDung_Load(object sender, EventArgs e)
        {
            HienDsNguoiDung();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Giá Trị Tên Người Dùng Chưa Được Chọn, Vui Lòng Kiểm Tra Lại..", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lstvDs.Focus();
                return;
            }

            clsDungChung.bNguoiDungSua = true;
            frmNguoiDungThemSua fNguoiDungThemSua = new frmNguoiDungThemSua();
            fNguoiDungThemSua.ShowDialog();
            clsDungChung.bNguoiDungSua = false;
            txtUserName.Text = "";
            HienDsNguoiDung();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Equals(clsDungChung.strTenLogin.Trim()))
            {
                MessageBox.Show("Giá Trị Tên Người Dùng " + clsDungChung.strTenLogin.Trim() + " Không Thể Xoá, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lstvDs.Focus();
                return;
            }

            if (txtUserName.Text.Equals("Admin"))
            {
                MessageBox.Show("Giá Trị Tên Người Dùng Admin Không Thể Xoá, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lstvDs.Focus();
                return;
            }

            if (txtUserName.Text == "")
            {
                MessageBox.Show("Giá Trị Tên Người Dùng Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lstvDs.Focus();
                return;
            }
            try
            {
                string strNguoiDungXoa = "Select * From Nguoi_Dung Where UserName='" + txtUserName.Text.Trim() + "'";
                SqlDataAdapter daNguoiDungXoa = new SqlDataAdapter(strNguoiDungXoa, clsDungChung.con);
                DataSet dsNguoiDungXoa = new DataSet();
                daNguoiDungXoa.Fill(dsNguoiDungXoa, "Nguoi_Dung");
                DataTable tbNguoiDungXoa = dsNguoiDungXoa.Tables["Nguoi_Dung"];
                SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daNguoiDungXoa);
                daNguoiDungXoa.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rowNguoiDungXoa = tbNguoiDungXoa.Rows[0];
                if (MessageBox.Show("Bạn Có Chắc Chắn Muốn Xoá UserName Này Không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    rowNguoiDungXoa.Delete();
                    daNguoiDungXoa.Update(dsNguoiDungXoa, "Nguoi_Dung");
                }
                tbNguoiDungXoa.Dispose();
                dsNguoiDungXoa.Dispose();
                daNguoiDungXoa.Dispose();
                HienDsNguoiDung();
                txtUserName.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void lstvDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item = lstvDs.FocusedItem;
            clsDungChung.strLuuNguoiDungUserName = item.Text.Trim();
            clsDungChung.strLuuNguoiMaNhom = item.SubItems[1].Text.Trim();
            txtUserName.Text = item.Text;
        }
    }
}
