using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSF.Units;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAO
{
    public class EmployeeDao
    {
        private static EmployeeDao instance;
        public static EmployeeDao Instance
        {
            get { if (instance == null) instance = new EmployeeDao(); return EmployeeDao.instance; }
            private set { EmployeeDao.instance = value; }
        }
        private EmployeeDao() { }

        public List<Employee> SearchEmployeeByName(string tenNV)
        {
            List<Employee> list = new List<Employee>();

            string query = string.Format("select * from NhanVien where TenNV like N'%{0}%'", tenNV);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Employee employee = new Employee(item);
                list.Add(employee);
            }

            return list;

        }
        
        public List<Employee> LoadEmployeeList()
        {
            List<Employee> list = new List<Employee>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetEmployeeList");

            foreach (DataRow item in data.Rows) //lặp qua từng dòng data trong datatable 
            {
                Employee employee = new Employee(item);
                list.Add(employee);
            }

            return list;
        }
            
        public bool InsertEmployee( decimal luongCoBan, string tenNV, string soDienThoai)
        {
            string query = string.Format("INSERT INTO NhanVien ( TenNV, SoDienThoai, LuongCoBan )VALUES (N'{0}', N'{1}', '{2}')", tenNV, soDienThoai, luongCoBan);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateEmployee(int maNV, decimal luongCoBan, string tenNV, string soDienThoai)
        {
            string query = string.Format(
            "UPDATE NhanVien SET TenNV = N'{0}', SoDienThoai = '{1}', LuongCoBan = {2} WHERE MaNV = '{3}'", 
            tenNV, soDienThoai, luongCoBan, maNV);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteEmployee(int maNV)
        {
            string query = string.Format("Delete NhanVien where maNV = '{0}'", maNV);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        

    }
}
