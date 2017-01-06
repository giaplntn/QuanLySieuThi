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
    public partial class frmTraCuu : Form
    {
        private string strTimTheo = null;
        private int intDieuKien = 0;

        public frmTraCuu()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboTimTheo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboDieuKien_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void frmTraCuu_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadCMBTrangThai();
            btnXuLy.Visible = false;
            txtTinhTrang.Visible = false;
            txtTinhTrang.Text = "";
        }

        //thuc hien load data 
        public void LoadData()
        {
            //1. Load danh sach loai phong 
            
            DataTable dt = new DataTable();
            string strSqlTim = "SELECT * FROM Loai_Phong Order by LoaiPhong";
            SqlDataAdapter da = new SqlDataAdapter(strSqlTim, clsDungChung.con);
            da.Fill(dt);

            
            DataRow dr = dt.NewRow();
            dr["MaLoai"] = 0;
            dr["LoaiPhong"] = "ALL";
            dt.Rows.InsertAt(dr, 0);

            cmbLoaiPhong.DataSource = dt;
            cmbLoaiPhong.ValueMember = "MaLoai";
            cmbLoaiPhong.DisplayMember = "LoaiPhong";

        }
        public void LoadCMBTrangThai()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Ma", typeof(int));
            table.Columns.Add("Ten", typeof(string));

            table.Rows.Add(0, "ALL");
            table.Rows.Add(1, "Checkin");
            table.Rows.Add(2, "Booked");
            table.Rows.Add(3, "Avaiable");

            cmbTrangThai.DataSource = table;
            cmbTrangThai.ValueMember = "Ma";
            cmbTrangThai.DisplayMember = "Ten";
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            timKiem();
        }

        public void timKiem()
        {
            txtTinhTrang.Text = "";
            txtTinhTrang.Visible = false;
            btnXuLy.Visible = false;
            string sql = "SELECT  So_Phong.SoPhong, Loai_Phong.LoaiPhong,So_Phong.TinhTrang";
            sql += " FROM So_Phong ";
            sql += " INNER JOIN Loai_Phong ON Loai_Phong.MaLoai=So_Phong.MaLoai ";
            string cond = "WHERE 1=1";
            int loaiPhong = int.Parse(cmbLoaiPhong.SelectedValue.ToString());
            int trangThai = int.Parse(cmbTrangThai.SelectedValue.ToString());

            if (loaiPhong != 0)
                cond += " AND Loai_Phong.MaLoai=" + loaiPhong;
            switch (trangThai)
            {
                case 0:
                    break;
                case 1:
                    cond += " AND So_Phong.TinhTrang=2 ";
                    break;
                case 2:
                    cond += " AND So_Phong.TinhTrang=1 ";
                    break;
                case 3:
                    cond += " AND So_Phong.TinhTrang=0 ";
                    break;
            }


            string so_phong = txtPhong.Text.Trim();

            if (so_phong != "")
                cond += " AND So_Phong.SoPhong='" + so_phong + "'";

            string khach_hang = txtKhachHang.Text.Trim();

            if (khach_hang != "")
                cond += " AND So_Phong.TinhTrang!=0 ";
            sql += cond;
            //MessageBox.Show(sql);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, clsDungChung.con);
            da.Fill(dt);
            dt.Columns.Add("TrangThai", typeof(String));
            dt.Columns.Add("NgayDen", typeof(String));
            dt.Columns.Add("NgayDi", typeof(String));
            dt.Columns.Add("Khach", typeof(String));
            dt.Columns.Add("MaDK", typeof(String));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //voi moi dong co TinhTrang !=0 thi cho ket voi dang ky 
                if (int.Parse(dt.Rows[i]["TinhTrang"].ToString()) == 0)
                {
                    dt.Rows[i]["TrangThai"] = "Avaiable";
                }
                else
                {
                    string soPhong = dt.Rows[i]["SoPhong"].ToString();
                    sql = "SELECT Dang_Ky.*,Khach_Hang.HoTen FROM Dang_Ky INNER JOIN Khach_Hang ON Dang_Ky.MaKH=Khach_Hang.MaKH  WHERE Dang_Ky.SoPhong='" + soPhong + "' AND Dang_Ky.TrangThai!=4 AND Dang_Ky.TrangThai!=3 ";
                    //if (khach_hang != "")
                    // sql += " AND Khach_Hang.HoTen like '%" + khach_hang + "%'";
                    DataTable dt1 = new DataTable();
                    SqlDataAdapter da1 = new SqlDataAdapter(sql, clsDungChung.con);
                    da1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        dt.Rows[i]["NgayDen"] = dt1.Rows[0]["NgayDen"].ToString();
                        dt.Rows[i]["NgayDi"] = dt1.Rows[0]["NgayDi"].ToString();

                        if (int.Parse(dt1.Rows[0]["TrangThai"].ToString()) == 1)
                            dt.Rows[i]["TrangThai"] = "Booked";
                        if (int.Parse(dt1.Rows[0]["TrangThai"].ToString()) == 2)
                            dt.Rows[i]["TrangThai"] = "Checkin";
                        dt.Rows[i]["Khach"] = dt1.Rows[0]["HoTen"].ToString();
                        dt.Rows[i]["MaDK"] = dt1.Rows[0]["MaDK"].ToString();
                    }
                }
            }

            //loai bo dong dt co tinh trang =0 va khong co truong du lieu ho ten 

            dtgvPhong.DataSource = dt;

            dtgvPhong.Columns["LoaiPhong"].HeaderText = "Loại Phòng";
            dtgvPhong.Columns["SoPhong"].HeaderText = "Phòng";
            dtgvPhong.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dtgvPhong.Columns["NgayDen"].HeaderText = "Ngày đến";
            dtgvPhong.Columns["NgayDi"].HeaderText = "Ngày đi";
            dtgvPhong.Columns["Khach"].HeaderText = "Khách hàng";
            dtgvPhong.Columns["TinhTrang"].Visible = false;
            dtgvPhong.Columns["MaDK"].Visible = false;

            dtgvPhong.Columns["LoaiPhong"].Width = 100;
            dtgvPhong.Columns["SoPhong"].Width = 100;
            //dtgvPhong.Columns["TinhTrang"].Width = 100;
            dtgvPhong.ReadOnly = true;
            dtgvPhong.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            string str = "";

            dtgvPhong.RowHeadersWidth = 60;
            if (dtgvPhong.RowCount != 0)
            {
                for (int i = 0; i < dtgvPhong.RowCount; i++)
                {
                    str = string.Format("{0:00}", (i + 1));
                    dtgvPhong.Rows[i].HeaderCell.Value = str;
                }
            }
            // dtgvDanhSachDanToc.AutoSize = true;
            dtgvPhong.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            // dtgvDanhSachDanToc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgvPhong.BackgroundColor = this.BackColor;
            dtgvPhong.BorderStyle = BorderStyle.None;
            dtgvPhong.AllowUserToAddRows = false;
            dtgvPhong.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
        }
        private void dtgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dtgvPhong.CurrentRow !=null)
            {
                int index = dtgvPhong.CurrentRow.Index;
                int tinhtrang = int.Parse(dtgvPhong.Rows[index].Cells["TinhTrang"].Value.ToString());
                btnXuLy.Visible = true;
                txtTinhTrang.Text =tinhtrang.ToString();
                clsDungChung.strLuuSoPhong = dtgvPhong.Rows[index].Cells["SoPhong"].Value.ToString();
                if (tinhtrang==0)
                {
                    btnXuLy.Text = "Đặt phòng";
                }
                else if (tinhtrang==1)
                {
                    btnXuLy.Text = "Nhận phòng";
                    clsDungChung.strLuuMaDK = dtgvPhong.Rows[index].Cells["MaDK"].Value.ToString();
                }
                else
                {
                    btnXuLy.Text = "Trả phòng";
                    clsDungChung.strLuuMaDK = dtgvPhong.Rows[index].Cells["MaDK"].Value.ToString();
                }
            }
            
        }

        private void btnXuLy_Click(object sender, EventArgs e)
        {
            if(txtTinhTrang.Text!="")
            {
                int tinhtrang = int.Parse(txtTinhTrang.Text);
                if(tinhtrang==0)
                {
                    //truong hop new form dat phong moi 
                    frmDatNhanPhong frm = new frmDatNhanPhong();
                    frm.Show();
                }
                else if(tinhtrang==1)
                {
                    //MessageBox.Show(clsDungChung.strLuuMaDK);
                    string sql = "Update Dang_Ky set TrangThai=2 Where MaDK='" + clsDungChung.strLuuMaDK + "'";
                    SqlCommand cmd = new SqlCommand(sql, clsDungChung.con);
                    cmd.ExecuteNonQuery();
                    sql = "Update So_Phong set TinhTrang=2 Where SoPhong='"+clsDungChung.strLuuSoPhong+"'";
                    cmd = new SqlCommand(sql,clsDungChung.con);
                    cmd.ExecuteNonQuery();
                    
                    LoadData();
                    LoadCMBTrangThai();
                    btnXuLy.Visible = false;
                    txtTinhTrang.Visible = false;
                    txtTinhTrang.Text = "";
                    timKiem();
                }
                else
                {
                    //truong hop tra phong thi cap nhat trang thai = 4 
                    string sql = "Update Dang_Ky set TrangThai=4 Where MaDK='" + clsDungChung.strLuuMaDK + "'";
                    SqlCommand cmd = new SqlCommand(sql, clsDungChung.con);
                    cmd.ExecuteNonQuery();
                    sql = "Update So_Phong set TinhTrang=0 Where SoPhong='" + clsDungChung.strLuuSoPhong + "'";
                    cmd = new SqlCommand(sql, clsDungChung.con);
                    cmd.ExecuteNonQuery();

                    
                    LoadData();
                    LoadCMBTrangThai();
                    btnXuLy.Visible = false;
                    txtTinhTrang.Visible = false;
                    txtTinhTrang.Text = "";
                    timKiem();
                }
            }
        }
    }
}
