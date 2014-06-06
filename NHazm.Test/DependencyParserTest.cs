using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NHazm.Test
{
    [TestClass]
    public class DependencyParserTest
    {
        [TestMethod]
        public void ParseTest()
        {
            DependencyParser parser = new DependencyParser();
            string[] input = new string[] { 
                "1	از	_	PREP	PREP	_	12	VPP	_\t_",
                "2	نصایح	_	N	IANM	_",
                "3	باارزش	_	ADJ	AJP	_",
                "4	مولای	_	N	ANM	_",
                "5	متقیان	_	N	ANM	_",
                "6	حضرت	_	N	ANM	_",
                "7	علی	_	N	ANM	_",
                "8	(	_	PUNC	PUNC	_",
                "9	علیه‌السلام	_	N	IANM	_",
                "10	)	_	PUNC	PUNC	_",
                "11	پند	_	N	IANM	_",
                "12	بگیرید	_	V	ACT	_",
                "13	.	_	PUNC	PUNC	_",
            };
            var expected = 
                "1	از	_	PREP	PREP	_	12	VPP	_	_\n" +
                "2	نصایح	_	N	IANM	_	1	POSDEP	_	_\n" +
                "3	باارزش	_	ADJ	AJP	_	2	MOZ	_	_\n" +
                "4	مولای	_	N	ANM	_	3	MOZ	_	_\n" +
                "5	متقیان	_	N	ANM	_	4	MOZ	_	_\n" +
                "6	حضرت	_	N	ANM	_	5	MOZ	_	_\n" +
                "7	علی	_	N	ANM	_	6	MOZ	_	_\n" +
                "8	(	_	PUNC	PUNC	_	7	MOZ	_	_\n" +
                "9	علیه‌السلام	_	N	IANM	_	8	POSDEP	_	_\n" +
                "10	)	_	PUNC	PUNC	_	9	PUNC	_	_\n" +
                "11	پند	_	N	IANM	_	7	APP	_	_\n" +
                "12	بگیرید	_	V	ACT	_	11	ROOT	_	_\n" +
                "13	.	_	PUNC	PUNC	_	12	PUNC	_	_\n" ;
            var graph = parser.Parse(input);
            var actual = graph.toString();

            Assert.AreEqual(expected, actual, "Failed to stem of '" + input + "'");



            parser.Normalizer = new Normalizer();
            parser.Lemmatizer = new Lemmatizer();


            string inputSentence = "من به مدرسه رفته بودم.";
            expected = "1	من	من	PR	PR	_	4	SBJ	_	_\n" +
                        "2	به	به	PREP	PREP	_	4	VPP	_	_\n" +
                        "3	مدرسه	مدرسه	N	N	_	2	POSDEP	_	_\n" +
                        "4	رفته بودم	رفت#رو	V	V	_	0	ROOT	_	_\n" +
                        "5	.	.	PUNC	PUNC	_	4	PUNC	_	_\n";
            var iterator = parser.RawParse(inputSentence).GetEnumerator();
            iterator.MoveNext();
            graph = iterator.Current;
            actual = graph.toString();


            Assert.AreEqual(expected, actual, "Failed to stem of '" + input + "'");
        }
    }
}