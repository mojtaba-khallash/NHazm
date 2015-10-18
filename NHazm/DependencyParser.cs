using edu.stanford.nlp.ling;
using java.io;
using org.maltparser.concurrent;
using org.maltparser.concurrent.graph;
using System.Collections.Generic;

namespace NHazm
{
    public class DependencyParser
    {
        #region SentenceTokenizer
        private SentenceTokenizer sentenceTokenizer;
        public SentenceTokenizer SentenceTokenizer
        {
            set
            {
                this.sentenceTokenizer = value;
            }
            get
            {
                if (sentenceTokenizer == null)
                    sentenceTokenizer = new SentenceTokenizer();
                return sentenceTokenizer;
            }

        }
        #endregion

        #region  WordTokenizer
        private WordTokenizer wordTokenizer;
        public WordTokenizer WordTokenizer
        {
            set
            {
                this.wordTokenizer = value;
            }
            get
            {
                if (wordTokenizer == null)
                    wordTokenizer = new WordTokenizer();
                return wordTokenizer;
            }

        }
        #endregion

        public Normalizer Normalizer { get; set; }
        public Lemmatizer Lemmatizer { get; set; }
        #region  Tagger
        public POSTagger tagger;
        public POSTagger Tagger
        {
            set
            {
                this.tagger = value;
            }
            get
            {
                if (tagger == null)
                    tagger = new POSTagger();
                return tagger;
            }

        }
        #endregion
        private string modelFile;
        private ConcurrentMaltParserModel model;
        private ConcurrentMaltParserModel Model
        {
            get
            {
                if (model == null)
                {
                    var maltModelURL = new File(this.modelFile).toURI().toURL();
                    this.model = ConcurrentMaltParserService.initializeParserModel(maltModelURL);
                }
                return model;
            }
        }

        public DependencyParser()
            : this(null, null, "Resources/langModel.mco")
        { }

        public DependencyParser(POSTagger tagger, Lemmatizer lemmatizer, string modelFile)
        {
            this.Tagger = tagger;
            this.Lemmatizer = lemmatizer;
            this.modelFile = modelFile;
        }

        /// <summary>
        /// Gets list of raw text
        /// </summary>
        public IEnumerable<ConcurrentDependencyGraph> RawParse(string text)
        {
            if (this.Normalizer != null)
                text = this.Normalizer.Run(text);
            return RawParse(SentenceTokenizer.Tokenize(text));
        }

        /// <summary>
        /// Gets list of raw sentences
        /// </summary>
        public IEnumerable<ConcurrentDependencyGraph> RawParse(List<string> sentences)
        {
            foreach (var sentence in sentences)
            {
                var words = WordTokenizer.Tokenize(sentence);
                yield return RawParse(Tagger.BatchTag(words));
            }
        }
        public ConcurrentDependencyGraph RawParse(List<TaggedWord> sentence)
        {
            string[] conll = new string[sentence.Count];
            for (int i = 0; i < sentence.Count; i++)
            {
                var taggedWord = sentence[i];
                var word = taggedWord.word();
                var Lemma = "_";
                if (this.Lemmatizer != null)
                    Lemma = this.Lemmatizer.Lemmatize(word);
                var pos = taggedWord.tag();

                conll[i] = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                        i + 1, word, Lemma, pos, pos, "_");
            }
            return Parse(conll);
        }

        public ConcurrentDependencyGraph Parse(string[] conllSentence)
        {
            return this.Model.parse(conllSentence);
        }
    }
}
