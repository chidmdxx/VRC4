using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            int j=int.Parse(jBox.Text);
            string c1 = textBox.Text.Substring(0, j + 1);
            string c2 = textBox.Text.Substring(j + 1);
            string k1 = keyBox.Text.Substring(0, j + 1);
            string k2 = keyBox.Text.Substring(j + 1);

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

            ciphertext = c1 + c2 + j;
            Work.Text = ciphertext;
        }

        private void Decipher_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
