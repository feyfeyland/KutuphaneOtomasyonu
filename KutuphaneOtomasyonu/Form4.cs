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
    public partial class Form4: Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kitapAdi = textBox1.Text.Trim();
            string yazarAdi = textBox2.Text.Trim();
            string tur = comboBox1.Text.Trim(); // ComboBox ise böyle
            string yayinEvi = textBox5.Text.Trim();
            


            if(string.IsNullOrWhiteSpace(kitapAdi)|| string.IsNullOrWhiteSpace(yazarAdi)|| string.IsNullOrWhiteSpace(tur))
            {
                MessageBox.Show("Kitap Adı , Yazar Adı ve Tür boş olamaz!");
            }

            int sayfa, basimYili, adet;

            if (!int.TryParse(textBox3.Text, out sayfa) ||
                !int.TryParse(textBox4.Text, out basimYili) ||
                !int.TryParse(textBox6.Text, out adet))
            {
                MessageBox.Show("Sayfa sayısı, basım yılı ve adet alanlarına sadece sayı giriniz!");
                return;
            }

            string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO kitaplar (kitap_adi, yazar_adi, tur, sayfa_sayisi, basim_yili, yayin_evi, adet) " +
                               "VALUES (@kitapAdi, @yazarAdi, @tur, @sayfa, @basimYili, @yayinEvi, @adet)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@kitapAdi", kitapAdi);
                    command.Parameters.AddWithValue("@yazarAdi", yazarAdi);
                    command.Parameters.AddWithValue("@tur", tur);
                    command.Parameters.AddWithValue("@sayfa", sayfa);
                    command.Parameters.AddWithValue("@basimYili", basimYili);
                    command.Parameters.AddWithValue("@yayinEvi", yayinEvi);
                    command.Parameters.AddWithValue("@adet", adet);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Kitap başarıyla eklendi!");

            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Roman");
            comboBox1.Items.Add("Tarih");
            comboBox1.Items.Add("Biyografi");
            comboBox1.Items.Add("Şiir");
            comboBox1.Items.Add("Bilim");

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
