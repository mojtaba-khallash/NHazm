using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace NHazm.Test
{
    [TestClass]
    public class LemmatizerTest
    {
        [TestMethod]
        public void LemmatizeTest()
        {
            Lemmatizer lemmatizer = new Lemmatizer();

            string input, expected, actual;

            input = "کتاب‌ها";
            expected = "کتاب";
            actual = lemmatizer.Lemmatize(input);
            Assert.AreEqual(expected, actual, "Failed to lematize of '" + input + "' word");

            input = "آتشفشان";
            expected = "آتشفشان";
            actual = lemmatizer.Lemmatize(input);
            Assert.AreEqual(expected, actual, "Failed to lematize of '" + input + "' word");

            input = "می‌روم";
            expected = "رفت#رو";
            actual = lemmatizer.Lemmatize(input);
            Assert.AreEqual(expected, actual, "Failed to lematize of '" + input + "' word");

            input = "گفته شده است";
            expected = "گفت#گو";
            actual = lemmatizer.Lemmatize(input);
            Assert.AreEqual(expected, actual, "Failed to lematize of '" + input + "' word");

            input = "مردم";
            expected = "مردم";
            actual = lemmatizer.Lemmatize(input, "N");
            Assert.AreEqual(expected, actual, "Failed to lematize of '" + input + "' word");
        }

        [TestMethod]
        public void ConjugationsTest()
        {
            Lemmatizer lemmatizer = new Lemmatizer();

            string input;
            string[] expected, actual;

            input = "خورد#خور";
            expected = new string[] { 
                "خوردم", "خوردی", "خورد", "خوردیم", "خوردید", "خوردند", 
                "نخوردم", "نخوردی", "نخورد", "نخوردیم", "نخوردید", "نخوردند", 
                "خورم", "خوری", /*"خورد",*/ "خوریم", "خورید", "خورند", 
                "نخورم", "نخوری", /*"نخورد",*/ "نخوریم", "نخورید", "نخورند", 
                "می‌خوردم", "می‌خوردی", /*"می‌خورد",*/ "می‌خوردیم", "می‌خوردید", "می‌خوردند", 
                "نمی‌خوردم", "نمی‌خوردی", "نمی‌خورد", "نمی‌خوردیم", "نمی‌خوردید", "نمی‌خوردند", 
                "خورده‌ام", "خورده‌ای", "خورده", "خورده‌ایم", "خورده‌اید", "خورده‌اند", 
                "نخورده‌ام", "نخورده‌ای", "نخورده", "نخورده‌ایم", "نخورده‌اید", "نخورده‌اند", 
                "می‌خورم", "می‌خوری", "می‌خورد", "می‌خوریم", "می‌خورید", "می‌خورند", 
                "نمی‌خورم", "نمی‌خوری", /*"نمی‌خورد",*/ "نمی‌خوریم", "نمی‌خورید", "نمی‌خورند", 
                "بخورم", "بخوری", "بخورد", "بخوریم", "بخورید", "بخورند", 
                "بخور", "نخور" 
            };
            actual = lemmatizer.Conjugations(input).ToArray();
            Assert.AreEqual(expected.Length, actual.Length, "Failed to generate conjugations of '" + input + "' verb");
            for (int i = 0; i < expected.Length; i++)
            {
                if (!actual.Contains(expected[i]))
                    Assert.AreEqual(expected[i], actual[i], "Failed to generate conjugations of '" + input + "' verb");
            }

            input = "آورد#آور";
            expected = new string[] { 
                "آوردم", "آوردی", "آورد", "آوردیم", "آوردید", "آوردند", 
                "نیاوردم", "نیاوردی", "نیاورد", "نیاوردیم", "نیاوردید", "نیاوردند", 
                "آورم", "آوری", /*"آورد",*/ "آوریم", "آورید", "آورند", 
                "نیاورم", "نیاوری", /*"نیاورد",*/ "نیاوریم", "نیاورید", "نیاورند", 
                "می‌آوردم", "می‌آوردی", /*"می‌آورد",*/ "می‌آوردیم", "می‌آوردید", "می‌آوردند", 
                "نمی‌آوردم", "نمی‌آوردی", "نمی‌آورد", "نمی‌آوردیم", "نمی‌آوردید", "نمی‌آوردند", 
                "آورده‌ام", "آورده‌ای", "آورده", "آورده‌ایم", "آورده‌اید", "آورده‌اند", 
                "نیاورده‌ام", "نیاورده‌ای", "نیاورده", "نیاورده‌ایم", "نیاورده‌اید", "نیاورده‌اند", 
                "می‌آورم", "می‌آوری", "می‌آورد", "می‌آوریم", "می‌آورید", "می‌آورند", 
                "نمی‌آورم", "نمی‌آوری", /*"نمی‌آورد",*/ "نمی‌آوریم", "نمی‌آورید", "نمی‌آورند", 
                "بیاورم", "بیاوری", "بیاورد", "بیاوریم", "بیاورید", "بیاورند", 
                "بیاور", "نیاور"
            };
            actual = lemmatizer.Conjugations(input).ToArray();
            Assert.AreEqual(expected.Length, actual.Length, "Failed to generate conjugations of '" + input + "' verb");
            for (int i = 0; i < expected.Length; i++)
            {
                if (!actual.Contains(expected[i]))
                    Assert.AreEqual(expected[i], actual[i], "Failed to generate conjugations of '" + input + "' verb");
            }
        }
    }
}