using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DTO
{
    public class Employee
    {
        private int maNV;
        private string tenNV;
        private string soDienThoai;
        private decimal luongCoBan;
        public Employee(int maNV, decimal luongCoBan, string tenNV, string soDienThoai)
        {
            this.TenNV = tenNV;
            this.SoDienThoai = soDienThoai;
            this.LuongCoBan = luongCoBan;
            this.MaNV = maNV;
        }
        public Employee(DataRow row)
        {
            this.TenNV = row["tenNV"].ToString();
            this.SoDienThoai = row["soDienThoai"].ToString();
            this.LuongCoBan = (decimal)row["luongCoBan"];
            this.MaNV = (int)row["maNV"];

        }
        public int MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }
        public string TenNV
        {
            get { return tenNV; }
            set { tenNV = value; }
        }
        public string SoDienThoai
        {
            get { return soDienThoai; }
            set { soDienThoai = value; }
        }
        public decimal LuongCoBan
        {
            get { return luongCoBan; }
            set { luongCoBan = value; }
        }

    }
}
