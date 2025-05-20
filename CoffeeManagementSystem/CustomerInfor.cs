using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeManagementSystem.DAL;
namespace CoffeeManagementSystem
{

    public partial class FormChitiet : Form
    {
        private KhachhangDAL khachhangDAL = new KhachhangDAL(); // Đối tượng DAL
        private Khachhang currentKhachhang; // Đối tượng Khachhang hiện tại (null nếu thêm mới)

        public FormChitiet()
        {
            InitializeComponent(); // GIỮ NGUYÊN DÒNG NÀY - Nó được tạo bởi Designer

            // khachhangDAL = new KhachhangDAL(); // Đã khởi tạo ở trên

            this.Text = "Thêm Khách Hàng Mới"; // Tiêu đề Form
            // Hiển thị nút Lưu, ẩn nút Cập Nhật
            // Đảm bảo tên control btnLuu và btnCapNhat khớp
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            // Cho phép nhập Mã KH khi thêm mới
            // Đảm bảo tên control txtMaKH khớp
            txtMaKH.Enabled = true;

            // Không gọi DisplayKhachhangInfo() ở đây vì đây là Form thêm mới
        }

        // Constructor cho chế độ Cập nhật
        public FormChitiet(Khachhang khachhangToEdit)
        {
            InitializeComponent(); // GIỮ NGUYÊN DÒNG NÀY - Nó được tạo bởi Designer

            this.Text = "Cập Nhật Thông Tin Khách Hàng"; // Tiêu đề Form
            currentKhachhang = khachhangToEdit; // Lưu đối tượng khách hàng cần sửa
            // Hiển thị nút Cập Nhật, ẩn nút Lưu
            // Đảm bảo tên control btnLuu và btnCapNhat khớp
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            // Vô hiệu hóa ô Mã KH khi sửa
            // Đảm bảo tên control txtMaKH khớp
            txtMaKH.Enabled = false;

            // Hiển thị thông tin khách hàng lên các control
            DisplayKhachhangInfo();
        }

        private void DisplayKhachhangInfo()
        {
            if (currentKhachhang != null)
            {
                txtMaKH.Text = currentKhachhang.Makhachhang;
                txtHoTen.Text = currentKhachhang.Hoten;
                txtSDT.Text = currentKhachhang.Sodienthoai;
                txtEmail.Text = currentKhachhang.Email;
                // Xử lý hiển thị DateTimePicker
                dateTimePickerNgayDangKy.Value = currentKhachhang.Ngaydangky;
                // Xử lý hiển thị NumericUpDown
                numericUpDownDiem.Value = currentKhachhang.Diemtichluy;
            }
        }
        // Phương thức lấy thông tin từ các control và tạo/cập nhật đối tượng Khachhang
        private Khachhang GetKhachhangInfoFromControls()
        {
            // Tạo đối tượng Khachhang mới hoặc sử dụng currentKhachhang nếu đang sửa
            Khachhang khachhang = currentKhachhang ?? new Khachhang();

            khachhang.Makhachhang = txtMaKH.Text.Trim();
            khachhang.Hoten = txtHoTen.Text.Trim();
            // Xử lý các cột có thể NULL
            string sdt = txtSDT.Text.Trim();
            khachhang.Sodienthoai = string.IsNullOrEmpty(sdt) ? null : sdt;
            string email = txtEmail.Text.Trim();
            khachhang.Email = string.IsNullOrEmpty(email) ? null : email;
            // Lấy giá trị từ DateTimePicker
            khachhang.Ngaydangky = dateTimePickerNgayDangKy.Value;
            // Lấy giá trị từ NumericUpDown
            khachhang.Diemtichluy = (int)numericUpDownDiem.Value;

            return khachhang;
        }
        // Sự kiện click nút "Lưu" (cho chế độ Thêm mới)
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các control
            Khachhang newKhachhang = GetKhachhangInfoFromControls();

            // Kiểm tra dữ liệu bắt buộc (ví dụ: Mã KH, Họ tên)
            if (string.IsNullOrEmpty(newKhachhang.Makhachhang) || string.IsNullOrEmpty(newKhachhang.Hoten))
            {
                MessageBox.Show("Mã khách hàng và Họ tên không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                khachhangDAL.AddKhachhang(newKhachhang); // Gọi phương thức Thêm từ DAL
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Đặt kết quả Form là OK
                this.Close(); // Đóng Form
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện click nút "Cập Nhật" (cho chế độ Sửa)
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            // Lấy thông tin cập nhật từ các control
            // Sử dụng currentKhachhang để giữ nguyên Mã KH
            Khachhang updatedKhachhang = GetKhachhangInfoFromControls();

            // Kiểm tra dữ liệu bắt buộc (ví dụ: Họ tên)
            if (string.IsNullOrEmpty(updatedKhachhang.Hoten))
            {
                MessageBox.Show("Họ tên không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                khachhangDAL.UpdateKhachhang(updatedKhachhang); // Gọi phương thức Cập nhật từ DAL
                MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Đặt kết quả Form là OK
                this.Close(); // Đóng Form
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện click nút "Hủy"
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Đặt kết quả Form là Cancel
            this.Close(); // Đóng Form
        }

        // Sự kiện click nút đóng Form (X) ở góc trên bên phải
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Đặt kết quả Form là Cancel
            this.Close(); // Đóng Form
        }
    }
}
