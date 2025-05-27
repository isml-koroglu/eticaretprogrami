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
    public partial class Form4 : Form
    {

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DGFNFE1\SQLEXPRESS;Initial Catalog=isoOdev;Integrated Security=True;");


        public Form4()
        {
            InitializeComponent();
        }

        private void KategoriListele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kategoriler", baglanti);
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

        private void Form4_Load(object sender, EventArgs e)
        {
            KategoriListele();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO Kategoriler (KategoriAdi, KategoriTuru) VALUES (@adi, @turu)", baglanti);
                komut.Parameters.AddWithValue("@adi", textBox1.Text);
                komut.Parameters.AddWithValue("@turu", textBox2.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Kategori eklendi.");
                KategoriListele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KategoriListele();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["KategoriID"].Value);
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("DELETE FROM Kategoriler WHERE KategoriID = @id", baglanti);
                    komut.Parameters.AddWithValue("@id", id);
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    MessageBox.Show("Kategori silindi.");
                    KategoriListele();
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
