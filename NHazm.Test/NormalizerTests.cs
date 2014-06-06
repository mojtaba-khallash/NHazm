using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NHazm.Test
{
    [TestClass]
    public class NormalizerTests
    {
        [TestMethod]
        public void CharacterRefinementTest()
        {
            Normalizer normalizer = new Normalizer(true, false, false);

            string input, expected, actual;

            input = "اصلاح كاف و ياي عربي";
            expected = "اصلاح کاف و یای عربی";
            actual = normalizer.Run(input);
            Assert.AreEqual(expected, actual, "Failed to character refinement of '" + input + "'");

            input = "رمــــان";
            expected = "رمان";
            actual = normalizer.Run(input);
            Assert.AreEqual(expected, actual, "Failed to character refinement of '" + input + "'");

            input = "1,2,3,...";
            expected = "۱,۲,۳, …";
            actual = normalizer.Run(input);
            Assert.AreEqual(expected, actual, "Failed to character refinement of '" + input + "'");
        }

        [TestMethod]
        public void PunctuationSpacing()
        {
            Normalizer normalizer = new Normalizer(false, true, false);

            string input, expected, actual;

            input = "اصلاح ( پرانتزها ) در متن .";
            expected = "اصلاح (پرانتزها) در متن.";
            actual = normalizer.Run(input);
            Assert.AreEqual(expected, actual, "Failed to punctuation spacing of '" + input + "'");
        }

        [TestMethod]
        public void AffixSpacing()
        {
            Normalizer normalizer = new Normalizer(false, false, true);

            string input, expected, actual;

            input = "خانه ی پدری";
            expected = "خانه‌ی پدری";
            actual = normalizer.Run(input);
            Assert.AreEqual(expected, actual, "Failed to affix spacing of '" + input + "'");

            input = "فاصله میان پیشوند ها و پسوند ها را اصلاح می کند.";
            expected = "فاصله میان پیشوند‌ها و پسوند‌ها را اصلاح می‌کند.";
            actual = normalizer.Run(input);
            Assert.AreEqual(expected, actual, "Failed to affix spacing of '" + input + "'");

            input = "می روم";
            expected = "می‌روم";
            actual = normalizer.Run(input);
            Assert.AreEqual(expected, actual, "Failed to affix spacing of '" + input + "'");

            input = "حرفه ای";
            expected = "حرفه‌ای";
            actual = normalizer.Run(input);
            Assert.AreEqual(expected, actual, "Failed to affix spacing of '" + input + "'");
        }
    }
}
