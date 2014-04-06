﻿using System.Text;

namespace VRC4.Model
{
    class Vigenere
    {
        public byte[] Key { get; set; }

        public byte[] Plaintext { get; set; }

        public byte[] Ciphertext { get; set; }


        public string Work { get; set; }

        public Vigenere()
        {
        }

        public byte[] Cipher(byte[] plaintext)
        {
            Plaintext = plaintext;
            Ciphertext = new byte[plaintext.Length];
            Work = string.Empty;
            int keyLenght = Key.Length;
            int count = 0;
            foreach (var letter in plaintext)
            {
                int ascii = letter;
                int k;
                k = Key[count % keyLenght];
                ascii += k;
                ascii %= 256;

                Ciphertext[count++] = (byte)ascii;
                Work += string.Format("{0}. Replaced {1} for {2} with key letter {3} \n", count, letter.ToString(), ((byte)ascii).ToString(), k.ToString());

            }
            return Ciphertext;
        }

        public byte[] Decipher(byte[] ciphertext)
        {
            Ciphertext = ciphertext;
            Plaintext = new byte[ciphertext.Length];
            Work = string.Empty;
            int keyLenght = Key.Length;
            int count = 0;
            foreach (var letter in ciphertext)
            {
                int ascii = letter;
                int k;
                k = Key[count % keyLenght];
                ascii -= k;
                ascii += 256;
                ascii %= 256;

                Plaintext[count++] = (byte)ascii;
                Work += string.Format("{0}. Replaced {1} for {2} with key letter {3} \n", count, letter.ToString(), ((byte)ascii).ToString(), k.ToString());

            }
            return Plaintext;
        }
    }
}
