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
    public partial class SalaryForm : Form
    {
        public SalaryForm()
        {
            InitializeComponent();
        }
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            // Gọi phương thức cập nhật hiển thị nút dựa trên tab hiện tại
            UpdateButtonVisibility();
        }

        // Sự kiện xảy ra khi tab được chọn trong TabControl thay đổi
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Gọi phương thức cập nhật hiển thị nút khi tab thay đổi
            UpdateButtonVisibility();
        }

        // Phương thức giúp cập nhật trạng thái hiển thị của các nút
        private void UpdateButtonVisibility()
        {
            // Kiểm tra xem tab page nào đang được chọn
            if (tabControl1.SelectedTab == tabPage1) 
                btnAdd.Visible = true;

            else // Nếu tab hiện tại không phải là tab page 1 (ví dụ: tab page 2, 3, ...)
                
                btnAdd.Visible = false;
            
        }
    }
}
