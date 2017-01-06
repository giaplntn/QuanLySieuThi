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
    public partial class frmNguoiDungThemSua : Form
    {
        public frmNguoiDungThemSua()
        {
            InitializeComponent();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            string strLuuNhom = null;
            try
            {
                if (clsDungChung.bNguoiDungThem)
                {
                    if (txtUserName.Text.Equals(""))
                    {
                        MessageBox.Show("Giá Trị Tên Người Dùng Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUserName.Focus();
                        return;
                    }

                    if (txtPassWord1.Text != txtPassWord2.Text)
                    {
                        MessageBox.Show("Giá Trị Các ô Mật Khẩu Không Hợp Lệ, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPassWord2.Focus();
                        return;
                    }

                    if (cboNhom.Text.Equals(""))
                    {
                        MessageBox.Show("Giá Trị Nhóm Chưa Được Chọn, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboNhom.Focus();
                        return;
                    }

                    //Kiem tra xem Ma so loai phong toan tai hay chua
                    string strNguoiDung = "Select * From Nguoi_Dung Where UserName='" + txtUserName.Text.Trim() + "'";
                    SqlDataAdapter daNguoiDung = new SqlDataAdapter(strNguoiDung, clsDungChung.con);
                    DataSet dsNguoiDung = new DataSet();
                    daNguoiDung.Fill(dsNguoiDung, "Nguoi_Dung");
                    DataTable tbNguoiDung = dsNguoiDung.Tables["Nguoi_Dung"];
                    if (tbNguoiDung.DefaultView.Count > 0)
                    {
                        MessageBox.Show("Giá Trị Tên Người Dùng " + txtUserName.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUserName.Focus();
                        return;
                    }
                    tbNguoiDung.Dispose();
                    dsNguoiDung.Dispose();
                    daNguoiDung.Dispose();


                    string strNguoiDungThem = "Select * From Nguoi_Dung";
                    SqlDataAdapter daNguoiDungThem = new SqlDataAdapter(strNguoiDungThem, clsDungChung.con);
                    DataSet dsNguoiDungThem = new DataSet();
                    daNguoiDungThem.Fill(dsNguoiDungThem, "Nguoi_Dung");
                    DataTable tbNguoiDungThem = dsNguoiDungThem.Tables["Nguoi_Dung"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daNguoiDungThem);
                    daNguoiDungThem.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rwNguoiDungThem = tbNguoiDungThem.NewRow();
                    rwNguoiDungThem["UserName"] = txtUserName.Text.Trim();
                    clsDungChung c = new clsDungChung();
                    rwNguoiDungThem["PassWord"] = c.MaHoa(txtPassWord1.Text.Trim());
                    if (cboNhom.Text.IndexOf(".") == -1)
                        strLuuNhom = cboNhom.Text;
                    else
                        strLuuNhom = cboNhom.Text.Substring(0, cboNhom.Text.IndexOf("."));
                    rwNguoiDungThem["MaNhom"] = strLuuNhom;
                    tbNguoiDungThem.Rows.Add(rwNguoiDungThem);
                    daNguoiDungThem.Update(dsNguoiDungThem, "Nguoi_Dung");

                    tbNguoiDungThem.Dispose();
                    dsNguoiDungThem.Dispose();
                    daNguoiDungThem.Dispose();

                }

                if (clsDungChung.bNguoiDungSua)
                {
                    if (txtUserName.Text.Trim() != clsDungChung.strLuuNguoiDungUserName)
                    {
                        string strNguoiDung = "Select * From Nguoi_Dung Where UserName='" + txtUserName.Text.Trim() + "'";
                        SqlDataAdapter daNguoiDung = new SqlDataAdapter(strNguoiDung, clsDungChung.con);
                        DataSet dsNguoiDung = new DataSet();
                        daNguoiDung.Fill(dsNguoiDung, "Nguoi_Dung");
                        DataTable tbNguoiDung = dsNguoiDung.Tables["Nguoi_Dung"];
                        if (tbNguoiDung.DefaultView.Count > 0)
                        {
                            MessageBox.Show("Giá Trị Tên Người Dùng " + txtUserName.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtUserName.Focus();
                            return;
                        }
                        tbNguoiDung.Dispose();
                        dsNguoiDung.Dispose();
                        daNguoiDung.Dispose();
                    }

                    if (txtPassWord1.Text != txtPassWord2.Text)
                    {
                        MessageBox.Show("Giá Trị Các ô Mật Khẩu Không Hợp Lệ, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPassWord2.Focus();
                        return;
                    }

                    string strNguoiDungSua = "Select * From Nguoi_Dung Where UserName='" + clsDungChung.strLuuNguoiDungUserName + "'";
                    SqlDataAdapter daNguoiDungSua = new SqlDataAdapter(strNguoiDungSua, clsDungChung.con);
                    DataSet dsNguoiDungSua = new DataSet();
                    daNguoiDungSua.Fill(dsNguoiDungSua, "Nguoi_Dung");
                    DataTable tbNguoiDungSua = dsNguoiDungSua.Tables["Nguoi_Dung"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daNguoiDungSua);
                    daNguoiDungSua.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowNguoiDungSua = tbNguoiDungSua.Rows[0];
                    rowNguoiDungSua.BeginEdit();
                    rowNguoiDungSua["UserName"] = txtUserName.Text.Trim();
                    clsDungChung c = new clsDungChung();
                    rowNguoiDungSua["PassWord"] = c.MaHoa(txtPassWord1.Text.Trim());
                    if (cboNhom.Text.IndexOf(".") == -1)
                    {
                        strLuuNhom = cboNhom.Text;
                    }
                    else
                    {
                        strLuuNhom = cboNhom.Text.Substring(0, cboNhom.Text.IndexOf("."));
                    }

                    rowNguoiDungSua["MaNhom"] = strLuuNhom;
                    rowNguoiDungSua.EndEdit();
                    daNguoiDungSua.Update(dsNguoiDungSua, "Nguoi_Dung");

                    tbNguoiDungSua.Dispose();
                    dsNguoiDungSua.Dispose();
                    daNguoiDungSua.Dispose();
                }

                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HienNhom()
        {
            try
            {
                string strCmdNhom = "Select MaNhom,TenNhom From Nhom_Quyen";
                SqlDataAdapter daNhom = new SqlDataAdapter(strCmdNhom, clsDungChung.con);
                DataSet dsNhom = new DataSet();
                daNhom.Fill(dsNhom, "Nhom_Quyen");
                DataTable tbNhom = dsNhom.Tables["Nhom_Quyen"];
                foreach (DataRow r1 in tbNhom.Rows)
                {
                    cboNhom.Items.Add(r1["MaNhom"].ToString() + ". " + r1["TenNhom"].ToString());
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

        private void frmNguoiDungThemSua_Load(object sender, EventArgs e)
        {
            if (clsDungChung.bNguoiDungSua)
            {
                txtUserName.Text = clsDungChung.strLuuNguoiDungUserName;
                txtPassWord1.Text = "123456";
                txtPassWord2.Text = "123456";
                cboNhom.Text = clsDungChung.strLuuNguoiMaNhom; 
            }
            HienNhom();
        }

        private void cboNhom_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
