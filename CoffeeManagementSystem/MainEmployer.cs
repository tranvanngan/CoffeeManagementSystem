﻿using Guna.UI2.WinForms;
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
            LoadFormCon(new DashboardForm());
        }

        // Constructor MỚI để nhận thông tin nhân viên từ DangNhapForm
        public MainEmployer(string maNhanVien, string tenNhanVien) : this() 
        {
            _loggedInMaNhanVien = maNhanVien; 
            _maNhanVienHienTai = maNhanVien;
            _tenNhanVienHienTai = tenNhanVien;
            if (lblName != null) 
            {
                lblName.Text = tenNhanVien;
            }
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
        }

        private void add1_Load(object sender, EventArgs e)
        {
          
        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            LoadFormCon(new CustomerForm());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            // Tạo OrderForm và truyền mã nhân viên, tên nhân viên
            OrderForm orderForm = new OrderForm(_maNhanVienHienTai, _tenNhanVienHienTai);
            LoadFormCon(orderForm); 
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
