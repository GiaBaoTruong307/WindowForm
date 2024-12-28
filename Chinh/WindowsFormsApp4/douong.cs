using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BTN
{
    public partial class DoUong : Form
    {
        string sCon = "Data Source=LAPTOP-8I721H54\\GIABAOTRUONG;Initial Catalog=Banhang;Integrated Security=True;Encrypt=True";
        public DoUong()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    txtMaDoUong.Text = row.Cells["MaDoUong"].Value.ToString();
                    txtTenDoUong.Text = row.Cells["TenDoUong"].Value.ToString();
                    txtGia.Text = row.Cells["Gia"].Value.ToString();
                    txtKichCo.Text = row.Cells["KichCo"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xảy ra lỗi: {ex.Message}");
            }
            txtMaDoUong.Enabled = false;

        }

        

        private void btVe_Click(object sender, EventArgs e)
        {
            this.Hide();
            var TrangChu = new TrangChu();
            TrangChu.Show();

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtMaDoUong.Text = dataGridView1.Rows[e.RowIndex].Cells["MaDoUong"].Value.ToString();
            txtTenDoUong.Text = dataGridView1.Rows[e.RowIndex].Cells["TenDoUong"].Value.ToString();
            txtGia.Text = dataGridView1.Rows[e.RowIndex].Cells["Gia"].Value.ToString();
            txtKichCo.Text = dataGridView1.Rows[e.RowIndex].Cells["KichCo"].Value.ToString();
        }

        private void DoUong_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi trong quá trình kết nối DB");
            }

            string sQuery = "select * from DoUong";
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, con);

            DataSet ds = new DataSet();

            adapter.Fill(ds, "DoUong");
            dataGridView1.DataSource = ds.Tables["DoUong"];

            con.Close();

        }

        private void LUU_Click_1(object sender, EventArgs e)
        {
            string sMaDoUong = txtMaDoUong.Text.Trim();
            string sTenDoUong = txtTenDoUong.Text.Trim();
            string sGia = txtGia.Text.Trim();
            string sKichCo = txtKichCo.Text.Trim();

            if (string.IsNullOrWhiteSpace(sMaDoUong) ||
                string.IsNullOrWhiteSpace(sTenDoUong) ||
                string.IsNullOrWhiteSpace(sGia) ||
                string.IsNullOrWhiteSpace(sKichCo))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open();
                string sQuery = "INSERT INTO DoUong (MaDoUong, TenDoUong, Gia, KichCo) VALUES (@MaDoUong, @TenDoUong, @Gia, @KichCo)";
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.AddWithValue("@MaDoUong", sMaDoUong);
                cmd.Parameters.AddWithValue("@TenDoUong", sTenDoUong);
                cmd.Parameters.AddWithValue("@Gia", sGia);
                cmd.Parameters.AddWithValue("@KichCo", sKichCo);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DoUong_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Không thể lưu dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }

        }

        private void Sua_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sCon);
            {
                try
                {
                    con.Open();

                    string sMaDoUong = txtMaDoUong.Text.Trim();
                    string sTenDoUong = txtTenDoUong.Text.Trim();
                    string sGia = txtGia.Text.Trim();
                    string sKichCo = txtKichCo.Text.Trim();

                    if (string.IsNullOrWhiteSpace(sTenDoUong) || string.IsNullOrWhiteSpace(sGia) || string.IsNullOrWhiteSpace(sKichCo))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin (trừ Mã Đồ Uống).");
                        return;
                    }

                    if (!decimal.TryParse(sGia, out decimal gia))
                    {
                        MessageBox.Show("Giá phải là một số hợp lệ.");
                        return;
                    }

                    string sQuery = "UPDATE DoUong SET TenDoUong = @TenDoUong, Gia = @Gia, KichCo = @KichCo WHERE MaDoUong = @OriginalMaDoUong";
                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.AddWithValue("@OriginalMaDoUong", txtMaDoUong.Text.Trim());
                    cmd.Parameters.AddWithValue("@TenDoUong", sTenDoUong);
                    cmd.Parameters.AddWithValue("@Gia", gia);
                    cmd.Parameters.AddWithValue("@KichCo", sKichCo);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!");
                        txtTenDoUong.Text = "";
                        txtGia.Text = "";
                        txtKichCo.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã đồ uống để cập nhật.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Xảy ra lỗi trong quá trình cập nhật: {ex.Message}");
                }
            }


        }

        private void Xoa_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open();

                string sMaDoUong = txtMaDoUong.Text.Trim();

                if (string.IsNullOrWhiteSpace(sMaDoUong))
                {
                    MessageBox.Show("Vui lòng chọn đồ uống để xóa (MaDoUong không được để trống).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sQuery = "DELETE FROM DoUong WHERE MaDoUong = @MaDoUong";
                SqlCommand cmd = new SqlCommand(sQuery, con);

                cmd.Parameters.AddWithValue("@MaDoUong", sMaDoUong);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Xóa đồ uống thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtMaDoUong.Text = "";
                    txtTenDoUong.Text = "";
                    txtGia.Text = "";
                    txtKichCo.Text = "";

                    DoUong_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã đồ uống để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xảy ra lỗi trong quá trình xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }

        }
    }
}
