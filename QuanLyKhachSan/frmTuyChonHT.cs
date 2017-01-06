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
    public partial class frmTuyChonHT : Form
    {
        public frmTuyChonHT()
        {
            InitializeComponent();
        }

        private void frmTuyChonHT_Load(object sender, EventArgs e)
        {
            string SqlKS = "Select * From Tuy_Chon Where MaKS='01'";
            SqlDataAdapter daKS = new SqlDataAdapter(SqlKS, clsDungChung.con);
            DataSet dsKS = new DataSet();
            daKS.Fill(dsKS, "Tuy_Chon");
            DataTable tbKS = dsKS.Tables["Tuy_Chon"];
            if (tbKS.DefaultView.Count > 0)
            {
                DataRow row = tbKS.Rows[0];
                txtTenKS.Text = row["TenKS"].ToString();
                txtDiaChi.Text = row["DiaChi"].ToString();
                txtDienThoai.Text = row["DienThoai"].ToString();
                txtFax.Text = row["Fax"].ToString();
                txtMaSoThue.Text = row["MaThue"].ToString();
            }
            tbKS.Dispose();
            dsKS.Dispose();
            daKS.Dispose();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true; 
        }

        private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true; 
        }

        private void txtMaSoThue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true; 
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            try
            {
                string SqlKSSua = "Select * From Tuy_Chon Where MaKS='01'";
                SqlDataAdapter daKSSua = new SqlDataAdapter(SqlKSSua, clsDungChung.con);
                DataSet dsKSSua = new DataSet();
                SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daKSSua);
                daKSSua.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                daKSSua.Fill(dsKSSua, "Tuy_Chon");
                DataTable tbKSSua = dsKSSua.Tables["Tuy_Chon"];

                DataRow row1 = tbKSSua.Rows[0];
                row1.BeginEdit();
                row1["TenKS"] = txtTenKS.Text.Trim();
                row1["DiaChi"] = txtDiaChi.Text.Trim();
                row1["DienThoai"] = txtDienThoai.Text.Trim();
                row1["Fax"] = txtFax.Text.Trim();
                row1["MaThue"] = txtMaSoThue.Text.Trim();
                row1.EndEdit();
                daKSSua.Update(dsKSSua, "Tuy_Chon");

                tbKSSua.Dispose();
                dsKSSua.Dispose();
                daKSSua.Dispose();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }
    }
}
