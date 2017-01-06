using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data.SqlClient; 
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMayChu_Click(object sender, EventArgs e)
        {
        //    frmMayChu fMayChu = new frmMayChu();
        //    fMayChu.ShowDialog();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            XmlTextReader read = new XmlTextReader(Application.StartupPath + "\\Setting.xml");
            read.MoveToContent();
            read.MoveToFirstAttribute();
            string[] mang = new string[4];
            int i = 0;
            mang[3] = "";
            while (read.Read())
            {
                if (read.HasValue)
                {
                    mang[i] = read.Value.ToString();
                    i++;
                }
            }
            read.Close();
            //txtMayChu.Text = mang[0].Trim();
            //txtTenCSDL.Text = mang[1].Trim();
            //txtNguoiDung.Text = mang[2].Trim();
            //txtMatKhau.Text = mang[3].Trim(); 

            string strCon = "Integrated Security=SSPI;Persist Security Info=false;Initial Catalog=" + mang[1].Trim() + ";Data Source=" + mang[0].Trim() + ";uid=" + mang[2].Trim() + ";pwd=" + mang[3].Trim();
            clsDungChung.con = new SqlConnection(strCon);

            try
            {   
                clsDungChung.con.Open();      
            }
            catch
            {
                MessageBox.Show("Không Thể Kết Nối Với CSDL SQL Server, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //MessageBox.Show("ban vua click dang nhap");
                clsDungChung c = new clsDungChung();
                string strNguoiDung = "Select * From Nguoi_Dung Where UserName='" + txtTenNguoiDung.Text.Trim() + "' And PassWord='" + txtMatKhau.Text.Trim() + "'";
                SqlDataAdapter daNguoiDung = new SqlDataAdapter(strNguoiDung, clsDungChung.con);
                DataSet dsNguoiDung = new DataSet();
                daNguoiDung.Fill(dsNguoiDung, "Nguoi_Dung");
                DataTable tbNguoiDung = dsNguoiDung.Tables["Nguoi_Dung"];
                if (tbNguoiDung.DefaultView.Count > 0)
                {
                    DataRow r1 = tbNguoiDung.Rows[0];
                    clsDungChung.strTenLogin = r1["UserName"].ToString().Trim();
                    clsDungChung.strMaNhom = r1["MaNhom"].ToString().Trim();
                    this.Hide();
                    frmMain fMain = new frmMain();
                    fMain.ShowDialog();
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("Tên Người Dùng Hoặc Mật Khẩu Không Đúng \r\n Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenNguoiDung.Focus();
                }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
