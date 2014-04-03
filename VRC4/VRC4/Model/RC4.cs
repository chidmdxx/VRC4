using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRC4.Model
{
    class RC4
    {
        private string key;
        private string plaintext;
        private string ciphertext;
        private string work;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        public string Plaintext
        {
            get { return plaintext; }
            set { plaintext = value; }
        }
        public string Ciphertext
        {
            get { return ciphertext; }
            set { ciphertext = value; }
        }
        public string Work
        {
            get { return work; }
            set { work = value; }
        }
        public RC4()
        {

        }

        public void Cipher(string plaintext)
        {
            Plaintext = plaintext;
            Ciphertext = string.Empty;
            Work = string.Empty;
            byte[] plainBytes = Encoding.ASCII.GetBytes(Plaintext);
            byte[] K = Encoding.ASCII.GetBytes(Key);
            int keyLenght = K.Length;
            byte[] S = new byte[256];
            byte[] T = new byte[256];
            byte[] cipherBytes = new byte[plainBytes.Length];
            int j, i;
            /*Initialization*/
            for (var c = 0; c < 256; c++)
            {
                S[c] = (byte)(c);
                T[c] = K[c % keyLenght];
            }
            /*Initial permutation of S*/
            j = 0;
            for (var c = 0; c < 256; c++)
            {
                j = (j + S[c] + T[c]) % 256;
                byte temp = S[c];//swap (S[c], S[j]);
                S[c] = S[j];
                S[j] = temp;

            }
            /*Stream Generation*/
            i = 0;
            j = 0;
            int count = 0;
            foreach (byte Mi in plainBytes)
            {
                i = (i + 1) % 256;
                j = (j + S[i]) % 256;
                byte temp = S[i];//swap (S[i], S[j]);
                S[i] = S[j];
                S[j] = temp;
                int t = (S[i] + S[j]) % 256;
                byte k = S[t];
                cipherBytes[count++] = (byte)(Mi ^ k);
            }
        }
    }
}
