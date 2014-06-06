using edu.stanford.nlp.ling;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NHazm
{
    // interfaces Bijankhan Corpus (http://ece.ut.ac.ir/dbrg/bijankhan/Corpus/BijanKhan_Corpus_Processed.zip) that 
    // you must download and extract it.
    public partial class BijankhanReader
    {
        private readonly string[] punctuation = new string[] { "#", "*", ".", "؟", "!" };

        private string _bijankhanFile;
        private bool _joinedVerbParts;
        private string _posMap;

        private Normalizer normalizer;
        private WordTokenizer tokenizer;

        public BijankhanReader()
            : this("resources/bijankhan.txt", true, "data/posMaps.dat")
        { }

        public BijankhanReader(bool joinedVerbParts) : this("resources/bijankhan.txt", joinedVerbParts, "data/posMaps.dat")
        {}

        public BijankhanReader(string posMap)
            : this("resources/bijankhan.txt", true, posMap)
        { }

        public BijankhanReader(bool joinedVerbParts, string posMap)
            : this("resources/bijankhan.txt", joinedVerbParts, posMap)
        { }

        public BijankhanReader(string bijankhanFile, bool joinedVerbParts, string posMap)
        {
            this._bijankhanFile = bijankhanFile;
            this._joinedVerbParts = joinedVerbParts;
            this._posMap = posMap;
            this.normalizer = new Normalizer(true, false, true);
            this.tokenizer = new WordTokenizer();
        }

        public IEnumerable<List<TaggedWord>> GetSentences()
        {
            var mapper = GetPosMap();
            var sentence = new List<TaggedWord>();
            foreach (var line in File.ReadAllLines(this._bijankhanFile))
            {
                var parts = Regex.Split(line.Trim(), "  +");
                if (parts.Length == 2)
                {
                    var word = parts[0];
                    var tag = parts[1];
                    if (!(word.Equals("#") || word.Equals("*")))
                    {
                        word = this.normalizer.Run(word);
                        if (word.Length == 0)
                            word = "_";
                        sentence.Add(new TaggedWord(word, tag));
                    }
                    if (tag.Equals("DELM") && punctuation.Contains(word))
                    {
                        if (sentence.Count > 0)
                        {
                            if (this._joinedVerbParts)
                                sentence = this.JoinVerbParts(sentence);

                            if (mapper != null)
                                sentence.ForEach(x => { 
                                    x.setTag(mapper[x.tag()].ToString()); 
                                });

                            yield return sentence;
                            sentence = new List<TaggedWord>();
                        }
                    }
                }
            }
        }

        private List<TaggedWord> JoinVerbParts(List<TaggedWord> sentence)
        {
            sentence.Reverse();
            var result = new List<TaggedWord>();
            var beforeTaggedWord = new TaggedWord("", "");
            foreach (var taggedWord in sentence)
            {
                if (this.tokenizer.BeforeVerbs.Contains(taggedWord.word()) ||
                    (this.tokenizer.AfterVerbs.Contains(beforeTaggedWord.word()) &&
                     this.tokenizer.Verbs.Contains(taggedWord.word())))
                {
                    beforeTaggedWord.setWord(taggedWord.word() + " " + taggedWord.word());
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

        private Hashtable GetPosMap()
        {
            if (this._posMap != null)
            {
                var mapper = new Hashtable();
                foreach (var line in File.ReadAllLines(this._posMap))
                {
                    var parts = line.Split(',');
                    mapper.Add(parts[0], parts[1]);
                }

                return mapper;
            }
            else
                return null;
        }
    }
}