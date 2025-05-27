using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace isoOdevSon
{
    public partial class Form1 : Form
    {

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DGFNFE1\SQLEXPRESS;Initial Catalog=isoOdev;Integrated Security=True;");


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox1.Text;
            string sifre = textBox2.Text;

            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM Personeller WHERE KullaniciAdi = @kadi AND Sifre = @sifre", baglanti);
                komut.Parameters.AddWithValue("@kadi", kullaniciAdi);
                komut.Parameters.AddWithValue("@sifre", sifre);

                int sonuc = (int)komut.ExecuteScalar();
                baglanti.Close();

                if (sonuc > 0)
                {
                    MessageBox.Show("Giriş başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form2 frm = new Form2();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı hatası: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
