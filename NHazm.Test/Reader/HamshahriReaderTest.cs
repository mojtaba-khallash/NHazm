using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NHazm.Test
{
    [TestClass]
    public class HamshahriReaderTest
    {
        [TestMethod]
        public void GetDocumentsTest()
        {
            var hr = new HamshahriReader();
            var expected = "HAM2-750403-001";
            var iter = hr.GetDocuments().GetEnumerator();
            iter.MoveNext();
            var actual = iter.Current.ID;
            Assert.AreEqual(expected, actual, "Failed to read hamshahri corpus.");
        }
    }
}
