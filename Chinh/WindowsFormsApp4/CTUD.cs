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
using System.Data.SqlClient;

namespace BTN
{
    public partial class CTUD : Form
    {
        string sCon = "Data Source=LAPTOP-8I721H54\\GIABAOTRUONG;Initial Catalog=Banhang;Integrated Security=True;Encrypt=True";
        public CTUD()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void QuanlyCTUD_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Hẹn gặp lại lần sau", "Thông báo");
        }

        private void QuanlyCTUD_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open(); // Mở kết nối tới cơ sở dữ liệu
                MessageBox.Show("Kết nối cơ sở dữ liệu thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi trong quá trình kết nối DB: " + ex.Message, "Lỗi");
                return; // Dừng việc thực thi nếu kết nối thất bại
            }
            // Câu lệnh truy vấn để lấy dữ liệu
            string sQuery = "SELECT * FROM ChuongTrinhUuDai";
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, con);
            DataSet ds = new DataSet();


            try
            {
                // Đổ dữ liệu vào DataSet
                adapter.Fill(ds, "ChuongTrinhUuDai");

                // Gán dữ liệu vào DataGridView
                dataGridView1.DataSource = ds.Tables["ChuongTrinhUuDai"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi khi lấy dữ liệu: " + ex.Message, "Lỗi");
            }
            finally
            {
                con.Close(); // Đóng kết nối
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaUuDai.Text = dataGridView1.Rows[e.RowIndex].Cells["MaUuDai"].Value.ToString();
                iSoLanDenQuan.Text = dataGridView1.Rows[e.RowIndex].Cells["SoLanDenQuan"].Value.ToString();
            }
            txtMaUuDai.Enabled = false;

        }

        private void Sua_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open(); // Mở kết nối tới cơ sở dữ liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi trong quá trình kết nối DB: " + ex.Message, "Lỗi");
                return; // Dừng việc thực thi nếu kết nối thất bại
            }

            // Lấy dữ liệu từ các TextBox
            string sMaUuDai = txtMaUuDai.Text;
            string sSoLanDenQuan = iSoLanDenQuan.Text;

            // Câu truy vấn cập nhật
            string sQuery = "UPDATE ChuongTrinhUuDai SET SoLanDenQuan = @SoLanDenQuan WHERE MaUuDai = @MaUuDai";

            // Tạo đối tượng SqlCommand
            SqlCommand cmd = new SqlCommand(sQuery, con);
            cmd.Parameters.AddWithValue("@MaUuDai", sMaUuDai);
            cmd.Parameters.AddWithValue("@SoLanDenQuan", sSoLanDenQuan); // Sửa đúng tên tham số

            try
            {
                // Thực thi câu lệnh SQL
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật thành công", "Thông báo");

                    // Làm mới dữ liệu trong DataGridView sau khi cập nhật
                    string refreshQuery = "SELECT * FROM ChuongTrinhUuDai";
                    SqlDataAdapter adapter = new SqlDataAdapter(refreshQuery, con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không có dòng nào được cập nhật. Vui lòng kiểm tra lại dữ liệu!", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi trong quá trình cập nhật: " + ex.Message, "Lỗi");
            }
            finally
            {
                con.Close(); // Đóng kết nối
            }
        }

        private void btVe_Click(object sender, EventArgs e)
        {
            this.Hide();
            var TrangChu = new TrangChu();
            TrangChu.Show();
        }
    }
    
}
