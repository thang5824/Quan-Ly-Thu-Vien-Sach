using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DTO
{
    //tên biến = đầu thường cuối hoa  VD: maSach = biến, thuộc tính = MaSach
    public class Book
    {
        private int maSach;
        private string tenSach;
        private string tacGia;
        public Book(int maSach, string tenSach, string TacGia) //hàm tạo
        {
            this.MaSach = maSach; //gán giá trị từ đối số của hàm tạo cho phương thức của lớp hiện tại.
            this.TenSach = tenSach;
            this.TacGia = tacGia;
        }
        public Book(DataRow row) //data row lưu gtri dưới dạng object nên phải ép kiểu
        {   
            this.MaSach = (int)row["maSach"]; //Truy xuất giá trị từ cột MaHang trong DataRow và chuyển đổi nó thành chuỗi.
            this.TenSach = row["tenSach"].ToString();
            this.TacGia = row["tacGia"].ToString();
        }
        public Book(DataRow row, string a)
        {
                this.MaSach = (int)row["maSach"];
                this.TenSach = row["tenSach"].ToString();
                this.TacGia =  row["tacGia"].ToString();
        }
        
        public string TacGia
        {
            get { return tacGia; }
            set { tacGia = value; }
        }
        public string TenSach
        {
            get { return tenSach; }
            set { tenSach = value; }
        }
        public int MaSach
        {
            get{ return maSach; } 
            set { maSach = value; }
        }


    }
}
