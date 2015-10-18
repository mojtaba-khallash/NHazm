using edu.stanford.nlp.ling;
using System.Collections.Generic;

namespace NHazm
{
    public class PeykareReader
    {
        private static List<string> cpos = new List<string>()
        {
            "N",        // Noun
            "V",        // Verb
            "AJ",       // Adjective
            "ADV",      // Adverb
            "PRO",      // Pronoun
            "DET",      // Determiner
            "P",        // Preposition
            "POSTP",    // Postposition
            "NUM",      // Number
            "CONJ",     // Conjunction
            "PUNC",     // Punctuation
            "RES",      // Residual
            "CL",       // Classifier
            "INT"       // Interjection
        };
        private static WordTokenizer tokenizer = new WordTokenizer();

        /// <summary>
        /// Coarse POS tags of Peykare corpus:
        /// </summary>
        /// <param name=""></param>
        public static string CoarsePOS(List<string> tags)
        {
            try
            {
                var result = "N";
                foreach (var tag in tags)
                { 
                    if (cpos.Contains(tag))
                    {
                        result = tag;
                        break;
                    }
                }

                if (tags.Contains("EZ"))
                    result += "e";
                return result;
            }
            catch
            {
                return "N";
            }
        }

        /// <summary>
        /// Join verb parts like Dadedgan corpus.
        /// Input:
        ///     دیده/ADJ_INO
        ///     شد/V_PA
        /// Iutput:
        ///     دیده شد/V_PA
        /// </summary>
        /// <param name="sentence">List of TaggedWord object </param>
        /// <returns>List of TaggedWord</returns>
        public static List<TaggedWord> JoinVerbParts(List<TaggedWord> sentence)
        {
            sentence.Reverse();
            var result = new List<TaggedWord>();
            var beforeTaggedWord = new TaggedWord("", "");
            foreach (var taggedWord in sentence)
            {
                if (PeykareReader.tokenizer.BeforeVerbs.Contains(taggedWord.word()) ||
                    (PeykareReader.tokenizer.AfterVerbs.Contains(beforeTaggedWord.word()) &&
                     PeykareReader.tokenizer.Verbs.Contains(taggedWord.word())))
                {
                    beforeTaggedWord.setWord(taggedWord.word() + " " + beforeTaggedWord.word());
                    if (result.Count == 0)
                        result.Add(beforeTaggedWord);
                }
                else
                {
                    result.Add(taggedWord);
                    beforeTaggedWord = taggedWord;
                }
            }

            result.Reverse();
            return result;
        }
    }
}
