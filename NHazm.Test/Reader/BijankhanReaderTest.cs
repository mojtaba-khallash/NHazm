using edu.stanford.nlp.ling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NHazm.Test
{
    [TestClass]
    public class BijankhanReaderTest
    {
        [TestMethod]
        public void PosMapTest()
        {
            BijankhanReader reader = new BijankhanReader(false);

            List<TaggedWord> expected = new List<TaggedWord>();
            expected.Add(new TaggedWord("اولین", "ADJ"));
            expected.Add(new TaggedWord("سیاره", "N"));
            expected.Add(new TaggedWord("خارج", "ADJ"));
            expected.Add(new TaggedWord("از", "PREP"));
            expected.Add(new TaggedWord("منظومه", "N"));
            expected.Add(new TaggedWord("شمسی", "ADJ"));
            expected.Add(new TaggedWord("دیده", "ADJ"));
            expected.Add(new TaggedWord("شد", "V"));
            expected.Add(new TaggedWord(".", "PUNC"));
            var iter = reader.GetSentences().GetEnumerator();
            iter.MoveNext();
            var actual = iter.Current;

            Assert.AreEqual(expected.Count, actual.Count, "Failed to map pos of sentence");
            for (int i = 0; i < expected.Count; i++)
            {
                var actualTaggedWord = actual[i];
                var expectedTaggedWord = expected[i];
                if (!actualTaggedWord.tag().Equals(expectedTaggedWord.tag()))
                    Assert.AreEqual(expected[i], actual[i], "Failed to map pos of sentence");
            }
        }

        [TestMethod]
        public void JoinVerbPartsTest()
        {
            BijankhanReader reader = new BijankhanReader(null);

            List<TaggedWord> expected = new List<TaggedWord>();
            expected.Add(new TaggedWord("اولین", "ADJ_SUP"));
            expected.Add(new TaggedWord("سیاره", "N_SING"));
            expected.Add(new TaggedWord("خارج", "ADJ_SIM"));
            expected.Add(new TaggedWord("از", "P"));
            expected.Add(new TaggedWord("منظومه", "N_SING"));
            expected.Add(new TaggedWord("شمسی", "ADJ_SIM"));
            expected.Add(new TaggedWord("دیده شد", "V_PA"));
            expected.Add(new TaggedWord(".", "DELM"));
            var iter = reader.GetSentences().GetEnumerator();
            iter.MoveNext();
            var actual = iter.Current;
            
            Assert.AreEqual(expected.Count, actual.Count, "Failed to join verb parts of sentence");
            for (int i = 0; i < expected.Count; i++)
            {
                var actualTaggedWord = actual[i];
                var expectedTaggedWord = expected[i];
                if (!actualTaggedWord.tag().Equals(expectedTaggedWord.tag()))
                    Assert.AreEqual(expected[i], actual[i], "Failed to join verb parts of sentence");
            }
        }

        [TestMethod]
        public void PosMapJoinVerbPartsTest()
        {
            BijankhanReader reader = new BijankhanReader();

            List<TaggedWord> expected = new List<TaggedWord>();
            expected.Add(new TaggedWord("اولین", "ADJ"));
            expected.Add(new TaggedWord("سیاره", "N"));
            expected.Add(new TaggedWord("خارج", "ADJ"));
            expected.Add(new TaggedWord("از", "PREP"));
            expected.Add(new TaggedWord("منظومه", "N"));
            expected.Add(new TaggedWord("شمسی", "ADJ"));
            expected.Add(new TaggedWord("دیده شد", "V"));
            expected.Add(new TaggedWord(".", "PUNC"));
            var iter = reader.GetSentences().GetEnumerator();
            iter.MoveNext();
            var actual = iter.Current;

            Assert.AreEqual(expected.Count, actual.Count, "Failed to map pos and join verb parts of sentence");
            for (int i = 0; i < expected.Count; i++)
            {
                var actualTaggedWord = actual[i];
                var expectedTaggedWord = expected[i];
                if (!actualTaggedWord.tag().Equals(expectedTaggedWord.tag()))
                    Assert.AreEqual(expected[i], actual[i], "Failed to map pos and join verb parts of sentence");
            }
        }
    }
}