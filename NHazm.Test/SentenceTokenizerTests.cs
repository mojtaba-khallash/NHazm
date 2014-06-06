using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NHazm.Test
{
    [TestClass]
    public class SentenceTokenizerTests
    {
        [TestMethod]
        public void TokenizeTest()
        {
            SentenceTokenizer senTokenizer = new SentenceTokenizer();

            string input;
            string[] expected, actual;

            input = "جدا کردن ساده است. تقریبا البته!";
            expected = new string[] { "جدا کردن ساده است.", "تقریبا البته!" };
            actual = senTokenizer.Tokenize(input).ToArray();
            Assert.AreEqual(expected.Length, actual.Length, "Failed to tokenize sentences of '" + input + "' passage");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i], "Failed to tokenize sentences of '" + input + "' passage");
            }
        }
    }
}