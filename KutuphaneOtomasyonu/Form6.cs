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
    public partial class Form6: Form
    {
        private string kullaniciAdi;
        public Form6(string kullaniciAdiParametre)
        {
            InitializeComponent();
            kullaniciAdi = kullaniciAdiParametre;
            this.kullaniciAdi = kullaniciAdi;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234";


            if (textBox1.Text != textBox2.Text)
            {
                MessageBox.Show("Şifreler uyuşmuyor. Lütfen aynı şifreyi girin.");
                return;
            }

            using (var connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE kullanicilar SET sifre = @sifre WHERE kullaniciadi = @kadi";

                using (var command = new Npgsql.NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sifre", textBox1.Text);
                    command.Parameters.AddWithValue("@kadi", kullaniciAdi);

                    int sonuc = command.ExecuteNonQuery();

                    if (sonuc > 0)
                    {
                        MessageBox.Show("Şifre başarıyla güncellendi.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı bulunamadı.");
                    }
                }
            }

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            textBox1.UseSystemPasswordChar = true;
            textBox2.UseSystemPasswordChar = true;
        }
    }
    }

