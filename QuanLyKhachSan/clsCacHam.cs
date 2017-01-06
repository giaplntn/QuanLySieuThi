using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuanLyKhachSan
{
    class clsCacHam
    {
        public string chendau(string so)
        {
            for (int i = so.Length - 3; i >= 0; i = i - 3)
            {
                so = so.Insert(i, ",");
            }
            if (so.Substring(0, 1) == ",")
                so = so.Remove(0, 1);

            return so;
        }
        /// <summary>
        /// Loai dau phan cach ra khoi so vd: 100,000,000 --->100000000
        /// </summary>
        /// <param name="so">so can loai</param>
        /// <returns>so da duoc loai</returns>
        public string loaidau(string chuoi)
        {
            chuoi = chuoi.Replace(",", "");
            return chuoi;
        }

        public string BoKhoangTrang(string Chuoi)
        {
            Chuoi = Chuoi.Replace(" ", "");
            return Chuoi;
        }

        //ham doi 1 so
        private string doc1so(string so)
        {
            string[] mangso = { "không ", "một ", "hai ", "ba ", "bốn ", "năm ", "sáu ", "bảy ", "tám ", "chín " };
            return mangso[int.Parse(so)];
        }
        /// <summary>
        /// Ham doi 3 chu so
        /// </summary>
        /// <param name="so">so can doi</param>
        /// <returns>ten so bang chu</returns>
        private string doc3so(string so)
        {
            string luu = null;
            if (so.Length == 1)
                luu = doc1so(so);
            if (so.Length == 2)
            {
                switch (so.Substring(0, 1))
                {
                    case "0":
                        if (so.Substring(so.Length - 1, 1) != "0")
                            luu = "Lẻ " + doc1so(so.Substring(so.Length - 1, 1));
                        else
                            luu = "";
                        break;
                    case "1":
                        if (so.Substring(so.Length - 1, 1) == "0")
                            luu = "mười ";
                        else
                            luu = "mười " + doc1so(so.Substring(so.Length - 1, 1));
                        break;
                    default:
                        if (so.Substring(so.Length - 1, 1) != "0")
                            luu = doc1so(so.Substring(0, 1)) + "mươi " + doc1so(so.Substring(so.Length - 1, 1));
                        else
                            luu = doc1so(so.Substring(0, 1)) + "mươi ";
                        break;
                }
            }
            if (so.Length == 3)
            {
                if (so == "000")
                    luu = "";
                else
                    luu = doc1so(so.Substring(0, 1)) + "trăm " + doc3so(so.Substring(so.Length - 2, 2));
            }

            return luu;
        }
        /// <summary>
        /// Ham doi 4 so tro len
        /// </summary>
        /// <param name="so">so can doi</param>
        /// <returns>ten so bang chu</returns>
        public string docso(string so)
        {
            int k = 0;
            int d = 0;
            string kq = null;
            string[] mang = { "", "ngàn ", "triệu ", "tỷ " };
            for (int i = so.Length - 3; i >= -2; i = i - 3)
            {
                //MessageBox.Show("aaa");
                if (k == 4)
                    k = 1;
                if (i >= 0)
                {
                    if (doc3so(so.Substring(i, 3)) != "")
                        kq = doc3so(so.Substring(i, 3)) + mang[k] + kq;
                    else
                    {
                        if (k == 3)
                            kq = "tỷ " + kq;
                    }
                }
                else
                    kq = doc3so(so.Substring(0, so.Length - (3 * d))) + mang[k] + kq;


                k = k + 1;
                d = d + 1;
            }
            kq = kq.Replace("mươi một", "mươi mốt");
            return kq;
        }
    }
}
