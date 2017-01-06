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
    public partial class frmPhanNhom : Form
    {
        public frmPhanNhom()
        {
            InitializeComponent();
        }

        private void frmPhanNhom_Load(object sender, EventArgs e)
        {
            if (clsDungChung.bNhomSua)
            {
                txtMaNhom.Text = clsDungChung.strLuuMaNhom;
                txtTenNhom.Text = clsDungChung.strLuuTenNhom;
                HienQuyen();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            try
            {
            if (clsDungChung.bNhomThem)
            {
                if (txtMaNhom.Text.Equals(""))
                {
                    MessageBox.Show("Giá Trị Mã Nhóm Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNhom.Focus();
                    return;
                }

                if (txtTenNhom.Text.Equals(""))
                {
                    MessageBox.Show("Giá Trị Tên Nhóm Chưa Được Nhập, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenNhom.Focus();
                    return;
                }
            
                    //Kiem tra xem Ma so Nhóm tồn tai hay chua
                    string strNhom = "Select MaNhom From Nhom_Quyen Where MaNhom='" + txtMaNhom.Text.Trim() + "'";
                    SqlDataAdapter daNhom = new SqlDataAdapter(strNhom, clsDungChung.con);
                    DataSet dsNhom = new DataSet();
                    daNhom.Fill(dsNhom, "Nhom_Quyen");
                    DataTable tbNhom = dsNhom.Tables["Nhom_Quyen"];
                    if (tbNhom.DefaultView.Count > 0)
                    {
                        MessageBox.Show("Giá Trị Mã Nhóm " + txtMaNhom.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaNhom.Focus();
                        return;
                    }
                    tbNhom.Dispose();
                    dsNhom.Dispose();
                    daNhom.Dispose();


                    string strNhomThem = "Select * From Nhom_Quyen";
                    SqlDataAdapter daNhomThem = new SqlDataAdapter(strNhomThem, clsDungChung.con);
                    DataSet dsNhomThem = new DataSet();
                    daNhomThem.Fill(dsNhomThem, "Nhom_Quyen");
                    DataTable tbNhomThem = dsNhomThem.Tables["Nhom_Quyen"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daNhomThem);
                    daNhomThem.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rwNhomThem = tbNhomThem.NewRow();
                    rwNhomThem["MaNhom"] = txtMaNhom.Text.Trim();
                    rwNhomThem["TenNhom"] = txtTenNhom.Text.Trim();
                    rwNhomThem["mnuNhomPhanQuyen"] = trvPhanQuyen.Nodes[1].Nodes[0].Checked;
                    rwNhomThem["mnuNguoiDung"] = trvPhanQuyen.Nodes[1].Nodes[1].Checked;
                    rwNhomThem["mnuTuyChon"] = trvPhanQuyen.Nodes[1].Nodes[2].Checked;
                    rwNhomThem["mnuCSDL"] = trvPhanQuyen.Nodes[1].Nodes[3].Checked;
                    rwNhomThem["mnuBackup"] = trvPhanQuyen.Nodes[1].Nodes[3].Nodes[0].Checked;
                    rwNhomThem["mnuRestore"] = trvPhanQuyen.Nodes[1].Nodes[3].Nodes[1].Checked;
                    rwNhomThem["mnuLoaiPhong"] = trvPhanQuyen.Nodes[2].Nodes[0].Checked;
                    rwNhomThem["mnuSoPhong"] = trvPhanQuyen.Nodes[2].Nodes[1].Checked;
                    rwNhomThem["mnuThietBi"] = trvPhanQuyen.Nodes[2].Nodes[2].Checked;
                    rwNhomThem["mnuDichVu"] = trvPhanQuyen.Nodes[2].Nodes[3].Checked;
                    rwNhomThem["mnuNgoaiTe"] = trvPhanQuyen.Nodes[2].Nodes[4].Checked;
                    rwNhomThem["mnuDoiTac"] = trvPhanQuyen.Nodes[2].Nodes[5].Checked;
                    rwNhomThem["mnuTrangTB"] = trvPhanQuyen.Nodes[2].Nodes[6].Checked;
                    rwNhomThem["mnuDangKy"] = trvPhanQuyen.Nodes[3].Nodes[0].Checked;
                    rwNhomThem["mnuNhanPhong"] = trvPhanQuyen.Nodes[3].Nodes[1].Checked;
                    rwNhomThem["mnuHuyDK"] = trvPhanQuyen.Nodes[3].Nodes[2].Checked;
                    rwNhomThem["mnuCapNhap"] = trvPhanQuyen.Nodes[3].Nodes[3].Checked;
                    rwNhomThem["mnuDoiPhong"] = trvPhanQuyen.Nodes[3].Nodes[4].Checked;
                    rwNhomThem["mnuThanhToan"] = trvPhanQuyen.Nodes[3].Nodes[5].Checked;
                    rwNhomThem["mnuSuDungDV"] = trvPhanQuyen.Nodes[3].Nodes[6].Checked;
                    rwNhomThem["mnuDoanhThuPhong"] = trvPhanQuyen.Nodes[4].Nodes[0].Checked;
                    rwNhomThem["mnuDoanhThuDV"] = trvPhanQuyen.Nodes[4].Nodes[1].Checked;
                    rwNhomThem["mnuTongDoanhThu"] = trvPhanQuyen.Nodes[4].Nodes[2].Checked;
                    rwNhomThem["mnuHieuSuatPhong"] = trvPhanQuyen.Nodes[4].Nodes[3].Checked;
                    rwNhomThem["mnuThongKeKhach"] = trvPhanQuyen.Nodes[4].Nodes[4].Checked;
                    rwNhomThem["mnuThongKeKhachHuy"] = trvPhanQuyen.Nodes[4].Nodes[5].Checked;
                    tbNhomThem.Rows.Add(rwNhomThem);
                    daNhomThem.Update(dsNhomThem, "Nhom_Quyen");

                    tbNhomThem.Dispose();
                    dsNhomThem.Dispose();
                    daNhomThem.Dispose();

                }

                if (clsDungChung.bNhomSua)
                {
                    if (txtMaNhom.Text.Trim() != clsDungChung.strLuuMaNhom)
                    {
                        //Kiem tra xem Ma so Nhóm tồn tai hay chua
                        string strNhom = "Select MaNhom From Nhom_Quyen Where MaNhom='" + txtMaNhom.Text.Trim() + "'";
                        SqlDataAdapter daNhom = new SqlDataAdapter(strNhom, clsDungChung.con);
                        DataSet dsNhom = new DataSet();
                        daNhom.Fill(dsNhom, "Nhom_Quyen");
                        DataTable tbNhom = dsNhom.Tables["Nhom_Quyen"];
                        if (tbNhom.DefaultView.Count > 0)
                        {
                            MessageBox.Show("Giá Trị Mã Nhóm " + txtMaNhom.Text + " Đã Tồn Tại, Vui Lòng Kiểm Tra Lại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMaNhom.Focus();
                            return;
                        }
                        tbNhom.Dispose();
                        dsNhom.Dispose();
                        daNhom.Dispose();
                    }

                    string strNhomSua = "Select * From Nhom_Quyen Where MaNhom='" + clsDungChung.strLuuMaNhom + "'";
                    SqlDataAdapter daNhomSua = new SqlDataAdapter(strNhomSua, clsDungChung.con);
                    DataSet dsNhomSua = new DataSet();
                    daNhomSua.Fill(dsNhomSua, "Nhom_Quyen");
                    DataTable tbNhomSua = dsNhomSua.Tables["Nhom_Quyen"];
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(daNhomSua);
                    daNhomSua.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataRow rowNhomSua = tbNhomSua.Rows[0];
                    rowNhomSua.BeginEdit();
                    rowNhomSua["MaNhom"] = txtMaNhom.Text.Trim();
                    rowNhomSua["TenNhom"] = txtTenNhom.Text.Trim();
                    rowNhomSua["mnuNhomPhanQuyen"] = trvPhanQuyen.Nodes[1].Nodes[0].Checked;
                    rowNhomSua["mnuNguoiDung"] = trvPhanQuyen.Nodes[1].Nodes[1].Checked;
                    rowNhomSua["mnuTuyChon"] = trvPhanQuyen.Nodes[1].Nodes[2].Checked;
                    rowNhomSua["mnuCSDL"] = trvPhanQuyen.Nodes[1].Nodes[3].Checked;
                    rowNhomSua["mnuBackup"] = trvPhanQuyen.Nodes[1].Nodes[3].Nodes[0].Checked;
                    rowNhomSua["mnuRestore"] = trvPhanQuyen.Nodes[1].Nodes[3].Nodes[1].Checked;
                    rowNhomSua["mnuLoaiPhong"] = trvPhanQuyen.Nodes[2].Nodes[0].Checked;
                    rowNhomSua["mnuSoPhong"] = trvPhanQuyen.Nodes[2].Nodes[1].Checked;
                    rowNhomSua["mnuThietBi"] = trvPhanQuyen.Nodes[2].Nodes[2].Checked;
                    rowNhomSua["mnuDichVu"] = trvPhanQuyen.Nodes[2].Nodes[3].Checked;
                    rowNhomSua["mnuNgoaiTe"] = trvPhanQuyen.Nodes[2].Nodes[4].Checked;
                    rowNhomSua["mnuDoiTac"] = trvPhanQuyen.Nodes[2].Nodes[5].Checked;
                    rowNhomSua["mnuTrangTB"] = trvPhanQuyen.Nodes[2].Nodes[6].Checked;
                    rowNhomSua["mnuDangKy"] = trvPhanQuyen.Nodes[3].Nodes[0].Checked;
                    rowNhomSua["mnuNhanPhong"] = trvPhanQuyen.Nodes[3].Nodes[1].Checked;
                    rowNhomSua["mnuHuyDK"] = trvPhanQuyen.Nodes[3].Nodes[2].Checked;
                    rowNhomSua["mnuCapNhap"] = trvPhanQuyen.Nodes[3].Nodes[3].Checked;
                    rowNhomSua["mnuDoiPhong"] = trvPhanQuyen.Nodes[3].Nodes[4].Checked;
                    rowNhomSua["mnuThanhToan"] = trvPhanQuyen.Nodes[3].Nodes[5].Checked;
                    rowNhomSua["mnuSuDungDV"] = trvPhanQuyen.Nodes[3].Nodes[6].Checked;
                    rowNhomSua["mnuDoanhThuPhong"] = trvPhanQuyen.Nodes[4].Nodes[0].Checked;
                    rowNhomSua["mnuDoanhThuDV"] = trvPhanQuyen.Nodes[4].Nodes[1].Checked;
                    rowNhomSua["mnuTongDoanhThu"] = trvPhanQuyen.Nodes[4].Nodes[2].Checked;
                    rowNhomSua["mnuHieuSuatPhong"] = trvPhanQuyen.Nodes[4].Nodes[3].Checked;
                    rowNhomSua["mnuThongKeKhach"] = trvPhanQuyen.Nodes[4].Nodes[4].Checked;
                    rowNhomSua["mnuThongKeKhachHuy"] = trvPhanQuyen.Nodes[4].Nodes[5].Checked;
                    rowNhomSua.EndEdit();
                    daNhomSua.Update(dsNhomSua, "Nhom_Quyen");

                    tbNhomSua.Dispose();
                    dsNhomSua.Dispose();
                    daNhomSua.Dispose();
                }
            
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void HienQuyen()
        {
            try
            {
                string strNhom = "Select * From Nhom_Quyen Where MaNhom='" + txtMaNhom.Text.Trim() + "'";
                SqlDataAdapter daNhom = new SqlDataAdapter(strNhom, clsDungChung.con);
                DataSet dsNhom = new DataSet();
                daNhom.Fill(dsNhom, "Nhom_Quyen");
                DataTable tbNhom = dsNhom.Tables["Nhom_Quyen"];
                if (tbNhom.DefaultView.Count > 0)
                {
                    DataRow row = tbNhom.Rows[0];
                    trvPhanQuyen.Nodes[1].Nodes[0].Checked = Convert.ToBoolean(row["mnuNhomPhanQuyen"]);
                    trvPhanQuyen.Nodes[1].Nodes[1].Checked = Convert.ToBoolean(row["mnuNguoiDung"]);
                    trvPhanQuyen.Nodes[1].Nodes[2].Checked = Convert.ToBoolean(row["mnuTuyChon"]);
                    trvPhanQuyen.Nodes[1].Nodes[3].Checked = Convert.ToBoolean(row["mnuCSDL"]);
                    trvPhanQuyen.Nodes[1].Nodes[3].Nodes[0].Checked = Convert.ToBoolean(row["mnuBackup"]);
                    trvPhanQuyen.Nodes[1].Nodes[3].Nodes[1].Checked = Convert.ToBoolean(row["mnuRestore"]);
                    trvPhanQuyen.Nodes[2].Nodes[0].Checked = Convert.ToBoolean(row["mnuLoaiPhong"]);
                    trvPhanQuyen.Nodes[2].Nodes[1].Checked = Convert.ToBoolean(row["mnuSoPhong"]);
                    trvPhanQuyen.Nodes[2].Nodes[2].Checked = Convert.ToBoolean(row["mnuThietBi"]);
                    trvPhanQuyen.Nodes[2].Nodes[3].Checked = Convert.ToBoolean(row["mnuDichVu"]);
                    trvPhanQuyen.Nodes[2].Nodes[4].Checked = Convert.ToBoolean(row["mnuNgoaiTe"]);
                    trvPhanQuyen.Nodes[2].Nodes[5].Checked = Convert.ToBoolean(row["mnuDoiTac"]);
                    trvPhanQuyen.Nodes[2].Nodes[6].Checked = Convert.ToBoolean(row["mnuTrangTB"]);
                    trvPhanQuyen.Nodes[3].Nodes[0].Checked = Convert.ToBoolean(row["mnuDangKy"]);
                    trvPhanQuyen.Nodes[3].Nodes[1].Checked = Convert.ToBoolean(row["mnuNhanPhong"]);
                    trvPhanQuyen.Nodes[3].Nodes[2].Checked = Convert.ToBoolean(row["mnuHuyDK"]);
                    trvPhanQuyen.Nodes[3].Nodes[3].Checked = Convert.ToBoolean(row["mnuCapNhap"]);
                    trvPhanQuyen.Nodes[3].Nodes[4].Checked = Convert.ToBoolean(row["mnuDoiPhong"]);
                    trvPhanQuyen.Nodes[3].Nodes[5].Checked = Convert.ToBoolean(row["mnuThanhToan"]);
                    trvPhanQuyen.Nodes[3].Nodes[6].Checked = Convert.ToBoolean(row["mnuSuDungDV"]);
                    trvPhanQuyen.Nodes[4].Nodes[0].Checked = Convert.ToBoolean(row["mnuDoanhThuPhong"]);
                    trvPhanQuyen.Nodes[4].Nodes[1].Checked = Convert.ToBoolean(row["mnuDoanhThuDV"]);
                    trvPhanQuyen.Nodes[4].Nodes[2].Checked = Convert.ToBoolean(row["mnuTongDoanhThu"]);
                    trvPhanQuyen.Nodes[4].Nodes[3].Checked = Convert.ToBoolean(row["mnuHieuSuatPhong"]);
                    trvPhanQuyen.Nodes[4].Nodes[4].Checked = Convert.ToBoolean(row["mnuThongKeKhach"]);
                    trvPhanQuyen.Nodes[4].Nodes[5].Checked = Convert.ToBoolean(row["mnuThongKeKhachHuy"]);
                }
                tbNhom.Dispose();
                dsNhom.Dispose();
                daNhom.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }
    }
}
