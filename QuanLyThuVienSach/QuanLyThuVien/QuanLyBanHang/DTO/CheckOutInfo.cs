using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DTO
{
    internal class CheckOutInfo
    {   
        public CheckOutInfo(int soLuong, int idCheckOut, int idBook, int idCheckOutInfor, DateTime NgayTra)
        {
            this.IdCheckOut = idCheckOut;
            this.IdBook = idBook;
            this.SoLuong = soLuong;
            this.IdCheckOutInfor = idCheckOutInfor;
            this.NgayTra = ngayTra;
        }

        private DateTime? ngayTra;
        public DateTime? NgayTra
        {
            get { return ngayTra; }
            set { ngayTra = value; }
        }

        private int soLuong;

        public int SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }
        private int idCheckOutInfor;

        public int IdCheckOutInfor
        {
            get { return idCheckOutInfor; }
            set { idCheckOutInfor = value; }
        }
        private int idBook;

        public int IdBook
        {
            get { return idBook; }
            set { idBook = value; }
        }

        private int idCheckOut;

        public int IdCheckOut
        {
            get{ return idCheckOut; }
            set{ idCheckOut = value; }
        }
    }
}
