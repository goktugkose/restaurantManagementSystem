using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IEEE
{
    class Manage
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Restaurant_Database.mdf;Integrated Security=True");
        public int check = 0;
        int item_number = 0;
        String name;


        public void getMenu()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Menu ORDER BY item_no";
                cmd.Connection = con;

                cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        public void Order(String table_no)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM Orders WHERE table_no = " + table_no;

                cmd.ExecuteNonQuery();

                con.Close();

            }



        }

        public void HesapÖde(String table_no)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM Orders WHERE table_no = " + table_no;

                cmd.ExecuteNonQuery();

                con.Close();


            }
        }

        public float Hesap(String table_no)
        {
            float check = 0;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT (quantity * item_price) FROM Orders WHERE table_no =" + table_no;

                SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                while (rd.Read())
                {
                    check += (float)rd.GetDouble(0);
                }


            }
            con.Close();
            return check;
        }

        public bool isEmpty(String table_no)
        {

            String a = "";
            bool flag = true;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM Orders WHERE table_no = " + table_no;

                cmd.ExecuteNonQuery();

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);

                while (dr.Read())
                {
                    a = dr["table_no"].ToString();

                }

                dr.Close();

                if (String.IsNullOrEmpty(a))
                {
                    flag = false;
                }

                con.Close();
            }
            return flag;
        }

        public float Fiyat()
        {      
            float price = 0;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT t1.item_price FROM Menu t1 WHERE t1.item_no = " + item_number;

                cmd.ExecuteNonQuery();

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    price = float.Parse(rd["item_price"].ToString());
                }

                con.Close();

            }

            return price;
        }

        public String setName()
        {


            if (con.State == ConnectionState.Closed)
            {

                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT t.item_name FROM Menu t WHERE t.item_no = " + item_number;

                cmd.ExecuteNonQuery();

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    name = rd["item_name"].ToString();
                }

                con.Close();

            }
            return name;

        }

        public int getCount()
        {
            Random r = new Random();
            int count = r.Next(1000);

            return count;

        }

        public void setNo(int n)
        {
            item_number = n;
        }

        public void All()
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM Orders";

                cmd.ExecuteNonQuery();

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "Orders");




            }
        }

        public void Delete()
        {

            int n = 0;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM Orders WHERE quantity = " + n;

                cmd.ExecuteNonQuery();

                con.Close();

            }

        }

        public void Connect()
        {

            System.Net.Sockets.Socket soket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

            soket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 1234));

            soket.Listen(0);

            while (true)
            {
                new System.Threading.Thread(delegate (object obj)
                {
                    System.Net.Sockets.Socket gelenSoket = (System.Net.Sockets.Socket)obj;

                }).Start(soket.Accept());

            }







        }

    }
}