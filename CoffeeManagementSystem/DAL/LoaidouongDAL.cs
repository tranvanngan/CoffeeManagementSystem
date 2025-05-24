﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms; // Only for MessageBox in error handling examples
using System.Data;

// Ensure using namespace contains your BaseDataAccess class
// Ensure using namespace contains your Loaidouong Model class
using CoffeeManagementSystem; // Based on the namespace of your Loaidouong class

namespace CoffeeManagementSystem.DAL
{
    public class LoaidouongDAL : BaseDataAccess
    {
        public LoaidouongDAL() : base() { }

        /// <summary>
        /// Retrieves all drink categories from the database.
        /// </summary>
        /// <returns>A list of Loaidouong objects.</returns>
        public List<Loaidouong> GetAllLoaidouongs()
        {
            List<Loaidouong> loaidouongs = new List<Loaidouong>();
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = "SELECT Maloai, Tenloai FROM Loaidouong";
                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Loaidouong loaidouong = new Loaidouong
                                {
                                    Maloai = reader["Maloai"].ToString(),
                                    Tenloai = reader["Tenloai"].ToString()
                                };
                                loaidouongs.Add(loaidouong);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lấy danh sách loại đồ uống: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return loaidouongs;
        }

        /// <summary>
        /// Retrieves drink category information by ID.
        /// </summary>
        /// <param name="maloai">The ID of the drink category to retrieve.</param>
        /// <returns>A Loaidouong object if found, otherwise null.</returns>
        public Loaidouong GetLoaidouongById(string maloai)
        {
            Loaidouong loaidouong = null;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = "SELECT Maloai, Tenloai FROM Loaidouong WHERE Maloai = @Maloai";
                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        command.Parameters.AddWithValue("@Maloai", maloai);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                loaidouong = new Loaidouong
                                {
                                    Maloai = reader["Maloai"].ToString(),
                                    Tenloai = reader["Tenloai"].ToString()
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lấy loại đồ uống theo ID: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return loaidouong;
        }

        /// <summary>
        /// Adds a new drink category to the database.
        /// </summary>
        /// <param name="loaidouong">The Loaidouong object to add.</param>
        public void AddLoaidouong(Loaidouong loaidouong)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string insertSql = "INSERT INTO Loaidouong (Maloai, Tenloai) VALUES (@Maloai, @Tenloai)";
                    using (SQLiteCommand command = new SQLiteCommand(insertSql, connection))
                    {
                        command.Parameters.AddWithValue("@Maloai", loaidouong.Maloai);
                        command.Parameters.AddWithValue("@Tenloai", loaidouong.Tenloai);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm loại đồ uống: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Updates the information of a drink category.
        /// </summary>
        /// <param name="loaidouong">The Loaidouong object containing updated information (Maloai is required).</param>
        public void UpdateLoaidouong(Loaidouong loaidouong)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string updateSql = @"
                        UPDATE Loaidouong
                        SET Tenloai = @Tenloai
                        WHERE Maloai = @Maloai";
                    using (SQLiteCommand command = new SQLiteCommand(updateSql, connection))
                    {
                        command.Parameters.AddWithValue("@Tenloai", loaidouong.Tenloai);
                        command.Parameters.AddWithValue("@Maloai", loaidouong.Maloai);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật loại đồ uống: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Deletes a drink category from the database.
        /// </summary>
        /// <param name="maloai">The ID of the drink category to delete.</param>
        public void DeleteLoaidouong(string maloai)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string deleteSql = "DELETE FROM Loaidouong WHERE Maloai = @Maloai";
                    using (SQLiteCommand command = new SQLiteCommand(deleteSql, connection))
                    {
                        command.Parameters.AddWithValue("@Maloai", maloai);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa loại đồ uống: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Searches for drink categories based on a keyword in Maloai and Tenloai columns.
        /// </summary>
        /// <param name="searchTerm">The search keyword.</param>
        /// <returns>A list of matching Loaidouong objects.</returns>
        public List<Loaidouong> SearchLoaidouongs(string searchTerm)
        {
            List<Loaidouong> loaidouongs = new List<Loaidouong>();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return loaidouongs;
            }

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = @"
                        SELECT Maloai, Tenloai
                        FROM Loaidouong
                        WHERE LOWER(Maloai) LIKE @SearchTerm
                           OR LOWER(Tenloai) LIKE @SearchTerm";
                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm.ToLower() + "%");
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Loaidouong loaidouong = new Loaidouong
                                {
                                    Maloai = reader["Maloai"].ToString(),
                                    Tenloai = reader["Tenloai"].ToString()
                                };
                                loaidouongs.Add(loaidouong);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tìm kiếm loại đồ uống: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return loaidouongs;
        }
    }
}
