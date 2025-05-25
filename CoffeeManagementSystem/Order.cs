using CoffeeManagementSystem.DAL;
using CoffeeManagementSystem.BLL; // Thêm using cho BLL
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
    public partial class OrderForm : Form
    {
        public string CurrentManhanvien { get; set; }
        public string CurrentTenNhanvien { get; set; }

        private Timer _messageTimer;
        private DouongDAL douongDAL; // GIỮ NGUYÊN: Biến này vẫn được giữ theo yêu cầu của bạn
        

        // KHAI BÁO THÊM: Đối tượng BLL để xử lý logic nghiệp vụ
        private OrderBLL _orderBLL;

        // KHAI BÁO THIẾU: Danh sách chi tiết hóa đơn tạm thời
        private List<Chitietdonhang> danhSachChiTietHoaDonTamThoi;

        public OrderForm()
        {
            InitializeComponent(); // Đảm bảo dòng này luôn ở đầu constructor

            douongDAL = new DouongDAL();
            danhSachChiTietHoaDonTamThoi = new List<Chitietdonhang>();

            // KHỞI TẠO: OrderBLL với mã nhân viên mặc định là null.
            // Nó sẽ được cập nhật khi constructor có tham số được gọi.
            _orderBLL = new OrderBLL(null);

            // Khởi tạo lblStatusMessage nếu nó không được tạo trong Designer.cs
            // Nếu bạn đã kéo Label vào Form và đặt tên là lblStatusMessage, bạn không cần dòng này
            // Đảm bảo Label này có trên Form của bạn
            if (this.Controls.Find("lblStatusMessage", true).FirstOrDefault() is Label statusLabel)
            {
                lblStatusMessage = statusLabel;
            }
            else
            {
                // Nếu không tìm thấy Label, tạo một Label tạm thời để tránh lỗi biên dịch
                // Bạn nên thêm Label này vào Designer của OrderForm
                lblStatusMessage = new Label { Name = "lblStatusMessage", Text = "", AutoSize = true, Location = new Point(10, 10) };
                this.Controls.Add(lblStatusMessage);
            }

            _messageTimer = new Timer();
            _messageTimer.Interval = 3000;
            _messageTimer.Tick += MessageTimer_Tick;

            dgvDouong.AutoGenerateColumns = false;
            dgvDouong.AllowUserToAddRows = false;
            dgvDouong.AllowUserToDeleteRows = false;
            dgvDouong.EditMode = DataGridViewEditMode.EditProgrammatically;

            // Gán sự kiện DoubleClick cho dgvDouong
            this.dgvDouong.CellDoubleClick += new DataGridViewCellEventHandler(dgvDouong_CellDoubleClick); // Đã sửa tên phương thức
            // Gán sự kiện Click cho nút Tạo Hóa Đơn
            this.btnTaoHoaDon.Click += new EventHandler(btnTaoHoaDon_Click);

            // ĐÃ LOẠI BỎ: Các lời gọi và phương thức liên quan đến ListView tạm thời
            // SetupListViewHoaDonTamThoi();
            LoadDanhSachDouong();
            // CapNhatHienThiHoaDonTamThoi(); // Không còn ListView tạm thời để cập nhật hiển thị

            // Gán sự kiện cho các control trên tab Đồ uống
            this.txtTimkiemdouong.TextChanged += new EventHandler(txtTimkiemdouong_TextChanged);
            // ĐÃ LOẠI BỎ: dgvDouong.CellClick vì DoubleClick đã xử lý việc chọn món
            // this.dgvDouong.CellClick += new DataGridViewCellEventHandler(dgvDouong_CellClick);
        }

        public OrderForm(string manhanvien, string tenNhanVien) : this() // Gọi constructor mặc định
        {
            CurrentManhanvien = manhanvien;
            CurrentTenNhanvien = tenNhanVien;
            // CẬP NHẬT: OrderBLL với mã nhân viên chính xác
            _orderBLL = new OrderBLL(CurrentManhanvien);
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            LoadDanhSachDouong();
        }

        private void LoadDanhSachDouong()
        {
            try
            {
                // CẬP NHẬT: Gọi BLL để lấy danh sách đồ uống
                List<Douong> danhSach = _orderBLL.LoadAllDouongs();
                dgvDouong.DataSource = null;
                dgvDouong.DataSource = danhSach;
                dgvDouong.Refresh();
                dgvDouong.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách đồ uống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tải danh sách đồ uống đã lọc và hiển thị lên DataGridView.
        /// </summary>
        private void LoadFilteredDouongData(string searchTerm)
        {
            try
            {
                // CẬP NHẬT: Gọi BLL để tìm kiếm đồ uống
                List<Douong> ketQuaHienThi = _orderBLL.SearchDouongs(searchTerm);

                dgvDouong.DataSource = null;
                dgvDouong.DataSource = ketQuaHienThi;
                dgvDouong.Refresh();
                dgvDouong.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimkiemdouong_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtTimkiemdouong.Text.Trim();
            LoadFilteredDouongData(searchTerm);
        }
        private void dgvLoaidouong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDouong.Rows.Count - (dgvDouong.AllowUserToAddRows ? 1 : 0))
            {
                // Lấy đối tượng Loaidouong từ dòng được click
                // LƯU Ý: Tên phương thức dgvLoaidouong_CellClick cho dgvDouong có thể gây nhầm lẫn.
                // Nếu dgvLoaidouong là một DataGridView riêng, code này sẽ đúng cho nó.
                // Nếu đây là sự kiện của dgvDouong, bạn có thể muốn đổi tên phương thức
                // và đảm bảo kiểu dữ liệu được lấy ra là Douong, không phải Loaidouong.
                Loaidouong selectedLoaidouong = dgvDouong.Rows[e.RowIndex].DataBoundItem as Loaidouong;

                if (selectedLoaidouong != null)
                {
                    // Mở LoaidouongDetailForm ở chế độ chỉnh sửa
                    AddTypeofdrinkForm detailForm = new AddTypeofdrinkForm(selectedLoaidouong.Maloai);
                    if (detailForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadDanhSachDouong(); // Tải lại danh sách sau khi chỉnh sửa/xóa thành công
                    }
                }
            }
        }

        private void dgvDouong_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDouong.Rows.Count)
            {
                Douong selectedDoUong = dgvDouong.Rows[e.RowIndex].DataBoundItem as Douong;

                if (selectedDoUong != null)
                {
                    try
                    {
                        // CẬP NHẬT: Gọi BLL để thêm hoặc cập nhật chi tiết vào danh sách tạm thời
                        _orderBLL.AddOrUpdateChiTietHoaDonTamThoi(selectedDoUong, danhSachChiTietHoaDonTamThoi);

                        // Hiển thị thông báo chọn thành công
                        if (lblStatusMessage != null)
                        {
                            lblStatusMessage.Text = $"Đã thêm '{selectedDoUong.Tendouong}' vào hóa đơn tạm thời. Tổng số món đã chọn: {danhSachChiTietHoaDonTamThoi.Sum(item => item.Soluong)}";
                            _messageTimer.Stop();
                            _messageTimer.Start();
                        }
                        else
                        {
                            MessageBox.Show($"Đã thêm '{selectedDoUong.Tendouong}' vào hóa đơn tạm thời.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi thêm đồ uống vào hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private string GenerateUniqueChiTietHoaDonId()
        {
            return "CTHD" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            if (danhSachChiTietHoaDonTamThoi.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đồ uống để tạo hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(CurrentManhanvien) || string.IsNullOrEmpty(CurrentTenNhanvien))
            {
                MessageBox.Show("Không thể tạo hóa đơn. Thiếu thông tin nhân viên lập hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // CẬP NHẬT: Gọi BLL để tạo hóa đơn và lưu chi tiết vào DB trong một transaction
                bool hoaDonCreated = _orderBLL.CreateNewOrder(danhSachChiTietHoaDonTamThoi);

                if (hoaDonCreated)
                {
                    // Nếu hóa đơn được tạo thành công trong DB, mở PaymentForm
                    PaymentForm hoaDonForm = new PaymentForm(danhSachChiTietHoaDonTamThoi, CurrentManhanvien, CurrentTenNhanvien);

                    if (hoaDonForm.ShowDialog() == DialogResult.OK)
                    {
                        // Nếu hóa đơn được thanh toán/lưu thành công trong PaymentForm,
                        // xóa danh sách tạm thời và cập nhật lại giao diện
                        danhSachChiTietHoaDonTamThoi.Clear();
                        // ĐÃ LOẠI BỎ: CapNhatHienThiHoaDonTamThoi(); // Không còn ListView tạm thời để cập nhật hiển thị
                        if (lblStatusMessage != null)
                        {
                            lblStatusMessage.Text = "Hóa đơn đã được tạo và xử lý thành công!";
                            _messageTimer.Stop();
                            _messageTimer.Start();
                        }
                        else
                        {
                            MessageBox.Show("Hóa đơn đã được tạo và xử lý thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        // Người dùng hủy hóa đơn, có thể hỏi xem có muốn giữ lại danh sách tạm thời không
                        if (MessageBox.Show("Bạn có muốn hủy hóa đơn này và xóa các mục đã chọn không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            danhSachChiTietHoaDonTamThoi.Clear();
                            // ĐÃ LOẠI BỎ: CapNhatHienThiHoaDonTamThoi(); // Không còn ListView tạm thời để cập nhật hiển thị
                            if (lblStatusMessage != null)
                            {
                                lblStatusMessage.Text = "Đã hủy hóa đơn tạm thời.";
                                _messageTimer.Stop();
                                _messageTimer.Start();
                            }
                        }
                    }
                }
            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show(argEx.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException invOpEx)
            {
                MessageBox.Show(invOpEx.Message, "Lỗi thao tác", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MessageTimer_Tick(object sender, EventArgs e)
        {
            if (lblStatusMessage != null)
            {
                lblStatusMessage.Text = "";
            }
            _messageTimer.Stop();
        }
    }
}
