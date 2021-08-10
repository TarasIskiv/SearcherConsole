using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    internal class URLSelected : ISelectedType
    {
        internal string url;
        internal List<string> data;
        internal string searchedValue;
        internal bool isParsed;

        public URLSelected(string searchedValue, bool isParsed) 
        {
            this.searchedValue = searchedValue;
            this.isParsed = isParsed;
        }

        public string getResult(string pathToSource)
        {
            if (writeContent(pathToSource).ToString().Equals("Bad URI"))
            {
                return "Bad URI";
            }
            if (isParsed)
            {
                htmlParser();
            }

            return URLSearhcer(searchedValue);
        }

        private string writeContent(string uri)
        {
            var html = new List<string>();
            try
            {
                WebRequest request = WebRequest.Create(uri);
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                using (StreamReader sr = new StreamReader(data))
                {
                    while (!sr.EndOfStream)
                        html.Add(sr.ReadLine() + "\n");
                }
                this.data = html;
                this.url = uri;
            }
            catch (Exception ex)
            {
                return "Bad URI";
            }
            return "Good URI";
        }


        private string URLSearhcer(string searchedValue)
        {
            var content = this.data.ToList();
            int lineNumber = 1;
            var listResults = new List<string>();
            listResults.Add("Searched data : " + searchedValue);
            foreach (var line in content)
            {
                if (line.Contains(searchedValue))
                {
                    listResults.Add("Line Number : " + lineNumber + "\n" + "Line : " + line.ToString());
                }
                lineNumber++;
            }

            if (listResults.Count == 1)
            {
                return "No matches found";
            }
            string result = string.Join("\n", listResults.ToArray());
            return result;
        }

        private void htmlParser()
        {
            var htmlLinesList = this.data;
            var urlParsedData = new List<string>();
            const char startSymbol = '>';
            const char endSymbol = '<';
            foreach (var line in htmlLinesList)
            {
                char[] arrS = line.ToCharArray();

                int startIndex = -1;
                int endIndex = -1;
                int distance = -1;

                int cutIndexFrom = 0;
                int cutIndexTo = 0;
                for (int i = 0; i < arrS.Length; ++i)
                {
                    if (arrS[i] == startSymbol)
                    {
                        startIndex = i;
                    }

                    if (arrS[i] == endSymbol)
                    {
                        endIndex = i;
                        if (distance < (endIndex - startIndex))
                        {
                            distance = endIndex - startIndex;
                            cutIndexFrom = startIndex;
                            cutIndexTo = endIndex;
                        }
                    }
                }
                if ((cutIndexTo - cutIndexFrom - 1) > 0)
                {
                    string newLine = line.Substring(cutIndexFrom + 1, (cutIndexTo - cutIndexFrom - 1));
                    urlParsedData.Add(newLine.Trim());
                }
            }

            this.data = urlParsedData;
        }

    }
}
