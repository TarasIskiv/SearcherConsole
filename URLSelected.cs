using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    internal class URLSelected : SelectedType
    {
        private string _url;
        private List<string> _data;
        private bool _isParsed;

        public string Url { get => _url; set => _url = value; }
        public List<string> Data { get => _data; set => _data = value; }
        public bool IsParsed { get => _isParsed; set => _isParsed = value; }

        public URLSelected(string searchedValue, string pathToSource, bool isParsed) 
        {
            this.searchedValue = searchedValue;
            this.pathToSource = pathToSource;
            IsParsed = isParsed;
        }

        public override string search()
        {
            if (writeContent(pathToSource).ToString().Equals("Bad URL"))
            {
                return "Bad URL";
            }

            if (_isParsed)
            {
                htmlParser();
            }

            return URLSearhcer(searchedValue);
        }

        private string writeContent(string url)
        {
            var html = new List<string>();
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                using (StreamReader stream = new StreamReader(data))
                {
                    while (!stream.EndOfStream)
                    {
                        html.Add(stream.ReadLine() + "\n");
                    }   
                }

                _data = html;
                _url = url;
            }
            catch (Exception)
            {
                return "Bad URL";
            }

            return "Good URL";
        }


        private string URLSearhcer(string searchedValue)
        {
            var content = _data.ToList();
            int lineNumber = 1;
            var listResults = new List<string>() { "Searched data : " + searchedValue };
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
                return Message.NO_MATCHES;
            }

            return string.Join("\n", listResults.ToArray());
        }

        private void htmlParser()
        {
            var htmlLinesList = this._data;
            var urlParsedData = new List<string>();
            const char startSymbol = '>';
            const char endSymbol = '<';

            foreach (var line in htmlLinesList)
            {
                int startIndex = -1;
                int endIndex = -1;
                int distance = -1;
                int cutIndexFrom = 0;
                int cutIndexTo = 0;
                char[] arrayFromLineInHtmlLineList = line.ToCharArray();
               

                for (int i = 0; i < arrayFromLineInHtmlLineList.Length; ++i)
                {
                    if (arrayFromLineInHtmlLineList[i] == startSymbol)
                    {
                        startIndex = i;
                    }

                    if (arrayFromLineInHtmlLineList[i] == endSymbol)
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

            _data = urlParsedData;
        }

    }
}
