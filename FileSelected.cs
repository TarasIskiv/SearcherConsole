using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    internal class FileSelected : SelectType
    {
        private string _filePath;
        private List<string> _data;

        public string FilePath { get => _filePath; set => _filePath = value; }
        public List<string> Data { get => _data; set => _data = value; }

        public FileSelected(string searchedValue)
        {
            this.searchedValue = searchedValue;
            Data = new List<string>();
            FilePath = string.Empty;
        }

        public override string getResult(string pathToSource)
        {
            string result = string.Empty;
            string temporaryFilePath;
            const int MAX_NUMB_ITERATION = 50;
            int currentIteration = 0;

            try
            {
                string FILENAME = getFileName(pathToSource);

                if (!FILENAME.EndsWith(".txt"))
                {
                    return Message.BAD_INPUT;
                }

                string path = Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location);
                do
                {
                    path = Path.GetDirectoryName(path);
                    temporaryFilePath = String.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, FILENAME);

                    if (temporaryFilePath.StartsWith(@"\\") || currentIteration >= MAX_NUMB_ITERATION)
                    {
                        return Message.FILE_NOT_FOUND;
                    }

                    currentIteration++;

                } while (!File.Exists(temporaryFilePath));

                Console.WriteLine(temporaryFilePath);

                using (var reader = new StreamReader(temporaryFilePath))
                {
                    var content = new List<string>();

                    while (!reader.EndOfStream)
                    {
                        content.Add(reader.ReadLine());
                    }

                    _filePath = temporaryFilePath;
                    _data = content;

                    result = searcher(searchedValue);
                }
            }
            catch (Exception)
            {
                return Message.BAD_INPUT;
            }

            return result;
        }


        private string getFileName(string pathToSource)
        {
            string FileName = @pathToSource;
            var temporaryListForFileName = FileName.Split((char)92);
            string FILENAME = 
                temporaryListForFileName[temporaryListForFileName.Length - 2]
                + ((char)92)
                + temporaryListForFileName[temporaryListForFileName.Length - 1];

            return FILENAME;
        }


        private string searcher(string searchedValue)
        {
            var content = _data.ToList();
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
    }
}
