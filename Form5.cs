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

namespace IEEE
{
    public partial class Form5 : Form
    {
        String table_number = "";
        int item_number = 0; 
        Manage a = new Manage();
        

        public Form5(String txt)
        {
            InitializeComponent();
            populateComboBox();

            textBox1.Visible = false;

            this.Text = "Ekle";
            textBox2.Text = "1";
            String[] a;
            a = txt.Split(' ');
            table_number = a[1];
                        
        }

        public Form5(String n, String q)
        {
            InitializeComponent();
            populateComboBox();

            textBox1.Visible = false;

            textBox1.Text = n;
            comboBox1.SelectedIndex = int.Parse(n) - 1;
            textBox2.Text = q;
            button1.Text = "Güncelle";
            this.Text = "Güncelle";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(textBox2.Text)) 
            {
                MessageBox.Show("Lütfen adet giriniz.");
                textBox2.Text = "1";
            }

            else if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "[^0-9]"))
            {
                MessageBox.Show("Lütfen bir sayı giriniz.");
                textBox2.Text = "1";
            }

            else if(comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir ürün seçiniz.");
            }



            else
            {
                if (button1.Text == "Ekle")
                {
                    a.setNo(int.Parse(textBox1.Text));
                    if (textBox2.Text == "0")
                    {
                        MessageBox.Show("Lütfen sıfırdan farklı bir sayı giriniz.");
                        textBox2.Text = "1";
                    }

                    else
                    {
                        Ekle(table_number);
                    }
                  


                }

                if (button1.Text == "Güncelle")
                {
                    a.setNo(int.Parse(textBox1.Text));
                    Güncelle(table_number);

                }


            }
                       
            

        }

        public void Ekle(String table_number)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Restaurant_Database.mdf;Integrated Security=True");

            try
            {

                int table = int.Parse(table_number);
                item_number = int.Parse(textBox1.Text.ToString());
                int quantityy = int.Parse(textBox2.Text.ToString());
                float price = a.Fiyat();
                int order_no = a.getCount();
                String item_name = a.setName();

                if (con.State == ConnectionState.Closed)
                {

                    con.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO Orders (table_no, item_no, quantity, item_price,item_name,order_no) VALUES (@t_no,@i_no,@q,@i_price,@i_name,@o_no)";

                    cmd.Parameters.AddWithValue("@t_no", table);
                    cmd.Parameters.AddWithValue("@i_no", item_number);
                    cmd.Parameters.AddWithValue("@i_name", item_name);
                    cmd.Parameters.AddWithValue("@q", quantityy);
                    cmd.Parameters.AddWithValue("@i_price", price);
                    cmd.Parameters.AddWithValue("o_no", order_no);


                    cmd.ExecuteNonQuery();

                }

                con.Close();
                MessageBox.Show("Eklendi");
                this.Close();

            }

            catch
            {
                MessageBox.Show("Lütfen tekrar deneyiniz.");
            }
            
            
        }

        public void Güncelle(String table_number)
        {

            int i_num = int.Parse(textBox1.Text);
            int q = int.Parse(textBox2.Text);
            float p = a.Fiyat();
            int o = int.Parse(label3.Text);
            String name = a.setName();

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Restaurant_Database.mdf;Integrated Security=True");

            if(con.State == ConnectionState.Closed)
            {

                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE Orders SET item_no = @n, quantity = @q, item_price = @p, item_name = @nm WHERE order_no = @o";

                cmd.Parameters.AddWithValue("@n", i_num);
                cmd.Parameters.AddWithValue("@q", q);
                cmd.Parameters.AddWithValue("@p", p);
                cmd.Parameters.AddWithValue("@nm", name);
                cmd.Parameters.AddWithValue("@o", o);

                cmd.ExecuteNonQuery();
            }

                con.Close();
                MessageBox.Show("Güncellendi.");
                a.Delete();
                this.Close();
            

        }

        public void populateComboBox()
        {
            
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Restaurant_Database.mdf;Integrated Security=True");

            if(con.State == ConnectionState.Closed)
            {

                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT item_name FROM Menu ORDER BY item_no";



                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    comboBox1.Items.Add(dr["item_name"].ToString());
                    
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            textBox1.Text = (comboBox1.SelectedIndex +1).ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int n = int.Parse(textBox2.Text);
            n = n + 1;
            textBox2.Text = n.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            int n = int.Parse(textBox2.Text);

            if (n > 0)
            {
                n = n - 1;
                textBox2.Text = n.ToString();
            }

            else if(button1.Text == "Ekle")
            {
                MessageBox.Show("En az 1 ürün eklemelisiniz.");
            }
           

        }
    }
    }

