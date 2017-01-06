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
    public partial class frmNhatKyLuuTru : Form
    {
        public frmNhatKyLuuTru()
        {
            InitializeComponent();
        }

        private void frmNhatKyLuuTru_Load(object sender, EventArgs e)
        {
            dPickNgayThang.Value = DateTime.Now;
            HienDs();
        }

        private void HienDs()
        {
            try
            {
                //MessageBox.Show(cboNam.Text.ToString()); 
                string strSqlTim = "SELECT * FROM Dang_Ky INNER JOIN Khach_Hang ON Dang_Ky.MaKH = Khach_Hang.MaKH INNER JOIN Hoa_Don ON Khach_Hang.MaKH = Hoa_Don.MaKH WHERE (Dang_Ky.TrangThai = 4) AND (dbo.Hoa_Don.NgayTT Between " + "CONVERT(DATETIME,'" + dPickNgayThang.Value.Year + "-" + dPickNgayThang.Value.Month + "-" + dPickNgayThang.Value.Day + " 00:00:00') And CONVERT(DATETIME,'" + +dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + " 00:00:00'))";
                //MessageBox.Show(strSqlTim); 
                SqlCommand cmd = new SqlCommand(strSqlTim, clsDungChung.con);
                SqlDataReader reader = cmd.ExecuteReader();

                lstvDs.Items.Clear();
                while (reader.Read())
                {
                    //MessageBox.Show(reader["SoPhong"].ToString()); 
                    ListViewItem item = new ListViewItem(Convert.ToDateTime(reader["NgayDen"]).ToShortDateString());
                    item.SubItems.Add(Convert.ToDateTime(reader["NgayTT"]).ToShortDateString());
                    item.SubItems.Add(reader["SoPhong"].ToString());
                    item.SubItems.Add(reader["HoTen"].ToString());
                    item.SubItems.Add(Convert.ToDateTime(reader["NgaySinh"]).ToShortDateString());
                    item.SubItems.Add(reader["NoiSinh"].ToString());
                    item.SubItems.Add(reader["DiaChi"].ToString());
                    item.SubItems.Add(reader["DienThoai"].ToString());
                    item.SubItems.Add(reader["CMND_PP"].ToString());
                    item.SubItems.Add(reader["QuocTich"].ToString());
                    lstvDs.Items.Add(item);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void dPickNgayThang_ValueChanged(object sender, EventArgs e)
        {
            HienDs();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            HienDs();
        }
    }
}
