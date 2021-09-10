using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    internal class FileSelected : SelectedType
    {
        private string _fileFullPath;

        public FileSelected(string searchedValue, string pathToSource)
        {
            this.searchedValue = searchedValue;
            this.pathToSource = pathToSource;
            this._fileFullPath = string.Empty;
        }

        public override string search()
        {
            
            if (!setFileFullPath())
            {
                return Message.BAD_INPUT;
            }

            Console.WriteLine(_fileFullPath);

            var content = new FileOperation.FileContent(_fileFullPath).getFileContent().ToList();
            int lineNumber = 1;
            var outputList = new List<string>() { "Searched data : " + searchedValue };

            foreach (var line in content)
            {
                if (line.Contains(searchedValue))
                {
                    outputList.Add("Line Number : " + lineNumber.ToString() + "\nLine: " + line.ToString());
                }

                lineNumber++;
            }

            if (outputList.Count == 1)
            {
                return Message.NO_MATCHES;
            }

            return string.Join("\n", outputList.ToArray());

        }

        private bool setFileFullPath()
        {
            try
            {
                string FILENAME = new FileOperation.FileName(pathToSource).getFileName();

                if (!FILENAME.EndsWith(".txt"))
                {
                    return false;
                }

                _fileFullPath = new FileOperation.FileFullPath(FILENAME).getFullPath();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        } 
    }
}
