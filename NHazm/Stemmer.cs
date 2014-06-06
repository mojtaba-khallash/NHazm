namespace NHazm
{
    public class Stemmer
    {
        private string[] _ends = new string[] {
            "ات", "ان", "ترین", "تر", "م", "ت", "ش", "یی", "ی", "ها", "ٔ", "‌ا", //
        };

        public string Stem(string word)
        {
            foreach (var end in this._ends)
            {
                if (word.EndsWith(end))
                    word = word.Substring(0, word.Length - end.Length).Trim('‌');
            }

            if (word.EndsWith("ۀ"))
                word = word.Substring(0, word.Length - 1) + "ه";

            return word;
        }
    }
}