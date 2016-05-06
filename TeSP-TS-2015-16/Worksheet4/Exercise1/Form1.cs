using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise1
{
    public partial class Form1 : Form
    {
        RSACryptoServiceProvider rsa;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rsa = new RSACryptoServiceProvider();
            String publicKey = rsa.ToXmlString(false);
            String privatePublicKeys = rsa.ToXmlString(true);
            tbPublicKey.Text = publicKey;
            tbBothKeys.Text = privatePublicKeys;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            byte[] encodedData = Encoding.UTF8.GetBytes(tbSymmentricKey.Text);
            byte[] encryptedData = rsa.Encrypt(encodedData, false);
            tbSymmetricKeyEncrtypted.Text = Convert.ToBase64String(encryptedData);
            tbBitSize.Text = (encryptedData.Length * 8).ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            byte[] encryptedData = Convert.FromBase64String(tbSymmetricKeyEncrtypted.Text);
            byte[] encodedData = rsa.Decrypt(encryptedData, false);
            tbSymmetricKeyDecrypted.Text = Encoding.UTF8.GetString(encodedData);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            File.WriteAllText("publickey.txt", rsa.ToXmlString(false));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.WriteAllText("bothKeys.txt", rsa.ToXmlString(true));
        }

        
    }
}
