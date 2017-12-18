using System;
using System.Collections;
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
    public partial class Form4 : Form
    {
        Manage a = new Manage();
        String z = "";
        

        public Form4()
        {
            InitializeComponent();
            
            getMenu();
            listView1.Columns.Add("Ürün Kodu");
            listView1.Columns.Add("Ürün Adı");
            listView1.Columns.Add("Ürün Fiyatı");
            listView1.AutoResizeColumns(System.Windows.Forms.ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(System.Windows.Forms.ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.FullRowSelect = false;

            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            label1.Visible = false;
        }

        public Form4(String table_no)
        {
            InitializeComponent(); 

            getOrder(table_no);

            label1.Text += a.Hesap(table_no) + "₺";

            listView1.Columns.Add("Masa No");
            listView1.Columns.Add("Ürün Kodu");
            listView1.Columns.Add("Ürün Adı");
            listView1.Columns.Add("Ürün Adedi");
            listView1.Columns.Add("Ürün Fiyatı");
            listView1.Columns.Add("Sipariş Numarası");
            listView1.AutoResizeColumns(System.Windows.Forms.ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(System.Windows.Forms.ColumnHeaderAutoResizeStyle.HeaderSize);
            

        }

        private void button1_Click(object sender, EventArgs e)
        {

            String x = this.Text;
            String[] y = x.Split(' ');
            z = y[1];

            a.HesapÖde(z);


            MessageBox.Show("Ödeme başarılı!");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5(this.Text);
            f.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sil();
        }

        public void getOrder(String table_no)
        {
            listView1.Items.Clear();

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Restaurant_Database.mdf;Integrated Security=True");

            if (con.State == ConnectionState.Closed)
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM Orders WHERE table_no =" + table_no +"ORDER BY item_no";

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    ListViewItem item = new ListViewItem(dr["table_no"].ToString());
                    item.SubItems.Add(dr["item_no"].ToString());
                    item.SubItems.Add(dr["item_name"].ToString());
                    item.SubItems.Add(dr["quantity"].ToString());
                    item.SubItems.Add(dr["item_price"].ToString() + "₺");
                    item.SubItems.Add(dr["order_no"].ToString());
                    listView1.Items.Add(item);

                }

                con.Close();
                
            }

        }

        public void getMenu()
        {

            listView1.Items.Clear();

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Restaurant_Database.mdf;Integrated Security=True");

            if (con.State == ConnectionState.Closed)
            {
                con.Open();


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM Menu";

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);


                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["item_no"].ToString());
                    item.SubItems.Add(dr["item_name"].ToString());
                    item.SubItems.Add(dr["item_price"].ToString() + "₺");

                    listView1.Items.Add(item);
                }

                con.Close();
            }


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = this.listView1.SelectedIndices[0];

                String n = listView1.Items[index].SubItems[1].Text;
                String q = listView1.Items[index].SubItems[3].Text;

                Form5 f = new Form5(n, q);
                f.label3.Text = o_num().ToString();
                f.ShowDialog();
            }

            catch
            {

            }
           


        }

        public void Sil()

        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Restaurant_Database.mdf;Integrated Security=True");

            try
            {

                int index = this.listView1.SelectedIndices[0];
                String n = listView1.Items[index].SubItems[5].Text;
                int d = int.Parse(n);


                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "DELETE FROM Orders WHERE order_no = @p";

                    cmd.Parameters.AddWithValue(@"p", d);

                    cmd.ExecuteNonQuery();

                    con.Close();

                }


                MessageBox.Show("Kayıt silindi.");
            }

            catch (Exception e) {

                MessageBox.Show("Lütfen bir ürün seçiniz.");
            }
        }

        public int o_num()
        {
            int index = this.listView1.SelectedIndices[0];
            String n = listView1.Items[index].SubItems[5].Text;
            int d = int.Parse(n);

            return d;
        }

        private void timer1_Tick(object sender, EventArgs e)
        { 
            if(this.Text == "Menu")
            {
                getMenu();
            }
         
            else
            {

                String x = this.Text;
                String[] y = x.Split(' ');
                z = y[1];

                getOrder(z);
                label1.Text = "Hesap : " + a.Hesap(z) + "₺";

             
            }
        }

       

       
    }


}

