using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAO
{
    public class AccountDao
    {
        private static AccountDao instance;
        public static AccountDao Instance
        {
            get { if (instance == null) instance = new AccountDao(); return instance; }
            private set { instance = value; }
        }
        private AccountDao() { }

        public Account GetAccountByUserName(string tenDangNhap)
        {
            
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from TaiKhoan where TenDangNhap = '" +  tenDangNhap + "'"); 

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }

        public List<Account> SearchAccountByName(string tenHienThi)
        {
            List<Account> list = new List<Account>();

            string query = string.Format("select * from TaiKhoan where TenHienThi like N'%{0}%'", tenHienThi);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Account account = new Account(item);
                list.Add(account);
            }

            return list;

        }

        public bool Login(string TenDangNhap, string MatKhau)
        {
            string query = "USP_Login @TenDangNhap , @MatKhau";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { TenDangNhap, MatKhau });

            return result.Rows.Count > 0;
        }

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("Select TenDangNhap, TenHienThi, MatKhau, QuyenHan from TaiKhoan");
        }

        public bool InsertAccount(string tenDangNhap, string tenHienThi, string matKhau, int quyenHan)
        {
            string query = string.Format("INSERT INTO TaiKhoan ( TenDangNhap, TenHienThi, MatKhau, QuyenHan )VALUES (N'{0}', N'{1}', N'{2}', {3})", tenDangNhap, tenHienThi, matKhau, quyenHan);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateAccount(string tenDangNhap, string tenHienThi, string matKhau, int quyenHan)
        {
            string query = string.Format("UPDATE TaiKhoan SET TenHienThi = N'{1}', MatKhau = N'{2}',QuyenHan = {3} WHERE TenDangNhap = N'{0}'", tenDangNhap, tenHienThi, matKhau, quyenHan);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteAccount(string tenDangNhap)
        {
            string query = "DELETE FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tenDangNhap });
            return result > 0;
        }

    };
}
