using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAO
{
    public class BookDao
    {
        private static BookDao instance;
        public static BookDao Instance
        {
            get { if (instance == null) instance = new BookDao(); return BookDao.instance; }
            private set { BookDao.instance = value; }
        }
        private BookDao() { }
        public List<Book> SearchBookByName(string tenSach)
        {
            List<Book> list = new List<Book>();

            string query = string.Format("select * from Sach where TenSach like N'%{0}%'", tenSach);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Book product = new Book(item);
                list.Add(product);
            }
            return list;
        }
        public List<Book> SearchByName(string TenSach)
        {
            List<Book> list = new List<Book>();
            string query = string.Format("SELECT * FROM Sach WHERE dbo.fuConvertToUnsign1(TenSach) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", TenSach);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Book product = new Book(item," ");
                list.Add(product);
            }
            return list;

        }


        public List<Book> LoadBookList() 
        {
            List<Book> bookList = new List<Book>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetBookList");

            foreach (DataRow item in data.Rows) //lặp qua từng dòng datarow trong datatable 
            {
                Book product = new Book(item);
                bookList.Add(product);
            }

            return bookList;
        }     
        public List<Book> GetBookInfo(string maSach) 
        {
            List<Book> listBookInfo = new List<Book>();

            // Truy vấn dữ liệu với tham số SQL
            DataTable data = DataProvider.Instance.ExecuteQuery(
                "SELECT MaSach, TenSach, TacGia FROM Sach WHERE MaSach = @maSach",
                new object[] { maSach }
            );

            // Xử lý kết quả truy vấn
            foreach (DataRow item in data.Rows)
            {
                // Sử dụng constructor có 2 tham số
                Book product = new Book(item, maSach);
                listBookInfo.Add(product);
            }

            return listBookInfo;
        }

        public DataTable GetListBook()
        {
            return DataProvider.Instance.ExecuteQuery("select * from Sach");
        }
        public bool InsertBook( string tenSach, string tacGia)
        {
            string query = string.Format("INSERT INTO Sach ( TenSach, TacGia )VALUES (N'{0}', N'{1}')", tenSach, tacGia);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateBook( string tenSach, string tacGia, int maSach)
        {
            string query = string.Format(
            "UPDATE Sach SET TenSach = N'{0}', TacGia = N'{1}' WHERE MaSach = '{2}'",
            tenSach, tacGia, maSach);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteBook(int maSach)
        {
            string query = string.Format("Delete Sach where MaSach = '{0}'", maSach);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
