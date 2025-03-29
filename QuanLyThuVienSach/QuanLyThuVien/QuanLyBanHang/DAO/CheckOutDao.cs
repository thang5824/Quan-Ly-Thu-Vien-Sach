using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSF.Data.Model;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAO
{
    public class CheckOutDao
    {
        private static CheckOutDao instance;

        public static CheckOutDao Instance
        {
            get { if (instance == null) instance = new CheckOutDao(); return CheckOutDao.instance; }
            private set { CheckOutDao.instance = value;}
        }

        private CheckOutDao() { }

        public bool InsertCheckOut()
        {

            string query = string.Format("INSERT INTO PhieuMuon(NgayLap)VALUES (GETDATE());");

            int result = DataProvider.Instance.ExecuteNonQuery(query); //TRA RA SO DONG TUC LA THEM VAO THANH CONG 

            return result > 0;
        }
        
        public string GetCheckOutIDMax()
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT MAX(MaPhieuMuon) As idMax From PhieuMuon");

            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                return row["idMax"].ToString();
               
            }
            return "";
        }

        
        public string GetCheckOutID(string maphieumuon)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM PhieuMuon WHERE MaPhieuMuon = " + maphieumuon);

            if (data.Rows.Count > 0)
            {
                CheckOut checkout = new CheckOut(data.Rows[0]);
                return checkout.MaPhieuMuon.ToString();
            }
            return "";
        }

    }
}
