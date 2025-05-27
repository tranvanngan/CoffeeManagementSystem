using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Đảm bảo using namespace chứa lớp Nhanvien (Model)
using CoffeeManagementSystem;

// Đảm bảo using namespace chứa lớp NhanvienBLL (BLL)
using CoffeeManagementSystem.BLL; // Thay thế bằng namespace thực tế của bạn nếu khác

namespace CoffeeManagementSystem // Đảm bảo namespace này khớp với dự án của bạn
{
    public partial class FormChiTietNhanvien : Form
    {
        private NhanvienBLL nhanvienBLL; // Thay thế NhanvienDAL bằng NhanvienBLL
        private Nhanvien currentNhanvien; // Đối tượng Nhanvien hiện tại (null nếu thêm mới)

        // Constructor cho chế độ Thêm mới
        public FormChiTietNhanvien()
        {
            InitializeComponent();
            nhanvienBLL = new NhanvienBLL(); // Khởi tạo BLL
            this.Text = "Thêm Nhân Viên Mới"; // Tiêu đề Form

            // Hiển thị tất cả các nút
            btnSave.Visible = true;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;

            // Điều chỉnh trạng thái Enabled cho chế độ Thêm mới
            btnSave.Enabled = true;
            btnUpdate.Enabled = false; // Không thể cập nhật khi thêm mới
            btnDelete.Enabled = false; // Không thể xóa khi thêm mới

            // Cho phép nhập Mã NV khi thêm mới
            txtMaNV.Enabled = true;

            // Khởi tạo ComboBox Giới tính
            InitializeGioitinhComboBox();
        }

        // Constructor cho chế độ Cập nhật (NHẬN 1 ĐỐI SỐ)
        public FormChiTietNhanvien(Nhanvien nhanvienToEdit)
        {
            InitializeComponent();
            nhanvienBLL = new NhanvienBLL(); // Khởi tạo BLL
            this.Text = "Cập Nhật Thông Tin Nhân Viên"; // Tiêu đề Form
            currentNhanvien = nhanvienToEdit; // Lưu đối tượng nhân viên cần sửa

            // Hiển thị tất cả các nút
            btnSave.Visible = true;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;

            // Điều chỉnh trạng thái Enabled cho chế độ Cập nhật
            btnSave.Enabled = false; // Không thể lưu mới khi cập nhật
            btnUpdate.Enabled = true; // Có thể cập nhật
            btnDelete.Enabled = true; // Có thể xóa

            // Vô hiệu hóa ô Mã NV khi sửa
            txtMaNV.Enabled = false;

            // Khởi tạo ComboBox Giới tính
            InitializeGioitinhComboBox();

            // Hiển thị thông tin nhân viên lên các control
            DisplayNhanvienInfo();
        }

        // Phương thức khởi tạo ComboBox Giới tính
        private void InitializeGioitinhComboBox()
        {
            // Đảm bảo tên control là cbxGioitinh
            cbGioiTinh.Items.Add("Nam");
            cbGioiTinh.Items.Add("Nữ");
            cbGioiTinh.Items.Add("Khác");
            cbGioiTinh.SelectedIndex = 0; // Chọn mặc định là "Nam"
        }

        // Phương thức hiển thị thông tin nhân viên lên các control
        private void DisplayNhanvienInfo()
        {
            if (currentNhanvien != null)
            {
                // Đảm bảo tên control txtMaNV, txtHoTen, txtDiaChi, txtSDT, txtEmail khớp
                txtMaNV.Text = currentNhanvien.Manhanvien;
                txtHoTen.Text = currentNhanvien.Hoten;
                dateTimePickerNgaySinh.Value = currentNhanvien.Ngaysinh;
                cbGioiTinh.SelectedItem = currentNhanvien.Gioitinh; // Chọn giới tính trong ComboBox
                txtDiaChi.Text = currentNhanvien.Diachi;
                txtSDT.Text = currentNhanvien.Sodienthoai;
                txtEmail.Text = currentNhanvien.Email;
                dateTimePickerNgayVaoLam.Value = currentNhanvien.Ngayvaolam;
            }
        }

        // Phương thức lấy thông tin từ các control và tạo/cập nhật đối tượng Nhanvien
        private Nhanvien GetNhanvienInfoFromControls()
        {
            Nhanvien nhanvien = currentNhanvien ?? new Nhanvien();

            // Mã nhân viên chỉ được lấy từ TextBox khi THÊM MỚI
            if (currentNhanvien == null) // Chế độ thêm mới
            {
                nhanvien.Manhanvien = txtMaNV.Text.Trim();
            }
            // else: Manhanvien đã có giá trị từ currentNhanvien và không cần thay đổi từ txtMaNV

            nhanvien.Hoten = txtHoTen.Text.Trim();
            nhanvien.Ngaysinh = dateTimePickerNgaySinh.Value;
            nhanvien.Gioitinh = cbGioiTinh.SelectedItem?.ToString(); // Lấy giá trị từ ComboBox
            nhanvien.Diachi = txtDiaChi.Text.Trim();
            // Xử lý các cột có thể NULL
            string sdt = txtSDT.Text.Trim();
            nhanvien.Sodienthoai = string.IsNullOrEmpty(sdt) ? null : sdt;
            string email = txtEmail.Text.Trim();
            nhanvien.Email = string.IsNullOrEmpty(email) ? null : email;
            nhanvien.Ngayvaolam = dateTimePickerNgayVaoLam.Value;

            return nhanvien;
        }

        // =====================================================
        // XỬ LÝ CÁC NÚT TRÊN FORM CHI TIẾT
        // =====================================================

        // Sự kiện click nút "Lưu" (cho chế độ Thêm mới)
        private void btnSave_Click(object sender, EventArgs e)
        {
            Nhanvien newNhanvien = GetNhanvienInfoFromControls();

            // Kiểm tra dữ liệu bắt buộc (có thể chuyển logic này sang BLL để tái sử dụng)
            if (string.IsNullOrEmpty(newNhanvien.Manhanvien) || string.IsNullOrEmpty(newNhanvien.Hoten) || string.IsNullOrEmpty(newNhanvien.Gioitinh) || string.IsNullOrEmpty(newNhanvien.Diachi))
            {
                MessageBox.Show("Mã nhân viên, Họ tên, Giới tính và Địa chỉ không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Gọi BLL để thêm nhân viên và kiểm tra kết quả trả về
                if (nhanvienBLL.AddNhanvien(newNhanvien)) // Đã sửa: gọi BLL và kiểm tra bool
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại. Có thể mã nhân viên đã tồn tại hoặc dữ liệu không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (ArgumentException ex) // Bắt lỗi nghiệp vụ từ BLL (ví dụ: mã nhân viên trùng, định dạng sai)
            {
                MessageBox.Show($"Lỗi nhập liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) // Bắt các lỗi khác (ví dụ: lỗi kết nối CSDL)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện click nút "Cập Nhật" (cho chế độ Sửa)
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Nhanvien updatedNhanvien = GetNhanvienInfoFromControls();

            // Kiểm tra dữ liệu bắt buộc (có thể chuyển logic này sang BLL để tái sử dụng)
            if (string.IsNullOrEmpty(updatedNhanvien.Hoten) || string.IsNullOrEmpty(updatedNhanvien.Gioitinh) || string.IsNullOrEmpty(updatedNhanvien.Diachi))
            {
                MessageBox.Show("Họ tên, Giới tính và Địa chỉ không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Gọi BLL để cập nhật nhân viên và kiểm tra kết quả trả về
                if (nhanvienBLL.UpdateNhanvien(updatedNhanvien)) // Đã sửa: gọi BLL và kiểm tra bool
                {
                    MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật nhân viên thất bại. Không tìm thấy nhân viên hoặc dữ liệu không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (ArgumentException ex) // Bắt lỗi nghiệp vụ từ BLL
            {
                MessageBox.Show($"Lỗi nhập liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) // Bắt các lỗi khác
            {
                MessageBox.Show("Lỗi khi cập nhật nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện click nút "Xóa"
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentNhanvien == null)
            {
                MessageBox.Show("Không có nhân viên nào được chọn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên '{currentNhanvien.Hoten}' (Mã: {currentNhanvien.Manhanvien}) không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // Gọi BLL để xóa nhân viên và kiểm tra kết quả trả về
                    if (nhanvienBLL.DeleteNhanvien(currentNhanvien.Manhanvien)) // Đã sửa: gọi BLL và kiểm tra bool
                    {
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Xóa nhân viên thất bại. Không tìm thấy nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (ArgumentException ex) // Bắt lỗi nghiệp vụ từ BLL
                {
                    MessageBox.Show($"Lỗi nhập liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex) // Bắt các lỗi khác
                {
                    MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Sự kiện click nút đóng Form (X) ở góc trên bên phải
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }



        // Sự kiện click nút đóng Form (X) ở góc trên bên phải

        private void lblCid_Click(object sender, EventArgs e)
        {

        }
    }
}
