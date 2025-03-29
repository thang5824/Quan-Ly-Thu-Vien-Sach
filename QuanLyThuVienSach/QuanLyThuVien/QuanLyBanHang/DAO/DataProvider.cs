using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Design;

namespace QuanLyThuVien.DAO
{
    public class DataProvider //lớp lớn và tái sử dụng nên biến nó thành duy nhất
    {

        private static DataProvider instance; //tạo biến tĩnh để lưu data provider, đảm bảo chỉ có 1 data provider và chỉ có duy nhất 1 thể hiện của nó
        public static DataProvider Instance // thuộc tính instance để truy cập
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; } //tạo mới nếu chưa có
            private set { DataProvider.instance = value; } //không đc set từ bên ngoài, chỉ có nội bộ
        }

        private DataProvider() { }

        private string connectionSTR = @"Data Source=HuynhThang\SQLEXPRESS;Initial Catalog=QuanLyThuVien;Integrated Security=True"; 

        public SqlConnection GetConnection()
        { return new SqlConnection(connectionSTR); }





        public DataTable ExecuteQuery(string query, object[] parameter = null) //Trả ra bảng kết quả (ví dụ lấy danh sách)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]); //thêm được n parameter vòng lặp
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }
            return data;
        }
        public int ExecuteNonQuery(string query, object[] parameter = null) //trả ra số dòng thành công, k trả bảng (ví dụ insert update del nvien) 
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }

                }
                data = command.ExecuteNonQuery();
                connection.Close();
            }

            return data;
        }
        public object ExecuteScalar(string query, object[] parameter = null)//trả ra giá trị duy nhất (ví dụ đếm số lượng)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }

                }
                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }
    }
}
