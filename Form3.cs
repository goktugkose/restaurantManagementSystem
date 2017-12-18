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
    public partial class Form3 : Form
    {
        Manage a = new Manage();

        public Form3()
        {
            InitializeComponent();
            pics();
            checks();
            listView1.AutoResizeColumns(System.Windows.Forms.ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(System.Windows.Forms.ColumnHeaderAutoResizeStyle.HeaderSize);

        }

        public void pics()
        {
            for (int i = 0; i <= 15; i++)
            {

                var pics = Controls.Find("pictureBox" + i, true);
                foreach (PictureBox pic in pics)
                {

                    if (a.isEmpty(i.ToString()))
                    {
                        pic.BackgroundImage = Image.FromFile(@"red.png");
                    }

                    else
                    {
                        pic.BackgroundImage = Image.FromFile(@"green.png");
                    }


                }

            }
        }

        public void checks()
        {
            
            for (int i = 1; i <= 15; i++)
            {

                var checks = Controls.Find("lbl" + i, true);
                foreach (Label lbl in checks)
                {
                    String n = a.Hesap(i.ToString()) + "₺";
                    a.Hesap(i.ToString());
                    lbl.Text = n;

                    if(n!="0₺")
                    {
                        lbl.Visible = true;
                    }

                    else if(n == "0₺")
                    {
                        lbl.Visible = false;
                    }

                   
                   
                }
                }
                
            }  

        private void timer1_Tick(object sender, EventArgs e)
        {
            pics();
            checks();
            getOrder();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Text = "Menu";
            f.ShowDialog();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form4 f = new IEEE.Form4("1");
            f.Text = "Masa 1";
            f.ShowDialog();
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form4 f = new IEEE.Form4("2");
            f.Text = "Masa 2";
            f.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form4 f = new IEEE.Form4("3");
            f.Text = "Masa 3";
            f.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form4 f = new IEEE.Form4("4");
            f.Text = "Masa 4";
            f.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form4 f = new IEEE.Form4("5");
            f.Text = "Masa 5";
            f.ShowDialog();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Form4 f = new IEEE.Form4("6");
            f.Text = "Masa 6";
            f.ShowDialog();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Form4 f = new IEEE.Form4("7");
            f.Text = "Masa 7";
            f.ShowDialog();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Form4 f = new IEEE.Form4("8");
            f.Text = "Masa 8";
            f.ShowDialog();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Form4 f = new IEEE.Form4("9");
            f.Text = "Masa 9";
            f.ShowDialog();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Form4 f = new IEEE.Form4("10");
            f.Text = "Masa 10";
            f.ShowDialog();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Form4 f = new IEEE.Form4("11");
            f.Text = "Masa 11";
            f.ShowDialog();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Form4 f = new IEEE.Form4("12");
            f.Text = "Masa 12";
            f.ShowDialog();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Form4 f = new IEEE.Form4("13");
            f.Text = "Masa 13";
            f.ShowDialog();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            Form4 f = new IEEE.Form4("14");
            f.Text = "Masa 14";
            f.ShowDialog();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            Form4 f = new IEEE.Form4("15");
            f.Text = "Masa 15";
            f.ShowDialog();
        }

        public void getOrder()
        {

            listView1.Items.Clear();

    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Restaurant_Database.mdf;Integrated Security=True");

    if (con.State == ConnectionState.Closed)
    {
        con.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Orders";

        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);

        while (dr.Read())
        {

            ListViewItem item = new ListViewItem(dr["table_no"].ToString());
            item.SubItems.Add(dr["item_no"].ToString());
            item.SubItems.Add(dr["item_name"].ToString());
            item.SubItems.Add(dr["quantity"].ToString());
            item.SubItems.Add(dr["item_price"].ToString() + "₺");
            listView1.Items.Add(item);

        }

        con.Close();

    }


}




    }
}
