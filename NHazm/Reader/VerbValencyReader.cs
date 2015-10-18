using System.Collections.Generic;
using System.IO;

namespace NHazm
{
    /// <summary>
    /// interfaces [Verb Valency Corpus](http://dadegan.ir/catalog/pervallex)
    /// Mohammad Sadegh Rasooli, Amirsaeid Moloodi, Manouchehr Kouhestani, & Behrouz Minaei Bidgoli. (2011). A Syntactic Valency Lexicon for Persian Verbs: The First Steps towards Persian Dependency Treebank. in 5th Language & Technology Conference(LTC): Human Language Technologies as a Challenge for Computer Science and Linguistics(pp. 227–231). Poznań, Poland.
    /// </summary>
    public class VerbValencyReader
    {
        //
        // Fields
        //

        private string _valencyFile;



        //
        // Constructors
        //

        public VerbValencyReader()
            : this("Corpora/valency.txt")
        { }

        public VerbValencyReader(string valencyFile)
        {
            this._valencyFile = valencyFile;
        }




        // 
        // API
        //

        public IEnumerable<Verb> GetVerbs()
        {
            foreach (var text in File.ReadAllLines(this._valencyFile))
            {
                if (text.Contains("بن ماضی"))
                    continue;

                var line = text.Trim().Replace("-\t", "\t");
                var parts = line.Split('\t');
                if (parts.Length == 6)
                    yield return new Verb
                    {
                        PastLightVerb = parts[0],
                        PresentLightVerb = parts[1],
                        Prefix = parts[2],
                        NonVerbalElement = parts[3],
                        Preposition = parts[4],
                        Valency = parts[5]
                    };
            }
        }
    }

    public struct Verb
    {
        public string PastLightVerb;
        public string PresentLightVerb;
        public string Prefix;
        public string NonVerbalElement;
        public string Preposition;
        public string Valency;
    }
}
