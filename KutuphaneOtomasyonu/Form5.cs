using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyonu
{
    public partial class Form5: Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string aranan = textBox2.Text.Trim(); // Üye adı arama kutusu

            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT id, ad, soyad FROM uyeler WHERE ad ILIKE @aranan";

                using (var adapter = new NpgsqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@aranan", "%" + aranan + "%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView2.DataSource = dt; // Üyeleri listeleyen alt DataGridView
                }
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            string aranan = textBox1.Text.Trim(); // Kitap adı arama kutusu

            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM kitaplar WHERE kitap_adi ILIKE @aranan"; // kitaplar tablosu kullanılıyor
                using (var adapter = new NpgsqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@aranan", "%" + aranan + "%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt; // Sol üstteki kitaplar DataGridView
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                textBox4.Text = row.Cells["id"].Value.ToString(); // Üye ID'yi al
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox5.Text = row.Cells["kitap_adi"].Value.ToString(); // kitap_adi sütun adı doğruysa
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string kitapAdi = textBox5.Text.Trim();
            string uyeIdText = textBox4.Text.Trim();
            DateTime verilisTarihi = dateTimePicker1.Value.Date;
            DateTime teslimTarihi = dateTimePicker2.Value.Date;

            if (string.IsNullOrWhiteSpace(kitapAdi) || string.IsNullOrWhiteSpace(uyeIdText))
            {
                MessageBox.Show("Lütfen kitap ve üye seçiniz.");
                return;
            }

            if (!int.TryParse(uyeIdText, out int uyeId))
            {
                MessageBox.Show("Geçerli bir Üye ID giriniz.");
                return;
            }

            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Üye bilgilerini al
                string uyeSorgusu = "SELECT ad, soyad FROM uyeler WHERE id = @id";
                string uyeAdi = "", uyeSoyadi = "";

                using (var uyeKomut = new NpgsqlCommand(uyeSorgusu, connection))
                {
                    uyeKomut.Parameters.AddWithValue("@id", uyeId);
                    using (var reader = uyeKomut.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            uyeAdi = reader["ad"].ToString();
                            uyeSoyadi = reader["soyad"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Üye bulunamadı.");
                            return;
                        }
                    }
                }

                // Kayıt ekle
                string insertQuery = @"INSERT INTO odunc_kitaplar 
                               (uye_adi, uye_soyadi, kitap_adi, verilis_tarihi, teslim_tarihi)
                               VALUES (@ad, @soyad, @kitap, @verilis, @teslim)";

                using (var komut = new NpgsqlCommand(insertQuery, connection))
                {
                    komut.Parameters.AddWithValue("@ad", uyeAdi);
                    komut.Parameters.AddWithValue("@soyad", uyeSoyadi);
                    komut.Parameters.AddWithValue("@kitap", kitapAdi);
                    komut.Parameters.AddWithValue("@verilis", verilisTarihi);
                    komut.Parameters.AddWithValue("@teslim", teslimTarihi);

                    int sonuc = komut.ExecuteNonQuery();

                    if (sonuc > 0)
                    {
                        MessageBox.Show("Kitap ödünç verildi.");
                        KutulariTemizle();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt başarısız.");
                    }
                }
            }
        }
                
            

            private void KutulariTemizle()
        {
            textBox1.Clear(); // Kitap arama
            textBox2.Clear(); // Üye arama
            textBox4.Clear(); // Üye ID
            textBox5.Clear(); // Kitap Adı
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today.AddDays(7);
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            KitaplariListele();
            UyeleriListele();
        }
        private void UyeleriListele()
        {
            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT id, ad, soyad FROM uyeler";

                using (var adapter = new NpgsqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView2.DataSource = dt; // Üyeleri listeleyen DataGrid
                }
            }
        }
        private void KitaplariListele()
        {
            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM kitaplar";

                using (var adapter = new NpgsqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt; // Kitapları listeleyen DataGrid
                }
            }
        }
    }
    }







