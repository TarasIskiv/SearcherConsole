using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    internal class FolderTypeSelected : SelectedType
    {

        private FolderOperation.FilesInDirectory files;
        private List<string> _txtFilesFromFolder;
        private List<string> _results = new List<string>();

        public FolderTypeSelected(string searchedValue, string pathToSource)
        {
            this.searchedValue = searchedValue;
            this.pathToSource = pathToSource;
            files = new FolderOperation.FilesInDirectory(@pathToSource);
        }


        public override string search()
        {
            if (!string.IsNullOrEmpty(setTxtFilesFromDirectory())){
                return setTxtFilesFromDirectory();
            }

            int lineNumber = 1;

            for (int i = 0; i < _txtFilesFromFolder.Count; ++i)
            {
                var temporaryListOfLines = new List<string>();

                using (StreamReader stream = new StreamReader(@_txtFilesFromFolder[i]))
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
                        string toSet = "File : " + _txtFilesFromFolder[i];

                        if (!_results.Contains(toSet))
                        {
                            _results.Add("File : " + _txtFilesFromFolder[i]);
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

        private string setTxtFilesFromDirectory()
        {
            try
            {
                bool directoryNotEmpty = files.isFilesInDirectory();
                if (!directoryNotEmpty)
                {
                    return Message.BAD_INPUT;
                }

                _txtFilesFromFolder = files.getTxtFilesFromDirectory();

                if (_txtFilesFromFolder.Count == 0)
                {
                    return Message.FOLDER_WITHOUT_TXT_FILES;
                }
            }
            catch (Exception)
            {
                return Message.BAD_INPUT;
            }

            return string.Empty;
        }


    }

}
