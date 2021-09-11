using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole.URLOperation
{
    public class PageContent
    {
        private string _pathToSource;
        private List<string> _webPageContent;

        public List<string> WebPageContent 
        {
            get => _webPageContent;
            set => _webPageContent = value;
        }


        public PageContent(string pathToSource)
        {
            _pathToSource = pathToSource;
        }


        public string writeContent()
        {
            var html = new List<string>();
            try
            {
                WebRequest request = WebRequest.Create(_pathToSource);
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                using (StreamReader stream = new StreamReader(data))
                {
                    while (!stream.EndOfStream)
                    {
                        html.Add(stream.ReadLine() + "\n");
                    }
                }

                WebPageContent = html;
            }
            catch (Exception)
            {
                return Message.BAD_URL;
            }

            return Message.GOOD_URL;
        }
    }
}
