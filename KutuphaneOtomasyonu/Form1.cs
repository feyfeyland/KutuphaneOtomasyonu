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

namespace KutuphaneOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const string dogruKullaniciAdi = "admin";
        private const string dogruSifre = "1234";


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string kullaniciAdi = textBox1.Text.Trim();
            string sifre = textBox2.Text.Trim();

            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM kullanicilar WHERE kullaniciadi = @kullaniciadi AND sifre = @sifre";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                    command.Parameters.AddWithValue("@sifre", sifre);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        Form2 form2 = new Form2();
                        form2.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Hatalı kullanıcı adı veya şifre girdiniz!");
                    }
                }
            }







        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(kullaniciAdi))
            {
                MessageBox.Show("Lütfen önce kullanıcı adınızı giriniz.");
                return;
            }

            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";
            using (var connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM kullanicilar WHERE kullaniciadi = @kadi";

                using (var command = new Npgsql.NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@kadi", kullaniciAdi);
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        Form6 sifremiUnuttumForm = new Form6(kullaniciAdi);
                        sifremiUnuttumForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Bu kullanıcı adına ait kayıt bulunamadı.");
                    }
                }
            }
        }
    }
}
    
            
        
    

    
    
    

