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
    public partial class Form3: Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ad = textBox1.Text.Trim();
            string soyad = textBox2.Text.Trim();
            DateTime dogumTarihi = dateTimePicker1.Value.Date;
            string cinsiyet = radioButton1.Checked ? "KADIN" : "ERKEK";
            string telefon = maskedTextBox1.Text.Trim();
            string email = textBox3.Text.Trim();
            string adres = textBox4.Text.Trim();

            if (string.IsNullOrWhiteSpace(ad) || string.IsNullOrWhiteSpace(soyad))
            {
                MessageBox.Show("Ad ve Soyad boş olamaz!");
                return;
            }

            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO uyeler (ad, soyad, dogum_tarihi, cinsiyet, telefon, email, adres) " +
                               "VALUES (@ad, @soyad, @dogumTarihi, @cinsiyet, @telefon, @email, @adres)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ad", ad);
                    command.Parameters.AddWithValue("@soyad", soyad);
                    command.Parameters.AddWithValue("@dogumTarihi", dogumTarihi);
                    command.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                    command.Parameters.AddWithValue("@telefon", telefon);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@adres", adres);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Üye başarıyla eklendi!");

            // Formu temizlemek için:
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker1.Value = DateTime.Today;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            maskedTextBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
        

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
