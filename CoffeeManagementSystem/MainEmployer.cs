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
    public partial class MainEmployer : Form
    {
        private string _loggedInMaNhanVien; 
        private string _maNhanVienHienTai;
        private string _tenNhanVienHienTai;

        // Constructor mặc định (đã có sẵn)
        public MainEmployer()
        {
            InitializeComponent();
        }

        // Constructor MỚI để nhận thông tin nhân viên từ DangNhapForm
        public MainEmployer(string maNhanVien, string tenNhanVien) : this() // Gọi constructor mặc định
        {
            _loggedInMaNhanVien = maNhanVien; 
            _maNhanVienHienTai = maNhanVien;
            _tenNhanVienHienTai = tenNhanVien;

            // Tùy chọn: Hiển thị tên nhân viên trên lblName hoặc lblNhanVien nếu bạn có
            if (lblName != null) // Hoặc tên control label của bạn
            {
                lblName.Text = tenNhanVien;
            }
            // Hoặc nếu bạn có một label khác cho vị trí/tên người dùng
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

        private void MainForm_Load(object sender, EventArgs e) // Tên method nên là MainEmployer_Load để đúng với tên Form
        {
            // Bạn có thể đặt logic load ban đầu ở đây, ví dụ tải dữ liệu, thiết lập UI
        }

        private void add1_Load(object sender, EventArgs e)
        {
            // Sự kiện này có thể không cần thiết nếu 'add1' không phải là một control cụ thể
        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {
            // Sự kiện vẽ cho guna2GradientPanel2
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            LoadFormCon(new CustomerForm());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ĐÃ SỬA: Truyền thông tin nhân viên vào OrderForm
        private void btnOrder_Click(object sender, EventArgs e)
        {
            // Tạo OrderForm và truyền mã nhân viên, tên nhân viên
            OrderForm orderForm = new OrderForm(_maNhanVienHienTai, _tenNhanVienHienTai);
            LoadFormCon(orderForm); // Sử dụng phương thức LoadFormCon để hiển thị OrderForm
        }

        private void lblNhanVien_Click(object sender, EventArgs e)
        {
            // Xử lý sự kiện click cho label nhân viên
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            // Sự kiện vẽ cho panelMain
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Sự kiện vẽ cho guna2GradientPanel1
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            // Xử lý sự kiện click cho label tên (có thể là tên nhân viên)
        }

        private void btnQuanLyTaiKhoan_Click(object sender, EventArgs e)
        {
            //LoadFormCon(new Account()); // Uncomment nếu bạn có Form Account
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // Sự kiện vẽ cho panel2
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();

            // 2. Tạo một thể hiện mới của DangNhapForm
            DangNhapForm loginForm = new DangNhapForm();

            // 3. Hiển thị DangNhapForm
            loginForm.Show();
            this.Close();
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            LoadFormCon(new Infor(_loggedInMaNhanVien));
        }

        
    }
}
