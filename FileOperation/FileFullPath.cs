using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole.FileOperation
{
    public class FileFullPath
    {
        private string fileName;
        public FileFullPath(string fileName)
        {
            this.fileName = fileName;
        }

        public string getFullPath()
        {
            string fileFullPath = string.Empty;
            string path = Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location);
            const int MAX_NUMB_ITERATION = 50;
            int currentIteration = 0;
            do
            {
                path = Path.GetDirectoryName(path);
                fileFullPath = String.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, fileName);

                if (fileFullPath.StartsWith(@"\\") || currentIteration >= MAX_NUMB_ITERATION)
                {
                    return Message.FILE_NOT_FOUND;
                }

                currentIteration++;

            } while (!File.Exists(fileFullPath));

            return fileFullPath;
        }
    }
}
