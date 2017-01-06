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
    public partial class frmBackup : Form
    {
        public frmBackup()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

 
        private void txtDuongDan_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnChonThuMuc_Click(object sender, EventArgs e)
        {
            saveDialog.Filter = "Backup File (*.Bak)|*.Bak|All File (*.*)|*.*";
            saveDialog.FileName = "KhachSan08";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                txtDuongDan.Text = saveDialog.FileName;
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtDuongDan.Text.Equals("") )
            {
                MessageBox.Show("Giá Trị Đường Dẫn Chứa File Sao Lưu Chưa Được Chọn \n\r Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnChonThuMuc.Focus(); 
                return;
            }

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

         SqlConnection thisConnection = new SqlConnection("Integrated Security=SSPI;Persist Security Info=false;Initial Catalog=" + mang[1].Trim() + ";Data Source=" + mang[0].Trim() + ";uid=" + mang[2].Trim() + ";pwd=" + mang[3].Trim());
         SqlCommand nonqueryCommand = thisConnection.CreateCommand();

         try 
         {
            thisConnection.Open();
            nonqueryCommand.CommandText = "BACKUP DATABASE KhachSan08 TO DISK = '" + txtDuongDan.Text.Trim() + "' WITH NOFORMAT,NOINIT,SKIP,STATS = 10";
            nonqueryCommand.ExecuteNonQuery() ;
            for (int j = 1; j < 100; j++)
            {
                progressBar1.Value = j;
            }
            MessageBox.Show("Sao Lưu Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);   
         } 
         catch (Exception ex) 
         {
             MessageBox.Show("Lỗi : " + ex.Message, "Thông Báo"); 
         } 
         finally 
         {  
            thisConnection.Close();
         }


        }

  
    }
}
