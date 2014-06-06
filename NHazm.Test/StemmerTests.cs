using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NHazm.Test
{
    [TestClass]
    public class StemmerTests
    {
        [TestMethod]
        public void StemTest()
        {
            Stemmer stemmer = new Stemmer();

            string input, expected, actual;

            input = "کتابی";
            expected = "کتاب";
            actual = stemmer.Stem(input);
            Assert.AreEqual(expected, actual, "Failed to stem of '" + input + "'");

            input = "کتاب‌ها";
            expected = "کتاب";
            actual = stemmer.Stem(input);
            Assert.AreEqual(expected, actual, "Failed to stem of '" + input + "'");

            input = "کتاب‌هایی";
            expected = "کتاب";
            actual = stemmer.Stem(input);
            Assert.AreEqual(expected, actual, "Failed to stem of '" + input + "'");

            input = "کتابهایشان";
            expected = "کتاب";
            actual = stemmer.Stem(input);
            Assert.AreEqual(expected, actual, "Failed to stem of '" + input + "'");

            input = "اندیشه‌اش";
            expected = "اندیشه";
            actual = stemmer.Stem(input);
            Assert.AreEqual(expected, actual, "Failed to stem of '" + input + "'");
        }
    }
}