using CoffeeManagementSystem;
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
    public partial class CustomerForm : Form
    {
        private KhachhangDAL khachhangDAL; // Khai báo đối tượng DAL

        // Constructor của Form
        public CustomerForm()
        {
            InitializeComponent();
            khachhangDAL = new KhachhangDAL(); // Khởi tạo đối tượng DAL
            //this.txtSearch.KeyDown += new KeyEventHandler(txtSearch_KeyDown);
            LoadDanhSachKhachHang();
            // Gán sự kiện TextChanged cho TextBox tìm kiếm (để tìm khi gõ)
            this.txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
        }

        // Sự kiện Form Load: Tải dữ liệu khi Form được hiển thị
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            LoadDanhSachKhachHang(); // Gọi phương thức tải danh sách ban đầu
        }

        // Phương thức tải danh sách khách hàng và hiển thị lên DataGridView
        private void LoadDanhSachKhachHang()
        {
            try
            {
                // Lấy danh sách khách hàng từ DAL
                List<Khachhang> danhSach = khachhangDAL.GetAllKhachhangs();

                // Gán danh sách làm nguồn dữ liệu cho DataGridView
                // dgvKhachHang là tên DataGridView trên Form của bạn
                dgvKhachHang.DataSource = danhSach;

                // Tùy chọn: Tự động điều chỉnh kích thước cột
                // dgvKhachHang.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                // Tùy chọn: Xóa trắng các ô nhập liệu sau khi tải lại
                //ClearInputFields();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu không tải được dữ liệu
                MessageBox.Show("Không thể tải danh sách khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadFilteredData(string searchTerm)
        {
            try
            {
                List<Khachhang> ketQuaHienThi;

                // Kiểm tra nếu từ khóa tìm kiếm rỗng hoặc chỉ chứa khoảng trắng
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    // Nếu trống, tải toàn bộ danh sách khách hàng
                    ketQuaHienThi = khachhangDAL.GetAllKhachhangs();
                }
                else
                {
                    // Nếu có từ khóa, gọi phương thức tìm kiếm
                    ketQuaHienThi = khachhangDAL.SearchKhachhangs(searchTerm);
                }


                // Gán kết quả (toàn bộ hoặc đã lọc) làm nguồn dữ liệu cho DataGridView
                dgvKhachHang.DataSource = ketQuaHienThi;

                // Tùy chọn: Thông báo nếu không tìm thấy kết quả (chỉ khi có từ khóa và kết quả rỗng)
                if (ketQuaHienThi.Count == 0 && !string.IsNullOrWhiteSpace(searchTerm))
                {
                    // Bạn có thể hiển thị thông báo hoặc không, tùy ý
                    // MessageBox.Show($"Không tìm thấy khách hàng nào phù hợp với từ khóa '{searchTerm}'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Tùy chọn: Xóa trắng các ô nhập liệu sau khi tìm kiếm
                //ClearInputFields();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Lấy từ khóa hiện tại trong TextBox
            string searchTerm = txtSearch.Text.Trim();

            // Gọi phương thức tải dữ liệu đã lọc
            LoadFilteredData(searchTerm);
        }
        // Sự kiện khi click vào một dòng trong DataGridView
        // Đảm bảo sự kiện CellClick của dgvKhachHang được kết nối trong Designer
        // Sự kiện khi click vào một dòng trong DataGridView
        // Đảm bảo sự kiện CellClick của dgvKhachHang được kết nối trong Designer
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có phải click vào một dòng dữ liệu hợp lệ không
            // (Tránh click vào header hoặc các dòng không phải dữ liệu)
            // Đảm bảo tên control dgvKhachHang khớp
            if (e.RowIndex >= 0 && e.RowIndex < dgvKhachHang.Rows.Count)
            {
                // Lấy dòng được click
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

                // Lấy Mã khách hàng từ dòng được click
                // Lỗi "Column named Makhachhang cannot be found" xảy ra ở đây.
                // Điều này có thể do DataGridView đang tìm cột theo tên (Name) thay vì DataPropertyName.
                // Dựa trên ảnh bạn gửi, tên (Name) của cột Mã khách hàng là "Column1".

                string makhachhang = null;

                try
                {
                    // Thử truy cập bằng tên (Name) của cột trong Designer
                    // Thay "Column1" bằng tên (Name) thực tế của cột Mã khách hàng nếu khác
                    makhachhang = row.Cells["Column1"].Value?.ToString(); // <-- Sửa lỗi ở đây

                    // Nếu cách trên không hoạt động, bạn có thể thử tìm cột theo DataPropertyName
                    // DataGridViewColumn maKHColumn = dgvKhachHang.Columns.Cast<DataGridViewColumn>()
                    //                                     .FirstOrDefault(col => col.DataPropertyName == "Makhachhang");
                    // if (maKHColumn != null)
                    // {
                    //      makhachhang = row.Cells[maKHColumn.Index].Value?.ToString();
                    // }
                    // else
                    // {
                    //      MessageBox.Show("Cột 'Makhachhang' không được tìm thấy trong DataGridView. Vui lòng kiểm tra cấu hình cột.", "Lỗi cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //      return; // Dừng xử lý nếu không tìm thấy cột
                    // }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu việc truy cập cell bằng tên cột thất bại
                    MessageBox.Show($"Lỗi khi truy cập cột Mã khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Dừng xử lý
                }


                if (!string.IsNullOrEmpty(makhachhang))
                {
                    try
                    {
                        // Lấy thông tin chi tiết khách hàng từ DAL
                        // Đảm bảo đối tượng khachhangDAL đã được khởi tạo
                        // Đảm bảo lớp KhachhangDAL được using đúng namespace
                        Khachhang selectedKhachhang = khachhangDAL.GetKhachhangById(makhachhang);

                        if (selectedKhachhang != null)
                        {
                            // Tạo một instance mới của Form Chi Tiết ở chế độ Cập nhật
                            // Đảm bảo tên lớp Form Chi Tiết là CustomerInfor và namespace được using
                            FormChitiet formChiTiet = new FormChitiet(selectedKhachhang);

                            // Hiển thị Form Chi Tiết dưới dạng Dialog
                            if (formChiTiet.ShowDialog() == DialogResult.OK)
                            {
                                // Nếu Form Chi Tiết trả về DialogResult.OK (nghĩa là đã lưu thành công)
                                // Tải lại danh sách khách hàng trên Form chính
                                LoadDanhSachKhachHang();
                            }
                            // Nếu DialogResult không phải OK (ví dụ: Cancel), không làm gì
                        }
                        else
                        {
                            // Nếu GetKhachhangById trả về null (không tìm thấy khách hàng với mã này)
                            MessageBox.Show($"Không tìm thấy thông tin chi tiết cho khách hàng có mã '{makhachhang}'.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi khi lấy thông tin chi tiết khách hàng hoặc mở Form
                        MessageBox.Show("Lỗi khi lấy thông tin chi tiết khách hàng hoặc mở Form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // else
                // {
                //     // Nếu Makhachhang từ dòng được click là rỗng hoặc null (có thể xảy ra với dòng trống cuối cùng)
                //     // Không làm gì hoặc hiển thị thông báo tùy ý
                // }
            }
            // else
            // {
            //     // Nếu click vào header hoặc vùng không phải dòng dữ liệu
            //     // Không làm gì hoặc hiển thị thông báo tùy ý
            // }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormChitiet formChiTiet = new FormChitiet();
            // Hiển thị Form Chi Tiết dưới dạng Dialog
            // formChiTiet.ShowDialog() sẽ dừng thực thi code ở đây cho đến khi FormChiTiet đóng lại
            if (formChiTiet.ShowDialog() == DialogResult.OK)
            {
                // Nếu Form Chi Tiết trả về DialogResult.OK (nghĩa là đã lưu thành công)
                // Tải lại danh sách khách hàng trên Form chính để hiển thị dữ liệu mới
                LoadDanhSachKhachHang(); // Gọi lại phương thức tải danh sách khách hàng của Form chính
            }
        }
    }
}