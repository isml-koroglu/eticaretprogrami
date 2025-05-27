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
    public partial class Form7 : Form
    {

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DGFNFE1\SQLEXPRESS;Initial Catalog=isoOdev;Integrated Security=True;");


        public Form7()
        {
            InitializeComponent();
        }

        private void UrunListele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Urunler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }



        private void Form7_Load(object sender, EventArgs e)
        {
            // Kategori combobox'a yükleniyor
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kategoriler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "KategoriAdi";
            comboBox1.ValueMember = "KategoriID";

            UrunListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO Urunler (UrunAdi, Marka, Model, StokAdedi, SatisFiyati, KategoriID) VALUES (@ad, @marka, @model, @stok, @fiyat, @katid)", baglanti);
                komut.Parameters.AddWithValue("@ad", textBox1.Text);
                komut.Parameters.AddWithValue("@marka", textBox2.Text);
                komut.Parameters.AddWithValue("@model", textBox3.Text);
                komut.Parameters.AddWithValue("@stok", Convert.ToInt32(textBox4.Text));
                komut.Parameters.AddWithValue("@fiyat", Convert.ToDecimal(textBox5.Text));
                komut.Parameters.AddWithValue("@katid", comboBox1.SelectedValue);
                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Ürün eklendi.");
                UrunListele();
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
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["UrunID"].Value);

                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("UPDATE Urunler SET UrunAdi=@ad, Marka=@marka, Model=@model, StokAdedi=@stok, SatisFiyati=@fiyat, KategoriID=@katid WHERE UrunID=@id", baglanti);
                    komut.Parameters.AddWithValue("@ad", textBox1.Text);
                    komut.Parameters.AddWithValue("@marka", textBox2.Text);
                    komut.Parameters.AddWithValue("@model", textBox3.Text);
                    komut.Parameters.AddWithValue("@stok", Convert.ToInt32(textBox4.Text));
                    komut.Parameters.AddWithValue("@fiyat", Convert.ToDecimal(textBox5.Text));
                    komut.Parameters.AddWithValue("@katid", comboBox1.SelectedValue);
                    komut.Parameters.AddWithValue("@id", id);
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    MessageBox.Show("Ürün güncellendi.");
                    UrunListele();
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
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["UrunID"].Value);

                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("DELETE FROM Urunler WHERE UrunID=@id", baglanti);
                    komut.Parameters.AddWithValue("@id", id);
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    MessageBox.Show("Ürün silindi.");
                    UrunListele();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
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
    }
}
