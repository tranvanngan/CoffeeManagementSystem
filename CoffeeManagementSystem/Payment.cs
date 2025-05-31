﻿using System;
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
using CoffeeManagementSystem.Utilities;
using System.Drawing.Printing; // Thêm namespace này cho PrintDocument

namespace CoffeeManagementSystem
{
    public partial class PaymentForm : Form
    {
        private PaymentBLL _paymentBLL;
        private Khachhang currentSelectedCustomer;

        // Thêm các đối tượng in
        private PrintDocument printDocumentInvoice;
        private PrintPreviewDialog printPreviewDialogInvoice;

        public PaymentForm(List<Chitietdonhang> dsChiTiet, string manhanvien, string tenNhanVien)
        {
            InitializeComponent();
            _paymentBLL = new PaymentBLL(dsChiTiet, manhanvien, tenNhanVien);

            this.Text = "Payment";

            this.Load += PaymentForm_Load;
            this.btnThanhToan.Click += btnThanhToan_Click;
            this.txtKhachHangName.Leave += txtKhachHangName_Leave;

            // Khởi tạo PrintDocument và PrintPreviewDialog
            printDocumentInvoice = new PrintDocument();
            printDocumentInvoice.PrintPage += new PrintPageEventHandler(this.printDocumentInvoice_PrintPage);
            printPreviewDialogInvoice = new PrintPreviewDialog();
            printPreviewDialogInvoice.Document = printDocumentInvoice;

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

        //Tải dữ liệu chi tiết đơn hàng (từ danh sách tạm thời trong BLL) vào ListView.
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

        //Tính toán và hiển thị tổng thành tiền.
        private void TinhTongTien()
        {
            decimal tongTien = _paymentBLL.CalculateTongTien();
            txtTongThanhTienValue.Text = tongTien.ToString("N0");

            // LOG: Debug tổng tiền hiển thị trên UI
            Logger.LogDebug($"Tổng tiền hiển thị trên UI: {tongTien:N0}");
        }

        //Xử lý sự kiện click nút "Thanh toán".
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

                        // *** THÊM PHẦN IN HÓA ĐƠN Ở ĐÂY ***
                        DialogResult printConfirm = MessageBox.Show("Bạn có muốn in hóa đơn này không?", "In Hóa Đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (printConfirm == DialogResult.Yes)
                        {
                            printPreviewDialogInvoice.ShowDialog();
                        }
                        // *** KẾT THÚC PHẦN IN HÓA ĐƠN ***

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Logger.LogError($"Thanh toán thất bại (lỗi nghiệp vụ): {ex.Message}", ex);
                }
                catch (KhachhangNotFoundException ex) // Đây là lỗi bạn đã định nghĩa (nếu có)
                {
                    Logger.LogError($"Khách hàng '{txtKhachHangName.Text.Trim()}' không tìm thấy khi thanh toán.", ex);

                    DialogResult addCustomer = MessageBox.Show(
                        ex.Message + Environment.NewLine + "Bạn có muốn thêm mới khách hàng này không?",
                        "Xác nhận thêm khách hàng",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (addCustomer == DialogResult.Yes)
                    {
                        Logger.LogInfo($"Người dùng muốn thêm mới khách hàng: {txtKhachHangName.Text.Trim()}.");
                        try
                        {
                            currentSelectedCustomer = _paymentBLL.AddNewKhachhang(txtKhachHangName.Text.Trim());
                            MessageBox.Show($"Đã thêm mới khách hàng: {txtKhachHangName.Text.Trim()}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Logger.LogInfo($"Đã thêm mới khách hàng '{txtKhachHangName.Text.Trim()}' thông qua UI prompt.");
                        }
                        catch (Exception addEx)
                        {
                            MessageBox.Show($"Lỗi khi thêm mới khách hàng: {addEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Logger.LogError($"Lỗi khi thêm mới khách hàng '{txtKhachHangName.Text.Trim()}' từ UI.", addEx);
                            ClearCustomerInfo();
                        }
                    }
                    else
                    {
                        txtKhachHangName.Text = "";
                        ClearCustomerInfo();
                        Logger.LogInfo($"Người dùng từ chối thêm mới khách hàng '{txtKhachHangName.Text.Trim()}'.");
                    }
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Logger.LogError($"Thanh toán thất bại (tham số không hợp lệ): {ex.Message}", ex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thanh toán đơn hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.LogError($"Lỗi hệ thống không xác định khi thanh toán đơn hàng. Tên khách hàng: '{txtKhachHangName.Text.Trim()}'", ex);
                }
            }
            else
            {
                Logger.LogInfo("Người dùng hủy thanh toán.");
            }
        }
        /// Xử lý sự kiện Leave của txtKhachHangName.
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
            Logger.LogDebug("Thông tin khách hàng đã được xóa trên UI.");
        }
        private void lblNguoiLapValue_Click(object sender, EventArgs e)
        {

        }

        // Phương thức PrintPage để vẽ hóa đơn
        private void printDocumentInvoice_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font headerFont = new Font("Arial", 16, FontStyle.Bold);
            Font subHeaderFont = new Font("Arial", 11, FontStyle.Bold);
            Font normalFont = new Font("Arial", 10);
            Font smallFont = new Font("Arial", 9);
            Pen borderPen = new Pen(Color.Black, 1);

            float lineHeight = normalFont.GetHeight() + 2; // Khoảng cách dòng cho nội dung
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float currentX;

            // Tiêu đề hóa đơn
            StringFormat sfCenter = new StringFormat();
            sfCenter.Alignment = StringAlignment.Center;
            sfCenter.LineAlignment = StringAlignment.Center;
            graphics.DrawString("HÓA ĐƠN THANH TOÁN", headerFont, Brushes.Black, e.PageBounds.Width / 2, y, sfCenter);
            y += headerFont.GetHeight() + 20;

            // Thông tin hóa đơn
            graphics.DrawString($"Mã hóa đơn: {lblMaHoaDonValue.Text}", normalFont, Brushes.Black, x, y);
            y += lineHeight;
            graphics.DrawString($"Khách hàng: {txtKhachHangName.Text}", normalFont, Brushes.Black, x, y);
            y += lineHeight;
            graphics.DrawString($"Người lập: {lblNguoiLapValue.Text}", normalFont, Brushes.Black, x, y);
            y += lineHeight;
            graphics.DrawString($"Ngày: {lblNgayValue.Text}", normalFont, Brushes.Black, x, y);
            y += lineHeight + 20; // Khoảng cách trước bảng chi tiết

            // Vẽ tiêu đề bảng
            float colSTTWidth = 50;
            float colTenDoUongWidth = 200;
            float colSoLuongWidth = 80;
            float colDonGiaWidth = 100;
            float colThanhTienWidth = 120;

            // In tiêu đề cột
            currentX = x;
            RectangleF headerRect;
            StringFormat sfHeader = new StringFormat();
            sfHeader.Alignment = StringAlignment.Center;
            sfHeader.LineAlignment = StringAlignment.Center;

            headerRect = new RectangleF(currentX, y, colSTTWidth, lineHeight + 5);
            graphics.FillRectangle(Brushes.LightGray, headerRect);
            graphics.DrawRectangle(borderPen, currentX, y, colSTTWidth, lineHeight + 5);
            graphics.DrawString("STT", subHeaderFont, Brushes.Black, headerRect, sfHeader);
            currentX += colSTTWidth;

            headerRect = new RectangleF(currentX, y, colTenDoUongWidth, lineHeight + 5);
            graphics.FillRectangle(Brushes.LightGray, headerRect);
            graphics.DrawRectangle(borderPen, currentX, y, colTenDoUongWidth, lineHeight + 5);
            graphics.DrawString("Tên đồ uống", subHeaderFont, Brushes.Black, headerRect, sfHeader);
            currentX += colTenDoUongWidth;

            headerRect = new RectangleF(currentX, y, colSoLuongWidth, lineHeight + 5);
            graphics.FillRectangle(Brushes.LightGray, headerRect);
            graphics.DrawRectangle(borderPen, currentX, y, colSoLuongWidth, lineHeight + 5);
            graphics.DrawString("Số lượng", subHeaderFont, Brushes.Black, headerRect, sfHeader);
            currentX += colSoLuongWidth;

            headerRect = new RectangleF(currentX, y, colDonGiaWidth, lineHeight + 5);
            graphics.FillRectangle(Brushes.LightGray, headerRect);
            graphics.DrawRectangle(borderPen, currentX, y, colDonGiaWidth, lineHeight + 5);
            graphics.DrawString("Đơn giá", subHeaderFont, Brushes.Black, headerRect, sfHeader);
            currentX += colDonGiaWidth;

            headerRect = new RectangleF(currentX, y, colThanhTienWidth, lineHeight + 5);
            graphics.FillRectangle(Brushes.LightGray, headerRect);
            graphics.DrawRectangle(borderPen, currentX, y, colThanhTienWidth, lineHeight + 5);
            graphics.DrawString("Thành tiền", subHeaderFont, Brushes.Black, headerRect, sfHeader);
            currentX += colThanhTienWidth;

            y += lineHeight + 5;

            // In chi tiết đơn hàng
            StringFormat sfLeft = new StringFormat();
            sfLeft.Alignment = StringAlignment.Near;
            sfLeft.LineAlignment = StringAlignment.Center;
            sfLeft.Trimming = StringTrimming.EllipsisCharacter;
            sfLeft.FormatFlags = StringFormatFlags.NoWrap; // Không xuống dòng

            StringFormat sfCenterData = new StringFormat();
            sfCenterData.Alignment = StringAlignment.Center;
            sfCenterData.LineAlignment = StringAlignment.Center;

            StringFormat sfRight = new StringFormat();
            sfRight.Alignment = StringAlignment.Far;
            sfRight.LineAlignment = StringAlignment.Center;
            sfRight.Trimming = StringTrimming.EllipsisCharacter;
            sfRight.FormatFlags = StringFormatFlags.NoWrap;

            List<Chitietdonhang> dsChiTiet = _paymentBLL.GetDsChiTietHoaDon();
            for (int i = 0; i < dsChiTiet.Count; i++)
            {
                Chitietdonhang chiTiet = dsChiTiet[i];

                currentX = x;

                // STT
                graphics.DrawString((i + 1).ToString(), smallFont, Brushes.Black,
                                    new RectangleF(currentX, y, colSTTWidth, lineHeight), sfCenterData);
                graphics.DrawRectangle(borderPen, currentX, y, colSTTWidth, lineHeight);
                currentX += colSTTWidth;

                // Tên đồ uống
                graphics.DrawString(chiTiet.Tendouong, smallFont, Brushes.Black,
                                    new RectangleF(currentX, y, colTenDoUongWidth, lineHeight), sfLeft);
                graphics.DrawRectangle(borderPen, currentX, y, colTenDoUongWidth, lineHeight);
                currentX += colTenDoUongWidth;

                // Số lượng
                graphics.DrawString(chiTiet.Soluong.ToString(), smallFont, Brushes.Black,
                                    new RectangleF(currentX, y, colSoLuongWidth, lineHeight), sfCenterData);
                graphics.DrawRectangle(borderPen, currentX, y, colSoLuongWidth, lineHeight);
                currentX += colSoLuongWidth;

                // Đơn giá
                graphics.DrawString(chiTiet.Dongia.ToString("N0"), smallFont, Brushes.Black,
                                    new RectangleF(currentX, y, colDonGiaWidth, lineHeight), sfRight);
                graphics.DrawRectangle(borderPen, currentX, y, colDonGiaWidth, lineHeight);
                currentX += colDonGiaWidth;

                // Thành tiền
                graphics.DrawString(chiTiet.Thanhtien.ToString("N0"), smallFont, Brushes.Black,
                                    new RectangleF(currentX, y, colThanhTienWidth, lineHeight), sfRight);
                graphics.DrawRectangle(borderPen, currentX, y, colThanhTienWidth, lineHeight);
                currentX += colThanhTienWidth;

                y += lineHeight;
            }

            // Tổng thành tiền
            y += 20; // Khoảng cách sau bảng
            string totalText = $"Tổng thành tiền: {txtTongThanhTienValue.Text} VNĐ";
            graphics.DrawString(totalText, subHeaderFont, Brushes.Black,
                                e.MarginBounds.Right - graphics.MeasureString(totalText, subHeaderFont).Width, y);

            e.HasMorePages = false; // Đã in hết trên một trang
        }
    }

    // Đảm bảo bạn có class KhachhangNotFoundException nếu bạn đang sử dụng nó
    public class KhachhangNotFoundException : Exception
    {
        public KhachhangNotFoundException() { }
        public KhachhangNotFoundException(string message) : base(message) { }
        public KhachhangNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}