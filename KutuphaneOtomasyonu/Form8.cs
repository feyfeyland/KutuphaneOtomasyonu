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
    public partial class Form8: Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            string kitapAdi = textBox1.Text.Trim(); // Arama kutusu (kitap adı girilen yer)

            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT id, kitap_adi, yazar_adi, tur, sayfa_sayisi, basim_yili, yayin_evi, adet " +
                               "FROM kitaplar WHERE kitap_adi ILIKE @kitapAdi";

                using (var adapter = new NpgsqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@kitapAdi", "%" + kitapAdi + "%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox2.Text = row.Cells["kitap_adi"].Value.ToString();
                textBox3.Text = row.Cells["yazar_adi"].Value.ToString();
                textBox4.Text = row.Cells["tur"].Value.ToString();
                textBox5.Text = row.Cells["sayfa_sayisi"].Value.ToString();
                textBox6.Text = row.Cells["basim_yili"].Value.ToString();
                textBox7.Text = row.Cells["yayin_evi"].Value.ToString();
                textBox8.Text = row.Cells["adet"].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
                int mevcutAdet = Convert.ToInt32(dataGridView1.CurrentRow.Cells["adet"].Value);

                string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query;

                    if (mevcutAdet > 1)
                    {
                        // Sadece adet azalt
                        query = "UPDATE kitaplar SET adet = adet - 1 WHERE id = @id";
                    }
                    else
                    {
                        // Adet 1 ise, kitabı tamamen sil
                        query = "DELETE FROM kitaplar WHERE id = @id";
                    }

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Kitap başarıyla silindi");
                button1_Click(null, null); // Listeyi yenile
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz kitabı seçin.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);

                string kitapAdi = textBox2.Text.Trim();
                string yazarAdi = textBox3.Text.Trim();
                string tur = textBox4.Text.Trim();
                int sayfa = string.IsNullOrWhiteSpace(textBox5.Text) ? 0 : Convert.ToInt32(textBox5.Text);
                int basimYili = string.IsNullOrWhiteSpace(textBox6.Text) ? 0 : Convert.ToInt32(textBox6.Text);
                string yayinEvi = textBox7.Text.Trim();
                int adet = string.IsNullOrWhiteSpace(textBox8.Text) ? 1 : Convert.ToInt32(textBox8.Text);

                string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE kitaplar SET kitap_adi = @kitapAdi, yazar_adi = @yazarAdi, tur = @tur, " +
                                   "sayfa_sayisi = @sayfa, basim_yili = @basimYili, yayin_evi = @yayinEvi, adet = @adet " +
                                   "WHERE id = @id"; // dikkat: sadece ID'li kayıt güncellenir

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@kitapAdi", kitapAdi);
                        command.Parameters.AddWithValue("@yazarAdi", yazarAdi);
                        command.Parameters.AddWithValue("@tur", tur);
                        command.Parameters.AddWithValue("@sayfa", sayfa);
                        command.Parameters.AddWithValue("@basimYili", basimYili);
                        command.Parameters.AddWithValue("@yayinEvi", yayinEvi);
                        command.Parameters.AddWithValue("@adet", adet);
                        command.Parameters.AddWithValue("@id", id); // mutlaka ID'ye göre günceller

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Kitap başarıyla güncellendi.");
                button1_Click(null, null); // Listeyi yenile
            }
            else
            {
                MessageBox.Show("Lütfen güncellenecek kitabı seçin.");
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            KitaplariListele();
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
                    dataGridView1.DataSource = dt; // Kitapları gösteren DataGridView
                }
            }
        }
    }
    }
    
    
    
    
    

