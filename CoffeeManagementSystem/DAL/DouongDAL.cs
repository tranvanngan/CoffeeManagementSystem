﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms; // Chỉ dùng cho MessageBox ví dụ (sẽ được thay thế bằng throw)
using System.Linq; // Cần cho LINQ (FirstOrDefault)
using System.Data; // Needed for DBNull.Value

namespace CoffeeManagementSystem.DAL
{
    public class DouongDAL : BaseDataAccess
    {
        private GiadouongDAL giadouongDAL; // Khai báo đối tượng GiadouongDAL

        public DouongDAL() : base()
        {
            giadouongDAL = new GiadouongDAL(); // Khởi tạo GiadouongDAL trong constructor
        }

        // =====================================================
        // PHƯƠNG THỨC LẤY DANH SÁCH ĐỒ UỐNG
        // =====================================================

        /// <summary>
        /// Lấy tất cả đồ uống từ CSDL và điền giá mới nhất cho từng đồ uống.
        /// </summary>
        /// <returns>Danh sách các đối tượng Douong.</returns>
        public List<Douong> GetAllDouongs()
        {
            List<Douong> douongs = new List<Douong>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = "SELECT Madouong, Tendouong, Maloai, Mota, Hinhanh FROM Douong";

                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Douong douong = new Douong
                                {
                                    Madouong = reader["Madouong"].ToString(),
                                    Tendouong = reader["Tendouong"].ToString(),
                                    Maloai = reader["Maloai"].ToString(),
                                    Mota = reader["Mota"] != DBNull.Value ? reader["Mota"].ToString() : null,
                                    Hinhanh = reader["Hinhanh"] != DBNull.Value ? reader["Hinhanh"].ToString() : null
                                };

                                // Lấy giá mới nhất và gán vào thuộc tính CurrentGia
                                // Sửa lỗi: Truy cập thuộc tính Giaban của đối tượng Giadouong và xử lý null
                                douong.CurrentGia = giadouongDAL.GetLatestGiaByMadouong(douong.Madouong)?.Giaban ?? 0m;

                                douongs.Add(douong);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // ĐIỀU CHỈNH: Thay thế MessageBox.Show bằng cách ném lỗi để BLL xử lý
                    throw new Exception($"Lỗi DAL khi lấy tất cả đồ uống: {ex.Message}", ex);
                }
            }
            return douongs;
        }

        /// <summary>
        /// Tìm kiếm đồ uống theo tên hoặc mã.
        /// </summary>
        /// <param name="searchTerm">Từ khóa tìm kiếm.</param>
        /// <returns>Danh sách các đối tượng Douong phù hợp.</returns>
        public List<Douong> SearchDouongs(string searchTerm)
        {
            List<Douong> douongs = new List<Douong>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = @"
                        SELECT Madouong, Tendouong, Maloai, Mota, Hinhanh 
                        FROM Douong 
                        WHERE LOWER(Tendouong) LIKE @SearchTerm OR LOWER(Madouong) LIKE @SearchTerm";

                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm.ToLower() + "%");

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Douong douong = new Douong
                                {
                                    Madouong = reader["Madouong"].ToString(),
                                    Tendouong = reader["Tendouong"].ToString(),
                                    Maloai = reader["Maloai"].ToString(),
                                    Mota = reader["Mota"] != DBNull.Value ? reader["Mota"].ToString() : null,
                                    Hinhanh = reader["Hinhanh"] != DBNull.Value ? reader["Hinhanh"].ToString() : null
                                };
                                // Lấy giá mới nhất và gán vào thuộc tính CurrentGia
                                // Sửa lỗi: Truy cập thuộc tính Giaban của đối tượng Giadouong và xử lý null
                                douong.CurrentGia = giadouongDAL.GetLatestGiaByMadouong(douong.Madouong)?.Giaban ?? 0m;
                                douongs.Add(douong);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // ĐIỀU CHỈNH: Thay thế MessageBox.Show bằng cách ném lỗi để BLL xử lý
                    throw new Exception($"Lỗi DAL khi tìm kiếm đồ uống: {ex.Message}", ex);
                }
            }
            return douongs;
        }

        /// <summary>
        /// Lấy một đồ uống theo mã đồ uống.
        /// </summary>
        /// <param name="madouong">Mã đồ uống.</param>
        /// <returns>Đối tượng Douong hoặc null nếu không tìm thấy.</returns>
        public Douong GetDouongById(string madouong)
        {
            Douong douong = null;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = "SELECT Madouong, Tendouong, Maloai, Mota, Hinhanh FROM Douong WHERE Madouong = @Madouong";
                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        command.Parameters.AddWithValue("@Madouong", madouong);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                douong = new Douong
                                {
                                    Madouong = reader["Madouong"].ToString(),
                                    Tendouong = reader["Tendouong"].ToString(),
                                    Maloai = reader["Maloai"].ToString(),
                                    Mota = reader["Mota"] != DBNull.Value ? reader["Mota"].ToString() : null,
                                    Hinhanh = reader["Hinhanh"] != DBNull.Value ? reader["Hinhanh"].ToString() : null
                                };
                                // Lấy giá mới nhất cho đồ uống cụ thể này
                                // Sửa lỗi: Truy cập thuộc tính Giaban của đối tượng Giadouong và xử lý null
                                douong.CurrentGia = giadouongDAL.GetLatestGiaByMadouong(douong.Madouong)?.Giaban ?? 0m;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // ĐIỀU CHỈNH: Thay thế MessageBox.Show bằng cách ném lỗi để BLL xử lý
                    throw new Exception($"Lỗi DAL khi lấy đồ uống theo ID: {ex.Message}", ex);
                }
            }
            return douong;
        }

        // =====================================================
        // PHƯƠNG THỨC THÊM/CẬP NHẬT/XÓA ĐỒ UỐNG
        // =====================================================

        /// <summary>
        /// Thêm một đồ uống mới vào CSDL.
        /// </summary>
        /// <param name="douong">Đối tượng Douong cần thêm.</param>
        public void AddDouong(Douong douong)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string insertSql = "INSERT INTO Douong (Madouong, Tendouong, Maloai, Mota, Hinhanh) VALUES (@Madouong, @Tendouong, @Maloai, @Mota, @Hinhanh)";
                    using (SQLiteCommand command = new SQLiteCommand(insertSql, connection))
                    {
                        command.Parameters.AddWithValue("@Madouong", douong.Madouong);
                        command.Parameters.AddWithValue("@Tendouong", douong.Tendouong);
                        command.Parameters.AddWithValue("@Maloai", douong.Maloai);
                        command.Parameters.AddWithValue("@Mota", (object)douong.Mota ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Hinhanh", (object)douong.Hinhanh ?? DBNull.Value);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // ĐIỀU CHỈNH: Thay thế MessageBox.Show bằng cách ném lỗi để BLL xử lý
                    throw new Exception($"Lỗi DAL khi thêm đồ uống: {ex.Message}", ex);
                }
            }
        }

        /// <summary>
        /// Cập nhật thông tin đồ uống trong CSDL.
        /// </summary>
        /// <param name="douong">Đối tượng Douong cần cập nhật.</param>
        public void UpdateDouong(Douong douong)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string updateSql = "UPDATE Douong SET Tendouong = @Tendouong, Maloai = @Maloai, Mota = @Mota, Hinhanh = @Hinhanh WHERE Madouong = @Madouong";
                    using (SQLiteCommand command = new SQLiteCommand(updateSql, connection))
                    {
                        command.Parameters.AddWithValue("@Tendouong", douong.Tendouong);
                        command.Parameters.AddWithValue("@Maloai", douong.Maloai);
                        command.Parameters.AddWithValue("@Mota", (object)douong.Mota ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Hinhanh", (object)douong.Hinhanh ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Madouong", douong.Madouong);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // ĐIỀU CHỈNH: Thay thế MessageBox.Show bằng cách ném lỗi để BLL xử lý
                    throw new Exception($"Lỗi DAL khi cập nhật đồ uống: {ex.Message}", ex);
                }
            }
        }

        /// <summary>
        /// Xóa một đồ uống khỏi CSDL.
        /// </summary>
        /// <param name="madouong">Mã đồ uống cần xóa.</param>
        public void DeleteDouong(string madouong)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    // Lưu ý: Có thể cần thêm logic xóa các giá liên quan trong bảng Giadouong trước nếu cần
                    string deleteSql = "DELETE FROM Douong WHERE Madouong = @Madouong";
                    using (SQLiteCommand command = new SQLiteCommand(deleteSql, connection))
                    {
                        command.Parameters.AddWithValue("@Madouong", madouong);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // ĐIỀU CHỈNH: Thay thế MessageBox.Show bằng cách ném lỗi để BLL xử lý
                    throw new Exception($"Lỗi DAL khi xóa đồ uống: {ex.Message}", ex);
                }
            }
        }
    }
}
