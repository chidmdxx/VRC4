using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using VRC4.Model;

namespace VRC4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Cipher_Click(object sender, RoutedEventArgs e)
        {
            int j = int.Parse(jBox.Text);
            string c1Text = textBox.Text.Substring(0, j);
            string c2Text = textBox.Text.Substring(j);
            byte[] c1 = Encoding.ASCII.GetBytes(c1Text);
            byte[] c2 = Encoding.ASCII.GetBytes(c2Text);

            byte[] k1;
            byte[] k2;
            if (!(bool)randomKey.IsChecked)
            {
                string k1Text = keyBox.Text.Substring(0, j);
                string k2Text = keyBox.Text.Substring(j);
                k1 = Encoding.ASCII.GetBytes(k1Text);
                k2 = Encoding.ASCII.GetBytes(k2Text);
            }
            else
            {
                List<byte> allBytes = keyBox.Text.StringToByteArray().ToList();
                k1 = allBytes.Take(j).ToArray();
                k2 = allBytes.Skip(j).Take(allBytes.Count - j).ToArray();
            }
            

            string ciphertext;

            var rc4 = new RC4() { Key = k1 };
            var vigenere = new Vigenere() { Key = k1 };
            rc4.Cipher(c1);
            c1 = rc4.Ciphertext;
            vigenere.Cipher(c1);
            c1 = vigenere.Ciphertext;

            rc4.Key = k2;
            vigenere.Key = k2;
            rc4.Cipher(c2);
            c2 = rc4.Ciphertext;
            vigenere.Cipher(c2);
            c2 = vigenere.Ciphertext;


            ciphertext = c1.ByteArrayToStringValue() + c2.ByteArrayToStringValue() + ((byte)j).ByteArrayToStringValue();

            Work.Text = ciphertext;
        }

        private void Decipher_Click(object sender, RoutedEventArgs e)
        {
            int j = 0;
            List<byte> allBytes = textBox.Text.StringToByteArray().ToList();
            jBox.Text = "";
            j = allBytes[allBytes.Count - 1];
            allBytes.RemoveAt(allBytes.Count - 1);
            //string c1Text = textBox.Text.Substring(0, j);
            //string c2Text = textBox.Text.Substring(j);
            

            byte[] c1 = allBytes.Take(j).ToArray();
            byte[] c2 = allBytes.Skip(j).Take(allBytes.Count - j).ToArray();

            byte[] k1;
            byte[] k2;
            if (!(bool)randomKey.IsChecked)
            {
                string k1Text = keyBox.Text.Substring(0, j);
                string k2Text = keyBox.Text.Substring(j);
                k1 = Encoding.ASCII.GetBytes(k1Text);
                k2 = Encoding.ASCII.GetBytes(k2Text);
            }
            else
            {
                allBytes = keyBox.Text.StringToByteArray().ToList();
                k1 = allBytes.Take(j).ToArray();
                k2 = allBytes.Skip(j).Take(allBytes.Count - j).ToArray();
            }

            string ciphertext;

            var rc4 = new RC4() { Key = k1 };
            var vigenere = new Vigenere() { Key = k1 };
            vigenere.Decipher(c1);
            c1 = vigenere.Plaintext;
            rc4.Decipher(c1);
            c1 = rc4.Plaintext;


            rc4.Key = k2;
            vigenere.Key = k2;
            vigenere.Decipher(c2);
            c2 = vigenere.Plaintext;
            rc4.Decipher(c2);
            c2 = rc4.Plaintext;



            ciphertext = c1.ByteArrayToString() + c2.ByteArrayToString();

            Work.Text = ciphertext;
        }

        private void randomKey_Click(object sender, RoutedEventArgs e)
        {
            keyBox.IsEnabled = !(bool)randomKey.IsChecked;
            if ((bool)randomKey.IsChecked)
            {
                byte[] key = new byte[256];
                Random rand = new Random();
                rand.NextBytes(key);
                keyBox.Text = key.ByteArrayToStringValue();
            }
            else
            {
                keyBox.Text = "";
            }
        }
    }
}
