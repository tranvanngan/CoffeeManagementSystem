using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeManagementSystem.BLL;
using CoffeeManagementSystem.DAL;
using CoffeeManagementSystem.Utilities; // <-- THÊM DÒNG NÀY

namespace CoffeeManagementSystem
{
    public partial class PaymentForm : Form
    {
        private PaymentBLL _paymentBLL;
        private Khachhang currentSelectedCustomer;

        public PaymentForm(List<Chitietdonhang> dsChiTiet, string manhanvien, string tenNhanVien)
        {
            InitializeComponent();
            _paymentBLL = new PaymentBLL(dsChiTiet, manhanvien, tenNhanVien);

            this.Text = "Payment";

            this.Load += PaymentForm_Load;
            this.btnThanhToan.Click += btnThanhToan_Click;
            this.txtKhachHangName.Leave += txtKhachHangName_Leave;

            // LOG: Khi PaymentForm được khởi tạo
            Logger.LogInfo("PaymentForm đã được khởi tạo.");
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            lblMaHoaDonValue.Text = _paymentBLL.GetMaHoaDonHienTai();
            lblNguoiLapValue.Text = _paymentBLL.GetTenNhanVienLapHoaDon();
            lblNgayValue.Text = _paymentBLL.GetNgayLapHoaDon().ToShortDateString();

            SetupListViewColumns();
            LoadChiTietHoaDon();
            TinhTongTien();

            // LOG: Khi PaymentForm đã tải xong
            Logger.LogInfo("PaymentForm đã tải xong dữ liệu và hiển thị ban đầu.");
        }

        /// <summary>
        /// Thiết lập các cột cho ListView hiển thị chi tiết đơn hàng.
        /// </summary>
        private void SetupListViewColumns()
        {
            if (lvwChiTietHoaDon != null)
            {
                lvwChiTietHoaDon.View = View.Details;
                lvwChiTietHoaDon.GridLines = true;
                lvwChiTietHoaDon.FullRowSelect = true;

                lvwChiTietHoaDon.Columns.Clear();

                lvwChiTietHoaDon.Columns.Add("STT", 50, HorizontalAlignment.Center);
                lvwChiTietHoaDon.Columns.Add("Tên đồ uống", 200, HorizontalAlignment.Left);
                lvwChiTietHoaDon.Columns.Add("Số lượng", 80, HorizontalAlignment.Center);
                lvwChiTietHoaDon.Columns.Add("Đơn giá", 100, HorizontalAlignment.Right);
                lvwChiTietHoaDon.Columns.Add("Thành tiền", 120, HorizontalAlignment.Right);

                // LOG: Debug khi các cột ListView được thiết lập
                Logger.LogDebug("ListView columns for ChiTietHoaDon have been set up.");
            }
        }

        /// <summary>
        /// Tải dữ liệu chi tiết đơn hàng (từ danh sách tạm thời trong BLL) vào ListView.
        /// </summary>
        private void LoadChiTietHoaDon()
        {
            if (lvwChiTietHoaDon != null)
            {
                lvwChiTietHoaDon.Items.Clear();
                List<Chitietdonhang> dsChiTiet = _paymentBLL.GetDsChiTietHoaDon();

                for (int i = 0; i < dsChiTiet.Count; i++)
                {
                    Chitietdonhang chiTiet = dsChiTiet[i];

                    ListViewItem lvi = new ListViewItem((i + 1).ToString());
                    lvi.SubItems.Add(chiTiet.Tendouong);
                    lvi.SubItems.Add(chiTiet.Soluong.ToString());
                    lvi.SubItems.Add(chiTiet.Dongia.ToString("N0"));
                    lvi.SubItems.Add(chiTiet.Thanhtien.ToString("N0"));

                    lvwChiTietHoaDon.Items.Add(lvi);
                }
                // LOG: Debug khi chi tiết đơn hàng được tải vào ListView
                Logger.LogDebug($"Đã tải {dsChiTiet.Count} chi tiết đơn hàng vào ListView.");
            }
        }

        /// <summary>
        /// Tính toán và hiển thị tổng thành tiền.
        /// </summary>
        private void TinhTongTien()
        {
            decimal tongTien = _paymentBLL.CalculateTongTien();
            txtTongThanhTienValue.Text = tongTien.ToString("N0");

            // LOG: Debug tổng tiền hiển thị trên UI
            Logger.LogDebug($"Tổng tiền hiển thị trên UI: {tongTien:N0}");
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Thanh toán".
        /// Logic chính đã được chuyển vào BLL. Form sẽ bắt và hiển thị Exception.
        /// </summary>
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // LOG: Thông tin khi người dùng nhấn nút 'Thanh toán'
            Logger.LogInfo("Người dùng nhấn nút 'Thanh toán'.");

            DialogResult confirmResult = MessageBox.Show("Bạn có chắc chắn muốn thanh toán đơn hàng này không?", "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                // LOG: Thông tin khi người dùng xác nhận thanh toán
                Logger.LogInfo("Người dùng xác nhận thanh toán.");
                try
                {
                    Khachhang customerFromBLL;
                    bool success = _paymentBLL.ProcessPayment(txtKhachHangName.Text.Trim(), out customerFromBLL);
                    currentSelectedCustomer = customerFromBLL; // Cập nhật khách hàng được chọn sau khi BLL xử lý

                    if (success)
                    {
                        MessageBox.Show("Đơn hàng đã được thanh toán và lưu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // LOG: Thông tin khi thanh toán hoàn tất thành công
                        Logger.LogInfo($"Thanh toán hoàn tất thành công cho hóa đơn: {lblMaHoaDonValue.Text}");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // SỬA LỖI: Thay LogWarning bằng LogError vì có exception
                    Logger.LogError($"Thanh toán thất bại (lỗi nghiệp vụ): {ex.Message}", ex);
                }
                catch (KhachhangNotFoundException ex) // Đây là lỗi bạn đã định nghĩa (nếu có)
                {
                    // SỬA LỖI: Thay LogWarning bằng LogError vì có exception
                    Logger.LogError($"Khách hàng '{txtKhachHangName.Text.Trim()}' không tìm thấy khi thanh toán.", ex);

                    DialogResult addCustomer = MessageBox.Show(
                        ex.Message + Environment.NewLine + "Bạn có muốn thêm mới khách hàng này không?",
                        "Xác nhận thêm khách hàng",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (addCustomer == DialogResult.Yes)
                    {
                        // LOG: Thông tin người dùng muốn thêm khách hàng mới
                        Logger.LogInfo($"Người dùng muốn thêm mới khách hàng: {txtKhachHangName.Text.Trim()}.");
                        try
                        {
                            currentSelectedCustomer = _paymentBLL.AddNewKhachhang(txtKhachHangName.Text.Trim());
                            MessageBox.Show($"Đã thêm mới khách hàng: {txtKhachHangName.Text.Trim()}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // LOG: Thông tin đã thêm khách hàng thành công qua UI prompt
                            Logger.LogInfo($"Đã thêm mới khách hàng '{txtKhachHangName.Text.Trim()}' thông qua UI prompt.");
                            // Sau khi thêm khách hàng, bạn có thể tự động thử lại thanh toán hoặc yêu cầu người dùng nhấn nút "Thanh toán" một lần nữa.
                            // Để giữ đơn giản, tôi chỉ hiển thị thông báo.
                        }
                        catch (Exception addEx)
                        {
                            MessageBox.Show($"Lỗi khi thêm mới khách hàng: {addEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // LOG: Lỗi khi thêm khách hàng mới từ UI
                            Logger.LogError($"Lỗi khi thêm mới khách hàng '{txtKhachHangName.Text.Trim()}' từ UI.", addEx);
                            ClearCustomerInfo();
                        }
                    }
                    else
                    {
                        txtKhachHangName.Text = "";
                        ClearCustomerInfo();
                        // LOG: Thông tin người dùng từ chối thêm khách hàng
                        Logger.LogInfo($"Người dùng từ chối thêm mới khách hàng '{txtKhachHangName.Text.Trim()}'.");
                    }
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // SỬA LỖI: Thay LogWarning bằng LogError vì có exception
                    Logger.LogError($"Thanh toán thất bại (tham số không hợp lệ): {ex.Message}", ex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thanh toán đơn hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // LOG: Lỗi hệ thống không xác định khi thanh toán
                    Logger.LogError($"Lỗi hệ thống không xác định khi thanh toán đơn hàng. Tên khách hàng: '{txtKhachHangName.Text.Trim()}'", ex);
                }
            }
            else
            {
                // LOG: Thông tin khi người dùng hủy thanh toán
                Logger.LogInfo("Người dùng hủy thanh toán.");
            }
        }

        /// <summary>
        /// Xử lý sự kiện Leave của txtKhachHangName.
        /// Logic chính đã được chuyển vào BLL, Form chỉ hiển thị kết quả.
        /// </summary>
        private void txtKhachHangName_Leave(object sender, EventArgs e)
        {
            string customerName = txtKhachHangName.Text.Trim();
            // LOG: Debug khi sự kiện Leave kích hoạt
            Logger.LogDebug($"txtKhachHangName_Leave được kích hoạt với tên: '{customerName}'");

            if (string.IsNullOrEmpty(customerName))
            {
                ClearCustomerInfo();
                // LOG: Thông tin tên khách hàng rỗng
                Logger.LogInfo("Tên khách hàng rỗng, thông tin khách hàng đã được xóa.");
                return;
            }

            try
            {
                Khachhang existingCustomer = _paymentBLL.GetKhachhangByName(customerName);

                if (existingCustomer == null)
                {
                    // LOG: Thông tin khách hàng không tồn tại
                    Logger.LogInfo($"Khách hàng '{customerName}' không tồn tại. Hỏi người dùng có muốn thêm mới.");
                    DialogResult confirmResult = MessageBox.Show(
                        $"Khách hàng '{customerName}' chưa tồn tại. Bạn có muốn thêm mới khách hàng này không?",
                        "Xác nhận thêm khách hàng",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (confirmResult == DialogResult.Yes)
                    {
                        // LOG: Thông tin người dùng muốn thêm khách hàng mới
                        Logger.LogInfo($"Người dùng muốn thêm mới khách hàng: {customerName}.");
                        try
                        {
                            currentSelectedCustomer = _paymentBLL.AddNewKhachhang(customerName);
                            MessageBox.Show($"Đã thêm mới khách hàng: {customerName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // LOG: Thông tin đã thêm khách hàng thành công
                            Logger.LogInfo($"Đã thêm mới khách hàng '{customerName}'.");
                            // Cập nhật UI nếu cần (ví dụ: hiển thị điểm tích lũy)
                        }
                        catch (Exception addEx)
                        {
                            MessageBox.Show($"Lỗi khi thêm mới khách hàng: {addEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // LOG: Lỗi khi thêm khách hàng mới
                            Logger.LogError($"Lỗi khi thêm mới khách hàng '{customerName}'.", addEx);
                            ClearCustomerInfo();
                        }
                    }
                    else
                    {
                        txtKhachHangName.Text = "";
                        ClearCustomerInfo();
                        // LOG: Thông tin người dùng từ chối thêm khách hàng
                        Logger.LogInfo($"Người dùng từ chối thêm mới khách hàng '{customerName}'.");
                    }
                }
                else
                {
                    currentSelectedCustomer = existingCustomer;
                    MessageBox.Show($"Khách hàng '{customerName}' đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // LOG: Thông tin khách hàng đã tồn tại và được chọn
                    Logger.LogInfo($"Đã tìm thấy và chọn khách hàng '{customerName}' (Mã: {currentSelectedCustomer.Makhachhang}).");
                    // Cập nhật UI nếu cần (ví dụ: hiển thị điểm tích lũy)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // LOG: Lỗi khi xử lý khách hàng trong sự kiện Leave
                Logger.LogError($"Lỗi khi xử lý khách hàng '{customerName}' trong PaymentForm.txtKhachHangName_Leave.", ex);
                ClearCustomerInfo();
            }
        }

        private void ClearCustomerInfo()
        {
            currentSelectedCustomer = null;
            // Nếu bạn có label hiển thị điểm tích lũy, hãy bỏ comment dòng này:
            // lblDiemTichLuy.Text = "Điểm tích lũy: 0";
            // LOG: Debug khi thông tin khách hàng bị xóa
            Logger.LogDebug("Thông tin khách hàng đã được xóa trên UI.");
        }
        private void lblNguoiLapValue_Click(object sender, EventArgs e)
        {

        }
    }
}