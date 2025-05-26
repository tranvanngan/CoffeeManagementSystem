using System;
using System.Collections.Generic;
using System.Linq; // For potential LINQ operations if needed in BLL
using System.Windows.Forms; // Only for MessageBox in error handling examples

// Ensure these namespaces align with your project structure
using CoffeeManagementSystem.DAL; // Reference to your DAL
using CoffeeManagementSystem;    // Reference to your Loaidouong model

namespace CoffeeManagementSystem.BLL
{
    public class LoaidouongBLL
    {
        private LoaidouongDAL _loaidouongDAL;

        public LoaidouongBLL()
        {
            _loaidouongDAL = new LoaidouongDAL();
        }

        /// <summary>
        /// Retrieves all drink categories.
        /// Includes basic error handling and can incorporate business rules.
        /// </summary>
        /// <returns>A list of Loaidouong objects, or an empty list if an error occurs.</returns>
        public List<Loaidouong> GetAllLoaidouongs()
        {
            try
            {
                // You can add business rules here before calling DAL, e.g.,
                // checking user permissions, caching, etc.
                return _loaidouongDAL.GetAllLoaidouongs();
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using a logging framework)
                MessageBox.Show($"Lỗi BLL khi lấy danh sách loại đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Loaidouong>(); // Return an empty list on error
            }
        }

        /// <summary>
        /// Retrieves a drink category by its ID.
        /// </summary>
        /// <param name="maloai">The ID of the drink category.</param>
        /// <returns>A Loaidouong object if found, otherwise null.</returns>
        public Loaidouong GetLoaidouongById(string maloai)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(maloai))
            {
                MessageBox.Show("Mã loại đồ uống không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            try
            {
                return _loaidouongDAL.GetLoaidouongById(maloai);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi BLL khi lấy loại đồ uống theo ID: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Adds a new drink category to the system.
        /// Includes business validation for Maloai and Tenloai.
        /// </summary>
        /// <param name="loaidouong">The Loaidouong object to add.</param>
        /// <returns>True if the operation was successful, false otherwise.</returns>
        public bool AddLoaidouong(Loaidouong loaidouong)
        {
            // Business validation: Check if Maloai and Tenloai are provided and not just whitespace
            if (loaidouong == null)
            {
                MessageBox.Show("Đối tượng loại đồ uống không thể rỗng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(loaidouong.Maloai))
            {
                MessageBox.Show("Mã loại đồ uống không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(loaidouong.Tenloai))
            {
                MessageBox.Show("Tên loại đồ uống không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // You might add more complex business rules here, e.g.:
            // - Check if Maloai already exists before adding
            // if (_loaidouongDAL.GetLoaidouongById(loaidouong.Maloai) != null)
            // {
            //     MessageBox.Show("Mã loại đồ uống đã tồn tại. Vui lòng chọn mã khác.", "Lỗi trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     return false;
            // }

            try
            {
                _loaidouongDAL.AddLoaidouong(loaidouong);
                MessageBox.Show("Thêm loại đồ uống thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi BLL khi thêm loại đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Updates an existing drink category.
        /// Includes business validation.
        /// </summary>
        /// <param name="loaidouong">The Loaidouong object with updated information.</param>
        /// <returns>True if the operation was successful, false otherwise.</returns>
        public bool UpdateLoaidouong(Loaidouong loaidouong)
        {
            // Business validation
            if (loaidouong == null)
            {
                MessageBox.Show("Đối tượng loại đồ uống không thể rỗng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(loaidouong.Maloai))
            {
                MessageBox.Show("Mã loại đồ uống không được để trống khi cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(loaidouong.Tenloai))
            {
                MessageBox.Show("Tên loại đồ uống không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // You might add a check here to ensure the Maloai exists before attempting to update
            // if (_loaidouongDAL.GetLoaidouongById(loaidouong.Maloai) == null)
            // {
            //     MessageBox.Show("Loại đồ uống cần cập nhật không tồn tại.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     return false;
            // }

            try
            {
                _loaidouongDAL.UpdateLoaidouong(loaidouong);
                MessageBox.Show("Cập nhật loại đồ uống thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi BLL khi cập nhật loại đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Deletes a drink category by its ID.
        /// </summary>
        /// <param name="maloai">The ID of the drink category to delete.</param>
        /// <returns>True if the operation was successful, false otherwise.</returns>
        public bool DeleteLoaidouong(string maloai)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(maloai))
            {
                MessageBox.Show("Mã loại đồ uống không được để trống khi xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Business rule: Check for dependencies before deleting (e.g., if any drinks are linked to this category)
            // This would require a DAL method to check for dependencies in the 'Douong' table.
            // Example:
            // if (_douongDAL.DoesCategoryHaveDrinks(maloai)) // Assuming a DouongDAL exists
            // {
            //     MessageBox.Show("Không thể xóa loại đồ uống này vì có đồ uống đang sử dụng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     return false;
            // }

            try
            {
                _loaidouongDAL.DeleteLoaidouong(maloai);
                MessageBox.Show("Xóa loại đồ uống thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi BLL khi xóa loại đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Searches for drink categories based on a keyword.
        /// </summary>
        /// <param name="searchTerm">The keyword to search for.</param>
        /// <returns>A list of matching Loaidouong objects.</returns>
        public List<Loaidouong> SearchLoaidouongs(string searchTerm)
        {
            try
            {
                // The DAL already handles the empty/whitespace search term.
                return _loaidouongDAL.SearchLoaidouongs(searchTerm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi BLL khi tìm kiếm loại đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Loaidouong>();
            }
        }
    }
}