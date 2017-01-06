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
    public partial class frmRestore : Form
    {
        public frmRestore()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChonFile_Click(object sender, EventArgs e)
        {
            openDialog.Filter = "Backup File (*.Bak)|*.Bak|All File (*.*)|*.*";
            openDialog.FileName = "KhachSan08";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                txtDuongDan.Text = openDialog.FileName;
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtDuongDan.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Đường Dẫn Chứa File Cần Phục Hồi Chưa Được Chọn \n\r Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnChonFile.Focus();
                return;
            }

            SqlConnection thisConnection = new SqlConnection("server=(local);Integrated Security=SSPI;Persist Security Info=false;Initial Catalog=KhachSan08;Data Source=Localhost;uid=sa;pwd=");
            SqlCommand nonqueryCommand = thisConnection.CreateCommand();

            if (clsDungChung.con.State == ConnectionState.Open)
            {
                clsDungChung.con.Close();
                clsDungChung.con.Dispose();
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                SQLDMO._SQLServer srv = new SQLDMO.SQLServerClass();
                srv.Connect("(Local)","Sa","");
                SQLDMO.Restore res = new SQLDMO.RestoreClass();
                res.Devices = res.Files;
                res.Files = txtDuongDan.Text.Trim();
                res.Database ="KhachSan08";
                res.ReplaceDatabase = true;
                res.SQLRestore(srv);
                MessageBox.Show("Phục Hồi Thành Công", "Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(err.Message, "Error");
            }
        }
    }
}
