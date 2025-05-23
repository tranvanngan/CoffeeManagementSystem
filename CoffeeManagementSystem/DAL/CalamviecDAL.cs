using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms; // Only used for MessageBox in error handling examples
using System.Data; // Needed for DBNull.Value

// Ensure using namespace contains your BaseDataAccess class
// Example: using CoffeeManagementSystem.DAL;
// If BaseDataAccess is directly in CoffeeManagementSystem, you don't need a separate using.

// Ensure using namespace contains your Calamviec Model class
using CoffeeManagementSystem; // Based on the namespace of your Calamviec class

namespace CoffeeManagementSystem.DAL // Place DAL in a sub-namespace for better code organization
{
    public class CalamviecDAL : BaseDataAccess // Inherits from BaseDataAccess class
    {
        public CalamviecDAL() : base() // Call the base class constructor to get ConnectionString
        {
        }

        // =====================================================
        // METHOD TO GET LIST OF SHIFTS
        // =====================================================

        /// <summary>
        /// Retrieves all shifts from the database.
        /// </summary>
        /// <returns>A list of Calamviec objects.</returns>
        public List<Calamviec> GetAllCalamviecs()
        {
            List<Calamviec> calamviecs = new List<Calamviec>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = "SELECT Maca, Tenca, Thoigianbatdau, Thoigianketthuc, Sogio FROM Calamviec";

                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Calamviec calamviec = new Calamviec
                                {
                                    Maca = reader["Maca"].ToString(),
                                    Tenca = reader["Tenca"].ToString(),
                                    // Read TEXT and convert to TimeSpan
                                    Thoigianbatdau = TimeSpan.Parse(reader["Thoigianbatdau"].ToString()),
                                    Thoigianketthuc = TimeSpan.Parse(reader["Thoigianketthuc"].ToString()),
                                    Sogio = Convert.ToDecimal(reader["Sogio"])
                                };
                                calamviecs.Add(calamviec);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lấy danh sách ca làm việc: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return calamviecs;
        }

        // =====================================================
        // METHOD TO GET SHIFT BY ID
        // =====================================================

        /// <summary>
        /// Retrieves shift information by shift ID.
        /// </summary>
        /// <param name="maca">The ID of the shift to retrieve.</param>
        /// <returns>A Calamviec object if found, otherwise null.</returns>
        public Calamviec GetCalamviecById(string maca)
        {
            Calamviec calamviec = null;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = "SELECT Maca, Tenca, Thoigianbatdau, Thoigianketthuc, Sogio FROM Calamviec WHERE Maca = @Maca";

                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        command.Parameters.AddWithValue("@Maca", maca);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                calamviec = new Calamviec
                                {
                                    Maca = reader["Maca"].ToString(),
                                    Tenca = reader["Tenca"].ToString(),
                                    Thoigianbatdau = TimeSpan.Parse(reader["Thoigianbatdau"].ToString()),
                                    Thoigianketthuc = TimeSpan.Parse(reader["Thoigianketthuc"].ToString()),
                                    Sogio = Convert.ToDecimal(reader["Sogio"])
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lấy ca làm việc theo ID: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return calamviec;
        }

        // =====================================================
        // METHOD TO ADD A NEW SHIFT
        // =====================================================

        /// <summary>
        /// Adds a new shift to the database.
        /// </summary>
        /// <param name="calamviec">The Calamviec object to add.</param>
        public void AddCalamviec(Calamviec calamviec)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string insertSql = @"
                        INSERT INTO Calamviec (Maca, Tenca, Thoigianbatdau, Thoigianketthuc, Sogio)
                        VALUES (@Maca, @Tenca, @Thoigianbatdau, @Thoigianketthuc, @Sogio)";

                    using (SQLiteCommand command = new SQLiteCommand(insertSql, connection))
                    {
                        command.Parameters.AddWithValue("@Maca", calamviec.Maca);
                        command.Parameters.AddWithValue("@Tenca", calamviec.Tenca);
                        // Convert TimeSpan to TEXT for storage
                        command.Parameters.AddWithValue("@Thoigianbatdau", calamviec.Thoigianbatdau.ToString(@"hh\:mm\:ss"));
                        command.Parameters.AddWithValue("@Thoigianketthuc", calamviec.Thoigianketthuc.ToString(@"hh\:mm\:ss"));
                        command.Parameters.AddWithValue("@Sogio", calamviec.Sogio);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm ca làm việc: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =====================================================
        // METHOD TO UPDATE SHIFT INFORMATION
        // =====================================================

        /// <summary>
        /// Updates the information of a shift.
        /// </summary>
        /// <param name="calamviec">The Calamviec object containing updated information (Maca is required).</param>
        public void UpdateCalamviec(Calamviec calamviec)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string updateSql = @"
                        UPDATE Calamviec
                        SET Tenca = @Tenca,
                            Thoigianbatdau = @Thoigianbatdau,
                            Thoigianketthuc = @Thoigianketthuc,
                            Sogio = @Sogio
                        WHERE Maca = @Maca";

                    using (SQLiteCommand command = new SQLiteCommand(updateSql, connection))
                    {
                        command.Parameters.AddWithValue("@Tenca", calamviec.Tenca);
                        command.Parameters.AddWithValue("@Thoigianbatdau", calamviec.Thoigianbatdau.ToString(@"hh\:mm\:ss"));
                        command.Parameters.AddWithValue("@Thoigianketthuc", calamviec.Thoigianketthuc.ToString(@"hh\:mm\:ss"));
                        command.Parameters.AddWithValue("@Sogio", calamviec.Sogio);
                        command.Parameters.AddWithValue("@Maca", calamviec.Maca);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật ca làm việc: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =====================================================
        // METHOD TO DELETE A SHIFT
        // =====================================================

        /// <summary>
        /// Deletes a shift from the database.
        /// </summary>
        /// <param name="maca">The ID of the shift to delete.</param>
        public void DeleteCalamviec(string maca)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string deleteSql = "DELETE FROM Calamviec WHERE Maca = @Maca";

                    using (SQLiteCommand command = new SQLiteCommand(deleteSql, connection))
                    {
                        command.Parameters.AddWithValue("@Maca", maca);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa ca làm việc: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =====================================================
        // METHOD TO SEARCH SHIFTS
        // =====================================================

        /// <summary>
        /// Searches for shifts based on a keyword in Maca and Tenca columns.
        /// </summary>
        /// <param name="searchTerm">The search keyword.</param>
        /// <returns>A list of matching Calamviec objects.</returns>
        public List<Calamviec> SearchCalamviecs(string searchTerm)
        {
            List<Calamviec> calamviecs = new List<Calamviec>();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return calamviecs;
            }

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = @"
                        SELECT Maca, Tenca, Thoigianbatdau, Thoigianketthuc, Sogio
                        FROM Calamviec
                        WHERE LOWER(Maca) LIKE @SearchTerm
                           OR LOWER(Tenca) LIKE @SearchTerm";

                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm.ToLower() + "%");

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Calamviec calamviec = new Calamviec
                                {
                                    Maca = reader["Maca"].ToString(),
                                    Tenca = reader["Tenca"].ToString(),
                                    Thoigianbatdau = TimeSpan.Parse(reader["Thoigianbatdau"].ToString()),
                                    Thoigianketthuc = TimeSpan.Parse(reader["Thoigianketthuc"].ToString()),
                                    Sogio = Convert.ToDecimal(reader["Sogio"])
                                };
                                calamviecs.Add(calamviec);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tìm kiếm ca làm việc: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return calamviecs;
        }
    }
}
