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

namespace Exercise2
{
    public partial class Form1 : Form
    {
        SHA1 sha;

        public Form1()
        {
            InitializeComponent();
            sha = new SHA1CryptoServiceProvider(); 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] data = Encoding.UTF8.GetBytes(tbInput1.Text);
            byte[] output = new byte[data.Length];

            sha.TransformBlock(data, 0, data.Length, output,0);
            

        }

        private void btnInput2_Click(object sender, EventArgs e)
        {
            byte[] data = Encoding.UTF8.GetBytes(tbInput2.Text);
            byte[] output = new byte[data.Length];

            sha.TransformBlock(data, 0, data.Length, output, 0);


        }

        private void btnFinal_Click(object sender, EventArgs e)
        {
            byte[] data = Encoding.UTF8.GetBytes(tbInputFinal.Text);
            byte[] output = new byte[data.Length];

            sha.TransformFinalBlock(data, 0, data.Length);

            tbHash.Text = BitConverter.ToString(sha.Hash);

        }
    }
}
