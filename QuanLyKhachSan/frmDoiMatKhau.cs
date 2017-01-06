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
    public partial class frmDoiMatKhau : Form
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            try
            {
                clsDungChung c = new clsDungChung();
                string strNguoiDung = "Select * From Nguoi_Dung Where UserName='" + clsDungChung.strTenLogin + "' And PassWord='" + c.MaHoa(txtMatKhauCu.Text.Trim()) + "'";
                SqlDataAdapter daNguoiDung = new SqlDataAdapter(strNguoiDung, clsDungChung.con);
                DataSet dsNguoiDung = new DataSet();
                daNguoiDung.Fill(dsNguoiDung, "Nguoi_Dung");
                DataTable tbNguoiDung = dsNguoiDung.Tables["Nguoi_Dung"];
                if (tbNguoiDung.DefaultView.Count > 0)
                {
                    if (txtMatKhauMoi.Text != txtNhapLai.Text)
                    {
                        MessageBox.Show("Giá Trị Mật Khẩu Mới Không Hợp Lệ, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtNhapLai.Focus();
                    }
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daNguoiDung);
                    daNguoiDung.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowNguoiDung = tbNguoiDung.Rows[0];
                    rowNguoiDung.BeginEdit();
                    rowNguoiDung["PassWord"] = c.MaHoa(txtMatKhauMoi.Text.Trim());
                    rowNguoiDung.EndEdit();
                    daNguoiDung.Update(dsNguoiDung, "Nguoi_Dung");
                    tbNguoiDung.Dispose();
                    dsNguoiDung.Dispose();
                    daNguoiDung.Dispose();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Giá Trị Mật Khẩu Cũ Không Hợp Lệ, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauCu.Focus();
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
    }
}
