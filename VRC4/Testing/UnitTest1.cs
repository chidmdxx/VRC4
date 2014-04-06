using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VRC4.Model;
using System.Text;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RC4cipher()
        {
            string M = "HI";
            byte[] key = { 1, 7, 1, 7 };
            var test = new RC4() { Key=key};
            test.Cipher(Encoding.ASCII.GetBytes(M),4);

            Assert.AreEqual("4B48", test.Ciphertext.ByteArrayToStringValue(), true);
        }

        [TestMethod]
        public void RC4decipher()
        {
            byte[] M = "4B48".StringToByteArray();
            byte[] key = { 1, 7, 1, 7 };
            var test = new RC4() { Key = key };
            test.Decipher(M, 4);

            Assert.AreEqual("4849", test.Plaintext.ByteArrayToStringValue(), true);
        }
    }
}
