using System.Text;

namespace VRC4.Model
{
    class Vigenere
    {
        private byte[] key;

        public byte[] Key
        {
            get { return key; }
            set { key = value; }
        }
        private byte[] plaintext;

        public byte[] Plaintext
        {
            get { return plaintext; }
            set { plaintext = value; }
        }
        private byte[] ciphertext;

        public byte[] Ciphertext
        {
            get { return ciphertext; }
            set { ciphertext = value; }
        }

        private string work;

        public string Work
        {
            get { return work; }
            set { work = value; }
        }


        public Vigenere()
        {
        }

        public byte[] Cipher(byte[] plaintext)
        {
            Plaintext = plaintext;
            Ciphertext = new byte[plaintext.Length];
            Work = string.Empty;
            int keyLenght = key.Length;
            int count = 0;
            foreach (var letter in plaintext)
            {
                int ascii = letter;
                int k;
                k = key[count % keyLenght];
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
            plaintext = new byte[ciphertext.Length];
            Work = string.Empty;
            int keyLenght = key.Length;
            int count = 0;
            foreach (var letter in ciphertext)
            {
                int ascii = letter;
                int k;
                k = key[count % keyLenght];
                ascii += k;
                ascii += 256;
                ascii %= 256;

                plaintext[count++] = (byte)ascii;
                Work += string.Format("{0}. Replaced {1} for {2} with key letter {3} \n", count, letter.ToString(), ((byte)ascii).ToString(), k.ToString());

            }
            return plaintext;
        }
    }
}
