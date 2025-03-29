using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using GSF;
using QuanLyThuVien.DAO;
using QuanLyThuVien.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace QuanLyThuVien
{
    public partial class fBangQuanLy : Form
    {
        //    BindingSource productList = new BindingSource();
        private Account loginAccount;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.QuyenHan); }
        }
        public fBangQuanLy(Account Acc)
        {
            InitializeComponent();
            LoadLoad();
            AddColumsForLisView();
            this.LoginAccount = Acc;
        }

        public static int ProductWidth = 130;
        public static int ProductHeight = 130;

        private void fBangQuanLy_Load(object sender, EventArgs e)
        {

        }

        void LoadLoad()
        {
            DoiMauChu();
            LoadBook();
        }


        #region Method

        void ChangeAccount(int quyenHan)
        {
            adminToolStripMenuItem1.Enabled = quyenHan == 1;
        }

        private void DoiMauChu()
        {
            foreach (ToolStripMenuItem item in menuQL.Items) //// Đăng ký sự kiện MouseEnter và MouseLeave cho từng item trong MenuStrip
            {
                item.MouseEnter += (s, ev) =>                 // Khi di chuột qua một item
                {
                    item.ForeColor = Color.Black;     // Thay đổi màu chữ khi hover
                };
                item.MouseLeave += (s, ev) =>                // Khi chuột rời khỏi một item
                {
                    item.ForeColor = Color.White;        // Trả về màu chữ ban đầu
                };
            }
        }
        
        void LoadBookByeName(string name)
        {
            List<Book> bookList = BookDao.Instance.SearchByName(name);

            foreach (Book item in bookList)
            {
                Button btn = new Button() { Width = ProductWidth, Height = ProductHeight };
                btn.Text = item.TenSach + Environment.NewLine + item.TacGia;
                btn.Click += btn_Click;
                btn.Tag = item; //tag là object lưu bill vào tag

                //đặt lại thuộc tính của button
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.Font = new Font("Arial", 12, FontStyle.Bold);
                //thêm button vào flow
                flpBook.Controls.Add(btn);
            }
        }
        
        void LoadBook()
        {
            List<Book> productList = BookDao.Instance.LoadBookList();
            
            foreach (Book item in productList)
            {
                Button btn = new Button() {Width = ProductWidth, Height = ProductHeight};                         
                btn.Text = item.TenSach + Environment.NewLine + item.TacGia;
                btn.Click += btn_Click;
                btn.Tag = item; 

                //đặt lại thuộc tính của button
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.Font = new Font("Arial", 12, FontStyle.Bold);
                //thêm button vào flow
                flpBook.Controls.Add(btn);
            }
        }
        
        void AddColumsForLisView()
        {
            lsvTongSach.View = View.Details; // Chế độ hiển thị chi tiết
            lsvTongSach.Columns.Add("Mã Sách", 100);    // Cột Mã hàng
            lsvTongSach.Columns.Add("Tên sách", 200);   // Cột Tên hàng
            lsvTongSach.Columns.Add("Tác giả", 100);
            lsvTongSach.Columns.Add("Số Lượng", 100); // Cột Đơn giá

        }
        
        void ShowProduct(int masach)
        {
            // Lấy thông tin sản phẩm từ cơ sở dữ liệu
            List<Book> listBookInfo = BookDao.Instance.GetBookInfo(masach.ToString());

            // Decimal TongTien = decimal.Parse(txtTongTien.Text);
            
            // Thêm dữ liệu vào ListView
            foreach (Book product in listBookInfo)
            {
                
                // Tạo một dòng dữ liệu mới cho ListView
               
                ListViewItem item = new ListViewItem(product.MaSach.ToString());  // Giá trị cột đầu tiên (Mã hàng)
                item.SubItems.Add(product.TenSach);                   // Giá trị cột thứ hai (Tên hàng)
                item.SubItems.Add(product.TacGia.ToString());
                item.SubItems.Add("1");
                // Giá trị cột thứ ba (Đơn giá)
                //Decimal Giaban = Convert.ToDecimal(product.DonGia);
                //TongTien += Giaban;
                
                // Thêm dòng dữ liệu vào ListView
                lsvTongSach.Items.Add(item);
            }
            


            // Hiển thị tổng tiền trong TextBox với định dạng Việt Nam
            //txtTongTien.Text = TongTien.ToString();
           
            if (lsvTongSach.Items.Count > 1)
            {
                for (int i = 0; i < lsvTongSach.Items.Count-1; i++)
                {
                    
                    for (int j = i + 1; j < lsvTongSach.Items.Count; j++)
                    {

                        if (lsvTongSach.Items[i].SubItems[0].Text == lsvTongSach.Items[j].SubItems[0].Text)
                        {
                            int sl;
                            sl = int.Parse(lsvTongSach.Items[i].SubItems[3].Text);
                            sl += 1;
                            lsvTongSach.Items[i].SubItems[3].Text=sl.ToString();
;                           lsvTongSach.Items[j].Remove();


                        }

                    }


                }
            }


        }


        #endregion

        #region Events
        private void btnXuatPhieu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKH.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Chưa nhập thông tin khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng việc thực thi tiếp
            }

            if (CheckOutDao.Instance.InsertCheckOut())
            {
                 foreach (ListViewItem item in lsvTongSach.Items) 
                 {
                    CheckOutInfoDao.Instance.InsertCheckOutInFor(int.Parse(item.SubItems[0].Text), 
                    int.Parse(CheckOutDao.Instance.GetCheckOutIDMax()), 
                    int.Parse(item.SubItems[3].Text)); //lưu vào chi tiết PHIEU MUON
                      
                 }
                DialogResult result = MessageBox.Show("Bạn có muốn xuất phiếu không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                     SaveFileDialog saveFileDialog = new SaveFileDialog
                          {
                             Title = "Chọn nơi lưu file",
                             Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*", // Bộ lọc định dạng file
                             DefaultExt = "txt", // Mặc định là file .txt
                             FileName = "listview_data.txt" // Tên file mặc định
                          };
        
              // Hiển thị hộp thoại chọn đường dẫn
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                     // Mở file để ghi
                    using (StreamWriter writer = new StreamWriter(filePath))
                      {
                         // Ghi tiêu đề cột (header)
                        foreach (ColumnHeader column in lsvTongSach.Columns)
                            {
                                writer.Write(column.Text + "\t"); // Ngăn cách các cột bằng tab
                            }
                                writer.WriteLine(); // Xuống dòng sau khi ghi xong tiêu đề

                // Ghi từng dòng (item)
                    foreach (ListViewItem item in lsvTongSach.Items)
                    {
                    foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                    {
                        writer.Write(subItem.Text + "\t"); // Ghi giá trị từng cột
                    }
                        writer.WriteLine(); // Xuống dòng sau mỗi hàng
                    }
                        writer.Write("-Thông Tin Khách Hàng:"); // Ghi giá trị từng cột
                        writer.WriteLine();
                        writer.Write(txtTenKH.Text+"-"+txtSDT.Text+"-"+txtDiaChi.Text);
                    }

            // Hiển thị thông báo thành công
            MessageBox.Show("Xuất dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            // Hiển thị lỗi nếu có
            MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
                }
                //BillInFor.Instance.InsertBillInFor(item.Text)
            }
            else
            {
                MessageBox.Show("Có Lỗi");
            }
           

        }
        
        
        private void btn_Click(object sender, EventArgs e)
        {
            int maSach = ((sender as Button).Tag as Book).MaSach;
            ShowProduct(maSach);
        } 


        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void fBangQuanLy_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
        private void adminToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.ShowDialog();
        }

        #endregion

        private void btnTim_Click(object sender, EventArgs e)
        {
            flpBook.Controls.Clear();
            LoadBookByeName(txtTenSach.Text);     
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lsvTongSach.Items)
            {
                item.Remove();
                //txtTongTien.Text = "0";
            }
            // lsvTongSach.Items.Clear();
        
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
