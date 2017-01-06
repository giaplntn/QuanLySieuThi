using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient; 
using System.Text;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class frmThanhToan : Form
    {
        public frmThanhToan()
        {
            InitializeComponent();
        }

        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            try
            {
                clsCacHam h = new clsCacHam();
                txtMaKH.Text = clsDungChung.strLuuMaKH;
                txtMaDK.Text = clsDungChung.strLuuMaDK; 
                txtSoPhong.Text = clsDungChung.strLuuSoPhong;
                txtNgayDen.Text = clsDungChung.strLuuNgayDen.ToShortDateString();
                txtGioDen.Text = clsDungChung.strluuGioDen.ToShortTimeString();
                txtNgayTra.Text = Convert.ToDateTime(DateTime.Now).ToShortDateString();
                txtGioTra.Text = Convert.ToDateTime(DateTime.Now).ToShortTimeString();
                txtDuaTruoc.Text = clsDungChung.intDuaTruoc.ToString();
                TimeSpan s;
                DateTime a = Convert.ToDateTime(txtNgayTra.Text);
                DateTime b = Convert.ToDateTime(txtNgayDen.Text);
                s = a - b;
                int intLuuNgay = Convert.ToInt32(s.TotalDays);

                if (intLuuNgay == 0)
                    intLuuNgay = intLuuNgay + 1;

                txtSoNgayO.Text = intLuuNgay.ToString();

                string SqlPhong = "Select * From So_Phong Where SoPhong='" + txtSoPhong.Text.Trim() + "'";
                SqlDataAdapter daPhong = new SqlDataAdapter(SqlPhong, clsDungChung.con);
                DataSet dsPhong = new DataSet();
                daPhong.Fill(dsPhong, "So_Phong");
                DataTable tbPhong = dsPhong.Tables["So_Phong"];
                DataRow rowKH = tbPhong.Rows[0];
                string strLuuMaLoaiPhong = rowKH["MaLoai"].ToString();
                tbPhong.Dispose();
                dsPhong.Dispose();
                daPhong.Dispose();

                string SqlLoaiPhong = "Select * From Loai_Phong Where MaLoai='" + strLuuMaLoaiPhong.Trim() + "'";
                SqlDataAdapter daLoaiPhong = new SqlDataAdapter(SqlLoaiPhong, clsDungChung.con);
                DataSet dsLoaiPhong = new DataSet();
                daLoaiPhong.Fill(dsLoaiPhong, "Loai_Phong");
                DataTable tbLoaiPhong = dsLoaiPhong.Tables["Loai_Phong"];
                DataRow rowLP = tbLoaiPhong.Rows[0];
                txtDonGia.Text = Convert.ToInt32(rowLP["DonGia"]).ToString();
                tbLoaiPhong.Dispose();
                dsLoaiPhong.Dispose();
                daLoaiPhong.Dispose();


                txtTienTro.Text = (Int32.Parse(txtSoNgayO.Text) * Int32.Parse(h.loaidau(txtDonGia.Text))).ToString();

                string SqlSuDungDV = "Select * From SuDung_DichVu Where MaKH='" + txtMaKH.Text.Trim() + "'";
                SqlDataAdapter daSuDungDV = new SqlDataAdapter(SqlSuDungDV, clsDungChung.con);
                DataSet dsSuDungDV = new DataSet();
                daSuDungDV.Fill(dsSuDungDV, "SuDung_DichVu");
                DataTable tbSuDungDV = dsSuDungDV.Tables["SuDung_DichVu"];
                int intluuTienDV = 0;
                if (tbSuDungDV.DefaultView.Count > 0)
                {
                    foreach (DataRow r1 in tbSuDungDV.Rows)
                    {
                        intluuTienDV = intluuTienDV + Convert.ToInt32(r1["ThanhTien"]);
                    }
                }

                tbSuDungDV.Dispose();
                dsSuDungDV.Dispose();
                daSuDungDV.Dispose();
                txtTienDV.Text = intluuTienDV.ToString();

                string SqlDK = "Select * From Dang_Ky Where MaDK='" + txtMaDK.Text.Trim() + "'";
                SqlDataAdapter daDK = new SqlDataAdapter(SqlDK, clsDungChung.con);
                DataSet dsDK = new DataSet();
                daDK.Fill(dsDK, "Dang_Ky");
                DataTable tbDK = dsDK.Tables["Dang_Ky"];
                DataRow rowDK = tbDK.Rows[0];
                int intPhiDoiPhong = Convert.ToInt32(rowDK["PhiDoiPhong"]);
                tbDK.Dispose();
                dsDK.Dispose();
                daDK.Dispose();
                txtPhiDoiPhong.Text = intPhiDoiPhong.ToString();

                if (Int32.Parse(h.loaidau(txtTienTro.Text)) - Int32.Parse(h.loaidau(txtKhuyenMai.Text)) - Int32.Parse(h.loaidau(txtDuaTruoc.Text)) + Int32.Parse(h.loaidau(txtTienDV.Text)) + Int32.Parse(h.loaidau(txtPhiPhatSinh.Text)) + intPhiDoiPhong < 0)
                {
                    txtTongTien.Text = "0";
                }
                else
                {
                    txtTongTien.Text = Convert.ToString(Int32.Parse(h.loaidau(txtTienTro.Text)) - Int32.Parse(h.loaidau(txtKhuyenMai.Text)) - Int32.Parse(h.loaidau(txtDuaTruoc.Text)) + Int32.Parse(h.loaidau(txtTienDV.Text)) + Int32.Parse(h.loaidau(txtPhiPhatSinh.Text)) + intPhiDoiPhong);
                }
                txtTongTienChu.Text = h.docso(h.loaidau(txtTongTien.Text)) + "đồng";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }

        }

        private void txtMaKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtSoPhong_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtNgayDen_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtGioDen_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtSoNgayO_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtTienTro_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtKhuyenMai_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtTienDV_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtTongTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtTongTienChu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboKhuyenMai_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            if (txtDonGia.Text.Length > 0)
            {
                clsCacHam h = new clsCacHam();
                txtDonGia.Text = h.chendau(h.loaidau(txtDonGia.Text));
            }
        }

        private void txtKhuyenMai_TextChanged(object sender, EventArgs e)
        {
            if (txtKhuyenMai.Text.Length > 0)
            {
                clsCacHam h = new clsCacHam();
                txtKhuyenMai.Text = h.chendau(h.loaidau(txtKhuyenMai.Text));
            }
        }

        private void txtTienTro_TextChanged(object sender, EventArgs e)
        {
            if (txtTienTro.Text.Length > 0)
            {
                clsCacHam h = new clsCacHam();
                txtTienTro.Text = h.chendau(h.loaidau(txtTienTro.Text));
            }
        }

        private void txtTienDV_TextChanged(object sender, EventArgs e)
        {
            if (txtTienDV.Text.Length > 0)
            {
                clsCacHam h = new clsCacHam();
                txtTienDV.Text = h.chendau(h.loaidau(txtTienDV.Text));
            }
        }

        private void txtPhiPhatSinh_TextChanged(object sender, EventArgs e)
        {
            if (txtPhiPhatSinh.Text.Length > 0)
            {
                clsCacHam h = new clsCacHam();
                txtPhiPhatSinh.Text = h.chendau(h.loaidau(txtPhiPhatSinh.Text));
                txtPhiPhatSinh.SelectionStart = txtPhiPhatSinh.Text.Length + 1; 
            }
        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {
            if (txtTongTien.Text.Length > 0)
            {
                clsCacHam h = new clsCacHam();
                txtTongTien.Text = h.chendau(h.loaidau(txtTongTien.Text));
                txtTongTienChu.Text = h.docso(h.loaidau(txtTongTien.Text)) + "đồng"; 
            }
        }

        private void txtTongTienChu_TextChanged(object sender, EventArgs e)
        {
            //clsCacHam h=new clsCacHam();
            //txtTongTienChu.Text = h.docso(h.loaidau(txtTongTien.Text)); 
        }

        private void cboKhuyenMai_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsCacHam h = new clsCacHam();
            switch (cboKhuyenMai.SelectedIndex)
            {
                case 0:
                    txtKhuyenMai.Text = ((Int32.Parse(h.loaidau(txtTienTro.Text)) * 5) / 100).ToString();
                    break;
                case 1:
                    txtKhuyenMai.Text = ((Int32.Parse(h.loaidau(txtTienTro.Text)) * 10) / 100).ToString();
                    break;
                case 2:
                    txtKhuyenMai.Text = ((Int32.Parse(h.loaidau(txtTienTro.Text)) * 15) / 100).ToString();
                    break;
                case 3:
                    txtKhuyenMai.Text = ((Int32.Parse(h.loaidau(txtTienTro.Text)) * 20) / 100).ToString();
                    break;
                case 4:
                    txtKhuyenMai.Text = ((Int32.Parse(h.loaidau(txtTienTro.Text)) * 30) / 100).ToString();
                    break;
                case 5:
                    txtKhuyenMai.Text = ((Int32.Parse(h.loaidau(txtTienTro.Text)) * 50) / 100).ToString();
                    break;
                default:
                    txtKhuyenMai.Text = "0";
                    break;
            }

            if (Int32.Parse(h.loaidau(txtTienTro.Text)) - Int32.Parse(h.loaidau(txtKhuyenMai.Text)) - Int32.Parse(h.loaidau(txtDuaTruoc.Text)) + Int32.Parse(h.loaidau(txtTienDV.Text)) + Int32.Parse(h.loaidau(txtPhiPhatSinh.Text)) + Int32.Parse(h.loaidau(txtPhiDoiPhong.Text)) < 0)
            {
                txtTongTien.Text = "0";
            }
            else
            {
                txtTongTien.Text = Convert.ToString(Int32.Parse(h.loaidau(txtTienTro.Text)) - Int32.Parse(h.loaidau(txtKhuyenMai.Text)) - Int32.Parse(h.loaidau(txtDuaTruoc.Text)) + Int32.Parse(h.loaidau(txtTienDV.Text)) + Int32.Parse(h.loaidau(txtPhiPhatSinh.Text)) + Int32.Parse(h.loaidau(txtPhiDoiPhong.Text)));
            }
                   
            
        }

        private void btnCong_Click(object sender, EventArgs e)
        {
            if (txtPhiPhatSinh.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Phí Phát Sinh Không Thể Để Trống, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhiPhatSinh.Focus(); 
                return; 
            }
            clsCacHam h = new clsCacHam();
            txtTongTien.Text = Convert.ToString(Int32.Parse(h.loaidau(txtTienTro.Text)) - Int32.Parse(h.loaidau(txtKhuyenMai.Text)) - Int32.Parse(h.loaidau(txtDuaTruoc.Text)) + Int32.Parse(h.loaidau(txtTienDV.Text)) + Int32.Parse(h.loaidau(txtPhiPhatSinh.Text)) + Int32.Parse(h.loaidau(txtPhiDoiPhong.Text)));
            //txtTongTienChu.Text = h.docso(h.loaidau(txtTongTien.Text)) + "đồng"; 
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtPhiPhatSinh.Text.Equals(""))
            {
                MessageBox.Show("Giá Trị Phí Phát Sinh Không Thể Để Trống, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhiPhatSinh.Focus();
                return;
            }

            try
            {
                clsCacHam h = new clsCacHam();
                string sqlHoaDonThem = "Select * From Hoa_Don";
                SqlDataAdapter daHoaDonThem = new SqlDataAdapter(sqlHoaDonThem, clsDungChung.con);
                DataSet dsHoaDonThem = new DataSet();
                daHoaDonThem.Fill(dsHoaDonThem, "Hoa_Don");
                DataTable tbHoaDonThem = dsHoaDonThem.Tables["Hoa_Don"];
                SqlCommandBuilder cmdBuildKH = new SqlCommandBuilder(daHoaDonThem);
                daHoaDonThem.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rwHoaDonThem = tbHoaDonThem.NewRow();
                //rwHoaDonThem["MaHD"] = txtMaKH.Text;
                rwHoaDonThem["MaKH"] = txtMaKH.Text.Trim();
                rwHoaDonThem["NgayTT"] = txtNgayTra.Text.Trim();
                rwHoaDonThem["GioTT"] = txtGioTra.Text.Trim();
                rwHoaDonThem["SoNgayTro"] = txtSoNgayO.Text.Trim();
                rwHoaDonThem["TienPhong"] = h.loaidau(txtTienTro.Text.Trim());
                rwHoaDonThem["GiamGia"] = h.loaidau(txtKhuyenMai.Text.Trim());
                rwHoaDonThem["TienDV"] = h.loaidau(txtTienDV.Text.Trim());
                rwHoaDonThem["PhiDoiPhong"] = h.loaidau(txtPhiDoiPhong.Text.Trim());
                rwHoaDonThem["PhiKhac"] = h.loaidau(txtPhiPhatSinh.Text.Trim());
                rwHoaDonThem["TongTien"] = h.loaidau(txtTongTien.Text.Trim());
                rwHoaDonThem["TienChu"] = txtTongTienChu.Text.Trim();
                tbHoaDonThem.Rows.Add(rwHoaDonThem);
                daHoaDonThem.Update(dsHoaDonThem, "Hoa_Don");

                tbHoaDonThem.Dispose();
                dsHoaDonThem.Dispose();
                daHoaDonThem.Dispose();

                string sqlSoPhong = "Select * From So_Phong Where SoPhong='" + txtSoPhong.Text.Trim() + "'";
                SqlDataAdapter daSoPhong = new SqlDataAdapter(sqlSoPhong, clsDungChung.con);
                DataSet dsSoPhong = new DataSet();
                daSoPhong.Fill(dsSoPhong, "So_Phong");
                DataTable tbSoPhong = dsSoPhong.Tables["So_Phong"];
                SqlCommandBuilder cmdBuild1 = new SqlCommandBuilder(daSoPhong);
                daSoPhong.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rowSoPhongSua = tbSoPhong.Rows[0];
                rowSoPhongSua.BeginEdit();
                rowSoPhongSua["TinhTrang"] = 0;
                rowSoPhongSua.EndEdit();
                daSoPhong.Update(dsSoPhong, "So_Phong");

                tbSoPhong.Dispose();
                dsSoPhong.Dispose();
                daSoPhong.Dispose();

                string sqlDK = "Select * From Dang_Ky Where SoPhong='" + txtSoPhong.Text.Trim() + "' And TrangThai='02'";
                SqlDataAdapter daDK = new SqlDataAdapter(sqlDK, clsDungChung.con);
                DataSet dsDK = new DataSet();
                daDK.Fill(dsDK, "Dang_Ky");
                DataTable tbDK = dsDK.Tables["Dang_Ky"];
                SqlCommandBuilder cmdBuild2 = new SqlCommandBuilder(daDK);
                daDK.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataRow rowDKSua = tbDK.Rows[0];
                rowDKSua.BeginEdit();
                rowDKSua["TrangThai"] = 4;
                rowDKSua.EndEdit();
                daDK.Update(dsDK, "Dang_Ky");

                tbDK.Dispose();
                dsDK.Dispose();
                daDK.Dispose();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void txtDuaTruoc_TextChanged(object sender, EventArgs e)
        {
            if (txtDuaTruoc.Text.Length > 0)
            {
                clsCacHam h = new clsCacHam();
                txtDuaTruoc.Text = h.chendau(h.loaidau(txtDuaTruoc.Text));
            }
        }

        private void txtPhiPhatSinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            btnDongY_Click(sender, e);
            frmHoaDon fHoaDon = new frmHoaDon();
            fHoaDon.ShowDialog();
        }

        private void txtNgayTra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; 
        }

        private void txtGioTra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; 
        }

        private void txtDuaTruoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; 
        }

        private void txtDoiPhong_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtPhiDoiPhong_TextChanged(object sender, EventArgs e)
        {
             if (txtPhiDoiPhong.Text.Length > 0)
            {
                clsCacHam h = new clsCacHam();
                txtPhiDoiPhong.Text = h.chendau(h.loaidau(txtPhiDoiPhong.Text));
            }
        }
       
    }
}
