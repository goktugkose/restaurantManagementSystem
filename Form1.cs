using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IEEE
{
    public partial class Form1 : Form
    {
        protected String pass;
        MessageBoxButtons a = MessageBoxButtons.RetryCancel;
        DialogResult r = DialogResult.Cancel;
       

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            StreamReader sr = new StreamReader(@"p.txt");
            pass = sr.ReadLine();
            sr.Close();

            if (textBox1.Text == pass)
            {
                MessageBox.Show("Hoşgeldiniz!", "Başarılı!", MessageBoxButtons.OK,MessageBoxIcon.None);
                Hide();
                Form3 f3 = new Form3();
                    f3.ShowDialog();
                Show();
                    textBox1.Text = null;
            }

            else if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                var result = MessageBox.Show("Lütfen geçerli bir şifre girinz!", "Tekrar Dene?", a, MessageBoxIcon.Warning);
                textBox1.Text = null;

                if (result == r)
                {
                    Close();
                }
            }

            else
            {        
                var result = MessageBox.Show("Yanlış şifre!","Tekrar Dene?",a,MessageBoxIcon.Error);
                textBox1.Text = null;

                if (result == r)
                {
                    Close();
                }
  
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
            Show();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           if(e.KeyChar == (char)Keys.Enter)
            {
                button2_Click(null, null);
            }
        }
    }
    }

