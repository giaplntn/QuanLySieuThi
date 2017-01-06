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
    public partial class frmThongTinPhong : Form
    {
        public frmThongTinPhong()
        {
            InitializeComponent();
        }

        private void frmThongTinPhong_Load(object sender, EventArgs e)
        {
            txtSoPhong.Text = clsDungChung.strLuuSoPhong;
            HienTrangThietBi();
        }

        private void HienTrangThietBi()
        {
            try
            {
                string strCmdTrangThietBi = "Select * From Trang_ThietBi Where SoPhong='" + txtSoPhong.Text.Trim() + "'";
                SqlDataAdapter daTrangThietBi = new SqlDataAdapter(strCmdTrangThietBi, clsDungChung.con);
                DataSet dsTrangThietBi = new DataSet();
                daTrangThietBi.Fill(dsTrangThietBi, "Trang_ThietBi");
                DataTable tbTrangThietBi = dsTrangThietBi.Tables["Trang_ThietBi"];
                lstvDs.Items.Clear();
                foreach (DataRow r1 in tbTrangThietBi.Rows)
                {

                    string SqlTenTB = "Select * From Thiet_Bi Where MaTB='" + r1["MaTB"].ToString().Trim() + "'";
                    SqlDataAdapter daTenTB = new SqlDataAdapter(SqlTenTB, clsDungChung.con);
                    DataSet dsTenTB = new DataSet();
                    daTenTB.Fill(dsTenTB, "Thiet_Bi");
                    DataTable tbTenTB = dsTenTB.Tables["Thiet_Bi"];
                    DataRow row = tbTenTB.Rows[0];
                    ListViewItem item = new ListViewItem(row["TenTB"].ToString().Trim());
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

        private void lstvDs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
