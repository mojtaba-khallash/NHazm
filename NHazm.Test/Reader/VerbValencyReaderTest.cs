using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NHazm.Test
{
    [TestClass]
    public class VerbValencyReaderTest
    {
        [TestMethod]
        public void GetVerbsTest()
        {
            var vv = new VerbValencyReader();
            var expected = "بر";
            var iter = vv.GetVerbs().GetEnumerator();
            iter.MoveNext();
            iter.MoveNext();
            iter.MoveNext();
            iter.MoveNext();
            var actual = iter.Current.Prefix;
            Assert.AreEqual(expected, actual, "Failed to read verb valency corpus.");
        }
    }
}
