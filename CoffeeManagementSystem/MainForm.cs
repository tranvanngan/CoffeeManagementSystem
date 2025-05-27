using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagementSystem
{
    public partial class MainForm : Form
    {
        // Thêm các biến để lưu trữ mã và tên nhân viên đăng nhập
        private string _loggedInMaNhanVien;
        private string _loggedInTenNhanVien;

        // Constructor mặc định (đã có sẵn)
        public MainForm()
        {
            InitializeComponent();
            LoadFormCon(new DashboardForm());
        }

        // Constructor MỚI để nhận thông tin nhân viên từ DangNhapForm
        public MainForm(string maNhanVien, string tenNhanVien) : this() // Gọi constructor mặc định
        {
            _loggedInMaNhanVien = maNhanVien;
            _loggedInTenNhanVien = tenNhanVien;

            // Tùy chọn: Hiển thị tên nhân viên trên lblName hoặc một label khác trên MainForm
            if (lblName != null) // Giả định bạn có một Label tên là lblName trên MainForm để hiển thị tên người dùng
            {
                lblName.Text = tenNhanVien;
            }
            // Hoặc nếu bạn có một label khác cho vị trí/tên người dùng, ví dụ:
            // if (lblChaoMung != null) { lblChaoMung.Text = $"Chào mừng, {tenNhanVien}"; }
        }

        private void LoadFormCon(Form formCon)
        {
            // Xóa form con hiện tại nếu có
            panelMain.Controls.Clear();

            // Cấu hình form con
            formCon.TopLevel = false;
            formCon.FormBorderStyle = FormBorderStyle.None;
            formCon.Dock = DockStyle.Fill;

            // Thêm vào panel và hiển thị
            panelMain.Controls.Add(formCon);
            formCon.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Bạn có thể đặt logic load ban đầu ở đây, ví dụ tải dữ liệu, thiết lập UI
        }

        private void add1_Load(object sender, EventArgs e)
        {
            // Sự kiện này có thể không cần thiết nếu 'add1' không phải là một control cụ thể
        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            LoadFormCon(new DashboardForm());
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            LoadFormCon(new CustomerForm());
        }

        private void btnEmployer_Click(object sender, EventArgs e)
        {
            // Khi mở EmployerForm, truyền thông tin nhân viên đã đăng nhập
            // Đảm bảo EmployerForm cũng có constructor nhận 2 tham số này
            LoadFormCon(new EmployerForm());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTrangChu_Click_1(object sender, EventArgs e)
        {
            LoadFormCon(new DashboardForm());
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            LoadFormCon(new DrinkForm());
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            LoadFormCon(new ReportForm());
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            LoadFormCon(new SalaryForm());
        }

        private void lblQuanLy_Click(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            LoadFormCon(new Infor(_loggedInMaNhanVien));
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
           
            // 1. Ẩn MainForm hiện tại
            this.Hide();

            // 2. Tạo một thể hiện mới của DangNhapForm
            DangNhapForm loginForm = new DangNhapForm();

            // 3. Hiển thị DangNhapForm
            loginForm.Show();
            this.Close();

            // 4. Đóng MainForm hiện tại sau khi DangNhapForm được hiển thị
            // (Điều này quan trọng để đảm bảo MainForm được giải phóng tài nguyên)
            // Application.Exit(); // Không nên dùng Application.Exit() ở đây nếu bạn muốn DangNhapForm tiếp tục chạy.
            // this.Close(); // Dùng this.Close() để đóng MainForm hiện tại.
        }

    }
}
