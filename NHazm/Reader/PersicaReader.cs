using System.Collections.Generic;
using System.IO;

namespace NHazm.Reader
{
    /// <summary>
    /// interfaces [Persica Corpus](https://sourceforge.net/projects/persica/)
    /// </summary>
    public class PersicaReader
    {
        //
        // Fields
        //

        private string _persicaFile;



        //
        // Constructors
        //

        public PersicaReader()
            : this("Corpora/persica.csv")
        { }

        public PersicaReader(string persicaFile)
        {
            this._persicaFile = persicaFile;
        }




        // 
        // API
        //

        public IEnumerable<Doc> GetDocs()
        {
            List<string> lines = new List<string>();
            foreach (var text in File.ReadAllLines(this._persicaFile))
            {
                var line = text.Trim();
                if (line.Length > 0)
                {
                    if (line.EndsWith(","))
                        lines.Add(line.TrimEnd(','));
                    else
                    {
                        lines.Add(line);
                        yield return new Doc()
                        {
                            ID = int.Parse(lines[0]),
                            Title = lines[1],
						    Text = lines[2],
						    Date = lines[3],
						    Time = lines[4],
						    Category = lines[5],
						    Category2 = lines[6]
					    };

                        lines = new List<string>();
                    }
                }
            }
        }

        public IEnumerable<string> GetTexts()
        {
            foreach (Doc doc in GetDocs())
                yield return doc.Text;
        }
    }


    public struct Doc
    {
        public int ID;
        public string Title;
		public string Text;
		public string Date;
		public string Time;
		public string Category;
		public string Category2;
    }
}
