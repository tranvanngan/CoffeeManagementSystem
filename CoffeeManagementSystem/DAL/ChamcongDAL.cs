using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms; // Only used for MessageBox in error handling examples
using System.Data;

namespace CoffeeManagementSystem.DAL // Place DAL in a sub-namespace for better code organization
{
    public class ChamcongDAL : BaseDataAccess
    {
        public ChamcongDAL() : base() { }

        /// <summary>
        /// Thêm một bản ghi chấm công mới vào CSDL.
        /// </summary>
        /// <param name="chamcong">Đối tượng Chamcong cần thêm.</param>
        public void AddChamcong(Chamcong chamcong)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string insertSql = "INSERT INTO Chamcong (Machamcong, Manhanvien, Maca, Ngay) VALUES (@Machamcong, @Manhanvien, @Maca, @Ngay)";
                    using (SQLiteCommand command = new SQLiteCommand(insertSql, connection))
                    {
                        command.Parameters.AddWithValue("@Machamcong", chamcong.Machamcong);
                        command.Parameters.AddWithValue("@Manhanvien", chamcong.Manhanvien);
                        command.Parameters.AddWithValue("@Maca", chamcong.Maca);
                        command.Parameters.AddWithValue("@Ngay", chamcong.Ngay.ToString("yyyy-MM-dd HH:mm:ss")); // Save date as TEXT
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm chấm công: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // throw; // Re-throw exception if the calling layer needs to handle it
                }
            }
        }

        // You can add other methods for ChamcongDAL (GetAll, GetById, Update, Delete) later if needed.
        // For example:

        /// <summary>
        /// Lấy tất cả các bản ghi chấm công từ CSDL.
        /// </summary>
        /// <returns>Danh sách các đối tượng Chamcong.</returns>
        public List<Chamcong> GetAllChamcongs()
        {
            List<Chamcong> chamcongs = new List<Chamcong>();
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = "SELECT Machamcong, Manhanvien, Maca, Ngay FROM Chamcong";
                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Chamcong chamcong = new Chamcong
                                {
                                    Machamcong = reader["Machamcong"].ToString(),
                                    Manhanvien = reader["Manhanvien"].ToString(),
                                    Maca = reader["Maca"].ToString(),
                                    Ngay = DateTime.Parse(reader["Ngay"].ToString())
                                };
                                chamcongs.Add(chamcong);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lấy danh sách chấm công: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return chamcongs;
        }

        /// <summary>
        /// Lấy bản ghi chấm công theo Mã chấm công.
        /// </summary>
        /// <param name="machamcong">Mã chấm công cần lấy.</param>
        /// <returns>Đối tượng Chamcong nếu tìm thấy, ngược lại trả về null.</returns>
        public Chamcong GetChamcongById(string machamcong)
        {
            Chamcong chamcong = null;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = "SELECT Machamcong, Manhanvien, Maca, Ngay FROM Chamcong WHERE Machamcong = @Machamcong";
                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        command.Parameters.AddWithValue("@Machamcong", machamcong);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                chamcong = new Chamcong
                                {
                                    Machamcong = reader["Machamcong"].ToString(),
                                    Manhanvien = reader["Manhanvien"].ToString(),
                                    Maca = reader["Maca"].ToString(),
                                    Ngay = DateTime.Parse(reader["Ngay"].ToString())
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lấy chấm công theo ID: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return chamcong;
        }

        /// <summary>
        /// Cập nhật thông tin của một bản ghi chấm công.
        /// </summary>
        /// <param name="chamcong">Đối tượng Chamcong chứa thông tin cập nhật (cần có Machamcong).</param>
        public void UpdateChamcong(Chamcong chamcong)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string updateSql = @"
                        UPDATE Chamcong
                        SET Manhanvien = @Manhanvien,
                            Maca = @Maca,
                            Ngay = @Ngay
                        WHERE Machamcong = @Machamcong";
                    using (SQLiteCommand command = new SQLiteCommand(updateSql, connection))
                    {
                        command.Parameters.AddWithValue("@Manhanvien", chamcong.Manhanvien);
                        command.Parameters.AddWithValue("@Maca", chamcong.Maca);
                        command.Parameters.AddWithValue("@Ngay", chamcong.Ngay.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@Machamcong", chamcong.Machamcong);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật chấm công: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Xóa một bản ghi chấm công khỏi CSDL.
        /// </summary>
        /// <param name="machamcong">Mã chấm công cần xóa.</param>
        public void DeleteChamcong(string machamcong)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string deleteSql = "DELETE FROM Chamcong WHERE Machamcong = @Machamcong";
                    using (SQLiteCommand command = new SQLiteCommand(deleteSql, connection))
                    {
                        command.Parameters.AddWithValue("@Machamcong", machamcong);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa chấm công: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Tìm kiếm bản ghi chấm công dựa trên từ khóa trong các cột Machamcong, Manhanvien, Maca.
        /// </summary>
        /// <param name="searchTerm">Từ khóa tìm kiếm.</param>
        /// <returns>Danh sách các đối tượng Chamcong phù hợp.</returns>
        public List<Chamcong> SearchChamcongs(string searchTerm)
        {
            List<Chamcong> chamcongs = new List<Chamcong>();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return chamcongs;
            }

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = @"
                        SELECT Machamcong, Manhanvien, Maca, Ngay
                        FROM Chamcong
                        WHERE LOWER(Machamcong) LIKE @SearchTerm
                           OR LOWER(Manhanvien) LIKE @SearchTerm
                           OR LOWER(Maca) LIKE @SearchTerm";
                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm.ToLower() + "%");
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Chamcong chamcong = new Chamcong
                                {
                                    Machamcong = reader["Machamcong"].ToString(),
                                    Manhanvien = reader["Manhanvien"].ToString(),
                                    Maca = reader["Maca"].ToString(),
                                    Ngay = DateTime.Parse(reader["Ngay"].ToString())
                                };
                                chamcongs.Add(chamcong);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tìm kiếm chấm công: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return chamcongs;
        }
    }
}
