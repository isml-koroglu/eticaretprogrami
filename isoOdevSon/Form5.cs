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
    public partial class Form5 : Form
    {

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DGFNFE1\SQLEXPRESS;Initial Catalog=isoOdev;Integrated Security=True;");


        public Form5()
        {
            InitializeComponent();
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

        private void üRÜNLERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 frm = new Form7();
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MusteriSatislariListele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                // Örneğin satışı SatisTarihi ve Müşteri Adına göre silmek için (daha kesin eşleşme için SatisID gerekirse önce onu ekleyebilirsin)
                string ad = dataGridView1.CurrentRow.Cells["Ad"].Value.ToString();
                string soyad = dataGridView1.CurrentRow.Cells["Soyad"].Value.ToString();
                string urunAdi = dataGridView1.CurrentRow.Cells["Aldığı Ürün"].Value.ToString();
                string tarih = dataGridView1.CurrentRow.Cells["Satın Alma Tarihi"].Value.ToString();

                baglanti.Open();

                // MüşteriID ve ÜrünID'yi bulup silmek en garantili yol
                SqlCommand komut = new SqlCommand(@"
            DELETE s FROM Satislar s
            INNER JOIN Musteriler m ON s.MusteriID = m.MusteriID
            INNER JOIN Urunler u ON s.UrunID = u.UrunID
            WHERE m.Ad = @ad AND m.Soyad = @soyad AND u.UrunAdi = @urunAdi AND s.SatisTarihi = @tarih
        ", baglanti);

                komut.Parameters.AddWithValue("@ad", ad);
                komut.Parameters.AddWithValue("@soyad", soyad);
                komut.Parameters.AddWithValue("@urunAdi", urunAdi);
                komut.Parameters.AddWithValue("@tarih", tarih);

                int sonuc = komut.ExecuteNonQuery();
                baglanti.Close();

                if (sonuc > 0)
                    MessageBox.Show("Kayıt silindi.");
                else
                    MessageBox.Show("Kayıt silinemedi.");

                // Güncelle
                button1_Click(null, null);
            }
        }

        private void MusteriSatislariListele()
        {
            SqlDataAdapter da = new SqlDataAdapter(@"
        SELECT 
            m.Ad AS [Ad],
            m.Soyad AS [Soyad],
            m.Telefon AS [Telefon],
            m.FaturaAdresi AS [Fatura Adresi],
            u.UrunAdi AS [Aldığı Ürün],
            s.Adet AS [Adet],
            s.SatisFiyati AS [Birim Fiyat],
            (s.Adet * s.SatisFiyati) AS [Toplam Tutar],
            s.SatisTarihi AS [Satın Alma Tarihi]
        FROM Satislar s
        INNER JOIN Musteriler m ON s.MusteriID = m.MusteriID
        INNER JOIN Urunler u ON s.UrunID = u.UrunID
    ", baglanti);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }




        private void Form5_Load(object sender, EventArgs e)
        {
            MusteriSatislariListele();
        }
    }
}
