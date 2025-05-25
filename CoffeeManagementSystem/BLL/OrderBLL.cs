using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite; // Cần để quản lý transaction trong BLL
using CoffeeManagementSystem;
using CoffeeManagementSystem.DAL; // Vẫn cần để khởi tạo các DAL
using CoffeeManagementSystem.BLL; // Đảm bảo using namespace của DouongBLL

namespace CoffeeManagementSystem.BLL
{
    public class OrderBLL
    {
        private DouongBLL _douongBLL;
        private DonhangDAL _donhangDAL;
        private ChitietdonhangDAL _chitietdonhangDAL;
        private string _loggedInManhanvien; // Mã nhân viên lập hóa đơn

        public OrderBLL(string manhanvien)
        {
            _douongBLL = new DouongBLL(); // Khởi tạo DouongBLL
            _donhangDAL = new DonhangDAL();
            _chitietdonhangDAL = new ChitietdonhangDAL();
            _loggedInManhanvien = manhanvien;
        }

        /// <summary>
        /// Tải tất cả danh sách đồ uống từ DouongBLL (đã bao gồm giá mới nhất).
        /// </summary>
        /// <returns>Danh sách Douong.</returns>
        public List<Douong> LoadAllDouongs()
        {
            try
            {
                // Gọi DouongBLL để lấy dữ liệu đồ uống
                return _douongBLL.GetAllDouongsWithLatestPrice();
            }
            catch (Exception ex)
            {
                // Ném lại lỗi để Form xử lý và hiển thị thông báo
                throw new Exception($"Lỗi BLL khi tải danh sách đồ uống: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Tìm kiếm đồ uống theo từ khóa từ DouongBLL (đã bao gồm giá mới nhất).
        /// </summary>
        /// <param name="searchTerm">Từ khóa tìm kiếm.</param>
        /// <returns>Danh sách Douong phù hợp.</returns>
        public List<Douong> SearchDouongs(string searchTerm)
        {
            try
            {
                // Gọi DouongBLL để tìm kiếm đồ uống
                return _douongBLL.SearchDouongsWithLatestPrice(searchTerm);
            }
            catch (Exception ex)
            {
                // Ném lại lỗi để Form xử lý
                throw new Exception($"Lỗi BLL khi tìm kiếm đồ uống: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Thêm hoặc cập nhật chi tiết đồ uống vào danh sách tạm thời.
        /// Logic này vẫn được giữ ở BLL vì nó liên quan đến việc quản lý trạng thái của đơn hàng tạm thời.
        /// </summary>
        /// <param name="selectedDouong">Đồ uống được chọn.</param>
        /// <param name="currentChiTietList">Danh sách chi tiết đơn hàng hiện tại (truyền tham chiếu).</param>
        public void AddOrUpdateChiTietHoaDonTamThoi(Douong selectedDouong, List<Chitietdonhang> currentChiTietList)
        {
            if (selectedDouong == null)
            {
                throw new ArgumentNullException(nameof(selectedDouong), "Đồ uống được chọn không thể null.");
            }

            // Tìm kiếm xem món đồ uống đã có trong danh sách tạm thời chưa
            Chitietdonhang existingItem = currentChiTietList.FirstOrDefault(item => item.Madouong == selectedDouong.Madouong);

            if (existingItem != null)
            {
                // Nếu đã có, tăng số lượng và cập nhật thành tiền
                existingItem.Soluong++;
                existingItem.Thanhtien = existingItem.Dongia * existingItem.Soluong;
            }
            else
            {
                // Nếu chưa có, thêm món mới vào danh sách
                currentChiTietList.Add(new Chitietdonhang
                {
                    // Madonhang sẽ được gán khi lưu vào DB
                    Madouong = selectedDouong.Madouong,
                    Tendouong = selectedDouong.Tendouong,
                    Dongia = selectedDouong.CurrentGia, // Lấy giá hiện tại từ đối tượng Douong
                    Soluong = 1,
                    Thanhtien = selectedDouong.CurrentGia * 1
                });
            }
        }

        /// <summary>
        /// Tạo hóa đơn mới và lưu chi tiết đơn hàng vào cơ sở dữ liệu.
        /// Phương thức này sử dụng Transaction để đảm bảo tính toàn vẹn dữ liệu.
        /// </summary>
        /// <param name="chiTietList">Danh sách chi tiết đơn hàng tạm thời.</param>
        /// <returns>True nếu tạo hóa đơn thành công, False nếu không.</returns>
        public bool CreateNewOrder(List<Chitietdonhang> chiTietList)
        {
            // Kiểm tra ràng buộc nghiệp vụ
            if (chiTietList == null || chiTietList.Count == 0)
            {
                throw new ArgumentException("Danh sách chi tiết đơn hàng không được trống.");
            }
            if (string.IsNullOrEmpty(_loggedInManhanvien))
            {
                throw new InvalidOperationException("Mã nhân viên lập hóa đơn không được trống. Vui lòng đăng nhập.");
            }

            // Tính tổng tiền của hóa đơn
            decimal tongTien = chiTietList.Sum(item => item.Thanhtien);

            // Tạo đối tượng Donhang mới
            Donhang newDonhang = new Donhang
            {
                Madonhang = GenerateUniqueMadonhangId(), // Tạo mã hóa đơn duy nhất
                Manhanvien = _loggedInManhanvien,
                Thoigiandat = DateTime.Now,
                Tongtien = tongTien,
                Trangthaidon = "Chưa thanh toán" // Trạng thái mặc định khi mới tạo
            };

            // Sử dụng transaction để đảm bảo rằng việc thêm hóa đơn và tất cả chi tiết hóa đơn
            // hoặc thành công hoàn toàn, hoặc thất bại hoàn toàn (Rollback).
            // Lấy ConnectionString từ BaseDataAccess để khởi tạo kết nối.
            using (SQLiteConnection connection = new SQLiteConnection(new BaseDataAccess().ConnectionString))
            {
                connection.Open();
                SQLiteTransaction transaction = connection.BeginTransaction();
                try
                {
                    // 1. Thêm hóa đơn chính vào CSDL
                    _donhangDAL.AddDonhang(newDonhang, connection, transaction);

                    // 2. Thêm từng chi tiết hóa đơn vào CSDL
                    foreach (var chiTiet in chiTietList)
                    {
                        chiTiet.Madonhang = newDonhang.Madonhang; // Gán mã hóa đơn vừa tạo cho chi tiết
                        // Không gán Machitietdonhang vì DB schema sử dụng khóa chính kép (Madonhang, Madouong)
                        _chitietdonhangDAL.AddChitietdonhang(chiTiet, connection, transaction);
                    }

                    transaction.Commit(); // Commit transaction nếu mọi thao tác thành công
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback nếu có bất kỳ lỗi nào xảy ra
                    // Ném lại lỗi để Form xử lý và hiển thị thông báo
                    throw new Exception($"Lỗi BLL khi tạo hóa đơn: {ex.Message}", ex);
                }
            }
        }

        /// <summary>
        /// Tạo mã đơn hàng duy nhất.
        /// </summary>
        /// <returns>Mã đơn hàng duy nhất.</returns>
        private string GenerateUniqueMadonhangId()
        {
            return "HD" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }
    }
}
