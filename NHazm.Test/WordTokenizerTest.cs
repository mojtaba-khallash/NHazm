using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NHazm.Test
{
    [TestClass]
    public class WordTokenizerTest
    {
        [TestMethod]
        public void TokenizeTest()
        {
            WordTokenizer wordTokenizer = new WordTokenizer(false);

            string input;
            string[] expected, actual;

            input = "این جمله (خیلی) پیچیده نیست!!!";
            expected = new string[] { "این", "جمله", "(", "خیلی", ")", "پیچیده", "نیست", "!!!"};
            actual = wordTokenizer.Tokenize(input).ToArray();
            Assert.AreEqual(expected.Length, actual.Length, "Failed to tokenize words of '" + input + "' sentence");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i], "Failed to tokenize words of '" + input + "' sentence");
            }
        }

        [TestMethod]
        public void JoinVerbPartsTest()
        {
            WordTokenizer wordTokenizer = new WordTokenizer(true);

            string input;
            string[] expected, actual;

            input = "خواهد رفت";
            expected = new string[] { "خواهد رفت" };
            actual = wordTokenizer.Tokenize(input).ToArray();
            Assert.AreEqual(expected.Length, actual.Length, "Failed to tokenize words of '" + input + "' sentence");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i], "Failed to tokenize words of '" + input + "' sentence");
            }

            input = "رفته است";
            expected = new string[] { "رفته است" };
            actual = wordTokenizer.Tokenize(input).ToArray();
            Assert.AreEqual(expected.Length, actual.Length, "Failed to tokenize words of '" + input + "' sentence");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i], "Failed to tokenize words of '" + input + "' sentence");
            }

            input = "گفته شده است";
            expected = new string[] { "گفته شده است" };
            actual = wordTokenizer.Tokenize(input).ToArray();
            Assert.AreEqual(expected.Length, actual.Length, "Failed to tokenize words of '" + input + "' sentence");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i], "Failed to tokenize words of '" + input + "' sentence");
            }

            input = "گفته خواهد شد";
            expected = new string[] { "گفته خواهد شد" };
            actual = wordTokenizer.Tokenize(input).ToArray();
            Assert.AreEqual(expected.Length, actual.Length, "Failed to tokenize words of '" + input + "' sentence");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i], "Failed to tokenize words of '" + input + "' sentence");
            }

            input = "خسته شدید";
            expected = new string[] { "خسته", "شدید" };
            actual = wordTokenizer.Tokenize(input).ToArray();
            Assert.AreEqual(expected.Length, actual.Length, "Failed to tokenize words of '" + input + "' sentence");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i], "Failed to tokenize words of '" + input + "' sentence");
            }
        }
    }
}
