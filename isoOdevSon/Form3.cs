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
    public partial class Form3 : Form
    {

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DGFNFE1\SQLEXPRESS;Initial Catalog=isoOdev;Integrated Security=True;");


        public Form3()
        {
            InitializeComponent();
        }

        private void PersonelListele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Personeller", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void aNASAYFAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
            this.Hide();
        }

        private void pERSONELLERToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form3 frm = new Form3();
            frm.Show();
            this.Hide();
        }

        private void kATEGORILERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4();
            frm.Show();
            this.Hide();
        }

        private void mUSTERILERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 frm = new Form5();
            frm.Show();
            this.Hide();
        }

        private void sATISLARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 frm = new Form6();
            frm.Show();
            this.Hide();
        }

        private void cIKISYAPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            PersonelListele();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO Personeller (Isim, Soyisim, Telefon, Eposta, Birim, KullaniciAdi, Sifre) VALUES (@isim, @soyisim, @telefon, @eposta, @birim, @kadi, @sifre)", baglanti);
                komut.Parameters.AddWithValue("@isim", textBox1.Text);
                komut.Parameters.AddWithValue("@soyisim", textBox2.Text);
                komut.Parameters.AddWithValue("@telefon", textBox3.Text);
                komut.Parameters.AddWithValue("@eposta", textBox4.Text);
                komut.Parameters.AddWithValue("@birim", textBox5.Text);
                komut.Parameters.AddWithValue("@kadi", textBox6.Text);
                komut.Parameters.AddWithValue("@sifre", textBox7.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Personel eklendi.");
                PersonelListele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["PersonelID"].Value);
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("UPDATE Personeller SET Isim=@isim, Soyisim=@soyisim, Telefon=@telefon, Eposta=@eposta, Birim=@birim, KullaniciAdi=@kadi, Sifre=@sifre WHERE PersonelID=@id", baglanti);
                    komut.Parameters.AddWithValue("@isim", textBox1.Text);
                    komut.Parameters.AddWithValue("@soyisim", textBox2.Text);
                    komut.Parameters.AddWithValue("@telefon", textBox3.Text);
                    komut.Parameters.AddWithValue("@eposta", textBox4.Text);
                    komut.Parameters.AddWithValue("@birim", textBox5.Text);
                    komut.Parameters.AddWithValue("@kadi", textBox6.Text);
                    komut.Parameters.AddWithValue("@sifre", textBox7.Text);
                    komut.Parameters.AddWithValue("@id", id);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Güncellendi.");
                    PersonelListele();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["PersonelID"].Value);
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("DELETE FROM Personeller WHERE PersonelID=@id", baglanti);
                    komut.Parameters.AddWithValue("@id", id);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Personel silindi.");
                    PersonelListele();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void üRÜNLERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 frm = new Form7();
            frm.Show();
            this.Hide();
        }
    }
}
