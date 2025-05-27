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
    public partial class Form6 : Form
    {

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DGFNFE1\SQLEXPRESS;Initial Catalog=isoOdev;Integrated Security=True;");


        public Form6()
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

        private void Form6_Load(object sender, EventArgs e)
        {
            // Ürünleri getir
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT UrunID, UrunAdi, SatisFiyati FROM Urunler", baglanti);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            comboBox1.DataSource = dt1;
            comboBox1.DisplayMember = "UrunAdi";  // ekranda sadece ad görünsün
            comboBox1.ValueMember = "UrunID";

            // Personelleri getir
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT PersonelID, Isim + ' - ' + Birim AS PersonelBilgi FROM Personeller", baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            comboBox2.DataSource = dt2;
            comboBox2.DisplayMember = "PersonelBilgi";
            comboBox2.ValueMember = "PersonelID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();

                // 1. Müşteriyi ekle
                SqlCommand musteriKomut = new SqlCommand("INSERT INTO Musteriler (Ad, Soyad, Telefon, FaturaAdresi) OUTPUT INSERTED.MusteriID VALUES (@ad, @soyad, @tel, @adres)", baglanti);
                musteriKomut.Parameters.AddWithValue("@ad", textBox1.Text);
                musteriKomut.Parameters.AddWithValue("@soyad", textBox2.Text);
                musteriKomut.Parameters.AddWithValue("@tel", textBox3.Text);
                musteriKomut.Parameters.AddWithValue("@adres", textBox4.Text);
                int musteriID = (int)musteriKomut.ExecuteScalar();

                // 2. Seçilen ürün bilgilerini al
                int urunID = Convert.ToInt32(comboBox1.SelectedValue);
                int adet = Convert.ToInt32(textBox5.Text);

                // Ürünün satış fiyatını al
                SqlCommand fiyatKomut = new SqlCommand("SELECT SatisFiyati, StokAdedi FROM Urunler WHERE UrunID = @uid", baglanti);
                fiyatKomut.Parameters.AddWithValue("@uid", urunID);
                SqlDataReader reader = fiyatKomut.ExecuteReader();
                decimal fiyat = 0;
                int stok = 0;
                if (reader.Read())
                {
                    fiyat = Convert.ToDecimal(reader["SatisFiyati"]);
                    stok = Convert.ToInt32(reader["StokAdedi"]);
                }
                reader.Close();

                // 3. Yeterli stok var mı kontrol et
                if (stok < adet)
                {
                    baglanti.Close();
                    MessageBox.Show("Yeterli stok yok!");
                    return;
                }

                // 4. Satışı kaydet
                int personelID = Convert.ToInt32(comboBox2.SelectedValue);
                SqlCommand satisKomut = new SqlCommand("INSERT INTO Satislar (MusteriID, PersonelID, UrunID, Adet, SatisFiyati) VALUES (@mid, @pid, @uid, @adet, @fiyat)", baglanti);
                satisKomut.Parameters.AddWithValue("@mid", musteriID);
                satisKomut.Parameters.AddWithValue("@pid", personelID);
                satisKomut.Parameters.AddWithValue("@uid", urunID);
                satisKomut.Parameters.AddWithValue("@adet", adet);
                satisKomut.Parameters.AddWithValue("@fiyat", fiyat);
                satisKomut.ExecuteNonQuery();

                // 5. Stok düş
                SqlCommand stokKomut = new SqlCommand("UPDATE Urunler SET StokAdedi = StokAdedi - @adet WHERE UrunID = @uid", baglanti);
                stokKomut.Parameters.AddWithValue("@adet", adet);
                stokKomut.Parameters.AddWithValue("@uid", urunID);
                stokKomut.ExecuteNonQuery();

                baglanti.Close();

                MessageBox.Show("Satış başarılı! Kazanç: " + (adet * fiyat).ToString("C2"));
            }
            catch (Exception ex)
            {
                baglanti.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter(@"
        SELECT 
            s.SatisID,
            m.Ad + ' ' + m.Soyad AS Musteri,
            m.Telefon,
            m.FaturaAdresi,
            u.UrunAdi,
            s.Adet,
            s.SatisFiyati,
            (s.Adet * s.SatisFiyati) AS ToplamTutar,
            p.Isim + ' ' + p.Soyisim AS Personel,
            s.SatisTarihi
        FROM Satislar s
        INNER JOIN Musteriler m ON s.MusteriID = m.MusteriID
        INNER JOIN Urunler u ON s.UrunID = u.UrunID
        INNER JOIN Personeller p ON s.PersonelID = p.PersonelID
    ", baglanti);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
