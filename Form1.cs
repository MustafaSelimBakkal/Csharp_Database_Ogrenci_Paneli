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

namespace kutuphane
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-KMVKNF3\\SQLEXPRESS;Initial Catalog=kutuphane;Integrated Security=True");
        DataTable dt;
        SqlCommand cmd;
        SqlDataAdapter adapter;

        string cinsiyet;


        private void Form1_Load(object sender, EventArgs e)
        {
            VeriGetir();
            this.Text = "Ogrenci Uygulaması";
            radioButton1.Checked = true;
            textBox2.Enabled = false;
        }

        void VeriGetir()
        {
            dt = new DataTable();
            conn.Open();
            adapter = new SqlDataAdapter("SELECT * FROM ogrenci ", conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "K";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "E";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString(); //Oğrenci Numarası
                textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString(); //Öğrenci Adı
                textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString(); //Öğrenci Soyadı
                string cins = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                if (cins == "K")
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;                                    //Cinsiyetini Belli eden kod
                }
                else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                }
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString(); //Doğum  tarihi
                comboBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();    //Sınıfı
                textBox5.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();     //Puanı

            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "Insert Into ogrenci (ograd,ogrsoyad,cinsiyet,dtarih,sinif,puan) values (@ad,@soyad,@cinsiyet,@dtarih,@sinif,@puan)";
            cmd = new SqlCommand(sorgu, conn);
            cmd.Parameters.AddWithValue("@ad", textBox3.Text);
            cmd.Parameters.AddWithValue("@soyad", textBox4.Text);
            cmd.Parameters.AddWithValue("@cinsiyet", cinsiyet);
            cmd.Parameters.AddWithValue("@dtarih", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@sinif", comboBox1.Text);
            cmd.Parameters.AddWithValue("@puan", Convert.ToInt32(textBox5.Text));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            VeriGetir();
            MessageBox.Show("Kayıt Eklendi.");
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "ograd LIKE '" + textBox1.Text + "%'";
            dataGridView1.DataSource = dv;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string sql = "Delete From ogrenci Where ogrno=@no";
            cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@no", Convert.ToInt32(textBox2.Text));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            VeriGetir();
            MessageBox.Show("Kayıt silindi.");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string sql = "UPDATE ogrenci " +
                "SET ograd=@ad,ogrsoyad=@soyad,cinsiyet=@cinsiyet,dtarih=@dtarih,sinif=@sinif,puan=@puan" +
                " WHERE ogrno=@no";
            cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ad", textBox3.Text);
            cmd.Parameters.AddWithValue("@soyad", textBox4.Text);
            cmd.Parameters.AddWithValue("cinsiyet", cinsiyet);
            cmd.Parameters.AddWithValue("@dtarih", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@sinif", comboBox1.Text);
            cmd.Parameters.AddWithValue("@puan", Convert.ToInt32(textBox5.Text));
            cmd.Parameters.AddWithValue("@no", Convert.ToInt32(textBox2.Text));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            VeriGetir();
            MessageBox.Show("Kayıt güncellendi.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            
        }
    }
}
    
 
