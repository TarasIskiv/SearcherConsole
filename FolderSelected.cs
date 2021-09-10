using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    internal class FolderSelected : SelectedType
    {
        private List<string> _allFiles;
        private List<string> _results = new List<string>();
        private const string fileEndsWith = ".txt";

        public List<string> AllFiles { get => _allFiles; set => _allFiles = value; }
        public List<string> Results { get => _results; set => _results = value; }

        public FolderSelected(string searchedValue, string pathToSource)
        {
            this.searchedValue = searchedValue;
            this.pathToSource = pathToSource;
        }
        public override string search()
        {
            try
            {
                if (!getFiles(@pathToSource))
                {
                    return Message.BAD_INPUT;
                }

                return getContent(searchedValue);
            }
            catch (Exception)
            {
                return Message.BAD_INPUT;
            }
        }

        private string getContent(string searchedValue)
        {
            var filesFromFolder = new List<string>();
            int lineNumber = 1;

            foreach (var item in _allFiles)
            {
                if (item.ToString().EndsWith(fileEndsWith))
                {
                    filesFromFolder.Add(item);
                }
            }

            if (filesFromFolder.Count == 0)
            {
                return Message.FOLDER_WITHOUT_TXT_FILES;
            }


            for (int i = 0; i < filesFromFolder.Count; ++i)
            {
                var temporaryListOfLines = new List<string>();

                using (StreamReader stream = new StreamReader(@filesFromFolder[i]))
                {
                    while (!stream.EndOfStream)
                    {
                        temporaryListOfLines.Add(stream.ReadLine());
                    }
                }

                foreach (var line in temporaryListOfLines)
                {
                    if (line.Contains(searchedValue))
                    {
                        string toSet = "File : " + filesFromFolder[i];

                        if (!_results.Contains(toSet))
                        {
                            _results.Add("File : " + filesFromFolder[i]);
                        }

                        _results.Add("Line number : " + lineNumber.ToString() + "\nLine : " + line);

                    }

                    lineNumber++;
                }
            }

            if (_results.Count == 0)
            {
                return Message.NO_MATCHES;
            }

            return "Searched Value : " + searchedValue + "\n" + string.Join("\n", _results.ToArray());
        }
        private bool getFiles(string path)
        {
            try
            {
                var list = Directory.GetFiles(path);
                _allFiles = list.ToList();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
