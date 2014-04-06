using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRC4.Model
{
    public class RC4
    {

        public byte[] Key { get; set; }
        public byte[] Plaintext        { get; set; }
        public byte[] Ciphertext { get; set; }
        public string Work { get; set; }
        public RC4()
        {

        }

        public void Cipher(byte[] plaintext, int Slength = 256)
        {
            Plaintext = plaintext;
            Ciphertext = new byte[plaintext.Length];
            Work = string.Empty;

            byte[] S = new byte[Slength];
            byte[] T = new byte[Slength];

            /*Initialization*/
            Initialization(ref S, ref T, Slength);
            /*Initial permutation of S*/
            InitialPermutation(ref S, ref T, Slength);
            /*Stream Generation*/
            StreamGeneration(Ciphertext, ref S, Plaintext, Slength);
        }
        public void Decipher(byte[] ciphertext, int Slength = 256)
        {
            Ciphertext = ciphertext;
            Plaintext = new byte[ciphertext.Length];
            Work = string.Empty;

            byte[] S = new byte[Slength];
            byte[] T = new byte[Slength];


            /*Initialization*/
            Initialization(ref S, ref T, Slength);
            /*Initial permutation of S*/
            InitialPermutation(ref S, ref T, Slength);
            /*Stream Generation*/
            StreamGeneration(Plaintext, ref S, Ciphertext, Slength);
        }



        public void Initialization(ref byte[] S, ref byte[] T, int Slength)
        {
            int keyLenght = Key.Length;
            for (var c = 0; c < Slength; c++)
            {
                S[c] = (byte)(c);
                T[c] = Key[c % keyLenght];
            }
        }

        public void InitialPermutation(ref byte[] S, ref byte[] T, int Slength)
        {
            int j = 0;
            for (var c = 0; c < Slength; c++)
            {
                j = (j + S[c] + T[c]) % Slength;
                byte temp = S[c];//swap (S[c], S[j]);
                S[c] = S[j];
                S[j] = temp;

            }
        }

        public void StreamGeneration(byte[] resultBytes, ref byte[] S, byte[] workBytes, int Slength)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            foreach (var Mi in workBytes)
            {
                i = (i + 1) % Slength;
                j = (j + S[i]) % Slength;
                byte temp = S[i];//swap (S[i], S[j]);
                S[i] = S[j];
                S[j] = temp;
                int t = (S[i] + S[j]) % Slength;
                byte k = S[t];
                resultBytes[count++] = (byte)(Mi ^ k);
            }
        }
    }
}
