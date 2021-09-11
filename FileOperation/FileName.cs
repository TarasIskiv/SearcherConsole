using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole.FileOperation
{
    public class FileName
    {
        private string _pathToSource;
        public string PathToSource { get => _pathToSource; set => _pathToSource = value; }

        public FileName(string pathToSource)
        {
            PathToSource = pathToSource;
        }


        public string getFileName()
        {
            string FileName = @PathToSource;
            var temporaryListForFileName = FileName.Split((char)92);
            string newFileName =
                temporaryListForFileName[temporaryListForFileName.Length - 2]
                + ((char)92)
                + temporaryListForFileName[temporaryListForFileName.Length - 1];

            return newFileName;
        }
    }
}
