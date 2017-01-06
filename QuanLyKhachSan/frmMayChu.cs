using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Text;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class frmMayChu : Form
    {
        public frmMayChu()
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
                c.TaoFileSetting(Application.StartupPath + "\\Setting.xml", txtMayChu.Text.Trim(), txtTenCSDL.Text.Trim(), txtNguoiDung.Text.Trim(), txtMatKhau.Text.Trim());
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void frmMayChu_Load(object sender, EventArgs e)
        {
            try
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
                txtMayChu.Text = mang[0].Trim();
                txtTenCSDL.Text = mang[1].Trim();
                txtNguoiDung.Text = mang[2].Trim();
                txtMatKhau.Text = mang[3].Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }
    }
}
