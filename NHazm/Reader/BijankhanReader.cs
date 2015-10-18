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
        //
        // Fields
        //

        #region Fields
        private readonly string[] punctuation = new string[] { "#", "*", ".", "؟", "!" };

        private string _bijankhanFile;
        private bool _joinedVerbParts;
        private string _posMap;

        public Normalizer Normalizer { get; set; }
        #endregion





        //
        // Constructors
        //

        #region Constructors
        public BijankhanReader()
            : this("Corpora/bijankhan.txt", true, "Data/posMaps.dat")
        { }

        public BijankhanReader(bool joinedVerbParts) : this("Corpora/bijankhan.txt", joinedVerbParts, "Data/posMaps.dat")
        {}

        public BijankhanReader(string posMap)
            : this("Corpora/bijankhan.txt", true, posMap)
        { }

        public BijankhanReader(bool joinedVerbParts, string posMap)
            : this("Corpora/bijankhan.txt", joinedVerbParts, posMap)
        { }

        public BijankhanReader(string bijankhanFile, bool joinedVerbParts, string posMap)
        {
            this._bijankhanFile = bijankhanFile;
            this._joinedVerbParts = joinedVerbParts;
            this._posMap = posMap;
            this.Normalizer = new Normalizer(true, false, true);
        }
        #endregion




        //
        // API
        //

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
                        word = this.Normalizer.Run(word);
                        if (word.Length == 0)
                            word = "_";
                        sentence.Add(new TaggedWord(word, tag));
                    }
                    if (tag.Equals("DELM") && punctuation.Contains(word))
                    {
                        if (sentence.Count > 0)
                        {
                            if (this._joinedVerbParts)
                                sentence = PeykareReader.JoinVerbParts(sentence);

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





        //
        // Helper Methods
        //

        #region GetPosMap()
        /// <summary>
        /// Read POS Map file and put in Hashtable
        ///   - POS Map File Structure:
        ///     ADJ_CMPR,ADJ
        ///     ADJ_INO,ADJ
        ///     ADJ_ORD, ADJ
        ///     ADJ_SIM,ADJ
        ///     ADJ_SUP, ADJ
        ///     ...
        /// </summary>
        /// <returns>Hashtable fo mapping POS tags</returns>
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
        #endregion
    }
}