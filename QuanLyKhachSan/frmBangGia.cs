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
    public partial class frmBangGia : Form
    {
        public frmBangGia()
        {
            InitializeComponent();
        }

        private void frmBangGia_Load(object sender, EventArgs e)
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
                    ListViewItem item = new ListViewItem(r1["LoaiPhong"].ToString().Trim());
                    item.SubItems.Add(h.chendau((Convert.ToInt32(r1["DonGia"])).ToString()));
                    lstvDs.Items.Add(item);

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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
