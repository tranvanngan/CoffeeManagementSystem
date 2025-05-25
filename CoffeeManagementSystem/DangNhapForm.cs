using System;
using System.Windows.Forms;
using CoffeeManagementSystem.DAL; // Đảm bảo namespace này chứa TaikhoanDAL
using CoffeeManagementSystem;     // Đảm bảo namespace này chứa lớp Taikhoan (Model)

namespace CoffeeManagementSystem // Đảm bảo namespace này khớp với Form DangNhapForm của bạn
{
    public partial class DangNhapForm : Form
    {
        private TaikhoanDAL taikhoanDAL = new TaikhoanDAL(); // Khởi tạo đối tượng DAL

        public DangNhapForm()
        {
            InitializeComponent();
            // Gán sự kiện click cho nút Đăng nhập
            this.btnDangNhap.Click += new EventHandler(btnLogin_Click);

            // Tùy chọn: Xử lý sự kiện KeyDown trên TextBox mật khẩu để nhấn Enter cũng đăng nhập
            this.txtMatkhau.KeyDown += new KeyEventHandler(txtMatkhau_KeyDown);
        }

        /// <summary>
        /// Xử lý sự kiện KeyDown trên TextBox mật khẩu.
        /// </summary>
        private void txtMatkhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e); // Gọi phương thức xử lý nút Đăng nhập
                e.Handled = true; // Ngăn chặn xử lý phím Enter mặc định (ví dụ: tiếng bíp)
                e.SuppressKeyPress = true; // Ngăn chặn sự kiện KeyPress tiếp theo
            }
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Đăng nhập".
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string tendangnhap = txtTenTaiKhoan.Text.Trim();
            string matkhau = txtMatkhau.Text.Trim();

            // 1. Kiểm tra người dùng có nhập thiếu gì không
            if (string.IsNullOrEmpty(tendangnhap) || string.IsNullOrEmpty(matkhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng xử lý
            }

            try
            {
                // 2. Kiểm tra tài khoản có tồn tại không
                // Giả định TaikhoanDAL có phương thức GetTaikhoanByTendangnhap(tendangnhap, matkhau)
                // Phương thức này sẽ trả về đối tượng Taikhoan nếu tìm thấy, ngược lại là null
                Taikhoan taiKhoan = taikhoanDAL.GetTaikhoanByTendangnhapAndMatkhau(tendangnhap, matkhau);

                if (taiKhoan != null)
                {
                    string tenNhanVienHienThi = taiKhoan.Tendangnhap; // Mặc định là tên đăng nhập

                    // Nếu tài khoản có liên kết với một Manhanvien, lấy Hoten của nhân viên đó
                    if (!string.IsNullOrEmpty(taiKhoan.Manhanvien))
                    {
                        NhanvienDAL nhanvienDAL = new NhanvienDAL(); // Khởi tạo NhanvienDAL
                        Nhanvien nhanVien = nhanvienDAL.GetNhanvienById(taiKhoan.Manhanvien);
                        if (nhanVien != null && !string.IsNullOrEmpty(nhanVien.Hoten))
                        {
                            tenNhanVienHienThi = nhanVien.Hoten; // Cập nhật tên hiển thị thành tên thật của nhân viên
                        }
                    }
                    // Đăng nhập thành công
                    MessageBox.Show($"Đăng nhập thành công! Chào mừng {tenNhanVienHienThi} ({taiKhoan.Vaitro}).", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 3. Xác định vai trò và hiển thị Main Form tương ứng
                    if (taiKhoan.Vaitro == "Quản lý")
                    {
                        MainForm QuanLyForm = new MainForm(taiKhoan.Manhanvien, tenNhanVienHienThi); // TRUYỀN THAM SỐ
                        QuanLyForm.Show();
                        this.Hide();
                    }
                    else if (taiKhoan.Vaitro == "Nhân viên")
                    {
                        MainEmployer NhanVienForm = new MainEmployer(taiKhoan.Manhanvien, tenNhanVienHienThi); // TRUYỀN THAM SỐ
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
                    // Tài khoản không tồn tại hoặc sai mật khẩu
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi trong quá trình đăng nhập: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle PasswordChar của txtMatkhau
            // Nếu checkbox được tích, hiển thị mật khẩu (PasswordChar là '\0')
            // Nếu checkbox không được tích, ẩn mật khẩu (PasswordChar là '●' hoặc '*')
            txtMatkhau.PasswordChar = chkHienMatKhau.Checked ? '\0' : '●';
        }
        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
