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
    public partial class HoaDonChiTiet : Form
    {
        string sCon = "Data Source=LAPTOP-8I721H54\\GIABAOTRUONG;Initial Catalog=Banhang;Integrated Security=True";
        public HoaDonChiTiet()
        {
            InitializeComponent();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy các giá trị từ TextBox
            string sMaHD = txtMaHD.Text.Trim();
            string sMaDoUong = txtMaDoUong.Text.Trim();
            string sSoLuong = txtSoLuong.Text.Trim();

            // Kiểm tra nếu mã phòng chưa được nhập
            if (string.IsNullOrEmpty(sMaHD) || string.IsNullOrEmpty(sMaDoUong) || string.IsNullOrEmpty(sSoLuong))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(sCon))
            {
                try
                {
                    con.Open();

                    // Kiểm tra mã hóa đơn tồn tại không
                    string sCheckQuery = "SELECT COUNT(*) FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                    SqlCommand checkcmd = new SqlCommand(sCheckQuery, con);
                    checkcmd.Parameters.AddWithValue("@MaHoaDon", sMaHD);

                    int count = (int)checkcmd.ExecuteScalar();
                    if (count == 0)
                    {
                        MessageBox.Show("Mã hóa đơn không tồn tại, vui lòng nhập mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra mã đồ uống có tồn tại không
                    string sCheckQuery1 = "SELECT COUNT(*) FROM DoUong WHERE MaDoUong = @MaDoUong";
                    SqlCommand checkcmd1 = new SqlCommand(sCheckQuery1, con);
                    checkcmd1.Parameters.AddWithValue("@MaDoUong", sMaDoUong);

                    int count1 = (int)checkcmd1.ExecuteScalar();
                    if (count1 == 0)
                    {
                        MessageBox.Show("Mã đồ uống không tồn tại, vui lòng nhập mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra mã hóa đơn và mã đồ uống đã tồn tại trong HoaDon_ChiTiet chưa
                    string sCheckExistQuery = "SELECT COUNT(*) FROM HoaDon_ChiTiet WHERE MaHoaDon = @MaHoaDon AND MaDoUong = @MaDoUong";
                    SqlCommand checkExistCmd = new SqlCommand(sCheckExistQuery, con);
                    checkExistCmd.Parameters.AddWithValue("@MaHoaDon", sMaHD);
                    checkExistCmd.Parameters.AddWithValue("@MaDoUong", sMaDoUong);

                    int existCount = (int)checkExistCmd.ExecuteScalar();

                    if (existCount > 0)
                    {
                        // Nếu đã tồn tại, cập nhật số lượng
                        string sUpdateQuery = "UPDATE HoaDon_ChiTiet SET SoLuong = SoLuong + @SoLuong WHERE MaHoaDon = @MaHoaDon AND MaDoUong = @MaDoUong";
                        SqlCommand updateCmd = new SqlCommand(sUpdateQuery, con);
                        updateCmd.Parameters.AddWithValue("@MaHoaDon", sMaHD);
                        updateCmd.Parameters.AddWithValue("@MaDoUong", sMaDoUong);
                        updateCmd.Parameters.AddWithValue("@SoLuong", int.Parse(sSoLuong)); // Cộng thêm số lượng mới

                        updateCmd.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật số lượng hóa đơn chi tiết thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Thực hiện thêm mới phòng
                        string sQuery = "INSERT INTO HoaDon_ChiTiet(MaHoaDon, MaDoUong, SoLuong) VALUES(@MaHoaDon, @MaDoUong, @SoLuong)";
                        SqlCommand cmd = new SqlCommand(sQuery, con);
                        cmd.Parameters.AddWithValue("@MaHoaDon", sMaHD);
                        cmd.Parameters.AddWithValue("@MaDoUong", sMaDoUong);
                        cmd.Parameters.AddWithValue("@SoLuong", int.Parse(sSoLuong));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm mới hóa đơn chi tiết thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

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
            string sMaDoUong = txtMaDoUong.Text.Trim();
            string sSoLuong = txtSoLuong.Text.Trim();

            if (string.IsNullOrEmpty(sSoLuong))
            {
                MessageBox.Show("Vui lòng nhập số lượng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlConnection con = new SqlConnection(sCon);

            try
            {
                con.Open();
                // Áp dụng thủ tục sửa hóa đơn chi tiết
                SqlCommand cmd = new SqlCommand("sp_SuaHoaDonChiTiet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHoaDon", sMaHD);
                cmd.Parameters.AddWithValue("@MaDoUong", sMaDoUong);
                cmd.Parameters.AddWithValue("@SoLuong", int.Parse(sSoLuong));

                // Lấy giá trị trả về từ thủ tục lưu
                object result = cmd.ExecuteScalar();

                // Kiểm tra nếu result là null
                if (result == null || result == DBNull.Value)
                {
                    MessageBox.Show("Lỗi: Thủ tục không trả về giá trị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int returnValue = Convert.ToInt32(result);

                    if (returnValue == 1)
                    {
                        MessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Hóa đơn chi tiết không tồn tại hoặc không thể cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
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
            string sMaDoUong = txtMaDoUong.Text;
            string sQuery = "delete from HoaDon_ChiTiet where MaHoaDon = @MaHoaDon and MaDoUong = @MaDoUong";
            SqlCommand cmd = new SqlCommand(sQuery, con);
            cmd.Parameters.AddWithValue("@MaHoaDon", sMaHD);
            cmd.Parameters.AddWithValue("@MaDoUong", sMaDoUong);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa thông tin hóa đơn chi tiết thành công");
                LoadData();
            }
            catch
            {
                MessageBox.Show("Lỗi không thể xóa thông tin hóa đơn chi tiết này");
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // Kiểm tra nếu click vào header
            {
                return;
            }

            // Kiểm tra giá trị trước khi gán để tránh lỗi NullReferenceException
            txtMaHD.Text = dataGridView1.Rows[e.RowIndex].Cells["MaHoaDon"].Value?.ToString() ?? string.Empty;
            txtMaDoUong.Text = dataGridView1.Rows[e.RowIndex].Cells["MaDoUong"].Value?.ToString() ?? string.Empty;
            txtSoLuong.Text = dataGridView1.Rows[e.RowIndex].Cells["SoLuong"].Value?.ToString() ?? string.Empty;

            // Đặt trạng thái của các textbox
            //txtMaHD.Enabled = false;
            //txtMaDoUong.Enabled = false;
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Reset dữ liệu trong các TextBox
            txtMaHD.Text = string.Empty;
            txtMaDoUong.Text = string.Empty;
            txtSoLuong.Text = string.Empty;

            // Reset giá trị mặc định cho DateTimePicker
            txtMaHD.Enabled = true;
            txtMaDoUong.Enabled = true;

            LoadData();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Ẩn form hóa đơn
            this.Hide();

            // Hiện lại form Main
            Form TrangChu = Application.OpenForms["TrangChu"]; // Tìm form Main đang tồn tại
            if (TrangChu != null)
            {
                TrangChu.Show();
            }
        }
        private void LoadData()
        {
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open();

                // Lấy dữ liệu
                string sQuery = "SELECT * FROM HoaDon_ChiTiet";
                SqlDataAdapter adapter = new SqlDataAdapter(sQuery, con);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "HoaDon_ChiTiet");

                // Gắn dữ liệu vào DataGridView
                dataGridView1.DataSource = ds.Tables["HoaDon_ChiTiet"];
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
        private void frmHoaDonChiTiet_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}