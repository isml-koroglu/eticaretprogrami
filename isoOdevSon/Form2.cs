using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace isoOdevSon
{
    public partial class Form2 : Form
    {

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DGFNFE1\SQLEXPRESS;Initial Catalog=isoOdev;Integrated Security=True;");

        public Form2()
        {
            InitializeComponent();
        }

        private void aNASAYFAToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        // PERSONELLER → Form3
        private void pERSONELLERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
            this.Hide();
        }

        // KATEGORİLER → Form4
        private void kATEGORILERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4();
            frm.Show();
            this.Hide();
        }

        // MÜŞTERİLER → Form5
        private void mUSTERILERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 frm = new Form5();
            frm.Show();
            this.Hide();
        }

        // SATIŞLAR → Form6
        private void sATISLARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 frm = new Form6();
            frm.Show();
            this.Hide();
        }

        // ÇIKIŞ YAP → Form1 (Giriş ekranına geri)
        private void cIKISYAPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Titles.Add("Top 10 Satılan Ürün");

            Series seri = new Series("Satış Adedi");
            seri.ChartType = SeriesChartType.Column;

            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand(@"
            SELECT TOP 10 u.UrunAdi, SUM(s.Adet) AS ToplamAdet
            FROM Satislar s
            INNER JOIN Urunler u ON s.UrunID = u.UrunID
            GROUP BY u.UrunAdi
            ORDER BY ToplamAdet DESC
        ", baglanti);

                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    seri.Points.AddXY(dr["UrunAdi"].ToString(), dr["ToplamAdet"]);
                }
                chart1.Series.Add(seri);
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

            seri.Color = Color.SteelBlue;
            seri.IsValueShownAsLabel = true;





            try
            {
                baglanti.Open();

                // LABEL1 → Toplam satılan ürün adedi
                SqlCommand komut1 = new SqlCommand("SELECT SUM(Adet) FROM Satislar", baglanti);
                object toplamSatis = komut1.ExecuteScalar();
                label1.Text = "Toplam Satılan Ürün: " + (toplamSatis != DBNull.Value ? toplamSatis.ToString() : "0");

                // LABEL2 → Toplam müşteri sayısı
                SqlCommand komut2 = new SqlCommand("SELECT COUNT(*) FROM Musteriler", baglanti);
                label2.Text = "Toplam Müşteri: " + komut2.ExecuteScalar().ToString();

                // LABEL3 → Stoktaki toplam ürün adedi
                SqlCommand komut3 = new SqlCommand("SELECT SUM(StokAdedi) FROM Urunler", baglanti);
                object toplamStok = komut3.ExecuteScalar();
                label3.Text = "Toplam Stok: " + (toplamStok != DBNull.Value ? toplamStok.ToString() : "0");

                // LABEL4 → Kategori sayısı
                SqlCommand komut4 = new SqlCommand("SELECT COUNT(*) FROM Kategoriler", baglanti);
                label4.Text = "Kategori Sayısı: " + komut4.ExecuteScalar().ToString();

                baglanti.Close();
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
