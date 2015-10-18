using edu.stanford.nlp.ling;
using edu.stanford.nlp.tagger.maxent;
using java.util;
using System.Collections.Generic;

namespace NHazm
{
    public class POSTagger
    {
        private MaxentTagger _tagger;

        public POSTagger()
            : this("Resources/persian.tagger")
        { }

        public POSTagger(string pathToModel)
        {
            this._tagger = new MaxentTagger(pathToModel);
        }

        public List<List<TaggedWord>> BatchTag(List<List<string>> sentences)
        {
            var result = new List<List<TaggedWord>>();

            foreach (var sentence in sentences)
            {
                result.Add(BatchTag(sentence));
            }

            return result;
        }

        public List<TaggedWord> BatchTag(List<string> sentence)
        {
            string[] sen = new string[sentence.Count];
            for (int i = 0; i < sentence.Count; i++)
               sen[i] = sentence[i].Replace(" ", "_");
            List newSent = Sentence.toWordList(sen);
            ArrayList taggedSentence = this._tagger.tagSentence(newSent);

            var taggedSen = new List<TaggedWord>();
            for (int i = 0; i < taggedSentence.size(); i++)
            {
                TaggedWord tw = (TaggedWord)taggedSentence.get(i);
                tw.setWord(sentence[i]);
                taggedSen.Add(tw);
            }
            return taggedSen;
        }
    }
}
