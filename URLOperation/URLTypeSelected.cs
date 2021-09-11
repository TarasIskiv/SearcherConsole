using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    internal class URLTypeSelected : SelectedType
    {
        private List<string> _webPageContent;
        private bool _isParsed;
        private URLOperation.PageContent pageContent;

        public List<string> WebPageContent 
        {
            get => _webPageContent;
            set => _webPageContent = value;
        }

        public URLTypeSelected(string searchedValue, string pathToSource, bool isParsed) 
        {
            this.searchedValue = searchedValue;
            this.pathToSource = pathToSource;
            _isParsed = isParsed;
            pageContent = new URLOperation.PageContent(pathToSource);
        }

        public override string search()
        {
            if (!setWebPageContent())
            {
                return Message.BAD_URL;
            }
            var content = _webPageContent.ToList();
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


        private bool setWebPageContent()
        {
            if (pageContent.writeContent().ToString().Equals(Message.BAD_URL))
            {
                return false;
            }
            WebPageContent = pageContent.WebPageContent;
            if (_isParsed)
            {
                WebPageContent = new URLOperation.ParsedWebPage(_webPageContent).parsedContent();
            }
            return true;
        }

    }
}
