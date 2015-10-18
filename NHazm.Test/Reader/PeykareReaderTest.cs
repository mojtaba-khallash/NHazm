using edu.stanford.nlp.ling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHazm.Utility;
using System.Collections.Generic;

namespace NHazm.Test
{
    [TestClass]
    public class PeykareReaderTest
    {
        [TestMethod]
        public void CoarsePOSTest()
        {
            List<string> input = new List<string>() { "N", "COM", "SING" };
            string expected = "N";
            var actual = PeykareReader.CoarsePOS(input);
            Assert.AreEqual(expected, actual, "Failed to find coarse pos.");
        }

        [TestMethod]
        public void JoinVerbPartsTest()
        {
            List<TaggedWord> input = new List<TaggedWord>()
            {
                new TaggedWord("اولین", "AJ"),
                new TaggedWord("سیاره", "Ne"),
                new TaggedWord("", "AJ"),
                new TaggedWord("از", "P"),
                new TaggedWord("منظومه", "Ne"),
                new TaggedWord("شمسی", "AJ"),
                new TaggedWord("دیده", "AJ"),
                new TaggedWord("شد", "V"),
                new TaggedWord(".", "PUNC")
            };

            List<TaggedWord> expected = new List<TaggedWord>()
            {
                new TaggedWord("اولین", "AJ"),
                new TaggedWord("سیاره", "Ne"),
                new TaggedWord("خارج", "AJ"),
                new TaggedWord("از", "P"),
                new TaggedWord("منظومه", "Ne"),
                new TaggedWord("شمسی", "AJ"),
                new TaggedWord("دیده شد", "V"),
                new TaggedWord(".", "PUNC")
            };
            var actual = PeykareReader.JoinVerbParts(input);

            Assert.AreEqual(expected.Count, actual.Count, "Failed to join verb parts of sentence");
            for (int i = 0; i < expected.Count; i++)
            {
                var actualTaggedWord = actual[i];
                var expectedTaggedWord = expected[i];
                if (!actualTaggedWord.tag().Equals(expectedTaggedWord.tag()))
                    Assert.AreEqual(expected[i], actual[i], "Failed to join verb parts of sentence");
            }
        }
    }
}
