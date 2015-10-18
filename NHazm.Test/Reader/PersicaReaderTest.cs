using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHazm.Reader;

namespace NHazm.Test
{
    [TestClass]
    public class PersicaReaderTest
    {
        [TestMethod]
        public void GetDocsTest()
        {
            var pr = new PersicaReader();
            var expected = 843656;
            var iter = pr.GetDocs().GetEnumerator();
            iter.MoveNext();
            var actual = iter.Current.ID;
            Assert.AreEqual(expected, actual, "Failed to read persica corpus.");
        }
    }
}
