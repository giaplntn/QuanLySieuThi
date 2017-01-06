using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Xml;
using System.Text;

namespace QuanLyKhachSan
{
    class clsDungChung
    {
        public static SqlConnection con;
        public static string strServerName = null;
        public static string strCSDL = null;
        public static string strUser = null;
        public static string strPass = null;
        public static string strTenLogin=null;
        public static string strMaNhom = null;
        public static Boolean bNguoiDungThem = false;
        public static Boolean bNguoiDungSua = false;
        public static string strLuuNguoiDungUserName = null;
        public static string strLuuNguoiMaNhom = null;
        public static Boolean bNhomThem = false;
        public static Boolean bNhomSua = false;
        public static string strLuuMaNhom = null;
        public static string strLuuTenNhom = null;
        public static string strLuuSoPhong = null;
        public static string strLuuMaDK = null;
        public static string strLuuMaKH = null;
        public static int intDuaTruoc = 0;
        public static DateTime strLuuNgayDen;
        public static DateTime strluuGioDen;
        public static DateTime datTuNgay;
        public static DateTime datDenNgay;

        public string MaHoa(string strchuoi)
        {
            byte[] chuoiChua = Encoding.UTF32.GetBytes(strchuoi);
            MD5 md = new MD5CryptoServiceProvider();
            byte[] ketqua = md.ComputeHash(chuoiChua);
            return Convert.ToBase64String(ketqua);
        }

        public void TaoFileSetting(string strDuongDan, string strServerName, string strCSDL, string strUser, string strPass)
        {
            XmlTextWriter write = new XmlTextWriter(@strDuongDan, null);
            write.WriteStartDocument();
            write.WriteStartElement("SQL2000", "");
            write.WriteStartElement("Server", "");
            write.WriteString(strServerName);
            write.WriteEndElement();
            write.WriteStartElement("CSDL", "");
            write.WriteString(strCSDL);
            write.WriteEndElement();
            write.WriteStartElement("UserName", "");
            write.WriteString(strUser);
            write.WriteEndElement();
            write.WriteStartElement("PassWord", "");
            write.WriteString(strPass);
            write.WriteEndElement();
            write.WriteEndDocument();
            write.Close();
        }
    }
}
