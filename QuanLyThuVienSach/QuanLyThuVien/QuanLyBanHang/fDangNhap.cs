using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.DAO;

namespace QuanLyThuVien
{
    public partial class fDangNhap : Form
    {
        public fDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string TenDangNhap = txtTenDN.Text;
            string MatKhau = txtMatKhau.Text;
            if (Login(TenDangNhap, MatKhau))
            {
                Account loginAccount = AccountDao.Instance.GetAccountByUserName(TenDangNhap);
                fBangQuanLy f = new fBangQuanLy(loginAccount); //đăng nhập xong chạy vào bảng quản lý, ẩn form đăng nhập, tắt bảng qly thì hiện lại
                this.Hide(); //viết trong phương thức btndangnhap nên this trả về lớp đăng nhập của pthức đó
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
            }
        }
        bool Login(string TenDangNhap, string MatKhau)
        {
            return AccountDao.Instance.Login(TenDangNhap, MatKhau);
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
