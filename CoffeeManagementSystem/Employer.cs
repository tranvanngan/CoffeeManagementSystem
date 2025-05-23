using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeManagementSystem.DAL;
namespace CoffeeManagementSystem
{
    public partial class EmployerForm : Form
    {
        private NhanvienDAL nhanvienDAL = new NhanvienDAL();
        private CalamviecDAL calamviecDAL = new CalamviecDAL();
        public EmployerForm()
        {
            InitializeComponent();
            this.Load += EmployerForm_Load;

            // =====================================================
            // Configure DataGridView and assign events for Employee Tab
            // =====================================================
            dgvNhanvien.AutoGenerateColumns = false;
            dgvNhanvien.AllowUserToAddRows = false;
            dgvNhanvien.AllowUserToDeleteRows = false;
            dgvNhanvien.EditMode = DataGridViewEditMode.EditProgrammatically;

            // Assign events to Employee tab controls
            this.txtSearch.KeyDown += new KeyEventHandler(txtSearch_KeyDown);
            this.txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
            this.dgvNhanvien.CellClick += new DataGridViewCellEventHandler(dgvNhanvien_CellClick);
            this.btnThem.Click += new EventHandler(btnThem_Click);
            //this.btnRefreshNhanvien.Click += new EventHandler(btnRefreshNhanvien_Click);
            //this.btnExportNhanvien.Click += new EventHandler(btnExportNhanvien_Click);
            // If there's a separate search button for employees, uncomment and assign its click event
            // this.btnSearchNhanvien.Click += new EventHandler(btnSearchNhanvien_Click);

            // =====================================================
            // Configure DataGridView and assign events for Shift Tab
            // =====================================================
            //dgvCalamviec.AutoGenerateColumns = false;
            //dgvCalamviec.AllowUserToAddRows = false;
            //dgvCalamviec.AllowUserToDeleteRows = false;
           // dgvCalamviec.EditMode = DataGridViewEditMode.EditProgrammatically;

            // Assign events to Shift tab controls
            //this.txtTimKiemCalamviec.KeyDown += new KeyEventHandler(txtTimKiemCalamviec_KeyDown);
           // this.txtTimKiemCalamviec.TextChanged += new EventHandler(txtTimKiemCalamviec_TextChanged);
           // this.dgvCalamviec.CellClick += new DataGridViewCellEventHandler(dgvCalamviec_CellClick);
           // this.btnAddCalamviec.Click += new EventHandler(btnAddCalamviec_Click);
            //this.btnRefreshCalamviec.Click += new EventHandler(btnRefreshCalamviec_Click);
            // this.btnExportCalamviec.Click += new EventHandler(btnExportCalamviec_Click);
            // If there's a separate search button for shifts, uncomment and assign its click event
            // this.btnSearchCalamviec.Click += new EventHandler(btnSearchCalamviec_Click);
        }
        // Form Load event for EmployerForm
        private void EmployerForm_Load(object sender, EventArgs e)
        {
            // Load data for both tabs when the main Form loads
            LoadDanhSachNhanvien();
            //LoadDanhSachCalamviec();

            // Prioritize selecting the Employee TabPage (TabPage 1) when the Form loads
            // Ensure the TabControl is named tabControlMain and the TabPage is named tabPageNhanvien
            //if (tabControlMain.TabPages.ContainsKey("tabPageNhanvien"))
            //{
               // tabControlMain.SelectedTab = tabControlMain.TabPages["tabPageNhanvien"];
            //}
        }
        private void LoadDanhSachNhanvien()
        {
            try
            {
                List<Nhanvien> danhSach = nhanvienDAL.GetAllNhanviens();
                dgvNhanvien.DataSource = danhSach;
                dgvNhanvien.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Click event for the "Add new" button for employees
        private void btnThem_Click(object sender, EventArgs e)
        {
            FormChiTietNhanvien formChiTiet = new FormChiTietNhanvien();
            if (formChiTiet.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachNhanvien(); // Reload list after successful save
            }
        }
        // KeyDown event on the employee search TextBox (to search on Enter key press)
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                string searchTerm = txtSearch.Text.Trim();
                LoadFilteredNhanvienData(searchTerm);
            }
        }
        // Method to load filtered employees and display them in the DataGridView
        private void LoadFilteredNhanvienData(string searchTerm)
        {
            try
            {
                List<Nhanvien> ketQuaHienThi;
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    ketQuaHienThi = nhanvienDAL.GetAllNhanviens();
                }
                else
                {
                    ketQuaHienThi = nhanvienDAL.SearchNhanviens(searchTerm);
                }

                dgvNhanvien.DataSource = ketQuaHienThi;
                dgvNhanvien.Refresh();

                if (ketQuaHienThi.Count == 0 && !string.IsNullOrWhiteSpace(searchTerm))
                {
                    // Optional: MessageBox.Show($"Không tìm thấy nhân viên nào phù hợp với từ khóa '{searchTerm}'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       // private void LoadDanhSachCalamviec()
        //{
         //   try
          //  {
          //      List<Calamviec> danhSach = calamviecDAL.GetAllCalamviecs();
           //     dgvCalamviec.DataSource = danhSach;
            //    dgvCalamviec.Refresh();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Không thể tải danh sách ca làm việc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
       // }

        // Method to load filtered shifts and display them in the DataGridView
        
        private void loadtabPageNhanvien()
        {
            // ... logic tải UserControl FormNhanvien vào TabPage ...
        }
        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Employer_Load(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvNhanvien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadFilteredNhanvienData(searchTerm);
        }

        
        private void dgvNhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvNhanvien.Rows.Count)
            {
                DataGridViewRow row = dgvNhanvien.Rows[e.RowIndex];

                // Get employee ID from the clicked row
                // Ensure the column name "Manhanvien" matches the DataPropertyName or Name of the column in the DGV
                string manhanvien = null;
                try
                {
                    // Thử truy cập bằng tên (Name) của cột trong Designer
                    // Thay "Column1" bằng tên (Name) thực tế của cột Mã khách hàng nếu khác
                    manhanvien = row.Cells["Column1"].Value?.ToString();
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu việc truy cập cell bằng tên cột thất bại
                    MessageBox.Show($"Lỗi khi truy cập cột Mã nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Dừng xử lý
                }

                if (!string.IsNullOrEmpty(manhanvien))
                {
                    try
                    {
                        Nhanvien selectedNhanvien = nhanvienDAL.GetNhanvienById(manhanvien);

                        if (selectedNhanvien != null)
                        {
                            FormChiTietNhanvien formChiTiet = new FormChiTietNhanvien(selectedNhanvien);
                            if (formChiTiet.ShowDialog() == DialogResult.OK)
                            {
                                LoadDanhSachNhanvien(); // Reload list after successful update/delete
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Không tìm thấy thông tin chi tiết cho nhân viên có mã '{manhanvien}'.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lấy thông tin chi tiết nhân viên hoặc mở Form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        //private void btnAddCalamviec_Click(object sender, EventArgs e)
        //{
        //    FormChiTietCalamviec formChiTiet = new FormChiTietCalamviec();
         //   if (formChiTiet.ShowDialog() == DialogResult.OK)
          //  {
          //      LoadDanhSachCalamviec(); // Reload list after successful save
          //  }
      //  }
      //  private void txtTimKiemCalamviec_KeyDown(object sender, KeyEventArgs e)
       // {
         //   if (e.KeyCode == Keys.Enter)
         //   {
           //     e.Handled = true;
            //    e.SuppressKeyPress = true;
              //  string searchTerm = txtTimKiemCalamviec.Text.Trim();
             //   LoadFilteredCalamviecData(searchTerm);
           // }
        //}

        // TextChanged event on the shift search TextBox
       // private void txtTimKiemCalamviec_TextChanged(object sender, EventArgs e)
        //{
        //    string searchTerm = txtTimKiemCalamviec.Text.Trim();
         //   LoadFilteredCalamviecData(searchTerm);
        //}

        // CellClick event for the shift DataGridView
        
        

        private void tabPageNhanvien_Click(object sender, EventArgs e)
        {

        }
    }
}