using NHazm.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NHazm
{
    public class WordTokenizer
    {
        private bool _joinVerbParts = true;
        private HashSet<string> _beforeVerbs, _afterVerbs;
        public HashSet<string> BeforeVerbs { get { return _beforeVerbs; } }
        public HashSet<string> AfterVerbs { get { return _afterVerbs; } }

        private RegexPattern _pattern;

        List<string> _verbs;
        public List<string> Verbs { get { return _verbs; } }

        public WordTokenizer() : this(true) { }

        public WordTokenizer(bool joinVerbParts) : this("Data/verbs.dat", joinVerbParts) { }

        public WordTokenizer(string verbsFile) : this(verbsFile, true) { }

        public WordTokenizer(string verbsFile, bool joinVerbParts)
        {
            this._joinVerbParts = joinVerbParts;
            this._pattern = new RegexPattern(@"([؟!\?]+|[:\.،؛»\]\)\}""«\[\(\{])", " $1 ");

            if (this._joinVerbParts)
            {
                string[] tokens;

                tokens = new string[] {
                    "ام", "ای", "است", "ایم", "اید", "اند", 
                    "بودم", "بودی", "بود", "بودیم", "بودید", "بودند", 
                    "باشم", "باشی", "باشد", "باشیم", "باشید", "باشند",
                    "شده ام", "شده ای", "شده است", "شده ایم", "شده اید", "شده اند", 
                    "شده بودم", "شده بودی", "شده بود", "شده بودیم", "شده بودید", "شده بودند", 
                    "شده باشم", "شده باشی", "شده باشد", "شده باشیم", "شده باشید", "شده باشند",
                    "نشده ام", "نشده ای", "نشده است", "نشده ایم", "نشده اید", "نشده اند", 
                    "نشده بودم", "نشده بودی", "نشده بود", "نشده بودیم", "نشده بودید", "نشده بودند", 
                    "نشده باشم", "نشده باشی", "نشده باشد", "نشده باشیم", "نشده باشید", "نشده باشند",
                    "شوم", "شوی", "شود", "شویم", "شوید", "شوند", 
                    "شدم", "شدی", "شد", "شدیم", "شدید", "شدند",
                    "نشوم", "نشوی", "نشود", "نشویم", "نشوید", "نشوند", 
                    "نشدم", "نشدی", "نشد", "نشدیم", "نشدید", "نشدند",
                    "می‌شوم", "می‌شوی", "می‌شود", "می‌شویم", "می‌شوید", "می‌شوند", 
                    "می‌شدم", "می‌شدی", "می‌شد", "می‌شدیم", "می‌شدید", "می‌شدند",
                    "نمی‌شوم", "نمی‌شوی", "نمی‌شود", "نمی‌شویم", "نمی‌شوید", "نمی‌شوند", 
                    "نمی‌شدم", "نمی‌شدی", "نمی‌شد", "نمی‌شدیم", "نمی‌شدید", "نمی‌شدند",
                    "خواهم شد", "خواهی شد", "خواهد شد", "خواهیم شد", "خواهید شد", "خواهند شد",
                    "نخواهم شد", "نخواهی شد", "نخواهد شد", "نخواهیم شد", "نخواهید شد", "نخواهند شد"
                };

                this._afterVerbs = new HashSet<string>(new List<string>(tokens));

                tokens = new string[] {
                    "خواهم", "خواهی", "خواهد", "خواهیم", "خواهید", "خواهند",
                    "نخواهم", "نخواهی", "نخواهد", "نخواهیم", "نخواهید", "نخواهند"
                };

                this._beforeVerbs = new HashSet<string>(new List<string>(tokens));

                this._verbs = new List<string>(File.ReadAllLines(verbsFile).Reverse());
                for (int i = 0; i < this._verbs.Count; i++)
                {
                    var verb = this._verbs[i];
                    this._verbs[i] = verb.Trim().Split('#')[0] + "ه";
                }
            }
        }

        public List<string> Tokenize(string sentence)
        {
            sentence = this._pattern.Apply(sentence).Trim();
            List<String> tokens = new List<string>(sentence.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            if (this._joinVerbParts)
                tokens = this.JoinVerbParts(tokens);
            return tokens;
        }

        private List<string> JoinVerbParts(List<string> tokens)
        {
            tokens.Reverse();
            List<string> newTokens = new List<string>();

            foreach (var token in tokens)
            {
                if (newTokens.Count > 0)
                {
                    string lastWord = newTokens[newTokens.Count - 1];
                    if (this._beforeVerbs.Contains(token) ||
                        (this._afterVerbs.Contains(lastWord) && this._verbs.Contains(token)))
                    {
                        lastWord = token + " " + lastWord;
                        newTokens[newTokens.Count - 1] = lastWord;
                    }
                    else
                        newTokens.Add(token);
                }
                else
                    newTokens.Add(token);
            }

            newTokens.Reverse();
            return newTokens;
        }
    }
}