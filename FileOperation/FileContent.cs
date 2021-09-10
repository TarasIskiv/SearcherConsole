using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole.FileOperation
{
    public class FileContent
    {
        private string _fileFullPath;
        public FileContent(string temporaryFilePath)
        {
            _fileFullPath = temporaryFilePath;
        }

        public List<string> getFileContent()
        {
            using (var reader = new StreamReader(_fileFullPath))
            {
                var content = new List<string>();

                while (!reader.EndOfStream)
                {
                    content.Add(reader.ReadLine());
                }
                return content;
            }
        }
    }
}

