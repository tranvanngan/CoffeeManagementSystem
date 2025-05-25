using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data; // Needed for DBNull.Value
using CoffeeManagementSystem; // Đảm bảo using namespace chứa lớp Model Luong

namespace CoffeeManagementSystem.DAL
{
    public class LuongDAL : BaseDataAccess
    {
        public LuongDAL() : base() { }

        /// <summary>
        /// Thêm một bản ghi lương mới vào CSDL.
        /// </summary>
        /// <param name="luong">Đối tượng Luong cần thêm.</param>
        public void AddLuong(Luong luong)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string insertSql = "INSERT INTO Luong (Maluong, Manhanvien, Thang, Nam, Tongca, Tonggio, LuongTong) VALUES (@Maluong, @Manhanvien, @Thang, @Nam, @Tongca, @Tonggio, @LuongTong)";
                    using (SQLiteCommand command = new SQLiteCommand(insertSql, connection))
                    {
                        command.Parameters.AddWithValue("@Maluong", luong.Maluong);
                        command.Parameters.AddWithValue("@Manhanvien", luong.Manhanvien);
                        command.Parameters.AddWithValue("@Thang", luong.Thang);
                        command.Parameters.AddWithValue("@Nam", luong.Nam);
                        command.Parameters.AddWithValue("@Tongca", luong.Tongca);
                        command.Parameters.AddWithValue("@Tonggio", luong.Tonggio);
                        command.Parameters.AddWithValue("@LuongTong", luong.LuongTong);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi DAL khi thêm lương: {ex.Message}", ex);
                }
            }
        }

        /// <summary>
        /// Lấy tất cả các bản ghi lương từ CSDL.
        /// </summary>
        /// <returns>Danh sách các đối tượng Luong.</returns>
        public List<Luong> GetAllLuongs()
        {
            List<Luong> luongs = new List<Luong>();
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = "SELECT Maluong, Manhanvien, Thang, Nam, Tongca, Tonggio, LuongTong FROM Luong";
                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Luong l = new Luong
                                {
                                    Maluong = reader["Maluong"].ToString(),
                                    Manhanvien = reader["Manhanvien"].ToString(),
                                    Thang = Convert.ToInt32(reader["Thang"]),
                                    Nam = Convert.ToInt32(reader["Nam"]),
                                    Tongca = Convert.ToInt32(reader["Tongca"]),
                                    Tonggio = Convert.ToDecimal(reader["Tonggio"]),
                                    LuongTong = Convert.ToDecimal(reader["LuongTong"])
                                };
                                luongs.Add(l);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi DAL khi lấy tất cả lương: {ex.Message}", ex);
                }
            }
            return luongs;
        }

        /// <summary>
        /// Lấy tổng lương thực nhận trong một khoảng thời gian (theo tháng/năm).
        /// </summary>
        /// <param name="startDate">Ngày bắt đầu (chỉ lấy tháng/năm).</param>
        /// <param name="endDate">Ngày kết thúc (chỉ lấy tháng/năm).</param>
        /// <returns>Tổng lương thực nhận (decimal).</returns>
        public decimal GetTotalLuongByDateRange(DateTime startDate, DateTime endDate)
        {
            decimal totalLuong = 0m;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = @"
                        SELECT SUM(LuongTong) 
                        FROM Luong 
                        WHERE (Nam > @StartYear OR (Nam = @StartYear AND Thang >= @StartMonth)) 
                          AND (Nam < @EndYear OR (Nam = @EndYear AND Thang <= @EndMonth))";
                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        command.Parameters.AddWithValue("@StartYear", startDate.Year);
                        command.Parameters.AddWithValue("@StartMonth", startDate.Month);
                        command.Parameters.AddWithValue("@EndYear", endDate.Year);
                        command.Parameters.AddWithValue("@EndMonth", endDate.Month);
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            totalLuong = Convert.ToDecimal(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi DAL khi lấy tổng lương theo khoảng thời gian: {ex.Message}", ex);
                }
            }
            return totalLuong;
        }

        /// <summary>
        /// Lấy các bản ghi lương trong một khoảng thời gian (theo tháng/năm).
        /// </summary>
        /// <param name="startDate">Ngày bắt đầu (chỉ lấy tháng/năm).</param>
        /// <param name="endDate">Ngày kết thúc (chỉ lấy tháng/năm).</param>
        /// <returns>Danh sách các đối tượng Luong trong khoảng thời gian.</returns>
        public List<Luong> GetLuongsByDateRange(DateTime startDate, DateTime endDate)
        {
            List<Luong> luongs = new List<Luong>();
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = @"
                        SELECT Maluong, Manhanvien, Thang, Nam, Tongca, Tonggio, LuongTong 
                        FROM Luong 
                        WHERE (Nam > @StartYear OR (Nam = @StartYear AND Thang >= @StartMonth)) 
                          AND (Nam < @EndYear OR (Nam = @EndYear AND Thang <= @EndMonth))";
                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        command.Parameters.AddWithValue("@StartYear", startDate.Year);
                        command.Parameters.AddWithValue("@StartMonth", startDate.Month);
                        command.Parameters.AddWithValue("@EndYear", endDate.Year);
                        command.Parameters.AddWithValue("@EndMonth", endDate.Month);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Luong l = new Luong
                                {
                                    Maluong = reader["Maluong"].ToString(),
                                    Manhanvien = reader["Manhanvien"].ToString(),
                                    Thang = Convert.ToInt32(reader["Thang"]),
                                    Nam = Convert.ToInt32(reader["Nam"]),
                                    Tongca = Convert.ToInt32(reader["Tongca"]),
                                    Tonggio = Convert.ToDecimal(reader["Tonggio"]),
                                    LuongTong = Convert.ToDecimal(reader["LuongTong"])
                                };
                                luongs.Add(l);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi DAL khi lấy lương theo khoảng thời gian: {ex.Message}", ex);
                }
            }
            return luongs;
        }
    }
}
