using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VRC4.Model;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string M = "HI";
            string key = "test";
            var test = new RC4() { Key=key};
            test.Cipher(M);

            Assert.AreEqual("0123456789ABCDEF", test.Plaintext, true);
        }
    }
}
