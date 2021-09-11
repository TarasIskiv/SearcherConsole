using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole.URLOperation
{
    public class ParsedWebPage
    {
        private List<string> _webPageContent;
        private const char startSymbol = '>';
        private const char endSymbol = '<';
        public ParsedWebPage(List<string> webPageContent)
        {
            _webPageContent = webPageContent;
        }


        public List<string> parsedContent()
        {
            var urlParsedData = new List<string>();
            foreach (var line in _webPageContent)
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

            return urlParsedData;
        }
    }
}
