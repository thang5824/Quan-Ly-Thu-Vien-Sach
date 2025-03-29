using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSF;
using QuanLyThuVien.DAO;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien
{
    public partial class fAdmin : Form
    {
        BindingSource employeeList = new BindingSource();
        BindingSource accountList = new BindingSource();
        BindingSource bookList = new BindingSource();
        public fAdmin()
        {
            InitializeComponent();
            
            LoadLoad();
        }
        void LoadLoad()
        {
            dtgvNhanVien.DataSource = employeeList;
            dtgvTaiKhoan.DataSource = accountList;
            dtgvKhoSach.DataSource = bookList;
            LoadEmployeeList();
            LoadBook();
            AddBookBinding();
            AddEmployeeBinding();
            AddAccountBinding();
            LoadAccount();
        }
        void AddBookBinding()
        {
            txtMaSach.DataBindings.Add(new Binding("Text", dtgvKhoSach.DataSource, "MaSach"));
            txtTenSach.DataBindings.Add(new Binding("Text", dtgvKhoSach.DataSource, "TenSach"));
            txtTacGia.DataBindings.Add(new Binding("Text", dtgvKhoSach.DataSource, "TacGia"));
        }

        void LoadBook()
        {
            bookList.DataSource = BookDao.Instance.GetListBook();
        }



        List<Employee> SearchEmployeeByName(string tenNV)
        {
            List<Employee> listEmployee = EmployeeDao.Instance.SearchEmployeeByName(tenNV);
            return listEmployee;
        }

        void AddAccountBinding()
        {
            txtTenTaiKhoan.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "TenDangNhap"));
            txtTenHienThi.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "TenHienThi"));
            txtMatKhau.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "MatKhau"));
            numQuyenHan.DataBindings.Add(new Binding("Value", dtgvTaiKhoan.DataSource, "QuyenHan"));
        }

        void AddAccount(string tenDangNhap, string tenHienThi, string matKhau, int quyenHan)
        {
            if (AccountDao.Instance.InsertAccount(tenDangNhap, tenHienThi, matKhau, quyenHan))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
            LoadAccount();
        }
        void EditAccount(string tenDangNhap, string tenHienThi, string matKhau, int quyenHan)
        {
            if (AccountDao.Instance.UpdateAccount(tenDangNhap, tenHienThi, matKhau, quyenHan))
            {
                MessageBox.Show("Sửa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
            LoadAccount();
        }
        void DeleteAccount(string tenDangNhap)
        {
            if (AccountDao.Instance.DeleteAccount(tenDangNhap))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
            LoadAccount();
        }

        private void btnXemTK_Click(object sender, EventArgs e)
        {
            
        }


        private void btnThemTK_Click(object sender, EventArgs e)
        {

            string tenDangNhap = txtTenTaiKhoan.Text;
            string tenHienThi = txtTenHienThi.Text;
            string matKhau = txtMatKhau.Text;
            int quyenHan = (int)numQuyenHan.Value;

            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(tenHienThi) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Cần điền đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AddAccount(tenDangNhap, tenHienThi, matKhau, quyenHan);
        }


        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenTaiKhoan.Text;
            DeleteAccount(tenDangNhap);
        }

        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenTaiKhoan.Text;
            string tenHienThi = txtTenHienThi.Text;
            string matKhau = txtMatKhau.Text;
            int quyenHan = (int)numQuyenHan.Value;

            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(tenHienThi) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Cần điền đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EditAccount(tenDangNhap, tenHienThi, matKhau, quyenHan);
        }


        private void btnTimNV_Click(object sender, EventArgs e)
        {
            employeeList.DataSource = SearchEmployeeByName(txtTimNV.Text);
        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDao.Instance.GetListAccount();
        }

        void AddEmployeeBinding()
        {
            txtMaNV.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "MaNV"));
            txtTenNV.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "TenNV"));
            txtLuong.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "LuongCoBan"));
            txtSDTNV.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "SoDienThoai"));
        }
        private void LoadEmployeeList()
        {
            employeeList.DataSource = EmployeeDao.Instance.LoadEmployeeList();
        }


        private void btnXemNV_Click(object sender, EventArgs e)
        {
            LoadEmployeeList();
        }
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            
            string tenNhanVien = txtTenNV.Text;
            string sDTNV = txtSDTNV.Text;
            decimal luongCoBan;
            if (!decimal.TryParse(txtLuong.Text, out luongCoBan))
            {
                MessageBox.Show("Lương cơ bản phải là một số hợp lệ.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tenNhanVien) || string.IsNullOrWhiteSpace(sDTNV))
            {
                MessageBox.Show("Cần điền đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (EmployeeDao.Instance.InsertEmployee( luongCoBan, tenNhanVien, sDTNV))
            {
                MessageBox.Show("Thêm thành công!");
                LoadEmployeeList();
            }
            else 
            {
                MessageBox.Show("Lỗi thêm");
            }

        }
        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            int maNhanVien = int.Parse(txtMaNV.Text);
            string tenNhanVien = txtTenNV.Text;
            string sDTNV = txtSDTNV.Text;
            
            decimal luongCoBan;
            if (!decimal.TryParse(txtLuong.Text, out luongCoBan))
            {
                MessageBox.Show("Lương cơ bản phải là một số hợp lệ.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tenNhanVien) || string.IsNullOrWhiteSpace(sDTNV))
            {
                MessageBox.Show("Cần điền đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (EmployeeDao.Instance.UpdateEmployee(maNhanVien, luongCoBan, tenNhanVien, sDTNV))
            {
                MessageBox.Show("Sửa thành công!");
                LoadEmployeeList();
            }
            else
            {
                MessageBox.Show("Lỗi sửa");
                
            }
             
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            int maNhanVien = int.Parse(txtMaNV.Text);
            if (EmployeeDao.Instance.DeleteEmployee(maNhanVien))
            {
                MessageBox.Show("Xóa thành công!");
                LoadEmployeeList();
            }
            else
            {
                MessageBox.Show("Lỗi xóa");
                
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
           LoadDoanhThuByDate(dtpkTuNgay.Value,dtpkToiNgay.Value);
        }
        void LoadDoanhThuByDate(DateTime statr,DateTime end)
        {
            string query = string.Format("SELECT c.MaPhieuMuon AS MaPhieuMuon, c.NgayLap AS NgayLap, s.TenSach AS TenSach,  Co.NgayTra As  NgayTra,Co.SoLuong AS SoLuong " +
                   "FROM PhieuMuon c " +
                   "JOIN ChiTietPhieuMuon Co ON c.MaPhieuMuon = Co.idCheckOut " +
                   "JOIN Sach s ON Co.idBook = s.MaSach " +
                   "WHERE c.NgayLap BETWEEN '{0}' AND '{1}'", statr, end);
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dtgvThongKe.DataSource = dt;
        }

        private void btnTimTK_Click(object sender, EventArgs e)
        {
            accountList.DataSource = AccountDao.Instance.SearchAccountByName(txtTimTK.Text);
        }

        void AddProduct(string tenSach, string tacGia)
        {
            if (BookDao.Instance.InsertBook(tenSach, tacGia))
            {
                MessageBox.Show("Thêm  thành công");
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
            LoadBook();
        }

        

        void UpdateBook( string tenSach, string tacGia, int maSach)
        {
            if (BookDao.Instance.UpdateBook(tenSach, tacGia, maSach))
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
            LoadBook();
        }
        void DeleteBook(int maSach)
        {
            if (BookDao.Instance.DeleteBook(maSach))
            {
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
            LoadBook();
        }




        private void btnThemSach_Click(object sender, EventArgs e)
        {
            string tenSach = txtTenSach.Text.Trim();
            string tacGia = txtTacGia.Text.Trim();

            // Kiểm tra nếu có ô nào bị bỏ trống
            if (string.IsNullOrWhiteSpace(tenSach) || string.IsNullOrWhiteSpace(tacGia))
            {
                MessageBox.Show("Cần điền đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi phương thức thêm sách
            AddProduct(tenSach, tacGia);

        }

        private void btnTimSach_Click(object sender, EventArgs e)
        {
            bookList.DataSource = BookDao.Instance.SearchBookByName(txtTimSach.Text);
        }

        private void btnSuaSach_Click(object sender, EventArgs e)
        {
            string tenSach = txtTenSach.Text;
            string tacGia = txtTacGia.Text;
            int maSach = int.Parse(txtMaSach.Text);

            if (string.IsNullOrWhiteSpace(tenSach) || string.IsNullOrWhiteSpace(tacGia))
            {
                MessageBox.Show("Cần điền đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            UpdateBook(tenSach, tacGia, maSach);
        }

        private void btnXemSach_Click(object sender, EventArgs e)
        {
            LoadBook();
        }

        private void btnXoaSach_Click(object sender, EventArgs e)
        {
            int maSach = int.Parse(txtMaSach.Text);
            DeleteBook(maSach);
        }

    }
}
