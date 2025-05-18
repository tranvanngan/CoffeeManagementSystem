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
        public MainForm()
        {
            InitializeComponent();
            LoadDashboard();
        }
        private void LoadDashboard()
        {
            // Xóa form con hiện tại nếu có
            this.panelMain.Controls.Clear();

            // Khởi tạo và cấu hình form con
            DashboardForm trangChu = new DashboardForm();
            trangChu.TopLevel = false;          // Cho phép nhúng vào panel
            trangChu.FormBorderStyle = FormBorderStyle.None;
            trangChu.Dock = DockStyle.Fill;

            // Thêm vào panel
            this.panelMain.Controls.Add(trangChu);
            trangChu.Show();
        }
        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {

        }

        private void close_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void add1_Load(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
