using NHazm.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace NHazm
{
    // interfaces Hamshahri Corpus (http://ece.ut.ac.ir/dbrg/hamshahri/files/HAM2/Corpus.zip) 
    // that you must download and extract it.
    public class HamshahriReader
    {
        private RegexPattern _paragraphPattern;

        private string _rootFolder;
        private string[] invalidFiles = new string[] {
		    "hamshahri.dtd", "HAM2-960622.xml", "HAM2-960630.xml", "HAM2-960701.xml", "HAM2-960709.xml", 
            "HAM2-960710.xml", "HAM2-960711.xml", "HAM2-960817.xml", "HAM2-960818.xml", "HAM2-960819.xml", 
            "HAM2-960820.xml", "HAM2-961019.xml", "HAM2-961112.xml", "HAM2-961113.xml", "HAM2-961114.xml", 
            "HAM2-970414.xml", "HAM2-970415.xml", "HAM2-970612.xml", "HAM2-970614.xml", "HAM2-970710.xml", 
            "HAM2-970712.xml", "HAM2-970713.xml", "HAM2-970717.xml", "HAM2-970719.xml", "HAM2-980317.xml", 
            "HAM2-040820.xml", "HAM2-040824.xml", "HAM2-040825.xml", "HAM2-040901.xml", "HAM2-040917.xml", 
            "HAM2-040918.xml", "HAM2-040920.xml", "HAM2-041025.xml", "HAM2-041026.xml", "HAM2-041027.xml", 
            "HAM2-041230.xml", "HAM2-041231.xml", "HAM2-050101.xml", "HAM2-050102.xml", "HAM2-050223.xml", 
            "HAM2-050224.xml", "HAM2-050406.xml", "HAM2-050407.xml", "HAM2-050416.xml"
        };

        public HamshahriReader()
            : this("resources/hamshahri")
        { }

        public HamshahriReader(string root)
        {
            this._rootFolder = root;
            this._paragraphPattern = new RegexPattern(@"(\n.{0,50})(?=\n)", "$1\n");
        }

        public IEnumerable<Document> GetDocuments()
        {
            DirectoryInfo dir = new DirectoryInfo(_rootFolder);
            foreach (var folder in dir.GetDirectories())
            {
                foreach (var file in folder.GetFiles())
                {
                    if (!this.invalidFiles.Contains(file.Name))
                    {
                        XmlDocument xDoc = new XmlDocument();
                        try
                        {
                            xDoc.Load(file.FullName);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("error in reading" + file.Name + ".\n" + e.Message);
                            continue;
                        }

                        foreach (XmlNode doc in xDoc.GetElementsByTagName("DOC"))
                        {
                            // refine text
                            var body = doc["TEXT"].InnerText;
                            body = this._paragraphPattern.Apply(body).Replace("\no ", "\n");

                            Document document = new Document()
                            {
                                ID = doc["DOCID"].InnerText,
                                Number = doc["DOCNO"].InnerText,
                                OriginalFile = doc["ORIGINALFILE"].InnerText,
                                Issue = doc["ISSUE"].InnerText,
                                WesternDate = doc.SelectSingleNode("DATE[@calender='Western']").InnerText,
                                PersianDate = doc.SelectSingleNode("DATE[@calender='Persian']").InnerText,
                                EnglishCategory = doc.SelectSingleNode("CAT[@*='en']").InnerText,
                                PersianCategory = doc.SelectSingleNode("CAT[@*='fa']").InnerText,
                                Title = doc["TITLE"].InnerText,
                                Body = body
                            };

                            yield return document;
                        }
                    }
                }
            }
        }
    }

    public class Document
    {
        public string ID { get; set; }

        public string Number { get; set; }

        public string OriginalFile { get; set; }

        public string Issue { get; set; }

        public string WesternDate { get; set; }

        public string PersianDate { get; set; }

        public string EnglishCategory { get; set; }

        public string PersianCategory { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }
}
