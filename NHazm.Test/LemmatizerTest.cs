using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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

            string input, expected, actual, p;

            List<string> inputs = new List<string>() {
                "کتاب‌ها", "آتشفشان", "می‌روم", "گفته شده است", "نچشیده است", "مردم", "اجتماعی"
            };
            List<string> expecteds = new List<string>() {
                "کتاب", "آتشفشان", "رفت#رو", "گفت#گو", "چشید#چش", "مردم", "اجتماعی"
            };
            List<string> pos = new List<string>() {
                null, null, null, null, null, "N", "AJ"
            };

            for (var i = 0; i < inputs.Count; i++)
            {
                input = inputs[i];
                expected = expecteds[i];
                p = pos[i]; 
                if (p == null)
                    actual = lemmatizer.Lemmatize(input);
                else
                    actual = lemmatizer.Lemmatize(input, p);
                Assert.AreEqual(expected, actual, "Failed to lematize of '" + input + "' word");
            }
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