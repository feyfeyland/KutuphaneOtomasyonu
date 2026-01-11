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
    public partial class Form9: Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int id))
            {
                MessageBox.Show("Geçerli bir ID seçin.");
                return;
            }

            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM odunc_kitaplar WHERE id = @id";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                        MessageBox.Show("Kitap teslim alındı.");
                    else
                        MessageBox.Show("Silme başarısız.");
                }
            }

            ListeyiYenile();
        }
        

        
        private void ListeyiYenile()
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;

            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Aktif (teslim edilmemiş) kitaplar
                string queryAktif = "SELECT * FROM odunc_kitaplar WHERE teslim_tarihi >= CURRENT_DATE";
                using (var adapter = new NpgsqlDataAdapter(queryAktif, connection))
                {
                    DataTable dtAktif = new DataTable();
                    adapter.Fill(dtAktif);
                    dataGridView1.DataSource = dtAktif;
                }

                // Gecikmiş kitaplar
                string queryGecikmis = "SELECT * FROM odunc_kitaplar WHERE teslim_tarihi < CURRENT_DATE";
                using (var adapter = new NpgsqlDataAdapter(queryGecikmis, connection))
                {
                    DataTable dtGecikmis = new DataTable();
                    adapter.Fill(dtGecikmis);
                    dataGridView2.DataSource = dtGecikmis;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            // ID kontrolü
            if (!int.TryParse(textBox5.Text.Trim(), out int oduncId))
            {
                MessageBox.Show("Lütfen geçerli bir Ödünç ID girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Yeni teslim tarihi
            DateTime yeniTeslimTarihi = dateTimePicker2.Value.Date;

            // Bağlantı bilgileri
            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE odunc_kitaplar SET teslim_tarihi = @tarih WHERE id = @id";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@tarih", yeniTeslimTarihi);
                        command.Parameters.AddWithValue("@id", oduncId);

                        int affected = command.ExecuteNonQuery();

                        if (affected > 0)
                        {
                            MessageBox.Show("Teslim tarihi başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Güncelleme başarısız! ID bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                // Güncel listeyi göster
                ListeyiYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void Form9_Load(object sender, EventArgs e)
        {
            ListeyiYenile();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // KİTAP TESLİM alanına
                textBox1.Text = row.Cells["id"].Value.ToString();           // Ödünç Id
                textBox2.Text = row.Cells["uye_adi"].Value.ToString();      // Teslim Eden Adı
                textBox3.Text = row.Cells["uye_soyadi"].Value.ToString();   // Soyad
                textBox4.Text = row.Cells["kitap_adi"].Value.ToString();    // Teslim Edilen Kitap Adı

                // SÜRE UZATMA alanına
                textBox5.Text = row.Cells["id"].Value.ToString();           // Ödünç Id
                textBox6.Text = row.Cells["uye_adi"].Value.ToString();      // Üye Adı
                textBox7.Text = row.Cells["uye_soyadi"].Value.ToString();   // Soyad
                textBox8.Text = row.Cells["kitap_adi"].Value.ToString();    // Kitap Adı
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["verilis_tarihi"].Value);  // Veriliş Tarihi
                dateTimePicker2.Value = Convert.ToDateTime(row.Cells["teslim_tarihi"].Value);   // Teslim Tarihi
            }
        }
        

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                // KİTAP TESLİM alanına
                textBox1.Text = row.Cells["id"].Value.ToString();           // Ödünç Id
                textBox2.Text = row.Cells["uye_adi"].Value.ToString();      // Teslim Eden Adı
                textBox3.Text = row.Cells["uye_soyadi"].Value.ToString();   // Soyad
                textBox4.Text = row.Cells["kitap_adi"].Value.ToString();    // Teslim Edilen Kitap Adı

                // SÜRE UZATMA alanına
                textBox5.Text = row.Cells["id"].Value.ToString();           // Ödünç Id
                textBox6.Text = row.Cells["uye_adi"].Value.ToString();      // Üye Adı
                textBox7.Text = row.Cells["uye_soyadi"].Value.ToString();   // Soyad
                textBox8.Text = row.Cells["kitap_adi"].Value.ToString();    // Kitap Adı
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["verilis_tarihi"].Value);  // Veriliş Tarihi
                dateTimePicker2.Value = Convert.ToDateTime(row.Cells["teslim_tarihi"].Value);   // Teslim Tarihi
            }
        }
    }
    }
    
    
    
    

