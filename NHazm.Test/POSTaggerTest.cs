using edu.stanford.nlp.ling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NHazm.Test
{
    [TestClass]
    public class POSTaggerTest
    {
        [TestMethod]
        public void BatchTagTest()
        {
            POSTagger tagger = new POSTagger();

            string[] input = new string[] { "من", "به", "مدرسه", "رفته بودم", "."};
            List<TaggedWord> expected = new List<TaggedWord>();
            expected.Add(new TaggedWord("من","PR"));
            expected.Add(new TaggedWord("به","PREP"));
            expected.Add(new TaggedWord("مدرسه","N"));
            expected.Add(new TaggedWord("رفته بودم","V"));
            expected.Add(new TaggedWord(".","PUNC"));
            List<TaggedWord> actual = tagger.BatchTag(new List<string>(input));

            Assert.AreEqual(expected.Count, actual.Count, "Failed to tagged words of '" + string.Join(" ", input) + "' sentence");
            for (int i = 0; i < expected.Count; i++)
            {
                var actualTaggedWord = actual[i];
                var expectedTaggedWord = expected[i];
                if (!actualTaggedWord.tag().Equals(expectedTaggedWord.tag()))
                    Assert.AreEqual(expected[i], actual[i], "Failed to tagged words of '" + string.Join(" ", input) + "' sentence");
            }
        }
    }
}
