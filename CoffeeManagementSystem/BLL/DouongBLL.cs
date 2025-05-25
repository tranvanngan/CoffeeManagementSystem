using System;
using System.Collections.Generic;
using CoffeeManagementSystem;
using CoffeeManagementSystem.DAL; // Cần để khởi tạo DouongDAL

namespace CoffeeManagementSystem.BLL
{
    public class DouongBLL
    {
        private DouongDAL _douongDAL;

        public DouongBLL()
        {
            _douongDAL = new DouongDAL();
        }

        /// <summary>
        /// Lấy tất cả đồ uống với giá mới nhất (giá từ CSDL).
        /// </summary>
        /// <returns>Danh sách các đối tượng Douong.</returns>
        public List<Douong> GetAllDouongsWithLatestPrice()
        {
            try
            {
                // Gọi DAL để lấy dữ liệu. Logic giá mới nhất được giả định nằm trong DAL
                // hoặc là cột 'Gia' trong bảng Douong là giá hiện tại.
                return _douongDAL.GetAllDouongs();
            }
            catch (Exception ex)
            {
                // Ném lại lỗi để lớp gọi (ví dụ: OrderBLL, OrderForm) xử lý
                throw new Exception($"Lỗi BLL khi tải tất cả đồ uống: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Tìm kiếm đồ uống theo từ khóa với giá mới nhất.
        /// </summary>
        /// <param name="searchTerm">Từ khóa tìm kiếm.</param>
        /// <returns>Danh sách các đối tượng Douong phù hợp.</returns>
        public List<Douong> SearchDouongsWithLatestPrice(string searchTerm)
        {
            try
            {
                // Gọi DAL để tìm kiếm dữ liệu.
                return _douongDAL.SearchDouongs(searchTerm);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL khi tìm kiếm đồ uống: {ex.Message}", ex);
            }
        }

        // Các logic nghiệp vụ khác liên quan đến Douong có thể được thêm vào đây,
        // ví dụ: kiểm tra tồn kho, tính toán giá khuyến mãi, v.v.
    }
}
