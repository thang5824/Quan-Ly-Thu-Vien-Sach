using QuanLyThuVien.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DAO
{
    public class CheckOutInfoDao
    {
        private static CheckOutInfoDao instance;

        public static CheckOutInfoDao Instance
        {
            get { if (instance == null) instance = new CheckOutInfoDao(); return CheckOutInfoDao.instance; }
            private set { CheckOutInfoDao.instance = value; }
        }

        private CheckOutInfoDao() { }
       
        public bool InsertCheckOutInFor( int idBook, int idCheckOut, int SoLuong)
        {

            string query = string.Format("INSERT INTO ChiTietPhieuMuon ( idBook, idCheckOut,SoLuong )VALUES ({0}, {1},{2})", idBook, idCheckOut, SoLuong);
            
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
       


    }
}
