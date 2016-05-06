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

namespace Exercise1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMD5_Click(object sender, EventArgs e)
        {
            using(MD5 md5 = MD5.Create())
            {
                byte[] data = Encoding.UTF8.GetBytes(tbData.Text);
                byte[] hash = md5.ComputeHash(data);
                tbHash.Text = BitConverter.ToString(hash);
                lblBits.Text = (hash.Length*8).ToString();
            }
        }

        private void btnSHA1_Click(object sender, EventArgs e)
        {
            using (SHA1 sha = SHA1.Create())
            {
                byte[] data = Encoding.UTF8.GetBytes(tbData.Text);
                byte[] hash = sha.ComputeHash(data);
                tbHash.Text = BitConverter.ToString(hash);
                lblBits.Text = (hash.Length * 8).ToString();
            }
        }

        private void btnSHA512_Click(object sender, EventArgs e)
        {
            using (SHA512 sha = SHA512.Create())
            {
                byte[] data = Encoding.UTF8.GetBytes(tbData.Text);
                byte[] hash = sha.ComputeHash(data);
                tbHash.Text = BitConverter.ToString(hash);
                lblBits.Text = (hash.Length * 8).ToString();
            }
        }
    }
}
