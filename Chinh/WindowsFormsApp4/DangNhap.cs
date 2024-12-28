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


namespace BTN
{
    public partial class DangNhap : Form
    {
        string sCon = "Data Source=LAPTOP-8I721H54\\GIABAOTRUONG;Initial Catalog=Banhang;Integrated Security=True;Encrypt=True";
        public DangNhap()
        {
            InitializeComponent();
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {

        }
        private void btDangnhap_Click_1(object sender, EventArgs e)
        {
            string tenDangNhap = txtTendangnhap.Text.Trim(); // Lấy tên đăng nhập từ TextBox
            string matKhau = txtMatkhau.Text.Trim(); // Lấy mật khẩu từ TextBox
            string chucVu = "";

            if (radioNV.Checked)
            {
                chucVu = "NhanVien";
            }
            else if (radioQL.Checked)
            {
                chucVu = "QuanLy";
            }

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Tên đăng nhập và mật khẩu không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (KiemTraDangNhap(tenDangNhap, matKhau, chucVu))
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Chuyển sang form Trang Chủ
                this.Hide(); // Ẩn Form Đăng Nhập
                TrangChu trangChu = new TrangChu();
                trangChu.Show();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập, mật khẩu hoặc chức vụ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private bool KiemTraDangNhap(string tenDangNhap, string matKhau, string chucVu)
        {
            bool isAuthenticated = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(sCon))
                {
                    conn.Open(); // Mở kết nối đến SQL Server

                    using (SqlCommand cmd = new SqlCommand("spDangNhap", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Thêm tham số vào thủ tục
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                        cmd.Parameters.AddWithValue("@ChucVu", chucVu);

                        // Thực thi thủ tục và kiểm tra kết quả
                        int result = (int)cmd.ExecuteScalar();
                        isAuthenticated = result == 1; // Kết quả trả về 1 là hợp lệ
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isAuthenticated;
        }



    }
}


