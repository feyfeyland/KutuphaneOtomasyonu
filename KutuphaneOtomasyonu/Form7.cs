using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KutuphaneOtomasyonu
{
    public partial class Form7: Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uyeAdi = textBox1.Text.Trim();
            string uyeSoyadi = textBox2.Text.Trim();

            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id, ad, soyad, dogum_tarihi, cinsiyet, telefon, email, adres " +
                               "FROM uyeler " +
                               "WHERE ad ILIKE @ad AND soyad ILIKE @soyad";

                using (var adapter = new NpgsqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@ad", "%" + uyeAdi + "%");
                    adapter.SelectCommand.Parameters.AddWithValue("@soyad", "%" + uyeSoyadi + "%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);

                string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM uyeler WHERE id = @id";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Üye başarıyla silindi.");
                UyeleriListele();
              
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);

                string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"UPDATE uyeler 
                             SET ad = @ad, soyad = @soyad, dogum_tarihi = @dogumTarihi, 
                                 cinsiyet = @cinsiyet, telefon = @telefon, 
                                 email = @email, adres = @adres 
                             WHERE id = @id";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ad", textBox3.Text.Trim());
                        command.Parameters.AddWithValue("@soyad", textBox4.Text.Trim());
                        command.Parameters.AddWithValue("@dogumTarihi", dateTimePicker1.Value.Date);
                        command.Parameters.AddWithValue("@cinsiyet", radioButton1.Checked ? "KADIN" : "ERKEK");
                        command.Parameters.AddWithValue("@telefon", maskedTextBox1.Text.Trim());
                        command.Parameters.AddWithValue("@email", textBox6.Text.Trim());
                        command.Parameters.AddWithValue("@adres", textBox7.Text.Trim());
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
                UyeleriListele();

                MessageBox.Show("Üye başarıyla güncellendi.");
            }
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox3.Text = row.Cells["ad"].Value.ToString();
                textBox4.Text = row.Cells["soyad"].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["dogum_tarihi"].Value);

                string cinsiyet = row.Cells["cinsiyet"].Value.ToString();
                if (cinsiyet == "KADIN")
                    radioButton1.Checked = true;
                else
                    radioButton2.Checked = true;

                maskedTextBox1.Text = row.Cells["telefon"].Value.ToString();
                textBox6.Text = row.Cells["email"].Value.ToString();
                textBox7.Text = row.Cells["adres"].Value.ToString();
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            UyeleriListele();
        }
        private void UyeleriListele()
        {
            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM uyeler";

                using (var adapter = new NpgsqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt; // Üye listesini gösteren DataGridView
                }
            }
        }
    }
    }
        
    
    
    
    

