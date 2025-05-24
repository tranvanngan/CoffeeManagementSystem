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
            // Gán sự kiện CellClick cho DataGridView (để hiển thị Form Chi Tiết)
            this.dgvKhachHang.CellClick += new DataGridViewCellEventHandler(dgvKhachHang_CellClick);

            // Gán sự kiện Click cho nút Thêm mới (giả định tên nút là btnAdd)
            this.btnAdd.Click += new EventHandler(btnAdd_Click); // Đã đổi tên nút
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
        // Sự kiện khi click vào một dòng trong DataGridView
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có phải click vào một dòng dữ liệu hợp lệ không
            // Tránh click vào header hoặc các dòng không phải dữ liệu
            // Tránh dòng trống thêm mới ở cuối (nếu AllowUserToAddRows là true)
            if (e.RowIndex >= 0 && e.RowIndex < dgvKhachHang.Rows.Count - (dgvKhachHang.AllowUserToAddRows ? 1 : 0))
            {
                // Lấy đối tượng Khachhang được liên kết với dòng
                // Đây là cách đáng tin cậy nhất để lấy dữ liệu từ dòng đã chọn
                Khachhang selectedKhachhang = dgvKhachHang.Rows[e.RowIndex].DataBoundItem as Khachhang;

                if (selectedKhachhang != null)
                {
                    try
                    {
                        // Tạo một instance mới của Form Chi Tiết ở chế độ Cập nhật
                        // Đảm bảo tên lớp Form Chi Tiết của bạn là FormChitiet hoặc CustomerInfor
                        FormChitiet formChiTiet = new FormChitiet(selectedKhachhang); // Hoặc CustomerInfor formChiTiet = new CustomerInfor(selectedKhachhang);

                        // Hiển thị Form Chi Tiết dưới dạng Dialog
                        if (formChiTiet.ShowDialog() == DialogResult.OK)
                        {
                            // Nếu Form Chi Tiết trả về DialogResult.OK (nghĩa là đã lưu thành công)
                            // Tải lại danh sách khách hàng trên Form chính
                            LoadDanhSachKhachHang();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lấy thông tin chi tiết khách hàng hoặc mở Form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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