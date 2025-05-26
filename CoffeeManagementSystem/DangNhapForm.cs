// Trong file: DangNhapForm.cs

using System;
using System.Windows.Forms;
using CoffeeManagementSystem.BLL; // Thêm dòng này để sử dụng BLL
using CoffeeManagementSystem;     // Đảm bảo namespace này chứa lớp Taikhoan (Model)

namespace CoffeeManagementSystem
{
    public partial class DangNhapForm : Form
    {
        private AuthBLL _authBLL; // Khai báo đối tượng BLL

        public DangNhapForm()
        {
            InitializeComponent();
            _authBLL = new AuthBLL(); // Khởi tạo BLL

            // Gán sự kiện click cho nút Đăng nhập
            this.btnDangNhap.Click += new EventHandler(this.btnLogin_Click);

            // Tùy chọn: Xử lý sự kiện KeyDown trên TextBox mật khẩu để nhấn Enter cũng đăng nhập
            this.txtMatkhau.KeyDown += new KeyEventHandler(this.txtMatkhau_KeyDown);
        }

        private void txtMatkhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string tendangnhap = txtTenTaiKhoan.Text.Trim();
            string matkhau = txtMatkhau.Text.Trim();

            try
            {
                // Gọi BLL để xác thực người dùng
                Taikhoan taiKhoan = _authBLL.AuthenticateUser(tendangnhap, matkhau);

                if (taiKhoan != null)
                {
                    // Lấy tên hiển thị từ BLL
                    string tenNhanVienHienThi = _authBLL.GetEmployeeDisplayName(taiKhoan.Manhanvien, taiKhoan.Tendangnhap);

                    // Đăng nhập thành công
                    MessageBox.Show($"Đăng nhập thành công! Chào mừng {tenNhanVienHienThi} ({taiKhoan.Vaitro}).", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Xác định vai trò và hiển thị Main Form tương ứng
                    if (taiKhoan.Vaitro == "Quản lý")
                    {
                        MainForm QuanLyForm = new MainForm(taiKhoan.Manhanvien, tenNhanVienHienThi);
                        QuanLyForm.Show();
                        this.Hide();
                    }
                    else if (taiKhoan.Vaitro == "Nhân viên")
                    {
                        MainEmployer NhanVienForm = new MainEmployer(taiKhoan.Manhanvien, tenNhanVienHienThi);
                        NhanVienForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show($"Vai trò '{taiKhoan.Vaitro}' không được hỗ trợ.", "Lỗi vai trò", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Tài khoản không tồn tại hoặc sai mật khẩu (BLL đã trả về null)
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex) // Bắt lỗi từ BLL nếu người dùng nhập thiếu
            {
                MessageBox.Show(ex.Message, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi trong quá trình đăng nhập: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatkhau.PasswordChar = chkHienMatKhau.Checked ? '\0' : '●';
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}