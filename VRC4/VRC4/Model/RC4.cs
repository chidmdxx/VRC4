
using System;
namespace VRC4.Model
{
    public class RC4
    {

        public byte[] Key { get; set; }
        public byte[] Plaintext { get; set; }
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

            Work += string.Format("input: {0}{1}", plaintext.ByteArrayToStringValue(), Environment.NewLine);

            byte[] S = new byte[Slength];
            byte[] T = new byte[Slength];

            /*Initialization*/
            Initialization(ref S, ref T, Slength);
            /*Initial permutation of S*/
            InitialPermutation(ref S, ref T, Slength);
            /*Stream Generation*/
            StreamGeneration(Ciphertext, ref S, Plaintext, Slength);

            Work += string.Format("output: {0}{1}", Ciphertext.ByteArrayToStringValue(), Environment.NewLine);
        }
        public void Decipher(byte[] ciphertext, int Slength = 256)
        {
            Ciphertext = ciphertext;
            Plaintext = new byte[ciphertext.Length];
            Work = string.Empty;

            Work += string.Format("input: {0}{1}", ciphertext.ByteArrayToStringValue(), Environment.NewLine);

            byte[] S = new byte[Slength];
            byte[] T = new byte[Slength];


            /*Initialization*/
            Initialization(ref S, ref T, Slength);
            /*Initial permutation of S*/
            InitialPermutation(ref S, ref T, Slength);
            /*Stream Generation*/
            StreamGeneration(Plaintext, ref S, Ciphertext, Slength);

            Work += string.Format("output: {0}{1}", Plaintext.ByteArrayToStringValue(), Environment.NewLine);
        }



        public void Initialization(ref byte[] S, ref byte[] T, int Slength)
        //hace la inicializacin de los valores s y t usando sus referencias
        {
            int keyLenght = Key.Length;
            for (var c = 0; c < Slength; c++)
            {
                S[c] = (byte)(c); //llena del 0 al 255
                T[c] = Key[c % keyLenght]; //llena con la llave
            }
            Work += string.Format("S: {0}{1}", S.ByteArrayToStringValue(true), Environment.NewLine);
            Work += string.Format("T: {0}{1}", T.ByteArrayToStringValue(true), Environment.NewLine);
        }

        public void InitialPermutation(ref byte[] S, ref byte[] T, int Slength)
        {
            int j = 0;
            for (var i = 0; i < Slength; i++)
            {
                j = (j + S[i] + T[i]) % Slength;
                byte temp = S[i];//swap (S[c], S[j]);
                S[i] = S[j];
                S[j] = temp;
                Work += string.Format("i= {0} \t S[{0}]= {1} \t T[{0}]= {2} \t j= {3}{4}", i, S[i].ByteArrayToStringValue(),
                    T[i].ByteArrayToStringValue(), j, Environment.NewLine);
                Work += string.Format("S: {0}{1}", S.ByteArrayToStringValue(true), Environment.NewLine);
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
                resultBytes[count++] = (byte)(Mi ^ k); //guarda el xor

                Work += string.Format("i= {0} \t S[{0}]= {1} \t j= {2} \t S[{2}]= {3} \t t= {4}{5}", i, S[i].ByteArrayToStringValue(), j,
                    S[j].ByteArrayToStringValue(), t, Environment.NewLine);
                Work += string.Format("Mi= {0} \t k= {1} \t xor= {2}{3}", Mi.ByteArrayToStringValue(), k, resultBytes[count - 1],
                    Environment.NewLine);
                Work += string.Format("S: {0}{1}", S.ByteArrayToStringValue(true), Environment.NewLine);
            }
        }
    }
}
