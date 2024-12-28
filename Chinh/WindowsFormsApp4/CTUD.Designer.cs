namespace BTN
{
    partial class CTUD
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MaUuDai = new System.Windows.Forms.Label();
            this.SoLanDenQuan = new System.Windows.Forms.Label();
            this.txtMaUuDai = new System.Windows.Forms.TextBox();
            this.iSoLanDenQuan = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Sua = new System.Windows.Forms.Button();
            this.btVe = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // MaUuDai
            // 
            this.MaUuDai.AutoSize = true;
            this.MaUuDai.BackColor = System.Drawing.Color.Transparent;
            this.MaUuDai.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaUuDai.Location = new System.Drawing.Point(34, 113);
            this.MaUuDai.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MaUuDai.Name = "MaUuDai";
            this.MaUuDai.Size = new System.Drawing.Size(59, 13);
            this.MaUuDai.TabIndex = 0;
            this.MaUuDai.Text = "MaUuDai";
            // 
            // SoLanDenQuan
            // 
            this.SoLanDenQuan.AutoSize = true;
            this.SoLanDenQuan.BackColor = System.Drawing.Color.Transparent;
            this.SoLanDenQuan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SoLanDenQuan.Location = new System.Drawing.Point(34, 163);
            this.SoLanDenQuan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SoLanDenQuan.Name = "SoLanDenQuan";
            this.SoLanDenQuan.Size = new System.Drawing.Size(96, 13);
            this.SoLanDenQuan.TabIndex = 1;
            this.SoLanDenQuan.Text = "SoLanDenQuan";
            // 
            // txtMaUuDai
            // 
            this.txtMaUuDai.Location = new System.Drawing.Point(165, 108);
            this.txtMaUuDai.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaUuDai.Name = "txtMaUuDai";
            this.txtMaUuDai.Size = new System.Drawing.Size(122, 20);
            this.txtMaUuDai.TabIndex = 2;
            this.txtMaUuDai.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // iSoLanDenQuan
            // 
            this.iSoLanDenQuan.Location = new System.Drawing.Point(165, 158);
            this.iSoLanDenQuan.Margin = new System.Windows.Forms.Padding(2);
            this.iSoLanDenQuan.Name = "iSoLanDenQuan";
            this.iSoLanDenQuan.Size = new System.Drawing.Size(122, 20);
            this.iSoLanDenQuan.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(319, 77);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(260, 237);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Sua
            // 
            this.Sua.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sua.Location = new System.Drawing.Point(108, 243);
            this.Sua.Margin = new System.Windows.Forms.Padding(2);
            this.Sua.Name = "Sua";
            this.Sua.Size = new System.Drawing.Size(68, 27);
            this.Sua.TabIndex = 6;
            this.Sua.Text = "Sửa";
            this.Sua.UseVisualStyleBackColor = true;
            this.Sua.Click += new System.EventHandler(this.Sua_Click);
            // 
            // btVe
            // 
            this.btVe.Location = new System.Drawing.Point(480, 331);
            this.btVe.Name = "btVe";
            this.btVe.Size = new System.Drawing.Size(108, 23);
            this.btVe.TabIndex = 7;
            this.btVe.Text = "Về Trang Chủ";
            this.btVe.UseVisualStyleBackColor = true;
            this.btVe.Click += new System.EventHandler(this.btVe_Click);
            // 
            // CTUD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.bg;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.btVe);
            this.Controls.Add(this.Sua);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.iSoLanDenQuan);
            this.Controls.Add(this.txtMaUuDai);
            this.Controls.Add(this.SoLanDenQuan);
            this.Controls.Add(this.MaUuDai);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CTUD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý chương trình ưu đãi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuanlyCTUD_FormClosing);
            this.Load += new System.EventHandler(this.QuanlyCTUD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MaUuDai;
        private System.Windows.Forms.Label SoLanDenQuan;
        private System.Windows.Forms.TextBox txtMaUuDai;
        private System.Windows.Forms.TextBox iSoLanDenQuan;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Sua;
        private System.Windows.Forms.Button btVe;
    }
}

