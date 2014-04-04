using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRC4.Model
{
    public class RC4
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

        public void Cipher(string plaintext, int Slength=256)
        {
            Plaintext = plaintext;
            Ciphertext = string.Empty;
            Work = string.Empty;
            byte[] plainBytes = Encoding.ASCII.GetBytes(Plaintext);

            byte[] S = new byte[Slength];
            byte[] T = new byte[Slength];
            byte[] resultBytes = new byte[plainBytes.Length];

            /*Initialization*/
            Initialization(ref S, ref T, Slength);
            /*Initial permutation of S*/
            InitialPermutation(ref S, ref T, Slength);
            /*Stream Generation*/
            StreamGeneration(ref resultBytes, ref S, plainBytes);
            foreach (var b in resultBytes)
            {
                Ciphertext += (char)b;
            }
        }
        public void Decipher(string ciphertext, int Slength = 256)
        {
            Ciphertext = ciphertext;
            Plaintext = string.Empty;
            Work = string.Empty;
            byte[] cipherBytes = Encoding.ASCII.GetBytes(Ciphertext);

            byte[] S = new byte[Slength];
            byte[] T = new byte[Slength];
            byte[] resultBytes = new byte[cipherBytes.Length];

            /*Initialization*/
            Initialization(ref S, ref T, Slength);
            /*Initial permutation of S*/
            InitialPermutation(ref S, ref T, Slength);
            /*Stream Generation*/
            StreamGeneration(ref resultBytes, ref S, cipherBytes);
            foreach (var b in resultBytes)
            {
                Ciphertext += (char)b;
            }
        }



        public void Initialization(ref byte[] S, ref byte[] T, int Slength)
        {
            byte[] K = Encoding.ASCII.GetBytes(Key);
            int keyLenght = K.Length;
            for (var c = 0; c < Slength; c++)
            {
                S[c] = (byte)(c);
                T[c] = K[c % keyLenght];
            }
        }

        public void InitialPermutation(ref byte[] S, ref byte[] T, int Slength)
        {
            int j = 0;
            for (var c = 0; c < Slength; c++)
            {
                j = (j + S[c] + T[c]) % 256;
                byte temp = S[c];//swap (S[c], S[j]);
                S[c] = S[j];
                S[j] = temp;

            }
        }

        public void StreamGeneration(ref byte[] resultBytes,ref byte[] S, byte[] workBytes)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            foreach (var Mi in workBytes)
            {
                i = (i + 1) % 256;
                j = (j + S[i]) % 256;
                byte temp = S[i];//swap (S[i], S[j]);
                S[i] = S[j];
                S[j] = temp;
                int t = (S[i] + S[j]) % 256;
                byte k = S[t];
                resultBytes[count++] = (byte)(Mi ^ k);
            }
        }
    }
}
