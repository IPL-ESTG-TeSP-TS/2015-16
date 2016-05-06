using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Worksheet6
{
    public partial class Form1 : Form
    {
        private RSACryptoServiceProvider rsa;
        private RSACryptoServiceProvider rsaRecipient;
        private String publicKey;

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider())
            {
                byte[] signature = rsa.SignData(Encoding.UTF8.GetBytes(textBox1.Text), 
                                                sha256);

                textBox2.Text = "";
                textBox5.Text = Convert.ToBase64String(signature);
                textBox3.Text = "";
                textBox4.Text = (signature.Length * 8).ToString();
            }  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rsa = new RSACryptoServiceProvider();
            publicKey = rsa.ToXmlString(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(textBox1.Text));

                byte[] signature = rsa.SignHash(hash, CryptoConfig.MapNameToOID("SHA256"));

                textBox2.Text = Convert.ToBase64String(hash);
                textBox5.Text = Convert.ToBase64String(signature);
                textBox3.Text = (hash.Length * 8).ToString();
                textBox4.Text = (signature.Length * 8).ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            rsaRecipient = new RSACryptoServiceProvider();
            rsaRecipient.FromXmlString(publicKey);

            using (SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(textBox1.Text));
                bool verified = rsaRecipient.VerifyHash(hash,
                                        CryptoConfig.MapNameToOID("SHA256"),
                                        Convert.FromBase64String(textBox5.Text));
                textBox6.Text = verified.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rsaRecipient = new RSACryptoServiceProvider();
            rsaRecipient.FromXmlString(publicKey);

            using (SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider())
            {

                bool verified = rsaRecipient.VerifyData(Encoding.UTF8.GetBytes(textBox1.Text),
                                                        sha256,
                                                        Convert.FromBase64String(textBox5.Text));
                textBox6.Text = verified.ToString();
            }
        }
    }
}
