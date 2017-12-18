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
    public partial class Form2 : Form 
    {
       
        protected String answer = "İzmir";
      
        public Form2()
        {
            InitializeComponent();
            
            richTextBox1.Text = "Şifreyi değiştirmek için önceden belirlenmiş güvenlik sorusuna cevap verin. " +
            "Eğer doğru cevabı hatırlamıyorsanız, bizimle iletişime geçin.";
            richTextBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.Text == "Doğrula")
            {
                if (textBox1.Text.ToUpper().Equals(answer.ToUpper()))
                {
                    var result = MessageBox.Show("Şifreyi değiştirmek için OK butonuna basın.", "Başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    textBox1.Text = null;

                    if (result == DialogResult.OK)
                    {

                        label1.Text = "Yeni şifreyi girin : ";
                        button1.Text = "Kaydet";
                        richTextBox1.Text = null;

                    }
                }

                else if (String.IsNullOrWhiteSpace(textBox1.Text))
                {
                    var result = MessageBox.Show("Lütfen soruyu cevaplayınız.", "Cevap giriniz.", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    textBox1.Text = null;

                    if (result == DialogResult.Cancel)
                    {
                        this.Close();
                    }
                }

                else
                {

                    var result = MessageBox.Show("Lütfen doğru cevabı giriniz.", "Yanlış cevap!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop);
                    textBox1.Text = null;

                    if (result == DialogResult.Cancel)
                    {
                        this.Close();
                    }
                }

            }

            else
            {
                
                String p = textBox1.Text;


                if (String.IsNullOrWhiteSpace(p))
                {
                    var result = MessageBox.Show("Lütfen şire giriniz.", "Tekrar dene?", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);

                    if(result == DialogResult.Cancel)
                    {
                        this.Close();
                    }

                }

                else
                {

                    StreamWriter sw = new StreamWriter(@"p.txt");
                    sw.WriteLine(p);
                    sw.Close();

                    var result = MessageBox.Show("Şifre başarıyla değiştirildi.", "Başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    textBox1.Text = null;

                    if (result == DialogResult.OK)
                    {
                        this.Close();
                    }

                }
                
              

            }


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(null, null);
            }
        }
    }
}
