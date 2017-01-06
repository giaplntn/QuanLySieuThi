using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TD.SandDock;
using System.Diagnostics;
using System.Data.SqlClient;
using TD.SandDock.Rendering;

namespace QuanLyKhachSan
{
    public partial class frmMain : Form
    {
        //private DockControl lastActivatedWindow;

        private int intDem = 0;
        private int intSoPhongTrong = 0;
        private int intSoDangKy = 0;
        private int intDangTro = 0;

        public frmMain()
        {
            InitializeComponent();
        }

        private void mnuSandSoDo_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            Guid windowID = new Guid((string)item.Tag);

            // Find the window by its Guid, and activate it
            DockControl window = sandDockManager1.FindControl(windowID);
            if (window != null)
                window.Open(WindowOpenMethod.OnScreenActivate);
        }

        private void mnuSandTyGia_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            Guid windowID = new Guid((string)item.Tag);

            // Find the window by its Guid, and activate it
            DockControl window = sandDockManager1.FindControl(windowID);
            if (window != null)
                window.Open(WindowOpenMethod.OnScreenActivate);
        }

        private void mnuSandThongTinPhong_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            Guid windowID = new Guid((string)item.Tag);

            // Find the window by its Guid, and activate it
            DockControl window = sandDockManager1.FindControl(windowID);
            if (window != null)
                window.Open(WindowOpenMethod.OnScreenActivate);
        }

        private void mnuDangNhap_Click(object sender, EventArgs e)
        {
            frmDangNhapTrong fDangNhapTrong = new frmDangNhapTrong();
            fDangNhapTrong.ShowDialog();
            HienLaiTatCa();
            frmMain_Load(sender, e);       
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuDoiMatKhau_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau fDoiMatKhau = new frmDoiMatKhau();
            fDoiMatKhau.ShowDialog();
        }

        private void mnuTacGia_Click(object sender, EventArgs e)
        {
            //frmTacGia fTacGia = new frmTacGia();
            //fTacGia.ShowDialog();
        }

        private string NgayThang()
        {
            string strThu="";
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday :
                    strThu = "Thứ Hai";
                    break;
                case DayOfWeek.Tuesday:
                    strThu = "Thứ Ba";
                    break;
                case DayOfWeek.Wednesday:
                    strThu = "Thứ Tư";
                    break;
                case DayOfWeek.Thursday:
                    strThu = "Thứ Năm";
                    break;
                case DayOfWeek.Friday:
                    strThu = "Thứ Sáu";
                    break;
                case DayOfWeek.Saturday:
                    strThu = "Thứ Bảy";
                    break;
                case DayOfWeek.Sunday:
                    strThu = "Chủ Nhật";
                    break;
            }
            return strThu + ", Ngày " + DateTime.Now.Day + " Tháng " + DateTime.Now.Month + " Năm " + DateTime.Now.Year;     
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            statusNgayThang.Text = NgayThang();
            statusGio.Text = DateTime.Now.ToLongTimeString();
            string strChuoi = "Quản Lý Khách Sạn 2008 - Copyright by Nhóm 8 ";
            statusMain.Text = statusMain.Text + strChuoi.Substring(intDem, 1);
            intDem = intDem + 1;
            if (intDem == strChuoi.Length)
            {
                intDem = 0;
                statusMain.Text = ""; 
            }
        }

        private void mnuSaoLuu_Click(object sender, EventArgs e)
        {
            frmBackup fBackup = new frmBackup();
            fBackup.ShowDialog(); 
        }

        private void mnuNguoiDung_Click(object sender, EventArgs e)
        {
            frmNguoiDung fNguoiDung =new frmNguoiDung(); 
            fNguoiDung.ShowDialog();
        }

        private void mnuTaoNhom_Click(object sender, EventArgs e)
        {
            frmNhom fNhom = new frmNhom();
            fNhom.ShowDialog();
        }

        private void mnuPhucHoi_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\Restore.exe");
            Application.Exit();
        }

        private void mnuTrangChu_Click(object sender, EventArgs e)
        {
            //Process.Start(""); 
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            taoSoDo();
            PhanQuyen();
            HienChu();
            HienTyGia();
            statusNguoiDung.Text = "Người Dùng : " + clsDungChung.strTenLogin; 
        }

        private void HienChu()
        {
            Label a1 = new Label();
            Label a2 = new Label();
            a1.Text = "Khaùch Saïn 2016";
            a2.Text = "Quản Lý";
            a1.Font = new System.Drawing.Font("VNI-HLThuphap", 38F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            a2.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           // a1.BackColor = Color.Transparent;
           // a2.BackColor = Color.Transparent;
            a1.Size = new Size(800, 120);
            a2.Size = new Size(150, 30);
            a1.ForeColor = Color.Silver;
            a2.ForeColor = Color.Silver;
            a1.Location = new Point(60, 40);
            a2.Location = new Point(5, 15);
            pictureBox1.Controls.Add(a2);
            pictureBox1.Controls.Add(a1);
        }

        private void taoSoDo()
        {
            try
            {
                int intDemNut = 0;
                int intLuuHinh = 2;
                intSoPhongTrong = 0;
                intSoDangKy = 0;
                intDangTro = 0;
                trvSoDo.Nodes.Clear();
                trvSoDo.Nodes.Add("Các Loại Phòng Khách");
                trvSoDo.Nodes[0].ImageIndex = 0;
                trvSoDo.Nodes[0].SelectedImageIndex = 0;
                string strComdLoaiPhong = "Select MaLoai,LoaiPhong From Loai_Phong";
                SqlDataAdapter daLoaiPhong = new SqlDataAdapter(strComdLoaiPhong, clsDungChung.con);
                DataSet dsLoaiPhong = new DataSet();
                daLoaiPhong.Fill(dsLoaiPhong, "Loai_Phong");
                DataTable tbLoaiPhong = dsLoaiPhong.Tables["Loai_Phong"];
                foreach (DataRow r1 in tbLoaiPhong.Rows)
                {
                    trvSoDo.Nodes[0].Nodes.Add(r1["LoaiPhong"].ToString().Trim());
                    trvSoDo.Nodes[0].Nodes[intDemNut].ImageIndex = 1;
                    trvSoDo.Nodes[0].Nodes[intDemNut].SelectedImageIndex = 1;

                    string strComdSoPhong = "Select SoPhong,MaLoai,TinhTrang From So_Phong Where MaLoai='" + r1["MaLoai"].ToString().Trim() + "'";
                    SqlDataAdapter daSoPhong = new SqlDataAdapter(strComdSoPhong, clsDungChung.con);
                    DataSet dsSoPhong = new DataSet();
                    daSoPhong.Fill(dsSoPhong, "So_Phong");
                    DataTable tbSoPhong = dsSoPhong.Tables["So_Phong"];
                    for (int i = 0; i < tbSoPhong.DefaultView.Count; i++)
                    {
                        switch (tbSoPhong.DefaultView[i].Row["TinhTrang"].ToString().Trim())
                        {
                            case "0":
                                intLuuHinh = 2;
                                intSoPhongTrong++;
                                break;
                            case "1":
                                intLuuHinh = 3;
                                intSoDangKy++;
                                break;
                            case "2":
                                intLuuHinh = 4;
                                intDangTro++;
                                break;
                        }

                        trvSoDo.Nodes[0].Nodes[intDemNut].Nodes.Add(tbSoPhong.DefaultView[i].Row["SoPhong"].ToString().Trim());
                        trvSoDo.Nodes[0].Nodes[intDemNut].Nodes[i].ImageIndex = intLuuHinh;
                        trvSoDo.Nodes[0].Nodes[intDemNut].Nodes[i].SelectedImageIndex = intLuuHinh;
                        trvSoDo.Nodes[0].Nodes[intDemNut].Nodes[i].Tag = "SoPhong";
                    }
                    dsSoPhong.Dispose();
                    daSoPhong.Dispose();
                    trvSoDo.Nodes[0].Nodes[intDemNut].Expand();
                    intDemNut++;
                }
                dsLoaiPhong.Dispose();
                daLoaiPhong.Dispose();
                lblPhongTrong.Text = "(" + intSoPhongTrong + ")";
                lblDatPhong.Text = "(" + intSoDangKy + ")";
                lblKhachTro.Text = "(" + intDangTro + ")";
                trvSoDo.Nodes[0].Expand();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void mnuLoaiPhong_Click(object sender, EventArgs e)
        {
            frmLoaiPhong fLoaiPhong = new frmLoaiPhong();
            fLoaiPhong.ShowDialog();
            taoSoDo();
        }

        private void mnuDsPhong_Click(object sender, EventArgs e)
        {
            frmDsPhong fDsPhong = new frmDsPhong();
            fDsPhong.ShowDialog();
            taoSoDo();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void mnuTyGia_Click(object sender, EventArgs e)
        {
            frmTyGiaNgoaiTe fTyGia = new frmTyGiaNgoaiTe();
            fTyGia.ShowDialog();
            HienTyGia();
        }

        private void HienTyGia()
        {
            try
            {
                clsCacHam h = new clsCacHam();
                string strCmdTyGia = "Select * From Ngoai_Te";
                SqlDataAdapter daTyGia = new SqlDataAdapter(strCmdTyGia, clsDungChung.con);
                DataSet dsTyGia = new DataSet();
                daTyGia.Fill(dsTyGia, "Ngoai_Te");
                DataTable tbTyGia = dsTyGia.Tables["Ngoai_Te"];
                lstvTyGia.Items.Clear();
                foreach (DataRow r1 in tbTyGia.Rows)
                {
                    ListViewItem item = new ListViewItem(r1["MaNT"].ToString().Trim());
                    item.SubItems.Add(h.chendau((Convert.ToInt32(r1["TyGia"])).ToString()));
                    lstvTyGia.Items.Add(item);

                }
                tbTyGia.Dispose();
                dsTyGia.Dispose();
                daTyGia.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Thông Báo");
            }
        }

        private void mnuThietBi_Click(object sender, EventArgs e)
        {
            frmDsThietBi fThietBi = new frmDsThietBi();
            fThietBi.ShowDialog(); 
        }

        private void mnuDichVu_Click(object sender, EventArgs e)
        {
            frmDsDichVu fdsDichVu = new frmDsDichVu();
            fdsDichVu.ShowDialog();
        }

        private void toolStripDangNhap_Click(object sender, EventArgs e)
        {
            mnuDangNhap_Click(sender, e);
            //HienLaiTatCa();
        }

        private void toolStripNhom_Click(object sender, EventArgs e)
        {
            mnuTaoNhom_Click(sender, e);
        }

        private void toolStripNguoiDung_Click(object sender, EventArgs e)
        {
            mnuNguoiDung_Click(sender, e);
        }

        private void toolStripDoiPass_Click(object sender, EventArgs e)
        {
            mnuDoiMatKhau_Click(sender, e);
        }

        private void mnuDoiTac_Click(object sender, EventArgs e)
        {
            frmDoiTac fDoiTac = new frmDoiTac();
            fDoiTac.ShowDialog();
        }

        private void trvSoDo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                Xoa_Trong();

                if ((string)e.Node.Tag == "SoPhong")
                {
                    panelThongTinKhach.Visible = true;
                    txtSoPhong.Text = e.Node.Text;
                    clsDungChung.strLuuSoPhong = txtSoPhong.Text.Trim();
                    string sqlSoPhong = "Select * From So_Phong Where SoPhong='" + txtSoPhong.Text.Trim() + "'";
                    SqlDataAdapter daSoPhong = new SqlDataAdapter(sqlSoPhong, clsDungChung.con);
                    DataSet dsSoPhong = new DataSet();
                    daSoPhong.Fill(dsSoPhong, "So_Phong");
                    DataTable tbSoPhong = dsSoPhong.Tables["So_Phong"];
                    DataRow rowPhong = tbSoPhong.Rows[0];
                    mnuThongTinPhong.Enabled = true;

                    switch (rowPhong["TinhTrang"].ToString())
                    {
                        case "0":
                            mnuDatPhong.Enabled = true;
                            toolStripDangKyPhong.Enabled = true;
                            mnuNhanPhong.Enabled = false;
                            toolStripNhanPhong.Enabled = false;
                            mnuHuyDatPhong.Enabled = false;
                            toolStripHuyDangKy.Enabled = false;
                            mnuDoiPhong.Enabled = false;
                            toolStripChuyenPhong.Enabled = false;
                            mnuCapNhapThongTinKhach.Enabled = false;
                            toolStripCapNhap.Enabled = false;
                            mnuTinhTien.Enabled = false;
                            toolStripThanhToan.Enabled = false;
                            break;
                        case "1":
                            mnuDatPhong.Enabled = false;
                            toolStripDangKyPhong.Enabled = false;
                            mnuNhanPhong.Enabled = true;
                            toolStripNhanPhong.Enabled = true;
                            mnuHuyDatPhong.Enabled = true;
                            toolStripHuyDangKy.Enabled = true;
                            mnuDoiPhong.Enabled = false;
                            toolStripChuyenPhong.Enabled = false;
                            mnuCapNhapThongTinKhach.Enabled = false;
                            toolStripCapNhap.Enabled = false;
                            mnuTinhTien.Enabled = false;
                            toolStripThanhToan.Enabled = false;
                            break;
                        case "2":
                            mnuDatPhong.Enabled = false;
                            toolStripDangKyPhong.Enabled = false;
                            mnuNhanPhong.Enabled = false;
                            toolStripNhanPhong.Enabled = false;
                            mnuHuyDatPhong.Enabled = false;
                            toolStripHuyDangKy.Enabled = false;
                            mnuDoiPhong.Enabled = true;
                            toolStripChuyenPhong.Enabled = true;
                            mnuCapNhapThongTinKhach.Enabled = true;
                            toolStripCapNhap.Enabled = true;
                            mnuTinhTien.Enabled = true;
                            toolStripThanhToan.Enabled = true;
                            mnuSuDungDichVu.Enabled = true;
                            toolStripSDDichVu.Enabled = true;
                            break;
                    }

                    string SqlDK = "Select * From Dang_Ky Where SoPhong='" + txtSoPhong.Text.Trim() + "' And TrangThai='" + rowPhong["TinhTrang"].ToString() + "'";
                    SqlDataAdapter daDangKy = new SqlDataAdapter(SqlDK, clsDungChung.con);
                    DataSet dsDangKy = new DataSet();
                    daDangKy.Fill(dsDangKy, "Dang_Ky");
                    DataTable tbDangKy = dsDangKy.Tables["Dang_Ky"];
                    if (tbDangKy.DefaultView.Count > 0)
                    {
                        DataRow row = tbDangKy.Rows[0];
                        clsDungChung.strLuuMaDK = row["MaDK"].ToString();
                        clsDungChung.strLuuNgayDen = Convert.ToDateTime(row["NgayDen"]);
                        clsDungChung.strluuGioDen = Convert.ToDateTime(row["GioDen"]);
                        txtNgayDat.Text = Convert.ToDateTime(row["NgayDK"]).ToShortDateString();
                        txtNgayDen.Text = Convert.ToDateTime(row["NgayDen"].ToString()).ToShortDateString();
                        txtGioDen.Text = Convert.ToDateTime(row["GioDen"].ToString()).ToShortTimeString();
                        txtNgayDi.Text = Convert.ToDateTime(row["NgayDi"].ToString()).ToShortDateString();
                        txtNguoiLon.Text = row["NguoiLon"].ToString();
                        txtTreEm.Text = row["TreEm"].ToString();
                        txtSoPhong.Text = row["SoPhong"].ToString();
                        txtDoiTac.Text = row["DoiTac"].ToString();
                        clsDungChung.intDuaTruoc = Convert.ToInt32(row["DuaTruoc"]);
                        txtDuaTruoc.Text = Convert.ToInt32(row["DuaTruoc"]).ToString();

                        string SqlThongTinKhach = "Select * From Khach_Hang Where MaKH='" + row["MaKH"].ToString().Trim() + "'";
                        SqlDataAdapter daKhachHang = new SqlDataAdapter(SqlThongTinKhach, clsDungChung.con);
                        DataSet dsKhachHang = new DataSet();
                        daKhachHang.Fill(dsKhachHang, "Khach_Hang");
                        DataTable tbKhachHang = dsKhachHang.Tables["Khach_Hang"];
                        DataRow rowKH = tbKhachHang.Rows[0];
                        clsDungChung.strLuuMaKH = rowKH["MaKH"].ToString();
                        txtHoTen.Text = rowKH["HoTen"].ToString();
                        txtGioiTinh.Text = rowKH["GioiTinh"].ToString();
                        txtQuocTich.Text = rowKH["QuocTich"].ToString();
                        txtNgaySinh.Text = Convert.ToDateTime(rowKH["NgaySinh"]).ToShortDateString();
                        txtNoiSinh.Text = rowKH["NoiSinh"].ToString();
                        txtDiaChi.Text = rowKH["DiaChi"].ToString();
                        txtDienThoai.Text = rowKH["DienThoai"].ToString();
                        txtMail.Text = rowKH["Mail"].ToString();
                        txtCMNDPassport.Text = rowKH["CMND_PP"].ToString();
                        txtNoiCap.Text = rowKH["NoiCap"].ToString();
                        txtPassPort.Text = rowKH["PassPort"].ToString();
                        txtNoiCapPass.Text = rowKH["NoiCapPass"].ToString(); 
                        txtYeuCau.Text = rowKH["YeuCau"].ToString();

                        tbKhachHang.Dispose();
                        dsKhachHang.Dispose();
                        daKhachHang.Dispose();
                        HienDV();
                    }
                    tbDangKy.Dispose();
                    dsDangKy.Dispose();
                    daDangKy.Dispose();
                    tbSoPhong.Dispose();
                    dsSoPhong.Dispose();
                    daSoPhong.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void Xoa_Trong()
        {
            lstvSuDungDV.Items.Clear();
            mnuDatPhong.Enabled = false;
            toolStripDangKyPhong.Enabled = false;
            mnuNhanPhong.Enabled = false;
            toolStripNhanPhong.Enabled = false;
            mnuHuyDatPhong.Enabled = false;
            toolStripHuyDangKy.Enabled = false;
            mnuDoiPhong.Enabled = false;
            toolStripChuyenPhong.Enabled = false;
            mnuCapNhapThongTinKhach.Enabled = false;
            toolStripCapNhap.Enabled = false;
            mnuTinhTien.Enabled = false;
            toolStripThanhToan.Enabled = false;
            mnuSuDungDichVu.Enabled = false;
            toolStripSDDichVu.Enabled = false;
            mnuThongTinPhong.Enabled = false;
            //txtSoPhong.Text = "";

            txtNgayDat.Text = "";
            txtNgayDen.Text = "";
            txtGioDen.Text = "";
            txtNgayDi.Text = "";
            txtNguoiLon.Text = "";
            txtTreEm.Text = "";
            txtSoPhong.Text = "";
            txtDoiTac.Text = "";
            txtDuaTruoc.Text = "";

            txtHoTen.Text = "";
            txtGioiTinh.Text = "";
            txtQuocTich.Text = "";
            txtNgaySinh.Text = "";
            txtNoiSinh.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            txtMail.Text = "";
            txtCMNDPassport.Text = "";
            txtNoiCap.Text = "";
            txtYeuCau.Text = "";
        }

        private void txtNgayDat_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtYeuCau_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNgayDi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtNguoiLon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtTreEm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtSoPhong_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtDoiTac_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtHoTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtGioiTinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtNgaySinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtNoiSinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtDiaChi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtMail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtCMNDPassport_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtNoiCap_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtDuaTruoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtDuaTruoc_TextChanged(object sender, EventArgs e)
        {
            if (txtDuaTruoc.Text.Length > 0)
            {
                clsCacHam h = new clsCacHam();
                txtDuaTruoc.Text = h.chendau(h.loaidau(txtDuaTruoc.Text));
            }
        }

        private void mnuDatPhong_Click_1(object sender, EventArgs e)
        {
            frmDatNhanPhong fDatNhanPhong = new frmDatNhanPhong();
            fDatNhanPhong.ShowDialog();
            taoSoDo();
            Xoa_Trong();
        }

        private void mnuNhanPhong_Click(object sender, EventArgs e)
        {
            frmNhanPhong fNhanPhong = new frmNhanPhong();
            fNhanPhong.ShowDialog();
            taoSoDo();
            Xoa_Trong();
        }

        private void mnuHuyDatPhong_Click(object sender, EventArgs e)
        {
            frmHuyDangKy fHuyDangKy = new frmHuyDangKy();
            fHuyDangKy.ShowDialog();
            taoSoDo();
        }

        private void mnuCapNhapThongTinKhach_Click(object sender, EventArgs e)
        {
            frmCapNhapThongTin fCapNhap = new frmCapNhapThongTin();
            fCapNhap.ShowDialog();
            //Xoa_Trong();
        }

        private void mnuSuDungDichVu_Click(object sender, EventArgs e)
        {
            frmSuDungDichVu fSuDungDichVu = new frmSuDungDichVu();
            fSuDungDichVu.ShowDialog();
            HienDV();
        }

        private void HienDV()
        {
            try
            {
                clsCacHam h = new clsCacHam();
                string strCmdDichVu = "Select * From SuDung_DichVu Where MaDK='" + clsDungChung.strLuuMaDK + "'";
                SqlDataAdapter daDichVu = new SqlDataAdapter(strCmdDichVu, clsDungChung.con);
                DataSet dsDichVu = new DataSet();
                daDichVu.Fill(dsDichVu, "SuDung_DichVu");
                DataTable tbDichVu = dsDichVu.Tables["SuDung_DichVu"];
                lstvSuDungDV.Items.Clear();
                foreach (DataRow r1 in tbDichVu.Rows)
                {
                    ListViewItem item = new ListViewItem(r1["MaSD"].ToString().Trim());
                    item.SubItems.Add(Convert.ToDateTime(r1["NgaySD"]).ToShortDateString());
                    item.SubItems.Add(r1["TenDV"].ToString().Trim());
                    item.SubItems.Add(r1["DonVT"].ToString().Trim());
                    item.SubItems.Add(r1["SoLuong"].ToString().Trim());
                    item.SubItems.Add(h.chendau((Convert.ToInt32(r1["DonGia"])).ToString().Trim()));
                    lstvSuDungDV.Items.Add(item);

                }
                tbDichVu.Dispose();
                dsDichVu.Dispose();
                daDichVu.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message,"Thông Báo");
            }
        }

        private void mnuTrangThietBi_Click(object sender, EventArgs e)
        {
            frmTrangThietBi fTrangThietBi = new frmTrangThietBi();
            fTrangThietBi.ShowDialog();
        }

        private void mnuBangGia_Click(object sender, EventArgs e)
        {
            frmBangGia fBangGia = new frmBangGia();
            fBangGia.ShowDialog();
        }

        private void mnuThongTinPhong_Click(object sender, EventArgs e)
        {
            frmThongTinPhong fThongTinPhong = new frmThongTinPhong();
            fThongTinPhong.ShowDialog();
        }

        private void mnuTraCuu_Click(object sender, EventArgs e)
        {
            frmTraCuu fTraCuu = new frmTraCuu();
            fTraCuu.Show();
        }

        private void lstvSuDungDV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mnuDsHuyDangKy_Click(object sender, EventArgs e)
        {
            frmDanhSachHuyDangKy fdsHuyDangKy = new frmDanhSachHuyDangKy();
            fdsHuyDangKy.ShowDialog();
        }

        private void mnuNhatKyLuuTru_Click(object sender, EventArgs e)
        {
            frmNhatKyLuuTru fNhatKyLuuTru = new frmNhatKyLuuTru();
            fNhatKyLuuTru.ShowDialog();
        }

        private void mnuDoiPhong_Click(object sender, EventArgs e)
        {
            frmDoiPhong fDoiPhong = new frmDoiPhong();
            fDoiPhong.ShowDialog();
            taoSoDo();
        }

        private void mnuTinhTien_Click(object sender, EventArgs e)
        {
            frmThanhToan fThanhToan = new frmThanhToan();
            fThanhToan.ShowDialog();
            taoSoDo();
            Xoa_Trong();
        }

        private void txtGhiChu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; 
        }

        private void mnuTuyChon_Click(object sender, EventArgs e)
        {
            frmTuyChonHT fTuyChonHT = new frmTuyChonHT();
            fTuyChonHT.ShowDialog();
        }

        private void mnuDoanhThuPhong_Click(object sender, EventArgs e)
        {
            frmHienDoanhThuPhong fHienDoanhThuPhong = new frmHienDoanhThuPhong();
            fHienDoanhThuPhong.ShowDialog();

        }

        private void mnuDoanhThuDichVu_Click(object sender, EventArgs e)
        {
            frmHienDoanhThuDV fHienDV = new frmHienDoanhThuDV();
            fHienDV.ShowDialog();
        }

        private void mnuTongDoanhThu_Click(object sender, EventArgs e)
        {
            frmHienTongDoanhThu fHienTongDT = new frmHienTongDoanhThu();
            fHienTongDT.ShowDialog();
        }

        private void mnuHieuSuatPhong_Click(object sender, EventArgs e)
        {
            frmHienBieuDo fHienBieuDo = new frmHienBieuDo();
            fHienBieuDo.ShowDialog();
        }

        private void mnuThongKeKhach_Click(object sender, EventArgs e)
        {
            frmHienThongKeKhach fHienThongKe = new frmHienThongKeKhach();
            fHienThongKe.ShowDialog();
        }

        private void mnuBaoCaoKhachHuy_Click(object sender, EventArgs e)
        {
            frmHienKhachHuy fKhachHuy = new frmHienKhachHuy();
            fKhachHuy.ShowDialog();
        }

        private void toolStripDangKyPhong_Click(object sender, EventArgs e)
        {
            mnuDatPhong_Click_1(sender,e);
        }

        private void toolStripNhanPhong_Click(object sender, EventArgs e)
        {
            mnuNhanPhong_Click(sender,e);
        }

        private void toolStripHuyDangKy_Click(object sender, EventArgs e)
        {
            mnuHuyDatPhong_Click(sender, e);
        }

        private void toolStripCapNhap_Click(object sender, EventArgs e)
        {
            mnuCapNhapThongTinKhach_Click(sender, e);
        }

        private void toolStripChuyenPhong_Click(object sender, EventArgs e)
        {
            mnuDoiPhong_Click(sender, e);
        }

        private void toolStripThanhToan_Click(object sender, EventArgs e)
        {
            mnuTinhTien_Click(sender, e);
        }

        private void toolStripSDDichVu_Click(object sender, EventArgs e)
        {
            mnuSuDungDichVu_Click(sender, e);
        }

        private void toolStripTraCuu_Click(object sender, EventArgs e)
        {
            mnuTraCuu_Click(sender, e);
        }

        private void toolStripDsKhachLuuTru_Click(object sender, EventArgs e)
        {
            //mnuNhatKyLuuTru_Click(sender, e);
            //giap
            mnuDatPhong_Click_1(sender, e);
        }

        private void toolStripDsKhachHuy_Click(object sender, EventArgs e)
        {
            mnuDsHuyDangKy_Click(sender, e);
        }

        private void toolStripWeb_Click(object sender, EventArgs e)
        {
            mnuTrangChu_Click(sender, e);
        }

        private void toolStripHelp_Click(object sender, EventArgs e)
        {
            mnuGiupDo_Click(sender, e);
        }

        private void mnuGiupDo_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\HuongDan.chm");
        }

        private void PhanQuyen()
        {
            try
            {
                string strNhom = "Select * From Nhom_Quyen Where MaNhom='" + clsDungChung.strMaNhom.Trim() + "'";
                SqlDataAdapter daNhom = new SqlDataAdapter(strNhom, clsDungChung.con);
                DataSet dsNhom = new DataSet();
                daNhom.Fill(dsNhom, "Nhom_Quyen");
                DataTable tbNhom = dsNhom.Tables["Nhom_Quyen"];
                if (tbNhom.DefaultView.Count > 0)
                {
                    DataRow row = tbNhom.Rows[0];
                    mnuTaoNhom.Enabled = Convert.ToBoolean(row["mnuNhomPhanQuyen"]);
                    toolStripNhom.Enabled = Convert.ToBoolean(row["mnuNhomPhanQuyen"]);
                    mnuNguoiDung.Enabled = Convert.ToBoolean(row["mnuNguoiDung"]);
                    toolStripNguoiDung.Enabled = Convert.ToBoolean(row["mnuNguoiDung"]);
                    mnuTuyChon.Enabled = Convert.ToBoolean(row["mnuTuyChon"]);
                    mnuCSDL.Enabled = Convert.ToBoolean(row["mnuCSDL"]);
                    mnuSaoLuu.Enabled = Convert.ToBoolean(row["mnuBackup"]);
                    mnuPhucHoi.Enabled = Convert.ToBoolean(row["mnuRestore"]);
                    mnuLoaiPhong.Enabled = Convert.ToBoolean(row["mnuLoaiPhong"]);
                    mnuDsPhong.Enabled = Convert.ToBoolean(row["mnuSoPhong"]);
                    mnuThietBi.Enabled = Convert.ToBoolean(row["mnuThietBi"]);
                    mnuDichVu.Enabled = Convert.ToBoolean(row["mnuDichVu"]);
                    mnuTyGia.Enabled = Convert.ToBoolean(row["mnuNgoaiTe"]);
                    mnuDoiTac.Enabled = Convert.ToBoolean(row["mnuDoiTac"]);
                    mnuTrangThietBi.Enabled = Convert.ToBoolean(row["mnuTrangTB"]);
                    mnuDatPhong.Visible = Convert.ToBoolean(row["mnuDangKy"]);
                    toolStripDangKyPhong.Visible = Convert.ToBoolean(row["mnuDangKy"]);
                    mnuNhanPhong.Visible = Convert.ToBoolean(row["mnuNhanPhong"]);
                    toolStripNhanPhong.Visible = Convert.ToBoolean(row["mnuNhanPhong"]);
                    mnuHuyDatPhong.Visible = Convert.ToBoolean(row["mnuHuyDK"]);
                    toolStripHuyDangKy.Visible = Convert.ToBoolean(row["mnuHuyDK"]);
                    mnuCapNhapThongTinKhach.Visible = Convert.ToBoolean(row["mnuCapNhap"]);
                    toolStripCapNhap.Visible = Convert.ToBoolean(row["mnuCapNhap"]);
                    mnuDoiPhong.Visible = Convert.ToBoolean(row["mnuDoiPhong"]);
                    toolStripChuyenPhong.Visible = Convert.ToBoolean(row["mnuDoiPhong"]);
                    mnuTinhTien.Visible = Convert.ToBoolean(row["mnuThanhToan"]);
                    toolStripThanhToan.Visible = Convert.ToBoolean(row["mnuThanhToan"]);
                    mnuSuDungDichVu.Visible = Convert.ToBoolean(row["mnuSuDungDV"]);
                    toolStripSDDichVu.Visible = Convert.ToBoolean(row["mnuSuDungDV"]);
                    mnuDoanhThuPhong.Enabled = Convert.ToBoolean(row["mnuDoanhThuPhong"]);
                    mnuDoanhThuDichVu.Enabled = Convert.ToBoolean(row["mnuDoanhThuDV"]);
                    mnuTongDoanhThu.Enabled = Convert.ToBoolean(row["mnuTongDoanhThu"]);
                    mnuHieuSuatPhong.Enabled = Convert.ToBoolean(row["mnuHieuSuatPhong"]);
                    mnuThongKeKhach.Enabled = Convert.ToBoolean(row["mnuThongKeKhach"]);
                    mnuBaoCaoKhachHuy.Enabled = Convert.ToBoolean(row["mnuThongKeKhachHuy"]);
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

        private void toolStripDangXuat_Click(object sender, EventArgs e)
        {
            AnTatCa();
        }

        private void HienLaiTatCa()
        {
            mnuDangNhap.Enabled = false;
            mnuDangXuat.Enabled = true;
            toolStripDangXuat.Enabled = true; 
            toolStripDangNhap.Enabled = false;
            trvSoDo.Enabled = true;
            menuStripMain.Enabled = true;
            documentContainer1.Enabled = true;
            toolStripNhom.Enabled = true;
            toolStripNguoiDung.Enabled = true;
            toolStripDoiPass.Enabled = true;
            toolStripDangKyPhong.Enabled = true;
            toolStripNhanPhong.Enabled = true;
            toolStripHuyDangKy.Enabled = true;
            toolStripCapNhap.Enabled = true;
            toolStripChuyenPhong.Enabled = true;
            toolStripThanhToan.Enabled = true;
            toolStripSDDichVu.Enabled = true;
            toolStripTraCuu.Enabled = true;
            toolStripDsKhachLuuTru.Enabled = true;
            toolStripDsKhachHuy.Enabled = true; 
        }

        private void AnTatCa()
        {
            mnuDangNhap.Enabled = true;
            mnuDangXuat.Enabled = false;
            toolStripDangXuat.Enabled = false; 
            toolStripDangNhap.Enabled = true;
            trvSoDo.Enabled = false;
            menuStripMain.Enabled = false;
            documentContainer1.Enabled = false;
            toolStripNhom.Enabled = false;
            toolStripNguoiDung.Enabled = false;
            toolStripDoiPass.Enabled = false;
            toolStripDangKyPhong.Enabled = false;
            toolStripNhanPhong.Enabled = false;
            toolStripHuyDangKy.Enabled = false;
            toolStripCapNhap.Enabled = false;
            toolStripChuyenPhong.Enabled = false;
            toolStripThanhToan.Enabled = false;
            toolStripSDDichVu.Enabled = false;
            toolStripTraCuu.Enabled = false;
            toolStripDsKhachLuuTru.Enabled = false;
            toolStripDsKhachHuy.Enabled = false; 
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        { 
           AnTatCa();
           
        }

        private void toolStripMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

    }
}
