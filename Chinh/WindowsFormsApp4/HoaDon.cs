using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class HoaDon : Form
    {
        string sCon = "Data Source=LAPTOP-8I721H54\\GIABAOTRUONG;Initial Catalog=Banhang;Integrated Security=True";
        public HoaDon()
        {
            InitializeComponent();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy các giá trị từ TextBox
            string sMaHD = txtMaHD.Text.Trim();
            string sMaKH = txtMaKH.Text.Trim();

            // Kiểm tra nếu mã phòng chưa được nhập
            if (string.IsNullOrEmpty(sMaHD) || string.IsNullOrEmpty(sMaKH))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(sCon))
            {
                try
                {
                    con.Open();

                    // Kiểm tra mã hóa đơn tồn tại
                    string sCheckQuery = "SELECT COUNT(*) FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                    SqlCommand checkcmd = new SqlCommand(sCheckQuery, con);
                    checkcmd.Parameters.AddWithValue("@MaHoaDon", sMaHD);

                    int count = (int)checkcmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Mã hóa đơn đã tồn tại, vui lòng nhập mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra mã khách hàng có tồn tại không
                    string sCheckQuery1 = "SELECT COUNT(*) FROM KhachHang WHERE MaKhachHang = @MaKhachHang";
                    SqlCommand checkcmd1 = new SqlCommand(sCheckQuery1, con);
                    checkcmd1.Parameters.AddWithValue("@MaKhachHang", sMaKH);

                    int count1 = (int)checkcmd1.ExecuteScalar();
                    if (count1 == 0)
                    {
                        MessageBox.Show("Mã khách hàng không tồn tại, vui lòng nhập mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DateTime selectedDateTime = txtThoiGianTao.Value;
                    string sThoiGianTao = selectedDateTime.ToString("yyyy/MM/dd HH:mm:ss");


                    // Thực hiện thêm mới phòng
                    string sQuery = "INSERT INTO HoaDon(MaHoaDon, MaKhachHang, ThoiGian, ChietKhau, TongTien) VALUES(@MaHoaDon, @MaKhachHang, @ThoiGianTao, 0 , 0)";
                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.AddWithValue("@MaHoaDon", sMaHD);
                    cmd.Parameters.AddWithValue("@MaKhachHang", sMaKH);
                    cmd.Parameters.AddWithValue("@ThoiGianTao", sThoiGianTao);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm mới hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Tải lại dữ liệu sau khi thêm mới
                    LoadData();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Dữ liệu nhập không hợp lệ. Vui lòng kiểm tra lại các trường số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Xảy ra lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sMaHD = txtMaHD.Text.Trim();
            string sMaKH = txtMaKH.Text.Trim();

            // Kiểm tra nếu mã phòng chưa được nhập
            if (string.IsNullOrEmpty(sMaHD) || string.IsNullOrEmpty(sMaKH))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlConnection con = new SqlConnection(sCon);

            try
            {
                con.Open();
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối DB");

            }

            // Kiểm tra mã khách hàng có tồn tại không
            string sCheckQuery = "SELECT COUNT(*) FROM KhachHang WHERE MaKhachHang = @MaKhachHang";
            SqlCommand checkcmd = new SqlCommand(sCheckQuery, con);
            checkcmd.Parameters.AddWithValue("@MaKhachHang", sMaKH);

            int count = (int)checkcmd.ExecuteScalar();
            if (count == 0)
            {
                MessageBox.Show("Mã khách hàng không tồn tại, vui lòng nhập mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sQuery = "update HoaDon set MaKhachHang = @MaKhachHang where MaHoaDon = @MaHoaDon";

            SqlCommand cmd = new SqlCommand(sQuery, con);
            cmd.Parameters.AddWithValue("@MaHoaDon", sMaHD);
            cmd.Parameters.AddWithValue("@MaKhachHang", sMaKH);


            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thông tin hóa đơn thành công");
                LoadData();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi quá trình cập nhật");
            }
            con.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Ẩn form hóa đơn
            this.Hide();

            // Hiện lại form Main
            Form TrangChu = Application.OpenForms["TrangChu"]; // Tìm form TrangChu đang tồn tại
            if (TrangChu != null)
            {
                TrangChu.Show();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open();
            }
            catch
            {

                MessageBox.Show("Lỗi kết nối DB");
            }
            string sMaHD = txtMaHD.Text;
            string sQuery = "delete from hoadon where MaHoaDon = @MaHoaDon";
            SqlCommand cmd = new SqlCommand(sQuery, con);
            cmd.Parameters.AddWithValue("@MaHoaDon", sMaHD);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa thông tin hóa đơn thành công");
                LoadData();
            }
            catch
            {
                MessageBox.Show("Lỗi không thể xóa thông tin hóa đơn này");
            }

        }
        private void LoadData()
        {
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open();

                // Lấy dữ liệu
                string sQuery = "SELECT * FROM HoaDon";
                SqlDataAdapter adapter = new SqlDataAdapter(sQuery, con);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "HoaDon");

                // Gắn dữ liệu vào DataGridView
                dataGridView1.DataSource = ds.Tables["HoaDon"];
            }
            catch (Exception)
            {
                MessageBox.Show("Xảy ra lỗi trong quá trình tải dữ liệu.");
            }
            finally
            {
                con.Close();
            }
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Reset dữ liệu trong các TextBox
            txtMaHD.Text = string.Empty;
            txtMaKH.Text = string.Empty;
            txtChietKhau.Text = string.Empty;
            txtTongTien.Text = string.Empty;

            // Reset giá trị mặc định cho DateTimePicker
            txtThoiGianTao.Value = DateTime.Now;

            txtMaHD.Enabled = true;

            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // Kiểm tra nếu click vào header
            {
                return;
            }

            // Kiểm tra giá trị trước khi gán để tránh lỗi NullReferenceException
            txtMaHD.Text = dataGridView1.Rows[e.RowIndex].Cells["MaHoaDon"].Value?.ToString() ?? string.Empty;
            txtMaKH.Text = dataGridView1.Rows[e.RowIndex].Cells["MaKhachHang"].Value?.ToString() ?? string.Empty;
            txtChietKhau.Text = dataGridView1.Rows[e.RowIndex].Cells["ChietKhau"].Value?.ToString() ?? string.Empty;

            // Xử lý nếu giá trị thời gian trống hoặc không hợp lệ
            var thoiGianValue = dataGridView1.Rows[e.RowIndex].Cells["ThoiGian"].Value;
            if (thoiGianValue != null && DateTime.TryParse(thoiGianValue.ToString(), out DateTime thoiGian))
            {
                txtThoiGianTao.Value = thoiGian;
            }
            else
            {
                txtThoiGianTao.Value = DateTime.Now; // Hoặc giá trị mặc định
            }

            txtTongTien.Text = dataGridView1.Rows[e.RowIndex].Cells["TongTien"].Value?.ToString() ?? string.Empty;

            // Đặt trạng thái của các textbox
            txtChietKhau.Enabled = false;
            txtTongTien.Enabled = false;
            txtMaHD.Enabled = false;
        }
    }
}




//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Data.SqlClient;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace Vi_s_Project
//{
//    public partial class frmHoaDon : Form
//    {
//        string sCon = "Data Source=LAPTOP-8I721H54\\GIABAOTRUONG;Initial Catalog=Banhang;Integrated Security=True";
//        public frmHoaDon()
//        {
//            InitializeComponent();
//        }
//        private void btnThem_Click(object sender, EventArgs e)
//        {
//            // Lấy các giá trị từ TextBox
//            string sMaHD = txtMaHD.Text.Trim();
//            string sMaKH = txtMaKH.Text.Trim();

//            // Kiểm tra nếu mã phòng chưa được nhập
//            if (string.IsNullOrEmpty(sMaHD) || string.IsNullOrEmpty(sMaKH))
//            {
//                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            using (SqlConnection con = new SqlConnection(sCon))
//            {
//                try
//                {
//                    con.Open();

//                    // Kiểm tra mã hóa đơn tồn tại
//                    string sCheckQuery = "SELECT COUNT(*) FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
//                    SqlCommand checkcmd = new SqlCommand(sCheckQuery, con);
//                    checkcmd.Parameters.AddWithValue("@MaHoaDon", sMaHD);

//                    int count = (int)checkcmd.ExecuteScalar();
//                    if (count > 0)
//                    {
//                        MessageBox.Show("Mã hóa đơn đã tồn tại, vui lòng nhập mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        return;
//                    }

//                    // Kiểm tra mã khách hàng có tồn tại không
//                    string sCheckQuery1 = "SELECT COUNT(*) FROM KhachHang WHERE MaKhachHang = @MaKhachHang";
//                    SqlCommand checkcmd1 = new SqlCommand(sCheckQuery1, con);
//                    checkcmd1.Parameters.AddWithValue("@MaKhachHang", sMaKH);

//                    int count1 = (int)checkcmd1.ExecuteScalar();
//                    if (count1 == 0)
//                    {
//                        MessageBox.Show("Mã khách hàng không tồn tại, vui lòng nhập mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        return;
//                    }

//                    DateTime selectedDateTime = txtThoiGianTao.Value;
//                    string sThoiGianTao = selectedDateTime.ToString("yyyy/MM/dd HH:mm:ss");


//                    // Thực hiện thêm mới phòng
//                    string sQuery = "INSERT INTO HoaDon(MaHoaDon, MaKhachHang, ThoiGian, ChietKhau, TongTien) VALUES(@MaHoaDon, @MaKhachHang, @ThoiGianTao, 0 , 0)";
//                    SqlCommand cmd = new SqlCommand(sQuery, con);
//                    cmd.Parameters.AddWithValue("@MaHoaDon", sMaHD);
//                    cmd.Parameters.AddWithValue("@MaKhachHang", sMaKH);
//                    cmd.Parameters.AddWithValue("@ThoiGianTao", sThoiGianTao);

//                    cmd.ExecuteNonQuery();
//                    MessageBox.Show("Thêm mới hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

//                    // Tải lại dữ liệu sau khi thêm mới
//                    LoadData();
//                }
//                catch (FormatException)
//                {
//                    MessageBox.Show("Dữ liệu nhập không hợp lệ. Vui lòng kiểm tra lại các trường số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Xảy ra lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//                con.Close();
//            }
//        }

//        private void btnSua_Click(object sender, EventArgs e)
//        {
//            string sMaHD = txtMaHD.Text.Trim();
//            string sMaKH = txtMaKH.Text.Trim();

//            // Kiểm tra nếu mã phòng chưa được nhập
//            if (string.IsNullOrEmpty(sMaHD) || string.IsNullOrEmpty(sMaKH))
//            {
//                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            SqlConnection con = new SqlConnection(sCon);

//            try
//            {
//                con.Open();
//            }
//            catch
//            {
//                MessageBox.Show("Lỗi kết nối DB");

//            }

//            // Kiểm tra mã khách hàng có tồn tại không
//            string sCheckQuery = "SELECT COUNT(*) FROM KhachHang WHERE MaKhachHang = @MaKhachHang";
//            SqlCommand checkcmd = new SqlCommand(sCheckQuery, con);
//            checkcmd.Parameters.AddWithValue("@MaKhachHang", sMaKH);

//            int count = (int)checkcmd.ExecuteScalar();
//            if (count == 0)
//            {
//                MessageBox.Show("Mã khách hàng không tồn tại, vui lòng nhập mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            string sQuery = "update HoaDon set MaKhachHang = @MaKhachHang where MaHoaDon = @MaHoaDon";

//            SqlCommand cmd = new SqlCommand(sQuery, con);
//            cmd.Parameters.AddWithValue("@MaHoaDon", sMaHD);
//            cmd.Parameters.AddWithValue("@MaKhachHang", sMaKH);


//            try
//            {
//                cmd.ExecuteNonQuery();
//                MessageBox.Show("Cập nhật thông tin hóa đơn thành công");
//                LoadData();
//            }
//            catch (Exception ex)
//            {

//                MessageBox.Show("Lỗi quá trình cập nhật");
//            }
//            con.Close();
//        }

//        private void btnThoat_Click(object sender, EventArgs e)
//        {
//            // Ẩn form hóa đơn
//            this.Hide();

//            // Hiện lại form Main
//            Form frmMain = Application.OpenForms["frmMain"]; // Tìm form Main đang tồn tại
//            if (frmMain != null)
//            {
//                frmMain.Show();
//            }
//        }

//        private void btnXoa_Click(object sender, EventArgs e)
//        {
//            SqlConnection con = new SqlConnection(sCon);
//            try
//            {
//                con.Open();
//            }
//            catch
//            {

//                MessageBox.Show("Lỗi kết nối DB");
//            }
//            string sMaHD = txtMaHD.Text;
//            string sQuery = "delete from hoadon where MaHoaDon = @MaHoaDon";
//            SqlCommand cmd = new SqlCommand(sQuery, con);
//            cmd.Parameters.AddWithValue("@MaHoaDon", sMaHD);
//            try
//            {
//                cmd.ExecuteNonQuery();
//                MessageBox.Show("Xóa thông tin hóa đơn thành công");
//                LoadData();
//            }
//            catch
//            {
//                MessageBox.Show("Lỗi không thể xóa thông tin hóa đơn này");
//            }

//        }
//        private void LoadData()
//        {
//            SqlConnection con = new SqlConnection(sCon);
//            try
//            {
//                con.Open();

//                // Lấy dữ liệu
//                string sQuery = "SELECT * FROM HoaDon";
//                SqlDataAdapter adapter = new SqlDataAdapter(sQuery, con);

//                DataSet ds = new DataSet();
//                adapter.Fill(ds, "HoaDon");

//                // Gắn dữ liệu vào DataGridView
//                dataGridView1.DataSource = ds.Tables["HoaDon"];
//            }
//            catch (Exception)
//            {
//                MessageBox.Show("Xảy ra lỗi trong quá trình tải dữ liệu.");
//            }
//            finally
//            {
//                con.Close();
//            }
//        }

//        private void frmHoaDon_Load(object sender, EventArgs e)
//        {
//            LoadData();
//        }

//        private void btnLamMoi_Click(object sender, EventArgs e)
//        {
//            // Reset dữ liệu trong các TextBox
//            txtMaHD.Text = string.Empty;
//            txtMaKH.Text = string.Empty;
//            txtChietKhau.Text = string.Empty;
//            txtTongTien.Text = string.Empty;

//            // Reset giá trị mặc định cho DateTimePicker
//            txtThoiGianTao.Value = DateTime.Now;

//            txtMaHD.Enabled = true;

//            LoadData();
//        }

//        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex < 0) // Kiểm tra nếu click vào header
//            {
//                return;
//            }

//            // Kiểm tra giá trị trước khi gán để tránh lỗi NullReferenceException
//            txtMaHD.Text = dataGridView1.Rows[e.RowIndex].Cells["MaHoaDon"].Value?.ToString() ?? string.Empty;
//            txtMaKH.Text = dataGridView1.Rows[e.RowIndex].Cells["MaKhachHang"].Value?.ToString() ?? string.Empty;
//            txtChietKhau.Text = dataGridView1.Rows[e.RowIndex].Cells["ChietKhau"].Value?.ToString() ?? string.Empty;

//            // Xử lý nếu giá trị thời gian trống hoặc không hợp lệ
//            var thoiGianValue = dataGridView1.Rows[e.RowIndex].Cells["ThoiGian"].Value;
//            if (thoiGianValue != null && DateTime.TryParse(thoiGianValue.ToString(), out DateTime thoiGian))
//            {
//                txtThoiGianTao.Value = thoiGian;
//            }
//            else
//            {
//                txtThoiGianTao.Value = DateTime.Now; // Hoặc giá trị mặc định
//            }

//            txtTongTien.Text = dataGridView1.Rows[e.RowIndex].Cells["TongTien"].Value?.ToString() ?? string.Empty;

//            // Đặt trạng thái của các textbox
//            txtChietKhau.Enabled = false;
//            txtTongTien.Enabled = false;
//            txtMaHD.Enabled = false;
//        }
//    }
//}
