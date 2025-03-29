using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DTO
{
    public class CheckOut
    {
        public CheckOut(int maPhieuMuon, DateTime ngayLap)
        {
            this.NgayLap = ngayLap;
           
            this.MaPhieuMuon = maPhieuMuon;
        }
        public CheckOut(DataRow row)
        {
            this.NgayLap = (DateTime?)row["ngayLap"];
            
            this.MaPhieuMuon = (int)row[maPhieuMuon];
        }

        private int maPhieuMuon;
        public int MaPhieuMuon
        { 
            get { return maPhieuMuon; }
            set { maPhieuMuon = value; }
        }

        private DateTime? ngayLap;
        public DateTime? NgayLap
        {
            get { return ngayLap; }
            set { ngayLap = value; }
        }
        

    }
}
