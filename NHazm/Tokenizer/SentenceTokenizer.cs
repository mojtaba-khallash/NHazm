using NHazm.Utility;
using System;
using System.Collections.Generic;

namespace NHazm
{
    public class SentenceTokenizer
    {
        private RegexPattern _pattern;

        public SentenceTokenizer()
        {
            this._pattern = new RegexPattern(@"([!\.\?⸮؟]+)[ \n]+", @"$1\n\n");
        }

        public List<string> Tokenize(string text)
        {
            text = this._pattern.Apply(text);
            List<string> sentences = new List<string>(
                text.Split(new string[] { @"\n\n" }, StringSplitOptions.RemoveEmptyEntries));
            sentences.ForEach(sentence => sentence.Replace("\n", " ").Trim());
            return sentences;
        }
    }
}